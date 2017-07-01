﻿/*
 * The MIT License (MIT)
 * 
 * Copyright (c) 2013-2017  Denis Kuzmin < entry.reg@gmail.com > :: github.com/3F
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
using net.r_eg.MvsSln.Core;

namespace net.r_eg.MvsSln
{
    /// <summary>
    /// Wrapper of the default solution parser.
    /// </summary>
    public sealed class Sln: IDisposable
    {
        private static ISlnContainer parser = new SlnParser();

        /// <summary>
        /// Parsed solution data.
        /// </summary>
        public ISlnResult Result
        {
            get;
            private set;
        }

        /// <param name="file">Solution file</param>
        /// <param name="type">Allowed type of operations.</param>
        public Sln(string file, SlnItems type)
        {
            Result = parser.Parse(file, type);
        }

        private void Free()
        {
            Result.Env.Dispose();
        }

        #region IDisposable

        // To detect redundant calls
        private bool disposed = false;

        // To correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if(disposed) {
                return;
            }
            disposed = true;

            Free();
        }

        #endregion
    }
}
