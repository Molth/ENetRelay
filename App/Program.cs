//------------------------------------------------------------
// Erinn Network
// Copyright © 2023 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    public sealed class Program
    {
        public static void Main() => Relay();

        public static void Relay()
        {
            var server = new NetworkServer();
            _ = new RoomMaster(server);
            server.Start(7777, 20);
            while (true)
                Thread.Sleep(100);
        }
    }
}