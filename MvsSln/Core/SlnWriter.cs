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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using net.r_eg.MvsSln.Core.ObjHandlers;
using net.r_eg.MvsSln.Core.SlnHandlers;
using net.r_eg.MvsSln.Exceptions;
using net.r_eg.MvsSln.Extensions;

namespace net.r_eg.MvsSln.Core
{
    public class SlnWriter: IDisposable
    {
        protected StreamWriter stream;

        /// <summary>
        /// Available writers to process sections.
        /// </summary>
        public IDictionary<Type, HandlerValue> Handlers
        {
            get;
            protected set;
        }

        /// <summary>
        /// To write all not ignored sections with rules from handlers.
        /// </summary>
        /// <param name="sections"></param>
        public void Write(IEnumerable<ISection> sections)
        {
            sections = sections.Select(s => s.Clone()).ToArray();
            Validate(sections);

            WritableSections(sections).ForEach(s => Write(s));
        }

        /// <summary>
        /// To write a single section with rules from handlers.
        /// </summary>
        /// <param name="section"></param>
        public void Write(ISection section)
        {
            if(section == null) {
                throw new ArgumentNullException();
            }

            if(section.Ignore) {
                return;
            }

            if(section.Handler == null) {
                Write(section.Raw.data);
                return;
            }

            var tid = section.Handler.GetType();
            Write
            (
                (Handlers.ContainsKey(tid) && Handlers[tid].handler != null) ?
                    Handlers[tid].handler.Extract(Handlers[tid].value)
                    : section.Raw.data
            );
        }

        /// <param name="sln">Destination file.</param>
        /// <param name="handlers">Should contain writers by specific types of readers.</param>
        public SlnWriter(string sln, IDictionary<Type, HandlerValue> handlers)
            : this(sln, handlers, Encoding.UTF8)
        {

        }

        /// <param name="sln">Destination file.</param>
        /// <param name="handlers">Should contain writers by specific types of readers.</param>
        /// <param name="enc">Use specific encoding.</param>
        public SlnWriter(string sln, IDictionary<Type, HandlerValue> handlers, Encoding enc)
            : this(new StreamWriter(sln, false, enc), handlers)
        {

        }

        /// <param name="writer"></param>
        /// <param name="handlers">Should contain writers by specific types of readers.</param>
        public SlnWriter(StreamWriter writer, IDictionary<Type, HandlerValue> handlers)
        {
            stream      = writer ?? throw new ArgumentNullException();
            Handlers    = handlers ?? throw new ArgumentNullException();
        }

        protected void Validate(IEnumerable<ISection> sections)
        {
            var coh = GetSlnHandlers(sections)
                        .Where(s => s.CoHandlers != null && s.CoHandlers.Count > 0)
                        .ToDictionary(key => key.GetType(), value => value.CoHandlers);

            foreach(var h in Handlers)
            {
                if(coh.ContainsKey(h.Key))
                {
                    if(coh[h.Key].Except(Handlers.Keys).Count() != coh[h.Key].Count) {
                        throw new CoHandlerRuleException(
                            $"Only parent handler is allowed '{h.Key}' <- {String.Join(", ", coh[h.Key].Select(c => c.Name))}"
                        );
                    }
                    continue;
                }

                if(coh.Where(v => v.Value.Contains(h.Key))
                        .Select(v => v.Key)
                        .Except(Handlers.Keys).Count() > 0)
                {
                    throw new CoHandlerRuleException($"Define parent handler instead of '{h.Key}'.");
                }
            }
        }

        protected HashSet<ISlnHandler> GetSlnHandlers(IEnumerable<ISection> sections)
        {
            //var sh = sections.Where(s => s.Handler is ISlnHandler)
            //                 .GroupBy(s => s.Handler).Select(s => s.Key);

            var sh = new HashSet<ISlnHandler>();
            sections.Where(s => s.Handler is ISlnHandler)
                    .ForEach(s => sh.Add((ISlnHandler)s.Handler));

            return sh;
        }

        protected IEnumerable<ISection> WritableSections(IEnumerable<ISection> sections)
        {
            var ret     = new List<ISection>();
            var hTypes  = new HashSet<Type>();

            foreach(var part in sections.Where(s => !s.Ignore))
            {
                if(part.Handler == null) {
                    ret.Add(part);
                    continue;
                }

                if(part.Handler is ISlnHandler sh) {
                    sh.CoHandlers?.ForEach(h => hTypes.Add(h));
                }

                var type = part.Handler.GetType();

                if(hTypes.Contains(type)) {
                    continue;
                }

                if(Handlers.ContainsKey(type)) {
                    hTypes.Add(type);
                }
                ret.Add(part);
            }

            return ret;
        }

        protected virtual void Write(string raw)
        {
            stream.WriteLine(raw);
        }

        private void Free()
        {
            stream?.Dispose();
        }

        #region IDisposable

        // To detect redundant calls
        private bool disposed = false;

        // To correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
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
