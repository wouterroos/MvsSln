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
using System.Diagnostics;
using System.Text;
using net.r_eg.MvsSln.Extensions;

namespace net.r_eg.MvsSln.Core
{
    [DebuggerDisplay("{DbgDisplay}")]
    public struct RawText
    {
        public Encoding encoding;

        public string data;

        /// <summary>
        /// data without whitespace characters.
        /// </summary>
        public string trimmed;

        public static implicit operator string(RawText raw)
        {
            return raw.ToString();
        }

        public static implicit operator RawText(string str)
        {
            return new RawText(str);
        }

        public static bool operator ==(RawText a, RawText b)
        {
            return Object.ReferenceEquals(a, null) ?
                    Object.ReferenceEquals(b, null) : a.Equals(b);
        }

        public static bool operator !=(RawText a, RawText b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if(Object.ReferenceEquals(obj, null) || !(obj is RawText)) {
                return false;
            }

            var b = (RawText)obj;

            return data == b.data
                    && trimmed == b.trimmed
                    && encoding == b.encoding;
        }

        public override int GetHashCode()
        {
            return 0.CalculateHashCode
            (
                data.GetHashCode(),
                trimmed.GetHashCode(),
                encoding.GetHashCode()
            );
        }

        public override string ToString()
        {
            return data;
        }

        /// <param name="data"></param>
        public RawText(string data)
            : this(data, Encoding.UTF8)
        {

        }

        /// <param name="data"></param>
        /// <param name="enc"></param>
        public RawText(string data, Encoding enc)
        {
            this.data   = data;
            encoding    = enc;
            trimmed     = data?.Trim();
        }

        #region DebuggerDisplay

        private string DbgDisplay
        {
            get => data;
        }

        #endregion
    }
}