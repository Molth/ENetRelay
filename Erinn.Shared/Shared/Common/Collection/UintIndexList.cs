//------------------------------------------------------------
// Erinn Network
// Copyright © 2023 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     索引List
    /// </summary>
    public sealed class UintIndexList
    {
        /// <summary>
        ///     闲置索引
        /// </summary>
        private readonly List<uint> _idleIndex = new();

        /// <summary>
        ///     当前索引
        /// </summary>
        private uint _index;

        /// <summary>
        ///     起始
        /// </summary>
        private readonly uint _start;

        /// <summary>
        ///     构造
        /// </summary>
        public UintIndexList()
        {
            _start = 1;
            _index = _start;
        }

        /// <summary>
        ///     构造
        /// </summary>
        /// <param name="start">起始</param>
        public UintIndexList(uint start)
        {
            _start = start;
            _index = _start;
        }

        /// <summary>
        ///     当前数量
        /// </summary>
        public int Count => _idleIndex.Count;

        /// <summary>
        ///     清空索引
        /// </summary>
        public void Clear()
        {
            _idleIndex.Clear();
            _index = _start;
        }

        /// <summary>
        ///     出队索引
        /// </summary>
        /// <returns>获得的新索引</returns>
        public uint Dequeue()
        {
            if (_idleIndex.Count > 0)
                return Dequeue(_idleIndex);
            var index = _index;
            _index++;
            return index;
        }

        /// <summary>
        ///     入队索引
        /// </summary>
        /// <param name="index">要推入的索引</param>
        public void Enqueue(uint index) => _idleIndex.Add(index);

        /// <summary>
        ///     弹出索引
        /// </summary>
        /// <returns>获得的新索引</returns>
        public uint Pop()
        {
            if (_idleIndex.Count > 0)
                return Pop(_idleIndex);
            var index = _index;
            _index++;
            return index;
        }

        /// <summary>
        ///     推入索引
        /// </summary>
        /// <param name="index">要推入的索引</param>
        public void Push(uint index) => _idleIndex.Add(index);

        /// <summary>
        ///     弹出索引
        /// </summary>
        /// <returns>获得的新索引</returns>
        private static uint Pop(List<uint> list)
        {
            var index = list.Count - 1;
            var value = list[index];
            list.RemoveAt(index);
            return value;
        }

        /// <summary>
        ///     出队索引
        /// </summary>
        /// <returns>获得的新索引</returns>
        private static uint Dequeue(List<uint> list)
        {
            var value = list[0];
            list.RemoveAt(0);
            return value;
        }
    }
}