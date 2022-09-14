﻿namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

public interface IScalarType : IQuantityType
{
    new public abstract IScalar Definition { get; }

    public abstract IReadOnlyList<IScalarConstant> Constants { get; }

    public abstract IReadOnlyList<IUnitInstanceInclusionList> UnitInstanceBaseInclusions { get; }
    public abstract IReadOnlyList<IUnitInstanceList> UnitInstanceBaseExclusions { get; }

    public IReadOnlyDictionary<string, IScalarConstant> ConstantsByName { get; }
    public IReadOnlyDictionary<string, IScalarConstant> ConstantsByMultiplesName { get; }
}
