//------------------------------------------------------------
// Erinn Network
// Copyright © 2023 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     通道写入器
    /// </summary>
    /// <typeparam name="TWrite">写入</typeparam>
    public readonly struct ChannelWriter<TWrite>
    {
        /// <summary>
        ///     写入委托
        /// </summary>
        public readonly Action<TWrite> Write;

        /// <summary>
        ///     构造
        /// </summary>
        /// <param name="write">写入委托</param>
        public ChannelWriter(Action<TWrite> write) => Write = write;
    }
}