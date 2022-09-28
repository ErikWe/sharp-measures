namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;
using System.Linq;

internal sealed record class SymbolicSharpMeasuresVectorGroupMemberDefinition : ARawAttributeDefinition<SymbolicSharpMeasuresVectorGroupMemberDefinition, SharpMeasuresVectorGroupMemberLocations>
{
    public static SymbolicSharpMeasuresVectorGroupMemberDefinition Empty { get; } = new(SharpMeasuresVectorGroupMemberLocations.Empty);

    public INamedTypeSymbol? VectorGroup { get; init; }

    public bool InheritDerivations { get; init; } = true;
    public bool InheritConversions { get; init; } = true;
    public bool InheritUnits { get; init; } = true;

    public bool? InheritDerivationsFromMembers { get; init; }
    public bool InheritProcessesFromMembers { get; init; } = true;
    public bool InheritConstantsFromMembers { get; init; } = true;
    public bool? InheritConversionsFromMembers { get; init; }
    public bool? InheritUnitsFromMembers { get; init; }

    public int? Dimension { get; init; }

    public bool? GenerateDocumentation { get; init; }

    protected override SymbolicSharpMeasuresVectorGroupMemberDefinition Definition => this;

    private SymbolicSharpMeasuresVectorGroupMemberDefinition(SharpMeasuresVectorGroupMemberLocations locations) : base(locations) { }

    public IEnumerable<INamedTypeSymbol> ForeignSymbols(string localAssemblyName, bool alreadyInForeignAssembly) => new[] { VectorGroup }.Where((symbol) => symbol is not null && (alreadyInForeignAssembly || symbol.ContainingAssembly.Name != localAssemblyName)).Select(static (symbol) => symbol!);
}
