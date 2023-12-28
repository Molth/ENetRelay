//------------------------------------------------------------
// Erinn Network
// Copyright © 2023 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     通道端点
    /// </summary>
    /// <typeparam name="TWrite">写入</typeparam>
    public abstract class ChannelEndPoint<TWrite> : IChannel<TWrite>
    {
        /// <summary>
        ///     写入器
        /// </summary>
        public readonly ChannelWriter<TWrite> Writer;

        /// <summary>
        ///     构造
        /// </summary>
        protected ChannelEndPoint() => Writer = new ChannelWriter<TWrite>(Write);

        /// <summary>
        ///     写入
        /// </summary>
        public void Write(TWrite write) => Translate(write);

        /// <summary>
        ///     处理
        /// </summary>
        /// <param name="write">写入</param>
        /// <returns>读取</returns>
        protected abstract void Translate(TWrite write);
    }
}