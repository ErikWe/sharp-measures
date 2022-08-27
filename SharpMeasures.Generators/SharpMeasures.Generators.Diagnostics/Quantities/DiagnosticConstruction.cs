namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

using System;

public static partial class DiagnosticConstruction
{
    public static Diagnostic DefineQuantityDefaultUnit(Location? location, string quantityTypeName)
    {
        return Diagnostic.Create(DiagnosticRules.DefineQuantityDefaultUnit, location, quantityTypeName);
    }

    public static Diagnostic DefineQuantityDefaultSymbol(Location? location, string quantityTypeName)
    {
        return Diagnostic.Create(DiagnosticRules.DefineQuantityDefaultSymbol, location, quantityTypeName);
    }

    public static Diagnostic DifferenceDisabledButQuantitySpecified(Location? location, string quantityTypeName)
    {
        return Diagnostic.Create(DiagnosticRules.DifferenceDisabledButQuantitySpecified, location, quantityTypeName);
    }

    public static Diagnostic QuantityGroupMissingRoot<TAttribute>(Location? location)
    {
        return Diagnostic.Create(DiagnosticRules.QuantityGroupMissingRoot, location, typeof(TAttribute).FullName);
    }

    public static Diagnostic InvalidConstantName(Location? location, string constantName)
    {
        return Diagnostic.Create(DiagnosticRules.InvalidConstantName, location, constantName);
    }

    public static Diagnostic NullConstantName(Location? location)
    {
        return Diagnostic.Create(DiagnosticRules.NullConstantName, location);
    }

    public static Diagnostic InvalidConstantMultiplesName(Location? location, string multiplesCode, string constantName)
    {
        return Diagnostic.Create(DiagnosticRules.InvalidConstantMultiplesName, location, multiplesCode, constantName);
    }

    public static Diagnostic NullConstantMultiplesName(Location? location, string constantName)
    {
        return Diagnostic.Create(DiagnosticRules.NullConstantMultiplesName, location, constantName);
    }

    public static Diagnostic DuplicateConstantName(Location? location, string constantName, string quantityTypeName)
    {
        return Diagnostic.Create(DiagnosticRules.DuplicateConstantName, location, quantityTypeName, constantName);
    }

    public static Diagnostic ConstantNameReservedByConstantMultiples(Location? location, string constantName, string quantityTypeName)
    {
        return Diagnostic.Create(DiagnosticRules.ConstantNameReservedByConstantMultiples, location, quantityTypeName, constantName);
    }

    public static Diagnostic DuplicateConstantMultiplesName(Location? location, string constantMultiplesName, string quantityTypeName)
    {
        return Diagnostic.Create(DiagnosticRules.DuplicateConstantMultiplesName, location, quantityTypeName, constantMultiplesName);
    }

    public static Diagnostic ConstantMultiplesNameReversedByConstantName(Location? location, string constantMultiplesName, string quantityTypeName)
    {
        return Diagnostic.Create(DiagnosticRules.ConstantMultiplesNameReservedByConstantName, location, quantityTypeName, constantMultiplesName);
    }

    public static Diagnostic ConstantIdenticalNameAndMultiples(Location? location, string name)
    {
        return Diagnostic.Create(DiagnosticRules.IdenticalConstantNameAndConstantMultiples, location, name);
    }

    public static Diagnostic ConstantSharesNameWithUnit(Location? location, string sharedName, string quantityTypeName)
    {
        return Diagnostic.Create(DiagnosticRules.ConstantSharesNameWithUnit, location, quantityTypeName, sharedName);
    }

    public static Diagnostic ConstantMultiplesDisabledButNameSpecified(Location? location, string singularForm)
    {
        return Diagnostic.Create(DiagnosticRules.ConstantMultiplesDisabledButNameSpecified, location, singularForm);
    }

    public static Diagnostic QuantityConvertibleToSelf(Location? location, string quantityTypeName)
    {
        return Diagnostic.Create(DiagnosticRules.QuantityConvertibleToSelf, location, quantityTypeName);
    }

    public static Diagnostic ContradictoryAttributes<TInclusionAttribute, TExclusionAttribute>(Location? location)
    {
        return ContradictoryAttributes(location, typeof(TInclusionAttribute), typeof(TExclusionAttribute));
    }

    public static Diagnostic ContradictoryAttributes(Location? location, Type inclusionAttributeType, Type exclusionAttributeType)
    {
        return ContradictoryAttributes(location, inclusionAttributeType.Name, exclusionAttributeType.Name);
    }

    public static Diagnostic ContradictoryAttributes(Location? location, string inclusionAttributeName, string exclusionAttributeName)
    {
        return Diagnostic.Create(DiagnosticRules.ContradictoryAttributes, location, Utility.AttributeName(inclusionAttributeName),
            Utility.AttributeName(exclusionAttributeName));
    }

    public static Diagnostic IncludingAlreadyIncludedUnitWithIntersection(Location? location, string unitName)
    {
        return Diagnostic.Create(DiagnosticRules.IncludingAlreadyIncludedUnitWithIntersection, location, unitName);
    }

    public static Diagnostic IncludingAlreadyIncludedUnitWithUnion<TOppositeAttribute>(Location? location, string unitName)
    {
        return Diagnostic.Create(DiagnosticRules.IncludingAlreadyIncludedUnitWithUnion, location, unitName, Utility.AttributeName(typeof(TOppositeAttribute).FullName));
    }

    public static Diagnostic ExcludingAlreadyExcludedUnit(Location? location, string unitName)
    {
        return Diagnostic.Create(DiagnosticRules.ExcludingAlreadyExcludedUnit, location, unitName);
    }

    public static Diagnostic IncludingExcludedUnit(Location? location, string unitName)
    {
        return Diagnostic.Create(DiagnosticRules.IncludingExcludedUnit, location, unitName);
    }

    public static Diagnostic UnionInclusionStackingModeRedundant(Location? location, string quantityTypeName)
    {
        return Diagnostic.Create(DiagnosticRules.UnionInclusionStackingModeRedundant, location, quantityTypeName);
    }
}
