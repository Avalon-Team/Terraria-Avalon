using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Xna.Framework;
using TAPI;
using Terraria;

namespace PoroCYon.MCT.Net
{
    static class NetHelper
    {
        internal static void SendText(string text, Color? c = null)
        {
            Color _c = c ?? Color.White;

            if (Main.netMode == 0)
                Main.NewText(text, _c.R, _c.G, _c.B);
            if (Main.netMode == 2)
                NetMessage.SendData(25, -1, -1, text, 255, _c.R, _c.G, _c.B, 0);
        }

        internal static void SendModData(ModBase @base, Enum message, params object[] toSend)
        {
            SendModData(@base, message, -1, -1, toSend);
        }
        internal static void SendModData(ModBase @base, int message, params object[] toSend)
        {
            SendModData(@base, message, -1, -1, toSend);
        }
        internal static void SendModData(ModBase @base, Enum message, int remoteClient, int ignoreClient, params object[] toSend)
        {
            SendModData(@base, Convert.ToInt32(message), remoteClient, ignoreClient, toSend);
        }
        internal static unsafe void SendModData(ModBase @base, int message, int remoteClient, int ignoreClient, params object[] toSend)
        {
            if (Main.netMode == 0)
                return;

            int num = 256;
            if (Main.netMode == 2 && remoteClient >= 0)
                num = remoteClient;

            lock (NetMessage.buffer[num])
            {
                BinBuffer bb = new BinBuffer(new BinBufferByte(NetMessage.buffer[num].writeBuffer, false));
                bb.Pos = 4; //for size
                bb.WriteX((byte)100, (byte)@base.modIndex, (byte)message);

                // write stuff here

                for (int i = 0; i < toSend.Length; i++)
                {
                    Type t = toSend[i].GetType();

                    #region primitives
                    if (t == typeof(byte))
                        bb.Write((byte)toSend[i]);
                    else if (t == typeof(sbyte))
                        bb.Write((sbyte)toSend[i]);

                    else if (t == typeof(ushort))
                        bb.Write((ushort)toSend[i]);
                    else if (t == typeof(short))
                        bb.Write((short)toSend[i]);

                    else if (t == typeof(int))
                        bb.Write((int)toSend[i]);
                    else if (t == typeof(uint))
                        bb.Write((uint)toSend[i]);

                    else if (t == typeof(long))
                        bb.Write((long)toSend[i]);
                    else if (t == typeof(ulong))
                        bb.Write((ulong)toSend[i]);

                    else if (t == typeof(float))
                        bb.Write((float)toSend[i]);
                    else if (t == typeof(double))
                        bb.Write((double)toSend[i]);
                    else if (t == typeof(decimal))
                        bb.Write((decimal)toSend[i]);

                    else if (t == typeof(DateTime))
                        bb.Write((DateTime)toSend[i]);
                    else if (t == typeof(TimeSpan))
                        bb.Write((TimeSpan)toSend[i]);

                    else if (t == typeof(BigInteger))
                        bb.Write((BigInteger)toSend[i]);
                    else if (t == typeof(Complex))
                        bb.Write((Complex)toSend[i]);

                    else if (t == typeof(MemoryStream))
                    {
                        bb.Write(((MemoryStream)toSend[i]).ToArray().Length);
                        bb.Write(((MemoryStream)toSend[i]).ToArray());
                    }
                    else if (t == typeof(BinBuffer))
                    {
                        bb.Write(((BinBuffer)toSend[i]).BytesLeft());
                        bb.Write(((BinBuffer)toSend[i]));
                    }
                    else if (t == typeof(BinBufferBuffer) || t.IsSubclassOf(typeof(BinBufferBuffer)))
                    {
                        bb.Write(((BinBufferBuffer)toSend[i]).BytesLeft());
                        bb.Write((new BinBuffer((BinBufferBuffer)toSend[i])));
                    }

                    else if (t == typeof(Vector2))
                        bb.Write((Vector2)toSend[i]);
                    else if (t == typeof(Color))
                        bb.Write((Color)toSend[i]);
                    else if (t == typeof(Item))
                        bb.Write((Item)toSend[i]);
                    #endregion

                    #region value type -> can read from memory
                    else if (t.IsValueType && (t.IsExplicitLayout || t.IsLayoutSequential) && !t.IsGenericType)
                    {
                        // this is probably lunacy
                        int size = Marshal.SizeOf(toSend[i]);

                        GCHandle argHandle = GCHandle.Alloc(toSend[i], GCHandleType.Pinned);
                        IntPtr offset = argHandle.AddrOfPinnedObject();

                        bb.Write(size);
                        for (IntPtr ptr = offset; ptr.ToInt64() < offset.ToInt64() + size; ptr += 1)
                            bb.Write(*(byte*)ptr.ToPointer());

                        argHandle.Free(); // HELLO.
                    }
                    #endregion

                    #region serilizable -> can use binaryformatter
                    else if (t.IsSerializable || t.GetInterface("System.Runtime.Serialization.ISerializable") != null)
                    {
                        BinaryFormatter bf = new BinaryFormatter();
                        MemoryStream ms = new MemoryStream();
                        bf.Serialize(ms, toSend[i]);
                        bb.Write(ms.ToArray().Length);
                        bb.Write(ms.ToArray());
                        ms.Close();
                    }
                    #endregion
                    else
                        throw new ArgumentException("Object must be a primitive, struct, or serializable.", "toSend[" + i + "]");
                }

                #region send some other stuff
                int pos = bb.Pos;
                bb.Pos = 0;
                bb.Write(pos - 4);
                bb.Pos = pos;

                if (Main.netMode == 1)
                    if (!Netplay.clientSock.tcpClient.Connected)
                        goto End;

                try
                {
                    NetMessage.buffer[num].spamCount++;
                    Main.txMsg++;
                    Main.txData += pos;
                    Main.txMsgType[100]++;
                    Main.txDataType[100] += pos;
                    Netplay.clientSock.networkStream.BeginWrite(NetMessage.buffer[num].writeBuffer, 0, pos, new AsyncCallback(Netplay.clientSock.ClientWriteCallBack), Netplay.clientSock.networkStream);
                }
                catch
                {
                    goto End;
                }

                if (remoteClient == -1)
                    for (int i = 0; i < 256; i++)
                        if (i != ignoreClient && NetMessage.buffer[i].broadcast && Netplay.serverSock[i].tcpClient.Connected)
                            try
                            {
                                NetMessage.buffer[i].spamCount++;
                                Main.txMsg++;
                                Main.txData += pos;
                                Main.txMsgType[100]++;
                                Main.txDataType[100] += pos;
                                Netplay.serverSock[i].networkStream.BeginWrite(NetMessage.buffer[num].writeBuffer, 0, pos, new AsyncCallback(Netplay.serverSock[i].ServerWriteCallBack), Netplay.serverSock[i].networkStream);
                            }
                            catch { }

                        else if (Netplay.serverSock[remoteClient].tcpClient.Connected)
                            try
                            {
                                NetMessage.buffer[remoteClient].spamCount++;
                                Main.txMsg++;
                                Main.txData += pos;
                                Main.txMsgType[100]++;
                                Main.txDataType[100] += pos;
                                Netplay.serverSock[remoteClient].networkStream.BeginWrite(NetMessage.buffer[num].writeBuffer, 0, pos, new AsyncCallback(Netplay.serverSock[remoteClient].ServerWriteCallBack), Netplay.serverSock[remoteClient].networkStream);
                            }
                            catch { }

            End:
                NetMessage.buffer[num].writeLocked = false;
                #endregion
            }
        }

        internal static unsafe object ReadObject(Type t, BinBuffer bb)
        {
            object ret = null;

            #region primitives
            if (t == typeof(byte))
                ret = bb.ReadByte();
            else if (t == typeof(sbyte))
                ret = bb.ReadSByte();

            else if (t == typeof(ushort))
                ret = bb.ReadUShort();
            else if (t == typeof(short))
                ret = bb.ReadShort();

            else if (t == typeof(int))
                ret = bb.ReadInt();
            else if (t == typeof(uint))
                ret = bb.ReadUInt();

            else if (t == typeof(long))
                ret = bb.ReadLong();
            else if (t == typeof(ulong))
                ret = bb.ReadULong();

            else if (t == typeof(float))
                ret = bb.ReadFloat();
            else if (t == typeof(double))
                ret = bb.ReadDouble();
            else if (t == typeof(decimal))
                ret = bb.ReadDecimal();

            else if (t == typeof(DateTime))
                ret = bb.ReadDateTime();
            else if (t == typeof(TimeSpan))
                ret = bb.ReadTimeSpan();

            else if (t == typeof(BigInteger))
                ret = bb.ReadBigInt();
            else if (t == typeof(Complex))
                ret = bb.ReadComplex();

            else if (t == typeof(MemoryStream))
                ret = new MemoryStream(bb.ReadBytes(bb.ReadInt()));
            else if (t == typeof(BinBuffer))
                ret = new BinBuffer(new BinBufferByte(bb.ReadBytes(bb.ReadInt())));
            else if (t == typeof(BinBufferBuffer))
                ret = new BinBuffer(new BinBufferByte(bb.ReadBytes(bb.ReadInt())));

            else if (t == typeof(Vector2))
                ret = bb.ReadVector2();
            else if (t == typeof(Color))
                ret = bb.ReadColor();
            else if (t == typeof(Item))
                ret = bb.ReadItem();
            #endregion

            #region value type -> can read from memory
            else if (t.IsValueType && (t.IsExplicitLayout || t.IsLayoutSequential) && !t.IsGenericType)
            {
                // this is probably lunacy
                int size = bb.ReadInt();
                byte[] data = bb.ReadBytes(size);

                // ret = *(T*)data, but with objects
                fixed (byte* dataPtr = data)
                {
                    ret = Marshal.PtrToStructure(new IntPtr(dataPtr), t);
                }
            }
            #endregion

            #region serilizable -> can use binaryformatter
            else if (t.IsSerializable || t.GetInterface("System.Runtime.Serialization.ISerializable") != null)
            {
                BinaryFormatter bf = new BinaryFormatter();
                MemoryStream ms = new MemoryStream(bb.ReadBytes(bb.ReadInt()));
                ret = bf.Deserialize(ms);
                ms.Close();
            }
            #endregion

            return ret;
        }
        internal static T ReadObject<T>(BinBuffer bb)
        {
            return (T)(dynamic)ReadObject(typeof(T), bb);
        }
    }
}
