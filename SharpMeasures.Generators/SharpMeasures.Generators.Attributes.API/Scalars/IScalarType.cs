namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

public interface IScalarType : IQuantityType
{
    new public abstract IScalar Definition { get; }

    public abstract IReadOnlyList<IProcessedQuantity> Processes { get; }
    public abstract IReadOnlyList<IScalarConstant> Constants { get; }

    public abstract IReadOnlyList<IUnitInstanceInclusionList> UnitBaseInstanceInclusions { get; }
    public abstract IReadOnlyList<IUnitInstanceList> UnitBaseInstanceExclusions { get; }

    public abstract IReadOnlyDictionary<string, IScalarConstant> ConstantsByName { get; }
    public abstract IReadOnlyDictionary<string, IScalarConstant> ConstantsByMultiplesName { get; }
}
