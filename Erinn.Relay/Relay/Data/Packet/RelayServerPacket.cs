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
    ///     中转数据包
    /// </summary>
    internal struct RelayServerPacket : IMemoryPackable<RelayServerPacket>, INetworkMessage
    {
        /// <summary>
        ///     负载
        /// </summary>
        public byte[] Payload;

        /// <summary>
        ///     构造
        /// </summary>
        /// <param name="payload">负载</param>
        public RelayServerPacket(byte[] payload) => Payload = payload;

        /// <summary>
        ///     静态构造
        /// </summary>
        static RelayServerPacket() => MemoryPackFormatterProvider.Register<RelayServerPacket>();

        /// <summary>
        ///     注册
        /// </summary>
        [Preserve]
        static void IMemoryPackFormatterRegister.RegisterFormatter()
        {
            if (!MemoryPackFormatterProvider.IsRegistered<RelayServerPacket>())
                MemoryPackFormatterProvider.Register(new MemoryPackableFormatter<RelayServerPacket>());
            if (!MemoryPackFormatterProvider.IsRegistered<RelayServerPacket[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<RelayServerPacket>());
            if (!MemoryPackFormatterProvider.IsRegistered<byte[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<byte>());
        }

        /// <summary>
        ///     序列化
        /// </summary>
        [Preserve]
        static void IMemoryPackable<RelayServerPacket>.Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, scoped ref RelayServerPacket value)
        {
            writer.WriteObjectHeader(1);
            writer.WriteValue(value.Payload);
        }

        /// <summary>
        ///     反序列化
        /// </summary>
        [Preserve]
        static void IMemoryPackable<RelayServerPacket>.Deserialize(ref MemoryPackReader reader, scoped ref RelayServerPacket value)
        {
            if (!reader.TryReadObjectHeader(out var memberCount))
            {
                value = new RelayServerPacket();
            }
            else
            {
                byte[] payload;
                if (memberCount == 1)
                {
                    payload = reader.ReadUnmanagedArray<byte>();
                }
                else
                {
                    if (memberCount > 1)
                    {
                        MemoryPackSerializationException.ThrowInvalidPropertyCount(typeof(RelayServerPacket), 1, memberCount);
                        return;
                    }

                    payload = null;
                    if (memberCount != 0)
                        reader.ReadValue(ref payload);
                }

                value = new RelayServerPacket(payload);
            }
        }
    }
}