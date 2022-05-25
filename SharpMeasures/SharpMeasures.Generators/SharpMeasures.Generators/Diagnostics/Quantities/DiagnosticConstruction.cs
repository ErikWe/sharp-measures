namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

using System;

internal static partial class DiagnosticConstruction
{
    public static Diagnostic InvalidConstantName(Location? location, string constantName)
    {
        return Diagnostic.Create(DiagnosticRules.InvalidConstantName, location, constantName);
    }

    public static Diagnostic InvalidConstantMultiplesName(Location? location, string constantName, string multiplesName)
    {
        return Diagnostic.Create(DiagnosticRules.InvalidConstantMultiplesName, location, multiplesName, constantName);
    }

    public static Diagnostic DuplicateConstantName(Location? location, string constantName, string quantityTypeName)
    {
        return Diagnostic.Create(DiagnosticRules.DuplicateConstantName, location, quantityTypeName, constantName);
    }

    public static Diagnostic DuplicateConstantMultiplesName(Location? location, string constantMultiplesName, string quantityTypeName)
    {
        return Diagnostic.Create(DiagnosticRules.DuplicateConstantMultiplesName, location, quantityTypeName, constantMultiplesName);
    }

    public static Diagnostic DuplicateConstantMultiplesName_Unit(Location? location, string constantMultiplesName, string quantityTypeName)
    {
        return Diagnostic.Create(DiagnosticRules.DuplicateConstantMultiplesName_Unit, location, quantityTypeName, constantMultiplesName);
    }

    public static Diagnostic ExcessiveExclusion<TInclusionAttribute, TExclusionAttribute>(Location? location)
    {
        return ExcessiveExclusion(location, typeof(TInclusionAttribute), typeof(TExclusionAttribute));
    }

    public static Diagnostic ExcessiveExclusion(Location? location, Type inclusionAttributeType, Type exclusionAttributeType)
    {
        return ExcessiveExclusion(location, inclusionAttributeType.Name, exclusionAttributeType.Name);
    }

    public static Diagnostic ExcessiveExclusion(Location? location, string inclusionAttributeName, string exclusionAttributeName)
    {
        return Diagnostic.Create(DiagnosticRules.ExcessiveExclusion, location, inclusionAttributeName, exclusionAttributeName);
    }
}
