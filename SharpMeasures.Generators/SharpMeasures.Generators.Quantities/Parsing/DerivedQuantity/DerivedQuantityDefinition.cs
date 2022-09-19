namespace SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

public record class DerivedQuantityDefinition : AAttributeDefinition<IDerivedQuantityLocations>, IDerivedQuantity
{
    public string Expression { get; }
    public IReadOnlyList<NamedType> Signature => signature;

    public DerivationOperatorImplementation OperatorImplementation { get; }

    public bool Permutations { get; }

    public IReadOnlyList<IExpandedDerivedQuantity> ExpandedScalarResults => expandedScalarResults;
    public IReadOnlyDictionary<int, IReadOnlyList<IExpandedDerivedQuantity>> ExpandedVectorResults => expandedVectorResults;

    private ReadOnlyEquatableList<NamedType> signature { get; }

    private ReadOnlyEquatableList<IExpandedDerivedQuantity> expandedScalarResults { get; }
    private ReadOnlyEquatableDictionary<int, IReadOnlyList<IExpandedDerivedQuantity>> expandedVectorResults { get; }

    public DerivedQuantityDefinition(string expression, IReadOnlyList<NamedType> signature, DerivationOperatorImplementation operatorImplementation, bool permutations, IDerivedQuantityLocations locations) : base(locations)
    {
        Expression = expression;
        this.signature = signature.AsReadOnlyEquatable();

        OperatorImplementation = operatorImplementation;

        Permutations = permutations;

        expandedScalarResults = ReadOnlyEquatableList<IExpandedDerivedQuantity>.Empty;
        expandedVectorResults = ReadOnlyEquatableDictionary<int, IReadOnlyList<IExpandedDerivedQuantity>>.Empty;
    }

    public DerivedQuantityDefinition(string expression, IReadOnlyList<NamedType> signature, DerivationOperatorImplementation operatorImplementation, bool permutations, IReadOnlyList<IExpandedDerivedQuantity> expandedScalarResults, IReadOnlyDictionary<int, IReadOnlyList<IExpandedDerivedQuantity>> expandedVectorResults, IDerivedQuantityLocations locations) : base(locations)
    {
        Expression = expression;
        this.signature = signature.AsReadOnlyEquatable();

        OperatorImplementation = operatorImplementation;

        Permutations = permutations;

        this.expandedScalarResults = expandedScalarResults.AsReadOnlyEquatable();
        this.expandedVectorResults = expandedVectorResults.Transform(static (list) => list.AsReadOnlyEquatable() as IReadOnlyList<IExpandedDerivedQuantity>).AsReadOnlyEquatable();
    }
}
