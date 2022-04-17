namespace SharpMeasures.Generators.Utility;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.SourceBuilding;

using System.Globalization;

internal readonly record struct NamedType(string Name, string NameSpace)
{
    public static NamedType FromSymbol(INamedTypeSymbol symbol) => new(symbol.Name, symbol.ContainingNamespace?.Name ?? string.Empty);

    public string FullyQualifiedName => string.IsNullOrEmpty(NameSpace) ? Name : $"{NameSpace}.{Name}";

    public string ParameterName => SourceBuildingUtility.ToParameterName(Name);
}
