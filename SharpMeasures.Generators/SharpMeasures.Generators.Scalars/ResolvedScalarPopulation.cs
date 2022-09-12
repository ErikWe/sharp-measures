namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

using System.Collections.Generic;
using System.Linq;

internal class ResolvedScalarPopulation : IResolvedScalarPopulation
{
    public IReadOnlyDictionary<NamedType, IResolvedScalarType> Scalars => scalars;
    public IReadOnlyDictionary<NamedType, IReadOnlyHashSet<IOperatorDerivation>> OperatorImplementationsByQuantity => operatorDerivationsByQuantity;

    private ReadOnlyEquatableDictionary<NamedType, IResolvedScalarType> scalars { get; }
    private ReadOnlyEquatableDictionary<NamedType, IReadOnlyHashSet<IOperatorDerivation>> operatorDerivationsByQuantity { get; }

    IReadOnlyDictionary<NamedType, IResolvedQuantityType> IResolvedQuantityPopulation.Quantities => Scalars.Transform(static (scalar) => scalar as IResolvedQuantityType);

    private ResolvedScalarPopulation(IReadOnlyDictionary<NamedType, IResolvedScalarType> scalars, IReadOnlyDictionary<NamedType, IReadOnlyHashSet<IOperatorDerivation>> operatorDerivationsByQuantity)
    {
        this.scalars = scalars.AsReadOnlyEquatable();
        this.operatorDerivationsByQuantity = operatorDerivationsByQuantity.AsReadOnlyEquatable();
    }

    public static ResolvedScalarPopulation Build(IReadOnlyList<IResolvedScalarType> scalarBases, IReadOnlyList<IResolvedScalarType> scalarSpecializations)
    {
        var allScalars = scalarBases.Concat(scalarSpecializations);

        Dictionary<NamedType, HashSet<IOperatorDerivation>> operatorDerivationsByQuantity = new();

        foreach (var scalar in allScalars)
        {
            operatorDerivationsByQuantity.Add(scalar.Type.AsNamedType(), new HashSet<IOperatorDerivation>());
        }

        foreach (var scalar in allScalars)
        {
            foreach (var derivation in scalar.DefinedDerivations)
            {
                foreach (var operatorDerivation in OperatorDerivationSearcher.GetDerivations(scalar.Type.AsNamedType(), derivation))
                {
                    if (operatorDerivationsByQuantity.TryGetValue(operatorDerivation.LeftHandSide, out var leftHandSideDerivations))
                    {
                        leftHandSideDerivations.Add(operatorDerivation);
                    }

                    if (operatorDerivationsByQuantity.TryGetValue(operatorDerivation.RightHandSide, out var rightHandSideDerivations))
                    {
                        rightHandSideDerivations.Add(operatorDerivation);
                    }
                }
            }
        }

        return new(allScalars.ToDictionary(static (scalar) => scalar.Type.AsNamedType()), operatorDerivationsByQuantity.Transform(static (set) => set.AsReadOnlyEquatable() as IReadOnlyHashSet<IOperatorDerivation>));
    }
}
