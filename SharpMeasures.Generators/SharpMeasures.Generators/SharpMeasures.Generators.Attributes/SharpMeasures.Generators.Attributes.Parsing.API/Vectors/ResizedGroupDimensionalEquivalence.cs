namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities.Utility;

public class ResizedGroupDimensionalEquivalence
{
    public ResizedGroup Group { get; }

    public ConversionOperationBehaviour CastOperatorBehaviour { get; }

    public ResizedGroupDimensionalEquivalence(ResizedGroup group, ConversionOperationBehaviour castOperatorBehaviour)
    {
        Group = group;

        CastOperatorBehaviour = castOperatorBehaviour;
    }
}
