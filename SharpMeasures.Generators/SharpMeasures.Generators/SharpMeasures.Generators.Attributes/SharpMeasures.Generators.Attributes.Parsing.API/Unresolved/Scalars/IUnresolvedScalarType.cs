namespace SharpMeasures.Generators.Unresolved.Scalars;

using SharpMeasures.Generators.Unresolved.Quantities;

using System.Collections.Generic;

public interface IUnresolvedScalarType : IUnresolvedQuantityType
{
    new public abstract IUnresolvedScalar Definition { get; }

    new public abstract IReadOnlyList<IUnresolvedScalarConstant> Constants { get; }
    public abstract IReadOnlyList<IUnresolvedConvertibleScalar> ConvertibleScalars { get; }

    public abstract IReadOnlyList<IUnresolvedUnitList> BaseInclusions { get; }
    public abstract IReadOnlyList<IUnresolvedUnitList> BaseExclusion { get; }

    new public IReadOnlyDictionary<string, IUnresolvedScalarConstant> ConstantsByName { get; }
    new public IReadOnlyDictionary<string, IUnresolvedScalarConstant> ConstantsByMultiplesName { get; }
}
