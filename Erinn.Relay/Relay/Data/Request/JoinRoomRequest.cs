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
    ///     加入房间请求
    /// </summary>
    internal struct JoinRoomRequest : IMemoryPackable<JoinRoomRequest>, INetworkMessage
    {
        /// <summary>
        ///     主机Id
        /// </summary>
        public uint HostId;

        /// <summary>
        ///     构造
        /// </summary>
        /// <param name="hostId">主机Id</param>
        public JoinRoomRequest(uint hostId) => HostId = hostId;

        /// <summary>
        ///     静态构造
        /// </summary>
        static JoinRoomRequest() => MemoryPackFormatterProvider.Register<JoinRoomRequest>();

        /// <summary>
        ///     注册序列化器
        /// </summary>
        [Preserve]
        static void IMemoryPackFormatterRegister.RegisterFormatter()
        {
            if (!MemoryPackFormatterProvider.IsRegistered<JoinRoomRequest>())
                MemoryPackFormatterProvider.Register(new MemoryPackableFormatter<JoinRoomRequest>());
            if (!MemoryPackFormatterProvider.IsRegistered<JoinRoomRequest[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<JoinRoomRequest>());
        }

        /// <summary>
        ///     序列化
        /// </summary>
        [Preserve]
        static void IMemoryPackable<JoinRoomRequest>.Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, scoped ref JoinRoomRequest value) => writer.WriteUnmanaged(value);

        /// <summary>
        ///     反序列化
        /// </summary>
        [Preserve]
        static void IMemoryPackable<JoinRoomRequest>.Deserialize(ref MemoryPackReader reader, scoped ref JoinRoomRequest value) => reader.ReadUnmanaged(out value);
    }
}