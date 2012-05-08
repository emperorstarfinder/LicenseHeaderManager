﻿#region copyright
// Copyright (c) 2011 rubicon IT GmbH

// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. 
#endregion
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using EnvDTE;

namespace LicenseHeaderManager.Headers
{
  class DocumentHeaderProperties : IEnumerable<DocumentHeaderProperty>
  {
    private readonly IEnumerable<DocumentHeaderProperty> _properties;

    public DocumentHeaderProperties (ProjectItem projectItem)
    {
      _properties = CreateProperties (projectItem);
    }

    public IEnumerator<DocumentHeaderProperty> GetEnumerator ()
    {
      return _properties.GetEnumerator ();
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
    {
      return GetEnumerator ();
    }

    private IEnumerable<DocumentHeaderProperty> CreateProperties (ProjectItem projectItem)
    {
      List<DocumentHeaderProperty> properties = new List<DocumentHeaderProperty> ()
      {
        new DocumentHeaderProperty(
          "%FullFileName%", 
          documentHeader => documentHeader.FileInfo != null, 
          documentHeader => documentHeader.FileInfo.FullName),
        new DocumentHeaderProperty(
          "%FileName%", 
          documentHeader => documentHeader.FileInfo != null, 
          documentHeader => documentHeader.FileInfo.Name),
        new DocumentHeaderProperty(
          "%CreationYear%", 
          documentHeader => documentHeader.FileInfo != null, 
          documentHeader => documentHeader.FileInfo.CreationTime.Year.ToString()),
        new DocumentHeaderProperty(
          "%CreationMonth%", 
          documentHeader => documentHeader.FileInfo != null, 
          documentHeader => documentHeader.FileInfo.CreationTime.Month.ToString()),
        new DocumentHeaderProperty(
          "%CreationDay%", 
          documentHeader => documentHeader.FileInfo != null, 
          documentHeader => documentHeader.FileInfo.CreationTime.Day.ToString()),
        new DocumentHeaderProperty(
          "%CreationTime%", 
          documentHeader => documentHeader.FileInfo != null, 
          documentHeader => documentHeader.FileInfo.CreationTime.ToShortTimeString()),
        new DocumentHeaderProperty(
          "%CurrentYear%", 
          documentHeader => true, 
          documentHeader => DateTime.Now.Year.ToString()),
        new DocumentHeaderProperty(
          "%CurrentMonth%", 
          documentHeader => true, 
          documentHeader => DateTime.Now.Month.ToString()),
        new DocumentHeaderProperty(
          "%CurrentDay%", 
          documentHeader => true, 
          documentHeader => DateTime.Now.Day.ToString()),
        new DocumentHeaderProperty(
          "%CurrentTime%", 
          documentHeader => true, 
          documentHeader => DateTime.Now.ToShortTimeString()),
        new DocumentHeaderProperty(
          "%UserName%", 
          documentHeader => true, 
          documentHeader => Environment.UserName),
        new DocumentHeaderProperty(
          "%Project%", 
          documentHeader => projectItem != null && projectItem.ContainingProject != null, 
          documentHeader => projectItem.ContainingProject.Name),
        new DocumentHeaderProperty(
          "%Namespace%", 
          documentHeader => documentHeader.FileInfo != null, 
          documentHeader => LookupNameSpaceInText(documentHeader.FileInfo.FullName)),
      };
      return properties;
    }

    private const string NamespaceKeyword = "namespace";
    private static string LookupNameSpaceInText (string fullFileName)
    {
      //Searching for first occurent "namespace .." and the first following "{"

      var text = File.ReadAllText(fullFileName);
      var startingNamespaceIdx = text.IndexOf (NamespaceKeyword) + NamespaceKeyword.Length;

      if (startingNamespaceIdx < 0)
        return string.Empty;

      var endingNamespaceIdx = text.IndexOf ("{", startingNamespaceIdx);
      if (endingNamespaceIdx < 0)
        return string.Empty;

      var line = text.Substring(startingNamespaceIdx, endingNamespaceIdx - startingNamespaceIdx);
      line = line.Trim();
      return line;
    }
  }
}