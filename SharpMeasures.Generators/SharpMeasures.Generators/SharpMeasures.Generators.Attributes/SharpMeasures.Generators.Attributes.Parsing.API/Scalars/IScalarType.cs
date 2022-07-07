namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Units.UnitInstances;

using System.Collections.Generic;

public interface IScalarType
{
    public abstract DefinedType Type { get; }
    public abstract IScalar ScalarDefinition { get; }

    public abstract IReadOnlyList<IDerivedQuantity> Derivations { get; }
    public abstract IReadOnlyList<IScalarConstant> Constants { get; }
    public abstract IReadOnlyList<IConvertibleQuantity> ConvertibleQuantities { get; }

    public abstract IReadOnlyList<IUnitInstance> IncludedBases { get; }
    public abstract IReadOnlyList<IUnitInstance> IncludedUnits { get; }

    public IReadOnlyDictionary<string, IScalarConstant> ConstantsByName { get; }
}
