//------------------------------------------------------------
// Erinn Network
// Copyright © 2023 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using MemoryPack;

namespace Erinn
{
    /// <summary>
    ///     房间管理器
    /// </summary>
    public sealed class RoomMaster : IServerCallback
    {
        /// <summary>
        ///     房间
        /// </summary>
        private readonly Dictionary<string, Room> _nameRoom = new();

        /// <summary>
        ///     房间
        /// </summary>
        private readonly Dictionary<uint, Room> _idRoom = new();

        /// <summary>
        ///     主管服务器
        /// </summary>
        private readonly NetworkServer _master;

        /// <summary>
        ///     构造
        /// </summary>
        /// <param name="master">主管服务器</param>
        public RoomMaster(NetworkServer master)
        {
            _master = master;
            _master.SetTimeout(0U);
            Room.Init(_master);
            _master.OnDisconnectedCallback += OnDisconnectedCallback;
            _master.RegisterHandler<RelayServerPacket>(OnRelayServerPacket);
            _master.RegisterHandler<RelayPacket>(OnRelayPacket);
            _master.RegisterHandler<CreateRoomRequest>(OnCreateRoomRequest);
            _master.RegisterHandler<JoinRoomRequest>(OnJoinRoomRequest);
            _master.RegisterHandler<DisconnectRemoteClientMessage>(OnDisconnectRemoteClientMessage);
        }

        /// <summary>
        ///     发送数据包
        /// </summary>
        /// <param name="id">客户端Id</param>
        /// <param name="obj">值</param>
        /// <typeparam name="T">类型</typeparam>
        private void Send<T>(uint id, T obj) where T : struct, INetworkMessage, IMemoryPackable<T> => _master.Send(id, obj);

        /// <summary>
        ///     中转服务器数据包
        /// </summary>
        [ServerCallback]
        private void OnRelayServerPacket(uint id, RelayServerPacket data)
        {
            if (!_idRoom.TryGetValue(id, out var room))
                return;
            room.OnRelayServerPacket(id, data);
        }

        /// <summary>
        ///     中转数据包
        /// </summary>
        [ServerCallback]
        private void OnRelayPacket(uint id, RelayPacket data)
        {
            if (!_idRoom.TryGetValue(id, out var room))
                return;
            room.OnRelayPacket(id, data);
        }

        /// <summary>
        ///     创建房间
        /// </summary>
        [ServerCallback]
        private void OnCreateRoomRequest(uint id, CreateRoomRequest data)
        {
            if (_idRoom.ContainsKey(id))
            {
                Send(id, new CreateRoomResponse { Success = false });
                return;
            }

            var roomName = $"Session{id:D6}";
            var room = Room.Rent(id, roomName);
            _nameRoom[roomName] = room;
            _idRoom[id] = room;
            Send(id, new CreateRoomResponse { Success = true, HostId = id });
            Log.Info($"客户端[{id}]创建房间[{roomName}]");
        }

        /// <summary>
        ///     加入房间
        /// </summary>
        [ServerCallback]
        private void OnJoinRoomRequest(uint id, JoinRoomRequest data)
        {
            if (_idRoom.ContainsKey(id))
            {
                Send(id, new JoinRoomResponse { Success = false });
                return;
            }

            var roomName = $"Session{data.HostId:D6}";
            if (!_nameRoom.TryGetValue(roomName, out var room))
            {
                Send(id, new JoinRoomResponse { Success = false });
            }
            else
            {
                room.OnJoinRoom(id);
                _idRoom[id] = room;
                Send(id, new JoinRoomResponse { Success = true });
                Log.Info($"客户端[{id}]加入房间[{roomName}]");
            }
        }

        /// <summary>
        ///     断开连接
        /// </summary>
        [ServerCallback]
        private void OnDisconnectRemoteClientMessage(uint id, DisconnectRemoteClientMessage data)
        {
            if (!_idRoom.TryGetValue(id, out var room))
                return;
            room.OnDisconnectRemoteClient(id, data);
        }

        /// <summary>
        ///     断开连接回调
        /// </summary>
        private void OnDisconnectedCallback(uint id)
        {
            if (!_idRoom.TryGetValue(id, out var room))
                return;
            _idRoom.Remove(id);
            var roomName = room.RoomName;
            if (room.MasterId != id)
            {
                room.OnLeftRoom(id);
                Log.Info($"客户端[{id}]离开房间[{roomName}]");
                return;
            }

            var connectionIds = new List<uint>(room.Connections);
            foreach (var connectionId in connectionIds)
            {
                _idRoom.Remove(connectionId);
                _master.Disconnect(connectionId);
            }

            _nameRoom.Remove(roomName);
            Room.Return(room);
            Log.Info($"客户端[{id}]移除房间[{roomName}]");
        }
    }
}