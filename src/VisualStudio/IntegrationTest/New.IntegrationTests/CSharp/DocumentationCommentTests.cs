﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Test.Utilities;
using Roslyn.Test.Utilities;
using Roslyn.VisualStudio.IntegrationTests;
using Xunit;

namespace Roslyn.VisualStudio.NewIntegrationTests.CSharp
{
    [Trait(Traits.Feature, Traits.Features.DocumentationComments)]
    public class DocumentationCommentTests : AbstractEditorTest
    {
        protected override string LanguageName => LanguageNames.CSharp;

        [IdeFact]
        [WorkItem(54391, "https://github.com/dotnet/roslyn/issues/54391")]
        public async Task TypingCharacter_MultiCaret()
        {
            var code =
@"
//$$[||]
class C1 { }

//[||]
class C2 { }

//[||]
class C3 { }
";
            await SetUpEditorAsync(code, HangMitigatingCancellationToken);
            await TestServices.Input.SendAsync('/');
            var expected =
@"
/// <summary>
/// $$
/// </summary>
class C1 { }

/// <summary>
///
/// </summary>
class C2 { }

/// <summary>
///
/// </summary>
class C3 { }
";

            await TestServices.EditorVerifier.TextContainsAsync(expected, cancellationToken: HangMitigatingCancellationToken);
        }
    }
}
