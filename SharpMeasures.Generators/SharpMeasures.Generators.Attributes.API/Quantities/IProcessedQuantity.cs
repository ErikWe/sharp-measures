namespace SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

public interface IProcessedQuantity : IAttributeDefinition
{
    public abstract string Name { get; }
    public abstract NamedType? Result { get; }
    public abstract string Expression { get; }

    public abstract bool ImplementAsProperty { get; }
    public abstract bool ImplementStatically { get; }

    public abstract bool ResultsInCurrentType { get; }

    public abstract IReadOnlyList<NamedType> ParameterTypes { get; }
    public abstract IReadOnlyList<string> ParameterNames { get; }

    new public abstract IProcessedQuantityLocations Locations { get; }
}

public interface IProcessedQuantityLocations : IAttributeLocations
{
    public abstract MinimalLocation? Name { get; }
    public abstract MinimalLocation? Result { get; }
    public abstract MinimalLocation? Expression { get; }

    public abstract MinimalLocation? ImplementAsProperty { get; }
    public abstract MinimalLocation? ImplementStatically { get; }

    public abstract MinimalLocation? ParameterTypesCollection { get; }
    public abstract IReadOnlyList<MinimalLocation> ParameterTypeElements { get; }
    
    public abstract MinimalLocation? ParameterNamesCollection { get; }
    public abstract IReadOnlyList<MinimalLocation> ParameterNameElements { get; }

    public abstract bool ExplicitlySetName { get; }
    public abstract bool ExplicitlySetResult { get; }
    public abstract bool ExplicitlySetExpression { get; }
    
    public abstract bool ExplicitlySetImplementAsProperty { get; }
    public abstract bool ExplicitlySetImplementStatically { get; }

    public abstract bool ExplicitlySetParameterTypes { get; }
    public abstract bool ExplicitlySetParameterNames { get; }
}
