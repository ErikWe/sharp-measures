namespace SharpMeasures.Generators.Raw.Scalars;

using SharpMeasures.Generators.Raw.Quantities;

using System.Collections.Generic;

public interface IRawScalarType : IRawQuantityType
{
    new public abstract IRawScalar Definition { get; }

    public abstract IReadOnlyList<IRawScalarConstant> Constants { get; }
    new public abstract IReadOnlyList<IRawConvertibleScalar> Conversions { get; }

    public abstract IReadOnlyList<IRawUnitList> BaseInclusions { get; }
    public abstract IReadOnlyList<IRawUnitList> BaseExclusion { get; }

    public IReadOnlyDictionary<string, IRawScalarConstant> ConstantsByName { get; }
    public IReadOnlyDictionary<string, IRawScalarConstant> ConstantsByMultiplesName { get; }
}
