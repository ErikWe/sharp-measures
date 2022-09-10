﻿namespace SharpMeasures.Generators.Units.Parsing.PrefixedUnitInstance;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal class PrefixedUnitInstanceValidator : AModifiedUnitInstanceValidator<IModifiedUnitInstanceValidationContext, PrefixedUnitInstanceDefinition>
{
    public PrefixedUnitInstanceValidator(IModifiedUnitInstanceValidationDiagnostics<PrefixedUnitInstanceDefinition> diagnostics) : base(diagnostics) { }
}