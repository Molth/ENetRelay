//------------------------------------------------------------
// Erinn Network
// Copyright © 2023 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     对象池
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    public class Pool<T>
    {
        /// <summary>
        ///     队列
        /// </summary>
        private readonly Queue<T> _objects = new();

        /// <summary>
        ///     生成
        /// </summary>
        private readonly Func<T> _objectGenerator;

        /// <summary>
        ///     重置
        /// </summary>
        private readonly Action<T> _objectResetter;

        /// <summary>
        ///     构造
        /// </summary>
        /// <param name="objectGenerator">生成</param>
        /// <param name="objectResetter">重置</param>
        /// <param name="initialCapacity">容量</param>
        public Pool(Func<T> objectGenerator, Action<T> objectResetter, int initialCapacity)
        {
            _objectGenerator = objectGenerator;
            _objectResetter = objectResetter;
            for (var i = 0; i < initialCapacity; ++i)
                _objects.Enqueue(objectGenerator());
        }

        /// <summary>
        ///     获取T
        /// </summary>
        /// <returns>获得的T</returns>
        public T Rent() => _objects.Count > 0 ? _objects.Dequeue() : _objectGenerator();

        /// <summary>
        ///     返回T
        /// </summary>
        /// <param name="item">T</param>
        public void Return(T item)
        {
            _objectResetter(item);
            _objects.Enqueue(item);
        }
    }
}