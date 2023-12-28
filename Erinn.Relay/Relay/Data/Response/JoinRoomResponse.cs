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
    ///     加入房间响应
    /// </summary>
    internal struct JoinRoomResponse : IMemoryPackable<JoinRoomResponse>, INetworkMessage
    {
        /// <summary>
        ///     是否成功
        /// </summary>
        public bool Success;

        /// <summary>
        ///     构造
        /// </summary>
        /// <param name="success">是否成功</param>
        public JoinRoomResponse(bool success) => Success = success;

        /// <summary>
        ///     静态构造
        /// </summary>
        static JoinRoomResponse() => MemoryPackFormatterProvider.Register<JoinRoomResponse>();

        /// <summary>
        ///     注册序列化器
        /// </summary>
        [Preserve]
        static void IMemoryPackFormatterRegister.RegisterFormatter()
        {
            if (!MemoryPackFormatterProvider.IsRegistered<JoinRoomResponse>())
                MemoryPackFormatterProvider.Register(new MemoryPackableFormatter<JoinRoomResponse>());
            if (!MemoryPackFormatterProvider.IsRegistered<JoinRoomResponse[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<JoinRoomResponse>());
        }

        /// <summary>
        ///     序列化
        /// </summary>
        [Preserve]
        static void IMemoryPackable<JoinRoomResponse>.Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, scoped ref JoinRoomResponse value) => writer.WriteUnmanaged(value);

        /// <summary>
        ///     反序列化
        /// </summary>
        [Preserve]
        static void IMemoryPackable<JoinRoomResponse>.Deserialize(ref MemoryPackReader reader, scoped ref JoinRoomResponse value) => reader.ReadUnmanaged(out value);
    }
}