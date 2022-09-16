namespace SharpMeasures.Generators;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors;

using System.Collections.Generic;

internal record class ResolvedForeignTypes
{
    public IReadOnlyList<IUnitType> Units => units;

    public IReadOnlyList<IResolvedScalarType> Scalars => scalars;

    public IReadOnlyList<IResolvedVectorGroupType> Groups => groups;
    public IReadOnlyList<IResolvedVectorType> Vectors => vectors;

    private ReadOnlyEquatableList<IUnitType> units { get; }

    private ReadOnlyEquatableList<IResolvedScalarType> scalars { get; }

    private ReadOnlyEquatableList<IResolvedVectorGroupType> groups { get; }
    private ReadOnlyEquatableList<IResolvedVectorType> vectors { get; }

    public ResolvedForeignTypes(IReadOnlyList<IUnitType> units, IReadOnlyList<IResolvedScalarType> scalars, IReadOnlyList<IResolvedVectorGroupType> groups, IReadOnlyList<IResolvedVectorType> vectors)
    {
        this.units = units.AsReadOnlyEquatable();

        this.scalars = scalars.AsReadOnlyEquatable();

        this.groups = groups.AsReadOnlyEquatable();
        this.vectors = vectors.AsReadOnlyEquatable();
    }
}
