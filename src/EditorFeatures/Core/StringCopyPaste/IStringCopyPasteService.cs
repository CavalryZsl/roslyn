﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Composition;
using Microsoft.CodeAnalysis.Host;
using Microsoft.CodeAnalysis.Host.Mef;

namespace Microsoft.CodeAnalysis.Editor.StringCopyPaste
{
    internal interface IStringCopyPasteService : IWorkspaceService
    {
        /// <summary>
        /// Attempts to retrieve a monotonically increasing value representing which copy value the clipboard currently
        /// has on it.  Can be used to determine if another copy to the clipboard happened between different times.
        /// </summary>
        bool TryGetClipboardSequenceNumber(out int sequenceNumber);
    }

    [ExportWorkspaceService(typeof(IStringCopyPasteService)), Shared]
    internal class DefaultStringCopyPasteService : IStringCopyPasteService
    {
        [ImportingConstructor]
        [Obsolete(MefConstruction.ImportingConstructorMessage, error: true)]
        public DefaultStringCopyPasteService()
        {
        }

        // Note: we very intentionally do not try to store/retrieve any data in this default implementation. at this
        // layer we have no information about the clipboard, so it would be dangerous to presume that that information
        // had been validly associated with latest clipboard operation and had not been affected by things outside our
        // awareness.

        public bool TryGetClipboardSequenceNumber(out int sequenceNumber)
        {
            sequenceNumber = 0;
            return false;
        }
    }
}
