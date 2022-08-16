namespace SharpMeasures.Generators.Quantities;

using SharpMeasures.Generators.Utility;
using SharpMeasures.Generators.Raw.Quantities;

using System.Collections.Generic;

public interface IConvertibleQuantity
{
    public abstract IReadOnlyList<IRawQuantityType> Quantities { get; }

    public abstract bool Bidirectional { get; }
    public abstract ConversionOperatorBehaviour CastOperatorBehaviour { get; }
}
