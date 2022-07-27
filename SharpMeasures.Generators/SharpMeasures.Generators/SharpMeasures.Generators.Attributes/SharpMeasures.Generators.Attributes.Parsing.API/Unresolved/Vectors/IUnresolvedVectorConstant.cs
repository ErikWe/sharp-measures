﻿namespace SharpMeasures.Generators.Unresolved.Vectors;

using SharpMeasures.Generators.Unresolved.Quantities;

using System.Collections.Generic;

public interface IUnresolvedVectorConstant : IUnresolvedQuantityConstant
{
    public abstract IReadOnlyList<double> Value { get; }
}
