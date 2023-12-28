//------------------------------------------------------------
// Erinn Network
// Copyright © 2023 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     映射
    /// </summary>
    public sealed class Map<TKey, TValue> where TKey : notnull where TValue : notnull
    {
        /// <summary>
        ///     键
        /// </summary>
        private readonly Dictionary<TKey, TValue> _keys = new();

        /// <summary>
        ///     值
        /// </summary>
        private readonly Dictionary<TValue, TKey> _values = new();

        /// <summary>
        ///     当前数量
        /// </summary>
        public int Count => _keys.Count;

        /// <summary>
        ///     键
        /// </summary>
        public Dictionary<TKey, TValue>.KeyCollection Keys => _keys.Keys;

        /// <summary>
        ///     值
        /// </summary>
        public Dictionary<TValue, TKey>.KeyCollection Values => _values.Keys;

        /// <summary>
        ///     获取值
        /// </summary>
        public TValue this[TKey tKey]
        {
            get => _keys[tKey];
            set
            {
                _keys[tKey] = value;
                _values[value] = tKey;
            }
        }

        /// <summary>
        ///     获取键
        /// </summary>
        public TKey this[TValue tValue]
        {
            get => _values[tValue];
            set
            {
                _values[tValue] = value;
                _keys[value] = tValue;
            }
        }

        /// <summary>
        ///     添加
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public void Add(TKey key, TValue value)
        {
            _keys.Add(key, value);
            _values.Add(value, key);
        }

        /// <summary>
        ///     尝试添加
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns>添加成功</returns>
        public bool TryAdd(TKey key, TValue value)
        {
            if (_keys.ContainsKey(key) || _values.ContainsKey(value))
                return false;
            _keys[key] = value;
            _values[value] = key;
            return true;
        }

        /// <summary>
        ///     移除键
        /// </summary>
        /// <param name="key">键</param>
        public void RemoveKey(TKey key)
        {
            if (!_keys.TryGetValue(key, out var value))
                return;
            _values.Remove(value);
            _keys.Remove(key);
        }

        /// <summary>
        ///     移除值
        /// </summary>
        /// <param name="value">值</param>
        public void RemoveValue(TValue value)
        {
            if (!_values.TryGetValue(value, out var key))
                return;
            _keys.Remove(key);
            _values.Remove(value);
        }

        /// <summary>
        ///     获取值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public TValue GetValue(TKey key) => _keys[key];

        /// <summary>
        ///     获取键
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>键</returns>
        public TKey GetKey(TValue value) => _values[value];

        /// <summary>
        ///     获取值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns>获取成功</returns>
        public bool TryGetValue(TKey key, out TValue? value) => _keys.TryGetValue(key, out value);

        /// <summary>
        ///     获取键
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="key">键</param>
        /// <returns>获取成功</returns>
        public bool TryGetKey(TValue value, out TKey? key) => _values.TryGetValue(value, out key);

        /// <summary>
        ///     含有键
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>含有键</returns>
        public bool ContainsKey(TKey key) => _keys.ContainsKey(key);

        /// <summary>
        ///     含有值
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>值</returns>
        public bool ContainsValue(TValue value) => _values.ContainsKey(value);

        /// <summary>
        ///     含有指定键值对
        /// </summary>
        /// <param name="tKey">键</param>
        /// <param name="tValue">值</param>
        /// <returns>是否含有指定键值对</returns>
        public bool Contains(TKey tKey, TValue tValue) => _keys.TryGetValue(tKey, out var value) && _values.TryGetValue(tValue, out var key) && value.Equals(tValue) && key.Equals(tKey);

        /// <summary>
        ///     确保容量
        /// </summary>
        /// <param name="capacity">容量</param>
        public void EnsureCapacity(int capacity)
        {
            _keys.EnsureCapacity(capacity);
            _values.EnsureCapacity(capacity);
        }

        /// <summary>
        ///     缩小容量
        /// </summary>
        public void TrimExcess()
        {
            _keys.TrimExcess();
            _values.TrimExcess();
        }

        /// <summary>
        ///     缩小容量
        /// </summary>
        /// <param name="capacity">容量</param>
        public void TrimExcess(int capacity)
        {
            _keys.TrimExcess(capacity);
            _values.TrimExcess(capacity);
        }

        /// <summary>
        ///     清空
        /// </summary>
        public void Clear()
        {
            _keys.Clear();
            _values.Clear();
        }
    }
}