//------------------------------------------------------------
// Erinn Network
// Copyright © 2023 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using MemoryPack;

#pragma warning disable CS8618
#pragma warning disable CS8625

namespace Erinn
{
    /// <summary>
    ///     房间
    /// </summary>
    internal sealed class Room
    {
        /// <summary>
        ///     主管服务器
        /// </summary>
        private static NetworkServer _master;

        /// <summary>
        ///     房间池
        /// </summary>
        private static readonly Pool<Room> RoomPool = new(Generator, Resetter, 128);

        /// <summary>
        ///     主管客户端Id
        /// </summary>
        public uint MasterId;

        /// <summary>
        ///     房间名称
        /// </summary>
        public string RoomName;

        /// <summary>
        ///     房间Id池
        /// </summary>
        private readonly UintIndexList _roomIdPool = new();

        /// <summary>
        ///     房间Id连接映射
        /// </summary>
        private readonly Map<uint, uint> _roomConnection = new();

        /// <summary>
        ///     连接
        /// </summary>
        public IEnumerable<uint> Connections => _roomConnection.Values;

        /// <summary>
        ///     发送数据包
        /// </summary>
        /// <param name="id">客户端Id</param>
        /// <param name="obj">object</param>
        /// <typeparam name="T">类型</typeparam>
        private static void Send<T>(uint id, T obj) where T : struct, INetworkMessage, IMemoryPackable<T> => _master.Send(id, obj);

        /// <summary>
        ///     初始化
        /// </summary>
        /// <param name="master">主管服务器</param>
        public static void Init(NetworkServer master) => _master = master;

        /// <summary>
        ///     获取房间
        /// </summary>
        /// <param name="masterId">主管客户端Id</param>
        /// <param name="roomName">房间名称</param>
        /// <returns>获得的房间</returns>
        public static Room Rent(uint masterId, string roomName)
        {
            var room = RoomPool.Rent();
            room.Generate(masterId, roomName);
            return room;
        }

        /// <summary>
        ///     返回房间
        /// </summary>
        /// <param name="room">房间</param>
        public static void Return(Room room) => RoomPool.Return(room);

        /// <summary>
        ///     生成房间
        /// </summary>
        /// <returns>生成的房间</returns>
        private static Room Generator() => new();

        /// <summary>
        ///     重置房间
        /// </summary>
        /// <param name="room">房间</param>
        private static void Resetter(Room room) => room.Reset();

        /// <summary>
        ///     生成
        /// </summary>
        /// <param name="masterId">主管客户端Id</param>
        /// <param name="roomName">房间名称</param>
        private void Generate(uint masterId, string roomName)
        {
            MasterId = masterId;
            RoomName = roomName;
            _roomConnection.Add(0, masterId);
        }

        /// <summary>
        ///     重置
        /// </summary>
        private void Reset()
        {
            MasterId = 0;
            RoomName = null;
            _roomConnection.Clear();
            _roomIdPool.Clear();
        }

        /// <summary>
        ///     中转服务器数据包
        ///     Only Client
        ///     To Host/Server
        /// </summary>
        public void OnRelayServerPacket(uint id, RelayServerPacket data)
        {
            if (MasterId == id)
            {
                var relayPacket = new RelayPacket(0U, data.Payload);
                Send(MasterId, relayPacket);
            }
            else if (_roomConnection.TryGetKey(id, out var senderId))
            {
                var relayPacket = new RelayPacket(senderId, data.Payload);
                Send(MasterId, relayPacket);
            }
        }

        /// <summary>
        ///     中转数据包
        ///     Only Host/Server
        ///     To Client
        /// </summary>
        public void OnRelayPacket(uint id, RelayPacket data)
        {
            if (MasterId != id)
                return;
            var targetId = data.RoomId;
            if (targetId == 0U)
            {
                var relayServerPacket = new RelayServerPacket(data.Payload);
                Send(MasterId, relayServerPacket);
            }
            else if (_roomConnection.TryGetValue(targetId, out var connectionId))
            {
                var relayServerPacket = new RelayServerPacket(data.Payload);
                Send(connectionId, relayServerPacket);
            }
        }

        /// <summary>
        ///     加入房间
        /// </summary>
        public void OnJoinRoom(uint id)
        {
            var roomId = _roomIdPool.Pop();
            _roomConnection.Add(roomId, id);
            Send(MasterId, new JoinedRoomMessage { RoomId = roomId });
        }

        /// <summary>
        ///     离开房间
        /// </summary>
        public void OnLeftRoom(uint id)
        {
            var roomId = _roomConnection.GetKey(id);
            _roomIdPool.Push(roomId);
            _roomConnection.RemoveKey(roomId);
            Send(MasterId, new LeftRoomMessage { RoomId = roomId });
        }

        /// <summary>
        ///     断开连接
        /// </summary>
        public void OnDisconnectRemoteClient(uint id, DisconnectRemoteClientMessage data)
        {
            if (MasterId != id)
                return;
            if (!_roomConnection.TryGetValue(data.RoomId, out var connectionId))
                return;
            if (MasterId == connectionId)
                return;
            _master.Disconnect(connectionId);
        }
    }
}