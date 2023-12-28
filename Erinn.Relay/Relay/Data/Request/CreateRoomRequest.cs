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
    ///     创建房间请求
    /// </summary>
    internal struct CreateRoomRequest : IMemoryPackable<CreateRoomRequest>, INetworkMessage
    {
        /// <summary>
        ///     静态构造
        /// </summary>
        static CreateRoomRequest() => MemoryPackFormatterProvider.Register<CreateRoomRequest>();

        /// <summary>
        ///     注册序列化器
        /// </summary>
        [Preserve]
        static void IMemoryPackFormatterRegister.RegisterFormatter()
        {
            if (!MemoryPackFormatterProvider.IsRegistered<CreateRoomRequest>())
                MemoryPackFormatterProvider.Register(new MemoryPackableFormatter<CreateRoomRequest>());
            if (!MemoryPackFormatterProvider.IsRegistered<CreateRoomRequest[]>())
                MemoryPackFormatterProvider.Register(new ArrayFormatter<CreateRoomRequest>());
        }

        /// <summary>
        ///     序列化
        /// </summary>
        [Preserve]
        static void IMemoryPackable<CreateRoomRequest>.Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, scoped ref CreateRoomRequest value) => writer.WriteUnmanaged(value);

        /// <summary>
        ///     反序列化
        /// </summary>
        [Preserve]
        static void IMemoryPackable<CreateRoomRequest>.Deserialize(ref MemoryPackReader reader, scoped ref CreateRoomRequest value) => reader.ReadUnmanaged(out value);
    }
}