namespace SharpMeasures.Generators.Raw.Quantities;

using SharpMeasures.Generators.Utility;

using System.Collections.Generic;

public interface IRawConvertibleQuantity
{
    public abstract IReadOnlyList<NamedType> Quantities { get; }

    public abstract bool Bidirectional { get; }
    public abstract ConversionOperatorBehaviour CastOperatorBehaviour { get; }
}
