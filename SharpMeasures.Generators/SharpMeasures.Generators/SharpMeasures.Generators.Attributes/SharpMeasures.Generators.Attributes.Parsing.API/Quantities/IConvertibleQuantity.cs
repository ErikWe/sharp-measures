namespace SharpMeasures.Generators.Quantities;

using SharpMeasures.Generators.Utility;

using System.Collections.Generic;

public interface IConvertibleQuantity
{
    public abstract IReadOnlyList<NamedType> Quantities { get; }

    public abstract bool Bidirectional { get; }
    public abstract ConversionOperatorBehaviour CastOperatorBehaviour { get; }
}
