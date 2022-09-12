namespace SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

public record class DerivedQuantityDefinition : AAttributeDefinition<DerivedQuantityLocations>, IDerivedQuantity
{
    public string Expression { get; }
    public IReadOnlyList<NamedType> Signature => signature;

    public DerivationOperatorImplementation OperatorImplementation { get; }

    public bool Permutations { get; }

    private ReadOnlyEquatableList<NamedType> signature { get; }

    IDerivedQuantityLocations IDerivedQuantity.Locations => Locations;

    public DerivedQuantityDefinition(string expression, IReadOnlyList<NamedType> signature, DerivationOperatorImplementation operatorImplementation, bool permutations, DerivedQuantityLocations locations) : base(locations)
    {
        Expression = expression;
        this.signature = signature.AsReadOnlyEquatable();

        OperatorImplementation = operatorImplementation;

        Permutations = permutations;
    }
}
