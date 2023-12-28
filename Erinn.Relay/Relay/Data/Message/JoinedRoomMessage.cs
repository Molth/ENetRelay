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
    ///     加入房间信息
    /// </summary>
    internal struct JoinedRoomMessage : IMemoryPackable<JoinedRoomMessage>, INetworkMessage
    {
        /// <summary>
        ///     房间Id
        /// </summary>
        public uint RoomId;

        /// <summary>
        ///     构造
        /// </summary>
        /// <param name="roomId">房间Id</param>
        public JoinedRoomMessage(uint roomId) => RoomId = roomId;

        /// <summary>
        ///     静态构造
        /// </summary>
        static JoinedRoomMessage() => MemoryPackFormatterProvider.Register<JoinedRoomMessage>();

        /// <summary>
        ///     注册序列化器
        /// </summary>
        [Preserve]
        static void IMemoryPackFormatterRegister.RegisterFormatter()
        {
            if (!MemoryPackFormatterProvider.IsRegistered<JoinedRoomMessage>())
                MemoryPackFormatterProvider.Register(new MemoryPackableFormatter<JoinedRoomMessage>());
            if (!MemoryPackFormatterProvider.IsRegistered<JoinedRoomMessage[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<JoinedRoomMessage>());
        }

        /// <summary>
        ///     序列化
        /// </summary>
        [Preserve]
        static void IMemoryPackable<JoinedRoomMessage>.Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, scoped ref JoinedRoomMessage value) => writer.WriteUnmanaged(value);

        /// <summary>
        ///     反序列化
        /// </summary>
        [Preserve]
        static void IMemoryPackable<JoinedRoomMessage>.Deserialize(ref MemoryPackReader reader, scoped ref JoinedRoomMessage value) => reader.ReadUnmanaged(out value);
    }
}