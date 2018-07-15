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

using System.Diagnostics;

namespace net.r_eg.MvsSln.Core
{
    /// <summary>
    /// Project Configuration
    /// </summary>
    [DebuggerDisplay("{DbgDisplay}")]
    public class ConfigPrj: ConfigItem, IConfPlatform, IConfPlatformPrj
    {
        /// <summary>
        /// Project Guid.
        /// </summary>
        public string PGuid
        {
            get;
            protected set;
        }

        /// <summary>
        /// Existence of `.Build.0` to activate project for build:
        /// {A7BF1F9C-F18D-423E-9354-859DC3CFAFD4}.CI_Release|Any CPU.Build.0 = Release|Any CPU
        /// </summary>
        public bool IncludeInBuild
        {
            get;
            internal set;
        }

        /// <summary>
        /// Link to solution configuration.
        /// </summary>
        public IConfPlatform Sln
        {
            get;
            protected set;
        }

        public ConfigPrj(string name, string platform, string pGuid, bool build, ConfigSln sln)
            : base(name, platform)
        {
            Set(pGuid, build, sln);
        }

        public ConfigPrj(string formatted, string pGuid, bool build, ConfigSln sln)
            : base(formatted)
        {
            Set(pGuid, build, sln);
        }

        private void Set(string pGuid, bool build, ConfigSln sln)
        {
            PGuid           = pGuid;
            IncludeInBuild  = build;
            Sln             = sln;
        }

        #region DebuggerDisplay

        private string DbgDisplay
        {
            get => $"{Format()} -> {Sln} : [{PGuid}]";
        }

        #endregion
    }
}
