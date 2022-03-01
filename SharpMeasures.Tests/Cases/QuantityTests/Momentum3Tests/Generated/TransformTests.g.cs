#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Momentum3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class TransformTests
{
    [Theory]
    [ClassData(typeof(Momentum3Dataset))]
    public void Transform_Identity_ShouldBeUnmodified(Momentum3 quantity)
    {
        Utility.QuantityTests.TransformTests.Identity_ShouldBeUnaltered(quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Momentum3Dataset, Vector3Dataset>))]
    public static void Translation_ShouldBeOffsetComponentwise(Momentum3 quantity, Vector3 translation)
    {
        Utility.QuantityTests.TransformTests.Translation_ShouldBeOffsetByBComponentwise(quantity, translation);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Momentum3Dataset, Vector3Dataset>))]
    public static void Scaling_ShouldBeScaledComponentwise(Momentum3 quantity, Vector3 scale)
    {
        Utility.QuantityTests.TransformTests.Scaling_ShouldBeScaledByBComponentwise(quantity, scale);
    }
}
