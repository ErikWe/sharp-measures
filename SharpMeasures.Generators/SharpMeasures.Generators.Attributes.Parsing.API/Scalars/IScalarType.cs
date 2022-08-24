namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

public interface IScalarType : IQuantityType
{
    new public abstract IScalar Definition { get; }

    public abstract IReadOnlyList<IScalarConstant> Constants { get; }
    new public abstract IReadOnlyList<IConvertibleScalar> Conversions { get; }

    public abstract IReadOnlyList<IUnitList> BaseInclusions { get; }
    public abstract IReadOnlyList<IUnitList> BaseExclusions { get; }

    public IReadOnlyDictionary<string, IScalarConstant> ConstantsByName { get; }
    public IReadOnlyDictionary<string, IScalarConstant> ConstantsByMultiplesName { get; }
}
