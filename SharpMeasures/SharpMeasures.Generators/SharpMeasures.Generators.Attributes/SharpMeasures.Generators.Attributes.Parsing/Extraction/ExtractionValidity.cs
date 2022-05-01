namespace SharpMeasures.Generators.Attributes.Parsing.Extraction;

using Microsoft.CodeAnalysis;

using System;
using System.Collections.Generic;

public readonly record struct ExtractionValidity(ExtractionValidity.State Validity, IEnumerable<Diagnostic> Diagnostics)
{
    public enum State { Valid, Invalid }

    public static ExtractionValidity Valid { get; } = new(State.Valid, Array.Empty<Diagnostic>());
    public static ExtractionValidity InvalidWithoutDiagnostics { get; } = new(State.Invalid, Array.Empty<Diagnostic>());

    public static ExtractionValidity Invalid(Diagnostic? diagnostics)
        => diagnostics is null ? InvalidWithoutDiagnostics : new(State.Invalid, new Diagnostic[] { diagnostics });
    public static ExtractionValidity Invalid(IEnumerable<Diagnostic> diagnostics) => new(State.Invalid, diagnostics);
    public static ExtractionValidity Invalid(params Diagnostic[] diagnostics) => new(State.Invalid, diagnostics);

    public bool IsValid => Validity == State.Valid;
    public bool IsInvalid => Validity == State.Invalid;
}