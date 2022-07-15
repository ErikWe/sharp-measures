namespace SharpMeasures.Generators.Unresolved.Quantities;

using SharpMeasures.Generators.Utility;

using System.Collections.Generic;

public interface IUnresolvedConvertibleQuantity
{
    public abstract IReadOnlyList<NamedType> Quantities { get; }

    public abstract bool Bidirectional { get; }
    public abstract ConversionOperatorBehaviour CastOperatorBehaviour { get; }
}
