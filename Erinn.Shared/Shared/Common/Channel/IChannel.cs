//------------------------------------------------------------
// Erinn Network
// Copyright © 2023 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     通道接口
    /// </summary>
    /// <typeparam name="TWrite">写入</typeparam>
    public interface IChannel<in TWrite>
    {
        /// <summary>
        ///     写入
        /// </summary>
        void Write(TWrite write);
    }
}