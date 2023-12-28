//------------------------------------------------------------
// Erinn Network
// Copyright © 2023 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     通道
    /// </summary>
    /// <typeparam name="TWrite">写入</typeparam>
    /// <typeparam name="TRead">读取</typeparam>
    public abstract class Channel<TWrite, TRead> : IChannel<TWrite>
    {
        /// <summary>
        ///     写入器
        /// </summary>
        public readonly ChannelWriter<TWrite> Writer;

        /// <summary>
        ///     连接的写入器
        /// </summary>
        private ChannelWriter<TRead> _nextWriter;

        /// <summary>
        ///     构造
        /// </summary>
        protected Channel() => Writer = new ChannelWriter<TWrite>(Write);

        /// <summary>
        ///     写入
        /// </summary>
        public void Write(TWrite write)
        {
            if (Translate(write, out var read))
                _nextWriter.Write(read);
        }

        /// <summary>
        ///     处理
        /// </summary>
        /// <param name="write">写入</param>
        /// <param name="read">读取</param>
        /// <returns>是否处理成功</returns>
        protected abstract bool Translate(TWrite write, out TRead read);

        /// <summary>
        ///     连接通道
        /// </summary>
        /// <param name="channel">写入</param>
        public Channel<TRead, T> Link<T>(Channel<TRead, T> channel)
        {
            _nextWriter = channel.Writer;
            return channel;
        }

        /// <summary>
        ///     连接通道
        /// </summary>
        /// <param name="channel">写入</param>
        public void Link(ChannelEndPoint<TRead> channel) => _nextWriter = channel.Writer;
    }
}