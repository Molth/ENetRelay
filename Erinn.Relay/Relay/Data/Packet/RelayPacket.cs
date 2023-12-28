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
    internal struct RelayPacket : IMemoryPackable<RelayPacket>, INetworkMessage
    {
        /// <summary>
        ///     房间Id
        /// </summary>
        public uint RoomId;

        /// <summary>
        ///     负载
        /// </summary>
        public byte[] Payload;

        /// <summary>
        ///     构造
        /// </summary>
        /// <param name="roomId">房间Id</param>
        /// <param name="payload">负载</param>
        public RelayPacket(uint roomId, byte[] payload)
        {
            RoomId = roomId;
            Payload = payload;
        }

        /// <summary>
        ///     静态构造
        /// </summary>
        static RelayPacket() => MemoryPackFormatterProvider.Register<RelayPacket>();

        /// <summary>
        ///     注册
        /// </summary>
        [Preserve]
        static void IMemoryPackFormatterRegister.RegisterFormatter()
        {
            if (!MemoryPackFormatterProvider.IsRegistered<RelayPacket>())
                MemoryPackFormatterProvider.Register(new MemoryPackableFormatter<RelayPacket>());
            if (!MemoryPackFormatterProvider.IsRegistered<RelayPacket[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<RelayPacket>());
            if (!MemoryPackFormatterProvider.IsRegistered<byte[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<byte>());
        }

        /// <summary>
        ///     序列化
        /// </summary>
        [Preserve]
        static void IMemoryPackable<RelayPacket>.Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, scoped ref RelayPacket value)
        {
            writer.WriteUnmanagedWithObjectHeader(2, value.RoomId);
            writer.WriteValue(value.Payload);
        }

        /// <summary>
        ///     反序列化
        /// </summary>
        [Preserve]
        static void IMemoryPackable<RelayPacket>.Deserialize(ref MemoryPackReader reader, scoped ref RelayPacket value)
        {
            if (!reader.TryReadObjectHeader(out var memberCount))
            {
                value = new RelayPacket();
            }
            else
            {
                uint roomId;
                byte[] payload;
                if (memberCount == 2)
                {
                    reader.ReadUnmanaged(out roomId);
                    payload = reader.ReadUnmanagedArray<byte>();
                }
                else
                {
                    if (memberCount > 2)
                    {
                        MemoryPackSerializationException.ThrowInvalidPropertyCount(typeof(RelayPacket), 2, memberCount);
                        return;
                    }

                    roomId = 0U;
                    payload = null;
                    if (memberCount != 0)
                    {
                        reader.ReadUnmanaged(out roomId);
                        if (memberCount != 1)
                            reader.ReadValue(ref payload);
                    }
                }

                value = new RelayPacket(roomId, payload);
            }
        }
    }
}