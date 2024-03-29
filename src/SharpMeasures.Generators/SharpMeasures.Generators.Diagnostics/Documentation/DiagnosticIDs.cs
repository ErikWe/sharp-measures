﻿namespace SharpMeasures.Generators.Diagnostics;

public static partial class DiagnosticIDs
{
    public const string UnresolvedDocumentationDependency = $"{Prefix}{Numbering.Hundreds.Documentation}00";
    public const string DocumentationFileDuplicateTag = $"{Prefix}{Numbering.Hundreds.Documentation}01";
    public const string DocumentationFileMissingRequestedTag = $"{Prefix}{Numbering.Hundreds.Documentation}02";
}
