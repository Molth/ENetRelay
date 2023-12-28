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
    ///     断开客户端连接信息
    /// </summary>
    internal struct DisconnectRemoteClientMessage : IMemoryPackable<DisconnectRemoteClientMessage>, INetworkMessage
    {
        /// <summary>
        ///     房间Id
        /// </summary>
        public uint RoomId;

        /// <summary>
        ///     构造
        /// </summary>
        /// <param name="roomId">房间Id</param>
        public DisconnectRemoteClientMessage(uint roomId) => RoomId = roomId;

        /// <summary>
        ///     静态构造
        /// </summary>
        static DisconnectRemoteClientMessage() => MemoryPackFormatterProvider.Register<DisconnectRemoteClientMessage>();

        /// <summary>
        ///     注册序列化器
        /// </summary>
        [Preserve]
        static void IMemoryPackFormatterRegister.RegisterFormatter()
        {
            if (!MemoryPackFormatterProvider.IsRegistered<DisconnectRemoteClientMessage>())
                MemoryPackFormatterProvider.Register(new MemoryPackableFormatter<DisconnectRemoteClientMessage>());
            if (!MemoryPackFormatterProvider.IsRegistered<DisconnectRemoteClientMessage[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<DisconnectRemoteClientMessage>());
        }

        /// <summary>
        ///     序列化
        /// </summary>
        [Preserve]
        static void IMemoryPackable<DisconnectRemoteClientMessage>.Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, scoped ref DisconnectRemoteClientMessage value) => writer.WriteUnmanaged(value);

        /// <summary>
        ///     反序列化
        /// </summary>
        [Preserve]
        static void IMemoryPackable<DisconnectRemoteClientMessage>.Deserialize(ref MemoryPackReader reader, scoped ref DisconnectRemoteClientMessage value) => reader.ReadUnmanaged(out value);
    }
}