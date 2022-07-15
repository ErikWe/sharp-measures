namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

public interface IScalarType : IQuantityType
{
    new public abstract IScalar Definition { get; }

    new public abstract IReadOnlyList<IScalarConstant> Constants { get; }
    public abstract IReadOnlyList<IConvertibleScalar> ConvertibleScalars { get; }

    public abstract IReadOnlyList<IUnitList> BaseInclusions { get; }
    public abstract IReadOnlyList<IUnitList> BaseExclusions { get; }

    new public IReadOnlyDictionary<string, IScalarConstant> ConstantsByName { get; }
    new public IReadOnlyDictionary<string, IScalarConstant> ConstantsByMultiplesName { get; }
}
