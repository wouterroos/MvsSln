﻿/*
 * The MIT License (MIT)
 *
 * Copyright (c) 2013-2019  Denis Kuzmin < entry.reg@gmail.com > GitHub/3F
 * Copyright (c) MvsSln contributors: https://github.com/3F/MvsSln/graphs/contributors
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
*/

using System;
using System.Collections;
using System.Collections.Generic;

namespace net.r_eg.MvsSln.Core
{
    public sealed class RoProperties: RoProperties<string, string>
    {
        public RoProperties(IDictionary<string, string> data)
            : base(data)
        {

        }
    }

    public class RoProperties<T, T2>: IDictionary<T, T2>
    {
        private IDictionary<T, T2> dict;

        public T2 this[T key] { get => dict[key]; set => throw new NotSupportedException(); }

        public ICollection<T> Keys => dict.Keys;

        public ICollection<T2> Values => dict.Values;

        public int Count => dict.Count;

        public bool IsReadOnly => true;

        public IDictionary<T, T2> ExtractDictionary { get => new Dictionary<T, T2>(dict); }

        public bool ContainsKey(T key) => dict.ContainsKey(key);

        public void CopyTo(KeyValuePair<T, T2>[] array, int index) => dict.CopyTo(array, index);

        public IEnumerator<KeyValuePair<T, T2>> GetEnumerator() => dict.GetEnumerator();

        public bool TryGetValue(T key, out T2 value) => dict.TryGetValue(key, out value);

        IEnumerator IEnumerable.GetEnumerator() => dict.GetEnumerator();

        public static implicit operator RoProperties<T, T2>(Dictionary<T, T2> dict)
        {
            return new RoProperties<T, T2>(dict);
        }

        public RoProperties(IDictionary<T, T2> data)
        {
            dict = data ?? throw new ArgumentNullException(nameof(data), MsgResource.ValueNoEmptyOrNull);
        }

        #region ExplicitImpl

        void IDictionary<T, T2>.Add(T key, T2 value) => Add(key, value);

        void ICollection<KeyValuePair<T, T2>>.Add(KeyValuePair<T, T2> item) => Add(item);

        void ICollection<KeyValuePair<T, T2>>.Clear() => Clear();

        bool ICollection<KeyValuePair<T, T2>>.Contains(KeyValuePair<T, T2> item) => Contains(item);

        bool IDictionary<T, T2>.Remove(T key) => Remove(key);

        bool ICollection<KeyValuePair<T, T2>>.Remove(KeyValuePair<T, T2> item) => Remove(item);

        #endregion

        #region NotSupported

        protected void Add(T key, T2 value)
        {
            throw new NotSupportedException();
        }

        protected void Add(KeyValuePair<T, T2> item)
        {
            throw new NotSupportedException();
        }

        protected void Clear()
        {
            throw new NotSupportedException();
        }

        protected bool Contains(KeyValuePair<T, T2> item)
        {
            throw new NotSupportedException();
        }

        protected bool Remove(T key)
        {
            throw new NotSupportedException();
        }

        protected bool Remove(KeyValuePair<T, T2> item)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}