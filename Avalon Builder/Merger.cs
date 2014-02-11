using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using TAPI.Avalon;

// NOT WORKING AT ALL...

static class Merger
{
    internal static void Merge(params Assembly[] asm)
    {
        #region assembly
        AssemblyName name = asm[0].GetName();
        name.Name = "Avalon";
        AssemblyBuilder ab = AssemblyBuilder.DefineDynamicAssembly(name, AssemblyBuilderAccess.Save);
        #endregion

        #region module
        ModuleBuilder mb = ab.DefineDynamicModule(ab.GetName().Name, ab.GetName().Name + ".dll");
        #endregion

        #region modules
        //foreach (Assembly a in asm)
        //{
        //    foreach (Module m in a.Modules)
        //    {
        //        ModuleBuilder mdb = ab.DefineDynamicModule(m.FullyQualifiedName);

        //        foreach (MethodInfo mi in m.GetMethods())
        //        {
        //            mdb.DefineGlobalMethod(mi.Name, mi.Attributes, mi.CallingConvention, mi.ReturnType, retmodreq, retmodopt, paramTypes, parammodreq, parammodopt);
        //        }

        //        mdb.CreateGlobalFunctions();
        //    }
        //}
        #endregion

        #region types
        List<Type> types = new List<Type>();

        foreach (Assembly a in asm)
            foreach (TypeInfo t in a.DefinedTypes)
            {
                if (!types.Any((T) => { return T.Name == t.Name; }))
                    types.Add(t);
                else // uh oh...
                {
                    int index = types.FindIndex((T) => { return T.FullName == t.FullName; });
                    if (index == -1)
                        continue;
                    types[index] = MergeType(types[index], t, mb);
                }
            }
        #endregion

        #region various assembly stuff
        // entry point
        foreach (Assembly a in asm)
            if (a.EntryPoint != null)
            {
                ab.SetEntryPoint(a.EntryPoint);
                break;
            }

        List<string> usedAttributes = new List<string>();
        foreach (Assembly a in asm)
        {
            // resource files
            foreach (string rname in a.GetManifestResourceNames())
            {
                Stream s = a.GetManifestResourceStream(rname);
                MemoryStream ms = new MemoryStream();
                s.CopyTo(ms);

                File.WriteAllBytes("tempres.tmp", ms.ToArray());

                ms.Dispose();
                s.Dispose();

                ab.AddResourceFile(rname, "tempres.tmp");

                File.Delete("tempres.tmp");
            }

            // assembly info
            foreach (CustomAttributeData cad in a.CustomAttributes)
            {
                List<object> args = new List<object>();
                foreach (CustomAttributeTypedArgument cata in cad.ConstructorArguments)
                    args.Add(cata.Value);

                if (!usedAttributes.Contains(cad.AttributeType.FullName))
                {
                    ab.SetCustomAttribute(new CustomAttributeBuilder(cad.Constructor, args.ToArray()));
                    usedAttributes.Add(cad.AttributeType.FullName);
                }
            }
        }
        #endregion

        ab.Save("Avalon.dll"); // you'd regret it if you specify a path
        if (File.Exists("bin\\Avalon.dll"))
            File.Delete("bin\\Avalon.dll");
        File.Move("Avalon.dll", "bin\\Avalon.dll");
    }

    static Type MergeType(Type t1, Type t2, ModuleBuilder mdb)
    {
        List<Type> interfaces = new List<Type>(t1.GetInterfaces());
        foreach (Type t in t2.GetInterfaces())
            if (!interfaces.Any((T) => { return T.FullName == t.FullName; }))
                interfaces.Add(t);

        TypeBuilder ret = mdb.DefineType(t1.FullName, t1.Attributes | t2.Attributes, t1.BaseType ?? t2.BaseType, interfaces.ToArray());

        #region variables (fields)
        foreach (FieldInfo fi in t1.GetFields())
        {
            FieldAttributes attr = fi.Attributes;

            // merge attributes
            foreach (FieldInfo fi2 in t2.GetFields())
                if (Equals(fi, fi2))
                    attr |= fi2.Attributes;

            ret.DefineField(fi.Name, fi.FieldType, fi.GetRequiredCustomModifiers(), fi.GetOptionalCustomModifiers(), attr);
        }

        foreach (FieldInfo fi in t2.GetFields())
            if (!ret.GetFields().Contains(fi)) // feel the power of LINQ!
                ret.DefineField(fi.Name, fi.FieldType, fi.GetRequiredCustomModifiers(), fi.GetOptionalCustomModifiers(), fi.Attributes);
        #endregion

        #region properties
        foreach (PropertyInfo pi in t1.GetProperties())
        {
            PropertyAttributes attr = pi.Attributes;

            // merge attributes
            foreach (PropertyInfo pi2 in t2.GetProperties())
                if (Equals(pi, pi2))
                    attr |= pi2.Attributes;

            // parameter stuff blergh
            Type[] paramTypes = new Type[pi.GetIndexParameters().Length];
            Type[][]
                modreq = new Type[paramTypes.Length][],
                modopt = new Type[paramTypes.Length][];

            for (int i = 0; i < paramTypes.Length; i++)
            {
                ParameterInfo pai = pi.GetIndexParameters()[i];

                paramTypes[i] = pai.ParameterType;

                pai.GetRequiredCustomModifiers().CopyTo(modreq[i], 0);
                pai.GetOptionalCustomModifiers().CopyTo(modopt[i], 0);
            }

            ret.DefineProperty(pi.Name, attr, pi.PropertyType, pi.GetRequiredCustomModifiers(), pi.GetOptionalCustomModifiers(), paramTypes, modreq, modopt);
        }

        foreach (PropertyInfo pi in t2.GetProperties())
            if (!ret.GetProperties().Any((PI) => { return Equals(PI, pi); }))
            {
                // parameter stuff blergh
                Type[] paramTypes = new Type[pi.GetIndexParameters().Length];
                Type[][]
                    modreq = new Type[paramTypes.Length][],
                    modopt = new Type[paramTypes.Length][];

                for (int i = 0; i < paramTypes.Length; i++)
                {
                    ParameterInfo pai = pi.GetIndexParameters()[i];

                    paramTypes[i] = pai.ParameterType;

                    pai.GetRequiredCustomModifiers().CopyTo(modreq[i], 0);
                    pai.GetOptionalCustomModifiers().CopyTo(modopt[i], 0);
                }

                ret.DefineProperty(pi.Name, pi.Attributes, pi.PropertyType, pi.GetRequiredCustomModifiers(), pi.GetOptionalCustomModifiers(), paramTypes, modreq, modopt);
            }
        #endregion

        #region constructors
        List<string> used = new List<string>();

        foreach (ConstructorInfo _ci in t1.GetConstructors())
        {
            ConstructorInfo ci = _ci;

            Type[] paramTypes = new Type[ci.GetParameters().Length];
            Type[][]
                modreq = new Type[paramTypes.Length][],
                modopt = new Type[paramTypes.Length][];

            for (int i = 0; i < paramTypes.Length; i++)
            {
                ci.GetParameters()[i].GetRequiredCustomModifiers().CopyTo(modreq[i], 0);
                ci.GetParameters()[i].GetOptionalCustomModifiers().CopyTo(modopt[i], 0);
            }

#pragma warning disable
            if (ci.CustomAttributes.Any((cad) => { return cad.AttributeType.FullName == typeof(OtherAssemblyAttribute).FullName; }))
#pragma warning restore
                foreach (ConstructorInfo C in t2.GetConstructors())
                    if (Equals(C, ci))
                    {
                        ci = C;
                        used.Add(C.DeclaringType.FullName + "::" + C.Name);
                        break;
                    }

            ret.DefineConstructor(ci.Attributes, ci.CallingConvention, paramTypes, modreq, modopt);
        }
        foreach (ConstructorInfo ci in t2.GetConstructors())
            if (!used.Contains(ci.DeclaringType.FullName + "::" + ci.Name))
            {
                Type[] paramTypes = new Type[ci.GetParameters().Length];
                Type[][]
                    modreq = new Type[paramTypes.Length][],
                    modopt = new Type[paramTypes.Length][];

                for (int i = 0; i < paramTypes.Length; i++)
                {
                    ci.GetParameters()[i].GetRequiredCustomModifiers().CopyTo(modreq[i], 0);
                    ci.GetParameters()[i].GetOptionalCustomModifiers().CopyTo(modopt[i], 0);
                }

                ret.DefineConstructor(ci.Attributes, ci.CallingConvention, paramTypes, modreq, modopt);
            }
        #endregion

        #region methods
        used = new List<string>();

        foreach (MethodInfo _mi in t1.GetMethods())
        {
            MethodInfo mi = _mi;

            Type[]
                retmodreq = new Type[mi.ReturnParameter.GetRequiredCustomModifiers().Length],
                retmodopt = new Type[mi.ReturnParameter.GetOptionalCustomModifiers().Length],
                paramTypes = new Type[mi.GetParameters().Length];
            Type[][]
                parammodreq = new Type[paramTypes.Length][],
                parammodopt = new Type[paramTypes.Length][];

            if (mi.ReturnParameter.GetRequiredCustomModifiers() != null)
                mi.ReturnParameter.GetRequiredCustomModifiers().CopyTo(retmodreq, 0);
            if (mi.ReturnParameter.GetOptionalCustomModifiers() != null)
                mi.ReturnParameter.GetOptionalCustomModifiers().CopyTo(retmodopt, 0);

            for (int i = 0; i < paramTypes.Length; i++)
            {
                paramTypes[i] = mi.GetParameters()[i].ParameterType;

                parammodreq[i] = (Type[])mi.GetParameters()[i].GetRequiredCustomModifiers().Clone();
                parammodopt[i] = (Type[])mi.GetParameters()[i].GetOptionalCustomModifiers().Clone();
            }

#pragma warning disable
            if (mi.CustomAttributes.Any((cad) => { return cad.AttributeType.FullName == typeof(OtherAssemblyAttribute).FullName; }))
#pragma warning restore
                foreach (MethodInfo M in t2.GetMethods())
                    if (Equals(M, mi))
                    {
                        mi = M;
                        used.Add(M.DeclaringType.FullName + "::" + M.Name);
                        break;
                    }

            MethodBody mtb = mi.GetMethodBody();
            try
            {
                if (typeof(object).GetMethod(mi.Name) != null)
                    if (mi.IsStatic)
                        continue;
                    else if (mi.IsVirtual && !mi.IsFinal)
                        continue;
            }
            catch (AmbiguousMatchException)
            {
                continue;
            }
            if (mtb != null)
                ret.DefineMethod(mi.Name, mi.Attributes, mi.CallingConvention, mi.ReturnType, retmodreq, retmodopt, paramTypes, parammodreq, parammodopt)
                    .CreateMethodBody(mtb.GetILAsByteArray(), mtb.GetILAsByteArray().Length);
        }
        foreach (MethodInfo mi in t2.GetMethods())
            if (!used.Contains(mi.DeclaringType.FullName + "::" + mi.Name))
            {
                Type[]
                    retmodreq = new Type[mi.ReturnParameter.GetRequiredCustomModifiers().Length],
                    retmodopt = new Type[mi.ReturnParameter.GetOptionalCustomModifiers().Length],
                    paramTypes = new Type[mi.GetParameters().Length];
                Type[][]
                    parammodreq = new Type[paramTypes.Length][],
                    parammodopt = new Type[paramTypes.Length][];

                if (mi.ReturnParameter.GetRequiredCustomModifiers() != null)
                    mi.ReturnParameter.GetRequiredCustomModifiers().CopyTo(retmodreq, 0);
                if (mi.ReturnParameter.GetOptionalCustomModifiers() != null)
                    mi.ReturnParameter.GetOptionalCustomModifiers().CopyTo(retmodopt, 0);

                for (int i = 0; i < paramTypes.Length; i++)
                {
                    paramTypes[i] = mi.GetParameters()[i].ParameterType;

                    parammodreq[i] = (Type[])mi.GetParameters()[i].GetRequiredCustomModifiers().Clone();
                    parammodopt[i] = (Type[])mi.GetParameters()[i].GetOptionalCustomModifiers().Clone();
                }

                MethodBody mtb = mi.GetMethodBody();
                try
                {
                    if (typeof(object).GetMethod(mi.Name) != null)
                        if (mi.IsStatic)
                            continue;
                        else if (mi.IsVirtual && !mi.IsFinal)
                            continue;
                }
                catch (AmbiguousMatchException)
                {
                    continue;
                }
                if (mtb != null)
                    ret.DefineMethod(mi.Name, mi.Attributes, mi.CallingConvention, mi.ReturnType, retmodreq, retmodopt, paramTypes, parammodreq, parammodopt)
                        .CreateMethodBody(mtb.GetILAsByteArray(), mtb.GetILAsByteArray().Length);
            }
        #endregion

        return ret.CreateType();
    }

    static bool Equals(MemberInfo mi, MemberInfo mi2)
    {
        return mi.Name == mi2.Name && mi.MemberType == mi2.MemberType;
    }

    static bool Equals(Type t, Type t2)
    {
        if (t == null && t2 == null)
            return true;
        bool ret = t.Attributes == t2.Attributes && Equals(t.BaseType, t2.BaseType) && t.ContainsGenericParameters == t2.ContainsGenericParameters
            && t.FullName == t2.FullName
            && t.GenericTypeArguments == t2.GenericTypeArguments
            && t.IsAbstract == t2.IsAbstract && t.IsArray == t2.IsArray && t.IsAutoClass == t2.IsAutoClass && t.IsAutoLayout == t2.IsAutoLayout
            && t.IsByRef == t2.IsByRef && t.IsClass == t2.IsClass && t.IsCOMObject == t2.IsCOMObject && t.IsConstructedGenericType == t2.IsConstructedGenericType
            && t.IsContextful == t2.IsContextful && t.IsEnum == t2.IsEnum && t.IsExplicitLayout == t2.IsExplicitLayout && t.IsGenericParameter == t2.IsGenericParameter
            && t.IsGenericType == t2.IsGenericType && t.IsGenericTypeDefinition == t2.IsGenericTypeDefinition && t.IsImport == t2.IsImport
            && t.IsInterface == t2.IsInterface && t.IsLayoutSequential == t2.IsLayoutSequential && t.IsMarshalByRef == t2.IsMarshalByRef
            && t.IsPublic == t2.IsPublic && t.IsSealed == t2.IsSealed && t.IsSecuritySafeCritical == t2.IsSecuritySafeCritical
            && t.IsSerializable == t2.IsSerializable && t.IsSpecialName == t2.IsSpecialName && t.IsValueType == t2.IsValueType
            && t.IsVisible == t2.IsVisible && t.Namespace == t2.Namespace
            && Equals(t as MemberInfo, t2);
        return ret;
    }

    static bool Equals(FieldInfo fi, FieldInfo fi2)
    {
        bool ret = fi.Attributes == fi2.Attributes && fi.DeclaringType == fi2.DeclaringType
            && fi.FieldType == fi2.FieldType && fi.IsAssembly == fi2.IsAssembly && fi.IsFamily == fi2.IsFamily && fi.IsFamilyAndAssembly == fi2.IsFamilyAndAssembly
            && fi.IsFamilyOrAssembly == fi2.IsFamilyOrAssembly && fi.IsInitOnly == fi2.IsInitOnly && fi.IsLiteral == fi2.IsLiteral
            && fi.IsNotSerialized == fi2.IsNotSerialized && fi.IsPinvokeImpl == fi2.IsPinvokeImpl && fi.IsPrivate == fi2.IsPrivate
            && fi.IsPublic == fi2.IsPublic && fi.IsSecurityCritical == fi2.IsSecurityCritical && fi.IsSecuritySafeCritical == fi2.IsSecuritySafeCritical
            && fi.IsSecurityTransparent == fi2.IsSecurityTransparent && fi.IsSpecialName == fi2.IsSpecialName && fi.IsStatic == fi2.IsStatic
            && Equals(fi as MemberInfo, fi2);
        return ret;
    }
    static bool Equals(PropertyInfo pi, PropertyInfo pi2)
    {
        bool ret = pi.Attributes == pi2.Attributes && pi.CanRead == pi2.CanRead && pi.CanWrite == pi2.CanWrite
            && pi.DeclaringType == pi2.DeclaringType && pi.IsSpecialName == pi2.IsSpecialName && pi.PropertyType == pi2.PropertyType
            && Equals(pi as MemberInfo, pi2);
        return ret;
    }
    static bool Equals(MethodBase mb, MethodBase mb2)
    {
        bool ret = mb.Attributes == mb2.Attributes && mb.CallingConvention == mb2.CallingConvention && mb.ContainsGenericParameters == mb2.ContainsGenericParameters
            && Equals(mb.DeclaringType, mb2.DeclaringType) && mb.IsAbstract == mb2.IsAbstract && mb.IsAssembly == mb2.IsAssembly && mb.IsConstructor == mb2.IsConstructor
            && mb.IsFamily == mb2.IsFamily && mb.IsFamilyAndAssembly == mb2.IsFamilyAndAssembly && mb.IsFamilyOrAssembly == mb2.IsFamilyOrAssembly
            && mb.IsFinal == mb2.IsFinal && mb.IsGenericMethod == mb2.IsGenericMethod && mb.IsGenericMethodDefinition == mb2.IsGenericMethodDefinition
            && mb.IsHideBySig == mb2.IsHideBySig && mb.IsPrivate == mb2.IsPrivate && mb.IsPublic == mb2.IsPublic && mb.IsSecurityCritical == mb2.IsSecurityCritical
            && mb.IsSecurityTransparent == mb2.IsSecurityTransparent && mb.IsSpecialName == mb2.IsSpecialName
            && mb.IsStatic == mb2.IsStatic && mb.IsVirtual == mb2.IsVirtual && mb.MethodImplementationFlags == mb2.MethodImplementationFlags
            && Equals(mb as MemberInfo, mb2);
        return ret;
    }
}
