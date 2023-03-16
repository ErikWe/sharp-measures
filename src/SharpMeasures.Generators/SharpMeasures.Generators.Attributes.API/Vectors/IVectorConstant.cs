namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

public interface IVectorConstant : IQuantityConstant
{
    public abstract IReadOnlyList<double>? Value { get; }
    public abstract IReadOnlyList<string>? Expressions { get; }

    new public abstract IVectorConstantLocations Locations { get; }
}

public interface IVectorConstantLocations : IQuantityConstantLocations
{
    public abstract MinimalLocation? ValueCollection { get; }
    public abstract IReadOnlyList<MinimalLocation> ValueElements { get; }

    public abstract MinimalLocation? ExpressionCollection { get; }
    public abstract IReadOnlyList<MinimalLocation> ExpressionElements { get; }

    public abstract bool ExplicitlySetValue { get; }
    public abstract bool ExplicitlySetExpressions { get; }
}
