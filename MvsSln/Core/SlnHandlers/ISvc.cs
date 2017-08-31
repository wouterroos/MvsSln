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
using System.Collections.Generic;
using System.IO;

namespace net.r_eg.MvsSln.Core.SlnHandlers
{
    public interface ISvc
    {
        /// <summary>
        /// Used stream.
        /// </summary>
        StreamReader Stream { get; set; }

        /// <summary>
        /// Prepared solution data.
        /// </summary>
        ISlnResultSvc Sln { get; set; }

        /// <summary>
        /// Unspecified storage of the user scope.
        /// </summary>
        Dictionary<Guid, object> UData { get; set; }

        /// <summary>
        /// Reads a line of characters from stream.
        /// </summary>
        /// <returns></returns>
        string ReadLine();

        /// <summary>
        /// Reads a line of characters from stream with tracking.
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        string ReadLine(object handler);

        /// <summary>
        /// Resets stream and its related data.
        /// </summary>
        void ResetStream();

        /// <summary>
        /// Tracking for line.
        /// </summary>
        /// <param name="line"></param>
        /// <param name="handler">Specific handler if used, or null as an unspecified.</param>
        void Track(RawText line, object handler = null);
    }
}