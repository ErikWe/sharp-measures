namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Utility;

using System;

internal static partial class DiagnosticConstruction
{
    public static Diagnostic DefineQuantityUnitAndSymbol_MissingUnit(Location? location)
    {
        return Diagnostic.Create(DiagnosticRules.DefineQuantityUnitAndSymbol_MissingUnit, location);
    }

    public static Diagnostic DefineQuantityUnitAndSymbol_MissingSymbol(Location? location)
    {
        return Diagnostic.Create(DiagnosticRules.DefineQuantityUnitAndSymbol_MissingSymbol, location);
    }

    public static Diagnostic InvalidConstantName(Location? location, string constantName)
    {
        return Diagnostic.Create(DiagnosticRules.InvalidConstantName, location, constantName);
    }

    public static Diagnostic InvalidConstantName_Null(Location? location)
    {
        return Diagnostic.Create(DiagnosticRules.InvalidConstantName_Null, location);
    }

    public static Diagnostic InvalidConstantMultiplesName(Location? location, string constantName, string multiplesName)
    {
        return Diagnostic.Create(DiagnosticRules.InvalidConstantMultiplesName, location, multiplesName, constantName);
    }

    public static Diagnostic InvalidConstantMultiplesName_Null(Location? location, string constantName)
    {
        return Diagnostic.Create(DiagnosticRules.InvalidConstantMultiplesName_Null, location, constantName);
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

    public static Diagnostic UnrecognizedConversionOperationBehaviour(Location? location, ConversionOperationBehaviour conversionOperationBehaviour)
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

    public static Diagnostic QuantityGroupMissingBase(Location? location, string baseAttributeName)
    {
        return Diagnostic.Create(DiagnosticRules.QuantityGroupMissingBase, location, baseAttributeName);
    }
}
