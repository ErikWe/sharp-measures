namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

using System.Collections.Generic;
using System.Linq;

internal class ResolvedVectorPopulation : IResolvedVectorPopulation
{
    public IReadOnlyDictionary<NamedType, IResolvedVectorGroupType> Groups => groups;
    public IReadOnlyDictionary<NamedType, IResolvedVectorType> Vectors => vectors;

    public IReadOnlyDictionary<NamedType, IReadOnlyHashSet<IOperatorDerivation>> OperatorImplementationsByQuantity => operatorDerivationsByQuantity;

    private ReadOnlyEquatableDictionary<NamedType, IResolvedVectorGroupType> groups { get; }
    private ReadOnlyEquatableDictionary<NamedType, IResolvedVectorType> vectors { get; }

    private ReadOnlyEquatableDictionary<NamedType, IReadOnlyHashSet<IOperatorDerivation>> operatorDerivationsByQuantity { get; }

    IReadOnlyDictionary<NamedType, IResolvedQuantityType> IResolvedQuantityPopulation.Quantities => Groups.Transform(static (vector) => vector as IResolvedQuantityType)
        .Concat(Vectors.Transform(static (vector) => vector as IResolvedQuantityType)).ToDictionary().AsEquatable();

    private ResolvedVectorPopulation(IReadOnlyDictionary<NamedType, IResolvedVectorGroupType> groups, IReadOnlyDictionary<NamedType, IResolvedVectorType> vectors, IReadOnlyDictionary<NamedType, IReadOnlyHashSet<IOperatorDerivation>> operatorDerivationsByQuantity)
    {
        this.groups = groups.AsReadOnlyEquatable();
        this.vectors = vectors.AsReadOnlyEquatable();

        this.operatorDerivationsByQuantity = operatorDerivationsByQuantity.AsReadOnlyEquatable();
    }

    public static ResolvedVectorPopulation Build(IReadOnlyList<IResolvedVectorType> vectorBases, IReadOnlyList<IResolvedVectorType> vectorSpecializations, IReadOnlyList<IResolvedVectorGroupType> groupBases,
        IReadOnlyList<IResolvedVectorGroupType> groupSpecializations, IReadOnlyList<IResolvedVectorType> groupMembers)
    {
        var allQuantities = (vectorBases as IReadOnlyList<IResolvedQuantityType>).Concat(vectorSpecializations).Concat(groupBases).Concat(groupSpecializations).Concat(groupMembers);
        
        Dictionary<NamedType, HashSet<IOperatorDerivation>> operatorDerivationsByQuantity = new();

        foreach (var quantity in allQuantities)
        {
            operatorDerivationsByQuantity.Add(quantity.Type.AsNamedType(), new HashSet<IOperatorDerivation>());
        }

        foreach (var quantity in allQuantities)
        {
            foreach (var derivation in quantity.DefinedDerivations)
            {
                foreach (var operatorDerivation in OperatorDerivationSearcher.GetDerivations(quantity.Type.AsNamedType(), derivation))
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

        return new(groupBases.Concat(groupSpecializations).ToDictionary(static (vector) => vector.Type.AsNamedType()), vectorBases.Concat(vectorSpecializations).Concat(groupMembers).ToDictionary(static (vector) => vector.Type.AsNamedType()),
            operatorDerivationsByQuantity.Transform(static (set) => set.AsReadOnlyEquatable() as IReadOnlyHashSet<IOperatorDerivation>));
    }
}
