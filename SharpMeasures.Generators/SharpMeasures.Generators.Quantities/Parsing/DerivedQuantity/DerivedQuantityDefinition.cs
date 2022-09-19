namespace SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

public sealed record class DerivedQuantityDefinition : AAttributeDefinition<IDerivedQuantityLocations>, IDerivedQuantity
{
    public string Expression { get; }
    public IReadOnlyList<NamedType> Signature { get; }

    public DerivationOperatorImplementation OperatorImplementation { get; }

    public bool Permutations { get; }

    public IReadOnlyList<IExpandedDerivedQuantity> ExpandedScalarResults { get; }
    public IReadOnlyDictionary<int, IReadOnlyList<IExpandedDerivedQuantity>> ExpandedVectorResults { get; }

    public DerivedQuantityDefinition(string expression, IReadOnlyList<NamedType> signature, DerivationOperatorImplementation operatorImplementation, bool permutations, IDerivedQuantityLocations locations) : base(locations)
    {
        Expression = expression;
        Signature = signature.AsReadOnlyEquatable();

        OperatorImplementation = operatorImplementation;

        Permutations = permutations;

        ExpandedScalarResults = ReadOnlyEquatableList<IExpandedDerivedQuantity>.Empty;
        ExpandedVectorResults = ReadOnlyEquatableDictionary<int, IReadOnlyList<IExpandedDerivedQuantity>>.Empty;
    }

    public DerivedQuantityDefinition(string expression, IReadOnlyList<NamedType> signature, DerivationOperatorImplementation operatorImplementation, bool permutations, IReadOnlyList<IExpandedDerivedQuantity> expandedScalarResults, IReadOnlyDictionary<int, IReadOnlyList<IExpandedDerivedQuantity>> expandedVectorResults, IDerivedQuantityLocations locations) : base(locations)
    {
        Expression = expression;
        Signature = signature.AsReadOnlyEquatable();

        OperatorImplementation = operatorImplementation;

        Permutations = permutations;

        ExpandedScalarResults = expandedScalarResults.AsReadOnlyEquatable();
        ExpandedVectorResults = expandedVectorResults.Transform(static (list) => list.AsReadOnlyEquatable() as IReadOnlyList<IExpandedDerivedQuantity>).AsReadOnlyEquatable();
    }
}
