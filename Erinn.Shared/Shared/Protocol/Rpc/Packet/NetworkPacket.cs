//------------------------------------------------------------
// Erinn Network
// Copyright © 2023 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using MemoryPack;
using MemoryPack.Formatters;
using MemoryPack.Internal;

#pragma warning disable CS8600
#pragma warning disable CS8604

namespace Erinn
{
    /// <summary>
    ///     网络数据包
    /// </summary>
    public struct NetworkPacket : IMemoryPackable<NetworkPacket>
    {
        /// <summary>
        ///     命令
        /// </summary>
        public byte[] Command;

        /// <summary>
        ///     值
        /// </summary>
        public byte[] Payload;

        /// <summary>
        ///     构造
        /// </summary>
        /// <param name="command">命令</param>
        /// <param name="payload">值</param>
        public NetworkPacket(byte[] command, byte[] payload)
        {
            Command = command;
            Payload = payload;
        }

        /// <summary>
        ///     静态构造
        /// </summary>
        static NetworkPacket() => MemoryPackFormatterProvider.Register<NetworkPacket>();

        /// <summary>
        ///     注册
        /// </summary>
        [Preserve]
        static void IMemoryPackFormatterRegister.RegisterFormatter()
        {
            if (!MemoryPackFormatterProvider.IsRegistered<NetworkPacket>())
                MemoryPackFormatterProvider.Register(new MemoryPackableFormatter<NetworkPacket>());
            if (!MemoryPackFormatterProvider.IsRegistered<NetworkPacket[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<NetworkPacket>());
            if (!MemoryPackFormatterProvider.IsRegistered<byte[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<byte>());
        }

        /// <summary>
        ///     序列化
        /// </summary>
        [Preserve]
        static void IMemoryPackable<NetworkPacket>.Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, scoped ref NetworkPacket value)
        {
            writer.WriteObjectHeader(2);
            writer.WriteUnmanagedArray(value.Command);
            writer.WriteUnmanagedArray(value.Payload);
        }

        /// <summary>
        ///     反序列化
        /// </summary>
        [Preserve]
        static void IMemoryPackable<NetworkPacket>.Deserialize(ref MemoryPackReader reader, scoped ref NetworkPacket value)
        {
            if (!reader.TryReadObjectHeader(out var memberCount))
            {
                value = new NetworkPacket();
            }
            else if (memberCount <= 2)
            {
                byte[] command;
                byte[] payload;
                if (memberCount == 2)
                {
                    command = reader.ReadUnmanagedArray<byte>();
                    payload = reader.ReadUnmanagedArray<byte>();
                }
                else
                {
                    command = null;
                    payload = null;
                    if (memberCount != 0)
                        reader.ReadUnmanagedArray(ref command);
                }

                value = new NetworkPacket(command, payload);
            }
            else
            {
                MemoryPackSerializationException.ThrowInvalidPropertyCount(typeof(NetworkPacket), 2, memberCount);
            }
        }
    }
}