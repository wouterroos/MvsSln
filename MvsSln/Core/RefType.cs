﻿/*
 * The MIT License (MIT)
 * 
 * Copyright (c) 2013-2019  Denis Kuzmin < entry.reg@gmail.com > GitHub/3F
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

namespace net.r_eg.MvsSln.Core
{
    [DebuggerDisplay("{Value}")]
    public sealed class RefType<T>
    {
        public T Value
        {
            get;
            set;
        }

        public static implicit operator T(RefType<T> v)
        {
            if(v == null) {
                return default(T);
            }
            return v.Value;
        }

        public static implicit operator RefType<T>(T v)
        {
            return new RefType<T>(v);
        }

        public static bool operator ==(RefType<T> a, RefType<T> b)
        {
            bool _EqNull(RefType<T> x)
            {
                return Object.ReferenceEquals(x, null)
                    || Object.ReferenceEquals(x.Value, null);
            }
            return _EqNull(a) ? _EqNull(b) : a.Equals(b);
        }

        public static bool operator !=(RefType<T> a, RefType<T> b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if(Object.ReferenceEquals(obj, null) || !(obj is RefType<T>)) {
                return false;
            }
            return Value.Equals(((RefType<T>)obj).Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public RefType(T value)
        {
            Value = value;
        }

        public RefType()
        {

        }
    }
}