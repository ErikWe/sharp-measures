namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Quantities;

public interface IScalar : IQuantity
{
    public abstract NamedType? Vector { get; }

    public abstract NamedType? Reciprocal { get; }
    public abstract NamedType? Square { get; }
    public abstract NamedType? Cube { get; }
    public abstract NamedType? SquareRoot { get; }
    public abstract NamedType? CubeRoot { get; }

    new public abstract IScalarLocations Locations { get; }
}

public interface IScalarLocations : IQuantityLocations
{
    public abstract MinimalLocation? Vector { get; }

    public abstract MinimalLocation? Reciprocal { get; }
    public abstract MinimalLocation? Square { get; }
    public abstract MinimalLocation? Cube { get; }
    public abstract MinimalLocation? SquareRoot { get; }
    public abstract MinimalLocation? CubeRoot { get; }

    public abstract bool ExplicitlySetVector { get; }

    public abstract bool ExplicitlySetReciprocal { get; }
    public abstract bool ExplicitlySetSquare { get; }
    public abstract bool ExplicitlySetCube { get; }
    public abstract bool ExplicitlySetSquareRoot { get; }
    public abstract bool ExplicitlySetCubeRoot { get; }
}
