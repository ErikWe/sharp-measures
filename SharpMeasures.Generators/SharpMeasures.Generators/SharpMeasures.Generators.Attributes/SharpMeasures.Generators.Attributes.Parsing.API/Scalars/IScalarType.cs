namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Raw.Units.UnitInstances;

using System.Collections.Generic;

public interface IScalarType : IQuantityType
{
    new public abstract IScalar Definition { get; }

    public abstract IReadOnlyList<IScalarConstant> Constants { get; }
    new public abstract IReadOnlyList<IConvertibleScalar> Conversions { get; }

    public abstract IReadOnlyList<IRawUnitInstance> IncludedBases { get; }

    public IReadOnlyDictionary<string, IScalarConstant> ConstantsByName { get; }
    public IReadOnlyDictionary<string, IScalarConstant> ConstantsByMultiplesName { get; }
}
