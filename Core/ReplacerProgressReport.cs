﻿/* Copyright (c) rubicon IT GmbH
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"),
 * to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,
 * and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
 * FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
 * WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. 
 */

using System;
using System.Collections.Generic;

namespace Core
{
  public class ReplacerProgressReport
  {
    /// <summary>
    ///   Initializes a new instance of the <see cref="ReplacerProgressReport" /> class.
    /// </summary>
    /// <param name="totalFileCount">The overall number of files that are to be updated.</param>
    /// <param name="processedFileCount">The number of file that have already been processed.</param>
    public ReplacerProgressReport (int totalFileCount, int processedFileCount)
    {
      TotalFileCount = totalFileCount;
      ProcessedFileCount = processedFileCount;
    }

    /// <summary>
    ///   Gets the overall number of files that are to be updated over the course of one invocation of the
    ///   <see
    ///     cref="LicenseHeaderReplacer.RemoveOrReplaceHeader(System.Collections.Generic.ICollection{Core.LicenseHeaderInput}, IProgress{ReplacerProgressReport}, Func{string, bool})" />
    ///   method.
    /// </summary>
    public int TotalFileCount { get; }

    /// <summary>
    ///   Gets the number of file that have already been processed over the course of one invocation of the
    ///   <see
    ///     cref="LicenseHeaderReplacer.RemoveOrReplaceHeader(System.Collections.Generic.ICollection{Core.LicenseHeaderInput}, IProgress{ReplacerProgressReport}, Func{string, bool})" />
    ///   method.
    /// </summary>
    public int ProcessedFileCount { get; }
  }
}