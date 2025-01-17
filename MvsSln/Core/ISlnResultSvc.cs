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

using System.Collections.Generic;

namespace net.r_eg.MvsSln.Core
{
    public interface ISlnResultSvc: ISlnResult
    {
        /// <summary>
        /// Solution configurations with platforms.
        /// </summary>
        IList<IConfPlatform> SolutionConfigList { get; set; }

        /// <summary>
        /// Project configurations with platforms.
        /// </summary>
        IList<IConfPlatformPrj> ProjectConfigList { get; set; }

        /// <summary>
        /// All found projects in solution.
        /// </summary>
        IList<ProjectItem> ProjectItemList { get; set; }

        /// <summary>
        /// List of solution folders.
        /// </summary>
        IList<SolutionFolder> SolutionFolderList { get; set; }

        /// <summary>
        /// Updates instance of the Solution Project Dependencies.
        /// </summary>
        /// <param name="dep"></param>
        void SetProjectDependencies(ISlnPDManager dep);

        /// <summary>
        /// Updates header info.
        /// </summary>
        /// <param name="info"></param>
        void SetHeader(SlnHeader info);
    }
}