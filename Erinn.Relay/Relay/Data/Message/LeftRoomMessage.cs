//------------------------------------------------------------
// Erinn Network
// Copyright © 2023 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using MemoryPack;
using MemoryPack.Formatters;
using MemoryPack.Internal;

namespace Erinn
{
    /// <summary>
    ///     离开房间信息
    /// </summary>
    internal struct LeftRoomMessage : IMemoryPackable<LeftRoomMessage>, INetworkMessage
    {
        /// <summary>
        ///     房间Id
        /// </summary>
        public uint RoomId;

        /// <summary>
        ///     构造
        /// </summary>
        /// <param name="roomId">房间Id</param>
        public LeftRoomMessage(uint roomId) => RoomId = roomId;

        /// <summary>
        ///     静态构造
        /// </summary>
        static LeftRoomMessage() => MemoryPackFormatterProvider.Register<LeftRoomMessage>();

        /// <summary>
        ///     注册序列化器
        /// </summary>
        [Preserve]
        static void IMemoryPackFormatterRegister.RegisterFormatter()
        {
            if (!MemoryPackFormatterProvider.IsRegistered<LeftRoomMessage>())
                MemoryPackFormatterProvider.Register(new MemoryPackableFormatter<LeftRoomMessage>());
            if (!MemoryPackFormatterProvider.IsRegistered<LeftRoomMessage[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<LeftRoomMessage>());
        }

        /// <summary>
        ///     序列化
        /// </summary>
        [Preserve]
        static void IMemoryPackable<LeftRoomMessage>.Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, scoped ref LeftRoomMessage value) => writer.WriteUnmanaged(value);

        /// <summary>
        ///     反序列化
        /// </summary>
        [Preserve]
        static void IMemoryPackable<LeftRoomMessage>.Deserialize(ref MemoryPackReader reader, scoped ref LeftRoomMessage value) => reader.ReadUnmanaged(out value);
    }
}