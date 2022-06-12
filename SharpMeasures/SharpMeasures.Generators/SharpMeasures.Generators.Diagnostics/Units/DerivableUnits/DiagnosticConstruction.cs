﻿namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

public static partial class DiagnosticConstruction
{
    public static Diagnostic InvalidUnitDerivationExpression(Location? location, string expression)
    {
        return Diagnostic.Create(DiagnosticRules.InvalidUnitDerivationExpression, location, expression);
    }

    public static Diagnostic NullUnitDerivationExpression(Location? location)
    {
        return Diagnostic.Create(DiagnosticRules.NullUnitDerivationExpression, location);
    }

    public static Diagnostic EmptyUnitDerivationSignature(Location? location)
    {
        return Diagnostic.Create(DiagnosticRules.EmptyUnitDerivationSignature, location);
    }

    public static Diagnostic DuplicateUnitDerivationSignature(Location? location, string unitName)
    {
        return Diagnostic.Create(DiagnosticRules.DuplicateUnitDerivationSignature, location, unitName);
    }

    public static Diagnostic UnrecognizedUnitDerivationSignature(Location? location, string unitName)
    {
        return Diagnostic.Create(DiagnosticRules.UnrecognizedUnitDerivationSignature, location, unitName);
    }

    public static Diagnostic UnitListNotMatchingSignature(Location? location, int expectedCount, int actualCount)
    {
        return Diagnostic.Create(DiagnosticRules.UnitListNotMatchingSignature, location, expectedCount, actualCount);
    }
}