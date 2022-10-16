namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

internal sealed record class SymbolicSharpMeasuresVectorGroupMemberDefinition : ARawAttributeDefinition<SymbolicSharpMeasuresVectorGroupMemberDefinition, SharpMeasuresVectorGroupMemberLocations>
{
    public static SymbolicSharpMeasuresVectorGroupMemberDefinition Empty { get; } = new(SharpMeasuresVectorGroupMemberLocations.Empty);

    public INamedTypeSymbol? VectorGroup { get; init; }

    public bool InheritOperations { get; init; } = true;
    public bool InheritConversions { get; init; } = true;
    public bool InheritUnits { get; init; } = true;

    public bool? InheritOperationsFromMembers { get; init; }
    public bool InheritProcessesFromMembers { get; init; } = true;
    public bool InheritConstantsFromMembers { get; init; } = true;
    public bool? InheritConversionsFromMembers { get; init; }
    public bool? InheritUnitsFromMembers { get; init; }

    public int? Dimension { get; init; }

    protected override SymbolicSharpMeasuresVectorGroupMemberDefinition Definition => this;

    private SymbolicSharpMeasuresVectorGroupMemberDefinition(SharpMeasuresVectorGroupMemberLocations locations) : base(locations) { }

    public IEnumerable<INamedTypeSymbol> ForeignSymbols(string localAssemblyName, bool alreadyInForeignAssembly)
    {
        if (VectorGroup is not null && (alreadyInForeignAssembly || VectorGroup.ContainingAssembly.Name != localAssemblyName))
        {
            yield return VectorGroup!;
        }
    }
}
