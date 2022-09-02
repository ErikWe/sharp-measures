namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

using System;

public static partial class DiagnosticConstruction
{
    public static Diagnostic DefineQuantityDefaultUnitInstanceName(Location? location, string quantityTypeName)
    {
        return Diagnostic.Create(DiagnosticRules.DefineQuantityDefaultUnitInstanceName, location, quantityTypeName);
    }

    public static Diagnostic DefineQuantityDefaultUnitInstanceSymbol(Location? location, string quantityTypeName)
    {
        return Diagnostic.Create(DiagnosticRules.DefineQuantityDefaultUnitInstanceSymbol, location, quantityTypeName);
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

    public static Diagnostic ConstantSharesNameWithUnitInstance(Location? location, string sharedName, string quantityTypeName)
    {
        return Diagnostic.Create(DiagnosticRules.ConstantSharesNameWithUnitInstance, location, quantityTypeName, sharedName);
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

    public static Diagnostic IncludingAlreadyIncludedUnitInstanceWithIntersection(Location? location, string unitName)
    {
        return Diagnostic.Create(DiagnosticRules.IncludingAlreadyIncludedUnitInstanceWithIntersection, location, unitName);
    }

    public static Diagnostic IncludingAlreadyIncludedUnitInstanceWithUnion<TOppositeAttribute>(Location? location, string unitName)
    {
        return Diagnostic.Create(DiagnosticRules.IncludingAlreadyIncludedUnitInstanceWithUnion, location, unitName, Utility.AttributeName(typeof(TOppositeAttribute).FullName));
    }

    public static Diagnostic ExcludingAlreadyExcludedUnitInstance(Location? location, string unitName)
    {
        return Diagnostic.Create(DiagnosticRules.ExcludingAlreadyExcludedUnitInstance, location, unitName);
    }

    public static Diagnostic IncludingExcludedUnitInstance(Location? location, string unitName)
    {
        return Diagnostic.Create(DiagnosticRules.IncludingExcludedUnitInstance, location, unitName);
    }

    public static Diagnostic UnionInclusionStackingModeRedundant(Location? location, string quantityTypeName)
    {
        return Diagnostic.Create(DiagnosticRules.UnionInclusionStackingModeRedundant, location, quantityTypeName);
    }
}
