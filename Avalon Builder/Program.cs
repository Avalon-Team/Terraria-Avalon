using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using TAPI;

namespace Avalon
{
    static class Program
    {
        // mods sources & binaries directories
        readonly static string
            modsSrcDir = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\My Games\\Terraria\\tAPI\\Mods\\Sources",
            modsBinDir = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\My Games\\Terraria\\tAPI\\Mods\\Unsorted";

        /// <summary>
        /// Builds the Avalon Mod
        /// </summary>
        /// <param name="args">Program arguments</param>
        static void Main(string[] args)
        {
            // loads mod hashes, will be needed later
            JsonData modHashes = null;

            #region load hashes
            modHashes = File.Exists(modsSrcDir + "\\.ModHashes.json") ?
                JsonMapper.ToObject(File.ReadAllText(modsSrcDir + "\\.ModHashes.json")) :
                JsonMapper.ToObject("{}");
            #endregion

            #region setup
            string name = "Avalon";
            string
                // directory of the built .dll
                dllPath = Path.GetFullPath(args[0]),
                // output folder
                outDir = modsSrcDir + "\\" + name,
                // output binary
                outPath = modsBinDir + "\\" + name + ".tapimod";

            // kinda necessary
            if (!Directory.Exists(outDir))
                Directory.CreateDirectory(outDir);
            if (File.Exists(outPath))
                File.Delete(outPath);

            // load assembly
            Assembly asm = Assembly.LoadFrom(dllPath);

            List<Tuple<string, byte[]>> files = new List<Tuple<string, byte[]>>();
            #endregion

            #region load all resources into the list
            // every embedded resource should be put in the mod file list, to it can be read when it's loaded
            foreach (string res in asm.GetManifestResourceNames())
                using (Stream stream = asm.GetManifestResourceStream(res))
                {
                    // cast the resource to a byte array, and put it in the file list
                    MemoryStream ms = new MemoryStream();
                    stream.CopyTo(ms);

                    bool foundExt = false;
                    int startIndex = 0;

                    for (int i = res.Length - 1; i >= 0; i--)
                        // find file name
                        if (res[i] == '.')
                            if (foundExt)
                            {
                                startIndex = i + 1;
                                break;
                            }
                            else
                                foundExt = true;

                    files.Add(new Tuple<string, byte[]>(res.Substring(startIndex), ms.ToArray()));
                    ms.Dispose();
                }
            #endregion

            #region write data
            /*
             * How a .tapimod file looks like:
             * 
             *   - version (int)
             * 
             *   - modinfo (string)
             * 
             *   - file amount (int)
             * 
             *    files: 
             *     - file name (string)
             *     - file data length (int)
             *   
             *    files:
             *     - file data (byte[])
             *   
             *   - assembly data
             */

            // open new binary buffer
            BinBuffer bb = new BinBuffer();

            // write version
            bb.Write(Constants.versionAssembly);

            // modinfo string to write
            string modInfo = null;

            // find modinfo.json in the files
            foreach (Tuple<string, byte[]> pfile in files)
                if (pfile.Item1.EndsWith("ModInfo.json"))
                {
                    File.WriteAllBytes("modinfo.tmp", pfile.Item2);
                    modInfo = File.ReadAllText("modinfo.tmp");
                    File.Delete("modinfo.tmp");
                }

            // if it's not found, provide a default one
            if (modInfo == null)
                modInfo = "{\n\t\"name\": \"" + name + "\",\n\t\"author\": \"<unknown>\"\n\t\"info\": \"\"\n}";

            // write modinfo
            bb.Write(modInfo.Length);
            bb.Write(modInfo);

            // write file amount
            bb.Write(files.Count);

            // write file name + length
            foreach (Tuple<string, byte[]> pfile in files)
            {
                // don't want to write modinfo here
                if (pfile.Item1.EndsWith("ModInfo.json"))
                    continue;

                // file name
                bb.Write(pfile.Item1);
                // file data length
                bb.Write(pfile.Item2.Length);
            }

            // write file data
            foreach (Tuple<string, byte[]> pfile in files)
                bb.Write(pfile.Item2);

            // write assembly
            bb.Write(new BinBuffer(new BinBufferByte(File.ReadAllBytes(dllPath))));

            // reset
            bb.Pos = 0;
            // write it to the .tapimod file
            File.WriteAllBytes(outPath, bb.ReadBytes(bb.GetSize()));
            #endregion

            #region generate false folders & files to foul the hash checker, and generate hashes
            /*
             * tAPI auto-checks if a mod is modified to rebuild it automatically.
             * We don't want this to happen, because everything is in the .dll file.
             * Thus, the program generates a 'false' hash set, to foul tAPI.
             */

            // get info to write to modinfo.cs
            JsonData jModInfo = JsonMapper.ToObject(modInfo);

            // strings we need to make a fake CS file set etc
            string
                actualModName = name,
                codeModBaseName = "TAPI." + actualModName + ".ModBase";

            // check modinfo.json
            if (jModInfo.Has("name"))
                codeModBaseName = "TAPI." + (actualModName = (string)jModInfo["name"]) + ".ModBase";

            if (jModInfo != null && jModInfo.Has("code") && jModInfo["code"].Has("modBaseName"))
                codeModBaseName = (string)jModInfo["code"]["modBaseName"];

            // get namespace (and modbase class name)
            string[] nsSplit = codeModBaseName.Split('.');

            string @namespace = nsSplit[0];
            for (int i = 1; i < nsSplit.Length - 1; i++)
                @namespace += "." + nsSplit[i];

            // write false modbase
            using (FileStream fs = new FileStream(outDir + "\\" + nsSplit[nsSplit.Length - 1] + ".cs", FileMode.Create))
            {
                using (StreamWriter bw = new StreamWriter(fs))
                {
                    bw.Write(
                            "using System;\n" +
                            "using TAPI;\n" +
                            "\n" +
                            "namespace " + @namespace + "\n" +
                            "{\n" +
                            "    public class " + nsSplit[nsSplit.Length - 1] + " : TAPI.ModBase\n" +
                            "    {\n" +
                            "        public " + nsSplit[nsSplit.Length - 1] + "() : base() { }\n" +
                            "    }\n" +
                            "}\n"
                        );
                }
            }

            // write modinfo
            File.WriteAllText(outDir + "\\ModInfo.json", modInfo);

            // write .dll file, you'll never know if...
            File.Copy(dllPath, outDir + "\\" + Path.GetFileName(dllPath), true);

            // generate hashes
            modHashes[name] = GenerateHashes(outDir);
            // and save them
            Util.WaitWhileFileLocked(modsSrcDir + "\\.ModHashes.json");
            File.WriteAllText(modsSrcDir + "\\.ModHashes.json", JsonMapper.ToJson(modHashes));
            #endregion
        }

        /// <summary>
        /// Generates hashes for a mod folder
        /// </summary>
        /// <param name="modPath">The mod folder to generate hashes from</param>
        /// <returns>Hashes of the mod folder</returns>
        static JsonData GenerateHashes(string modPath)
        {
            JsonData j = JsonMapper.ToObject("{}");

            foreach (string fileName in Directory.EnumerateFiles(modPath, "*.*", SearchOption.AllDirectories))
                j[fileName.Substring(modPath.Length + 1).Replace("\\", "/")] = Constants.ComputeFileMD5(fileName);

            return j;
        }
    }
}
