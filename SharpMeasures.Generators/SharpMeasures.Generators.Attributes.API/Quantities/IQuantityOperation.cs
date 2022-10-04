namespace SharpMeasures.Generators.Quantities;

public interface IQuantityOperation : IAttributeDefinition
{
    public abstract NamedType Result { get; }
    public abstract NamedType Other { get; }

    public abstract OperatorType OperatorType { get; }
    public abstract OperatorPosition Position { get; }

    public abstract bool Mirror { get; }
    public abstract bool MirrorMethod { get; }

    public abstract QuantityOperationImplementation Implementation { get; }

    public abstract string MethodName { get; }
    public abstract string MirroredMethodName { get; }

    new public abstract IQuantityOperationLocations Locations { get; }
}

public interface IQuantityOperationLocations : IAttributeLocations
{
    public abstract MinimalLocation? Result { get; }
    public abstract MinimalLocation? Other { get; }

    public abstract MinimalLocation? OperatorType { get; }
    public abstract MinimalLocation? Position { get; }

    public abstract MinimalLocation? Mirror { get; }
    public abstract MinimalLocation? MirroredMethodName { get; }

    public abstract MinimalLocation? Implementation { get; }

    public abstract MinimalLocation? MethodName { get; }

    public abstract bool ExplicitlySetResult { get; }
    public abstract bool ExplicitlySetOther { get; }

    public abstract bool ExplicitlySetOperatorType { get; }
    public abstract bool ExplicitlySetPosition { get; }

    public abstract bool ExplicitlySetMirror { get; }
    public abstract bool ExplicitlySetMirroredMethodName { get; }

    public abstract bool ExplicitlySetImplementation { get; }

    public abstract bool ExplicitlySetMethodName { get; }
}
