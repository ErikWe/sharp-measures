namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Utility;

using System;

public static partial class DiagnosticConstruction
{
    public static Diagnostic DefineQuantityDefaultUnit(Location? location)
    {
        return Diagnostic.Create(DiagnosticRules.DefineQuantityDefaultUnit, location);
    }

    public static Diagnostic DefineQuantityDefaultSymbol(Location? location)
    {
        return Diagnostic.Create(DiagnosticRules.DefineQuantityDefaultSymbol, location);
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

    public static Diagnostic DuplicateConstantMultiplesName(Location? location, string constantMultiplesName, string quantityTypeName)
    {
        return Diagnostic.Create(DiagnosticRules.DuplicateConstantMultiplesName, location, quantityTypeName, constantMultiplesName);
    }

    public static Diagnostic ConstantSharesNameWithUnit(Location? location, string sharedName, string quantityTypeName)
    {
        return Diagnostic.Create(DiagnosticRules.ConstantSharesNameWithUnit, location, quantityTypeName, sharedName);
    }

    public static Diagnostic ConstantMultiplesDisabledButNameSpecified(Location? location, string singularForm)
    {
        return Diagnostic.Create(DiagnosticRules.ConstantMultiplesDisabledButNameSpecified, location, singularForm);
    }

    public static Diagnostic UnrecognizedConversionOperationBehaviour(Location? location, ConversionOperatorBehaviour conversionOperationBehaviour)
    {
        return Diagnostic.Create(DiagnosticRules.UnrecognizedCastOperatorBehaviour, location, conversionOperationBehaviour.ToString());
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
        return Diagnostic.Create(DiagnosticRules.ContradictoryAttributes, location, inclusionAttributeName, exclusionAttributeName);
    }

    public static Diagnostic QuantityGroupMissingRoot<TAttribute>(Location? location)
    {
        return Diagnostic.Create(DiagnosticRules.QuantityGroupMissingRoot, location, typeof(TAttribute).FullName);
    }

    public static Diagnostic UnitAlreadyIncluded(Location? location, string unitName)
    {
        return Diagnostic.Create(DiagnosticRules.UnitAlreadyIncluded, location, unitName);
    }

    public static Diagnostic UnitNotIncluded(Location? location, string unitName)
    {
        return Diagnostic.Create(DiagnosticRules.UnitNotIncluded, location, unitName);
    }

    public static Diagnostic UnitAlreadyExcluded(Location? location, string unitName)
    {
        return Diagnostic.Create(DiagnosticRules.UnitAlreadyExcluded, location, unitName);
    }

    public static Diagnostic UnitNotExcluded(Location? location, string unitName)
    {
        return Diagnostic.Create(DiagnosticRules.UnitNotExcluded, location, unitName);
    }

    public static Diagnostic UnitAlreadyListed(Location? location, string unitName)
    {
        return Diagnostic.Create(DiagnosticRules.UnitAlreadyListed, location, unitName);
    }

    public static Diagnostic DifferenceDisabledButQuantitySpecified(Location? location, string quantityTypeName)
    {
        return Diagnostic.Create(DiagnosticRules.DifferenceDisabledButQuantitySpecified, location, quantityTypeName);
    }
}
