//------------------------------------------------------------
// Erinn Network
// Copyright © 2023 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     远程调用属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class ClientCallbackAttribute : Attribute;
}