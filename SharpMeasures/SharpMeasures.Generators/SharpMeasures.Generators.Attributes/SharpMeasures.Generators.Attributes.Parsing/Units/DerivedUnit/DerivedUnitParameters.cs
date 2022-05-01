namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using System.Collections.ObjectModel;

public readonly record struct DerivedUnitParameters(string Name, string Plural, ReadOnlyCollection<INamedTypeSymbol> Signature,
    ReadOnlyCollection<string> Units, DerivedUnitParsingData ParsingData)
    : IUnitDefinitionParameters;