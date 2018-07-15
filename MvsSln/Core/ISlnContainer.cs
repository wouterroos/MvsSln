﻿/*
 * The MIT License (MIT)
 * 
 * Copyright (c) 2013-2018  Denis Kuzmin < entry.reg@gmail.com > :: github.com/3F
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

using System.Collections.Generic;
using System.IO;
using net.r_eg.MvsSln.Core.SlnHandlers;

namespace net.r_eg.MvsSln.Core
{
    public interface ISlnContainer
    {
        /// <summary>
        /// Available solution handlers.
        /// </summary>
        SynchSubscribers<ISlnHandler> SlnHandlers { get; }

        /// <summary>
        /// Dictionary of raw xml projects by Guid.
        /// Will be used if projects cannot be accessed from filesystem.
        /// </summary>
        IDictionary<string, RawText> RawXmlProjects { get; set; }

        /// <summary>
        /// To reset and register all default handlers.
        /// </summary>
        void SetDefaultHandlers();

        /// <summary>
        /// Parse of selected .sln file.
        /// </summary>
        /// <param name="sln">Solution file</param>
        /// <param name="type">Allowed type of operations.</param>
        /// <returns></returns>
        ISlnResult Parse(string sln, SlnItems type);

        /// <summary>
        /// To parse data from used stream.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="type">Allowed type of operations.</param>
        /// <returns></returns>
        ISlnResult Parse(StreamReader reader, SlnItems type);
    }
}