namespace SharpMeasures.Generators.Unresolved.Scalars;

using SharpMeasures.Generators.Unresolved.Quantities;

using System.Collections.Generic;

public interface IUnresolvedScalarType : IUnresolvedQuantityType
{
    new public abstract IUnresolvedScalar Definition { get; }

    public abstract IReadOnlyList<IUnresolvedScalarConstant> Constants { get; }
    new public abstract IReadOnlyList<IUnresolvedConvertibleScalar> Conversions { get; }

    public abstract IReadOnlyList<IUnresolvedUnitList> BaseInclusions { get; }
    public abstract IReadOnlyList<IUnresolvedUnitList> BaseExclusion { get; }

    public IReadOnlyDictionary<string, IUnresolvedScalarConstant> ConstantsByName { get; }
    public IReadOnlyDictionary<string, IUnresolvedScalarConstant> ConstantsByMultiplesName { get; }
}
