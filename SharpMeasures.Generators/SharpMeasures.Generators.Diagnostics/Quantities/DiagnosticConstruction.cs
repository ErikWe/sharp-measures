﻿namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

using System.Globalization;

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
        return Diagnostic.Create(DiagnosticRules.QuantityGroupMissingRoot, location, Utility.FullAttributeName<TAttribute>());
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
        return Diagnostic.Create(DiagnosticRules.ContradictoryAttributes, location, Utility.ShortAttributeName<TInclusionAttribute>(), Utility.ShortAttributeName<TExclusionAttribute>(), "the latter");
    }

    public static Diagnostic IncludingAlreadyIncludedUnitInstanceWithIntersection(Location? location, string unitName)
    {
        return Diagnostic.Create(DiagnosticRules.IncludingAlreadyIncludedUnitInstanceWithIntersection, location, unitName);
    }

    public static Diagnostic IncludingAlreadyIncludedUnitInstanceWithUnion<TOppositeAttribute>(Location? location, string unitName)
    {
        return Diagnostic.Create(DiagnosticRules.IncludingAlreadyIncludedUnitInstanceWithUnion, location, unitName, Utility.FullAttributeName<TOppositeAttribute>());
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

    public static Diagnostic DerivationOperatorsIncompatibleExpression(Location? location)
    {
        return Diagnostic.Create(DiagnosticRules.DerivationOperatorsIncompatibleExpression, location);
    }

    public static Diagnostic UnmatchedDerivationExpressionQuantity(Location? location, int index)
    {
        return Diagnostic.Create(DiagnosticRules.UnmatchedDerivationExpressionQuantity, location, index.ToString(CultureInfo.InvariantCulture));
    }

    public static Diagnostic ExpressionDoesNotIncludeQuantity(Location? location, int index)
    {
        return Diagnostic.Create(DiagnosticRules.ExpressionDoesNotIncludeQuantity, location, index.ToString(CultureInfo.InvariantCulture));
    }

    public static Diagnostic MalformedDerivationExpression(Location? location)
    {
        return Diagnostic.Create(DiagnosticRules.MalformedDerivationExpression, location);
    }

    public static Diagnostic DerivationExpressionContainsConstant(Location? location)
    {
        return Diagnostic.Create(DiagnosticRules.DerivationExpressionContainsConstant, location);
    }

    public static Diagnostic IncompatibleQuantitiesInDerivation(Location? location)
    {
        return Diagnostic.Create(DiagnosticRules.IncompatibleQuantitiesInDerivation, location);
    }

    public static Diagnostic DerivationUnexpectedlyResultInScalar(Location? location)
    {
        return Diagnostic.Create(DiagnosticRules.DerivationUnexpectedlyResultInScalar, location);
    }

    public static Diagnostic DerivationUnexpectedlyResultInVector(Location? location)
    {
        return Diagnostic.Create(DiagnosticRules.DerivationUnexpectedlyResultInVector, location);
    }

    public static Diagnostic DerivationResultsInNonOverlappingDimension(Location? location, string quantityType)
    {
        return Diagnostic.Create(DiagnosticRules.DerivationResultsInNonOverlappingDimension, location, quantityType);
    }

    public static Diagnostic InvalidProcessName(Location? location)
    {
        return Diagnostic.Create(DiagnosticRules.InvalidProcessName, location);
    }

    public static Diagnostic DuplicateProcessName(Location? location, string quantityType, string processName)
    {
        return Diagnostic.Create(DiagnosticRules.DuplicateProcessName, location, quantityType, processName);
    }

    public static Diagnostic InvalidProcessExpression(Location? location)
    {
        return Diagnostic.Create(DiagnosticRules.InvalidProcessExpression, location);
    }

    public static Diagnostic ProcessPropertyIncompatibleWithParameters(Location? location, string processName)
    {
        return Diagnostic.Create(DiagnosticRules.ProcessPropertyIncompatibleWithParameters, location, processName);
    }

    public static Diagnostic UnmatchedProcessParameterDefinitions(Location? location, string processName, string parameterTypeCount, string parameterNameCount)
    {
        return Diagnostic.Create(DiagnosticRules.UnmatchedProcessParameterDefinitions, location, processName, parameterTypeCount.ToString(CultureInfo.InvariantCulture), parameterNameCount.ToString(CultureInfo.InvariantCulture));
    }

    public static Diagnostic NullProcessParameterType(Location? location)
    {
        return Diagnostic.Create(DiagnosticRules.NullProcessParameterType, location);
    }

    public static Diagnostic InvalidProcessParameterName(Location? location)
    {
        return Diagnostic.Create(DiagnosticRules.InvalidProcessParameterName, location);
    }

    public static Diagnostic DuplicateProcessParameterName(Location? location, string processName, string parameterName)
    {
        return Diagnostic.Create(DiagnosticRules.DuplicateProcessParameterName, location, processName, parameterName);
    }
}
