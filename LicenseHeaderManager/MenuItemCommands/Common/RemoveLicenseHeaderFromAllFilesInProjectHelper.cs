﻿#region copyright

// Copyright (c) rubicon IT GmbH

// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. 

#endregion

using System;
using System.Threading.Tasks;
using System.Windows;
using EnvDTE;
using LicenseHeaderManager.Headers;
using LicenseHeaderManager.Utils;

namespace LicenseHeaderManager.MenuItemCommands.Common
{
  public class RemoveLicenseHeaderFromAllFilesInProjectHelper
  {
    private readonly LicenseHeaderReplacer _licenseReplacer;
    private readonly Core.LicenseHeaderReplacer _licenseHeaderReplacer;

    public RemoveLicenseHeaderFromAllFilesInProjectHelper (LicenseHeaderReplacer licenseReplacer, Core.LicenseHeaderReplacer licenseHeaderReplacer)
    {
      _licenseReplacer = licenseReplacer;
      _licenseHeaderReplacer = licenseHeaderReplacer;
    }

    public async Task Execute (object projectOrProjectItem)
    {
      var project = projectOrProjectItem as Project;
      var item = projectOrProjectItem as ProjectItem;
      if (project == null && item == null)
        return;

      _licenseReplacer.ResetExtensionsWithInvalidHeaders();
      if (project != null)
      {
        foreach (ProjectItem i in project.ProjectItems)
        {
          var replacerInput = CoreHelpers.GetFilesToProcess (i, null, out _, false);
          var errors = await _licenseHeaderReplacer.RemoveOrReplaceHeader (replacerInput);
          if (errors.Count > 0)
            MessageBox.Show ($"Encountered {errors.Count} errors.", "Error(s)", MessageBoxButton.OK, MessageBoxImage.Error);
        }
      }
      else
      {
        foreach (ProjectItem i in item.ProjectItems)
        {
          var replacerInput = CoreHelpers.GetFilesToProcess (i, null, out _, false);
          var errors = await _licenseHeaderReplacer.RemoveOrReplaceHeader (replacerInput);
          if (errors.Count > 0)
            MessageBox.Show ($"Encountered {errors.Count} errors.", "Error(s)", MessageBoxButton.OK, MessageBoxImage.Error);
        }
      }
    }
  }
}