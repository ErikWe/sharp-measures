namespace SharpMeasures.Generators.Vectors;

public interface IVectorOperation : IAttributeDefinition
{
    public abstract NamedType Result { get; }
    public abstract NamedType Other { get; }

    public abstract VectorOperatorType OperatorType { get; }
    public abstract OperatorPosition Position { get; }

    public abstract bool Mirror { get; }

    public abstract string Name { get; }
    public abstract string MirroredName { get; }

    new public abstract IVectorOperationLocations Locations { get; }
}

public interface IVectorOperationLocations : IAttributeLocations
{
    public abstract MinimalLocation? Result { get; }
    public abstract MinimalLocation? Other { get; }

    public abstract MinimalLocation? OperatorType { get; }
    public abstract MinimalLocation? Position { get; }

    public abstract MinimalLocation? Mirror { get; }

    public abstract MinimalLocation? Name { get; }
    public abstract MinimalLocation? MirroredName { get; }

    public abstract bool ExplicitlySetResult { get; }
    public abstract bool ExplicitlySetOther { get; }

    public abstract bool ExplicitlySetOperatorType { get; }
    public abstract bool ExplicitlySetPosition { get; }

    public abstract bool ExplicitlySetMirror { get; }

    public abstract bool ExplicitlySetName { get; }
    public abstract bool ExplicitlySetMirroredName { get; }
}
