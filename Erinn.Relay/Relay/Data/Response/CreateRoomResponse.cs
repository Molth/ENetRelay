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
    ///     创建房间响应
    /// </summary>
    internal struct CreateRoomResponse : IMemoryPackable<CreateRoomResponse>, INetworkMessage
    {
        /// <summary>
        ///     是否成功
        /// </summary>
        public bool Success;

        /// <summary>
        ///     主机Id
        /// </summary>
        public uint HostId;

        /// <summary>
        ///     构造
        /// </summary>
        /// <param name="success">是否成功</param>
        /// <param name="hostId">主机Id</param>
        public CreateRoomResponse(bool success, uint hostId)
        {
            Success = success;
            HostId = hostId;
        }

        /// <summary>
        ///     静态构造
        /// </summary>
        static CreateRoomResponse() => MemoryPackFormatterProvider.Register<CreateRoomResponse>();

        /// <summary>
        ///     注册序列化器
        /// </summary>
        [Preserve]
        static void IMemoryPackFormatterRegister.RegisterFormatter()
        {
            if (!MemoryPackFormatterProvider.IsRegistered<CreateRoomResponse>())
                MemoryPackFormatterProvider.Register(new MemoryPackableFormatter<CreateRoomResponse>());
            if (!MemoryPackFormatterProvider.IsRegistered<CreateRoomResponse[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<CreateRoomResponse>());
        }

        /// <summary>
        ///     序列化
        /// </summary>
        [Preserve]
        static void IMemoryPackable<CreateRoomResponse>.Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, scoped ref CreateRoomResponse value) => writer.WriteUnmanaged(value);

        /// <summary>
        ///     反序列化
        /// </summary>
        [Preserve]
        static void IMemoryPackable<CreateRoomResponse>.Deserialize(ref MemoryPackReader reader, scoped ref CreateRoomResponse value) => reader.ReadUnmanaged(out value);
    }
}