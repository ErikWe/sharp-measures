namespace SharpMeasures.Generators.Utility;

using Microsoft.CodeAnalysis;

using System;
using System.Collections.Generic;

internal readonly record struct ResultWithDiagnostics<T>(T Result, IEnumerable<Diagnostic> Diagnostics)
{
    public ResultWithDiagnostics(T result) : this(result, Array.Empty<Diagnostic>()) { }
    public ResultWithDiagnostics(T result, Diagnostic diagnostic) : this(result, new Diagnostic[] { diagnostic }) { }
}