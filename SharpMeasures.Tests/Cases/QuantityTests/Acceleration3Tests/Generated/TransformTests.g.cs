#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Acceleration3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class TransformTests
{
    [Theory]
    [ClassData(typeof(Acceleration3Dataset))]
    public void Transform_Identity_ShouldBeUnmodified(Acceleration3 quantity)
    {
        Utility.QuantityTests.TransformTests.Identity_ShouldBeUnaltered(quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Acceleration3Dataset, Vector3Dataset>))]
    public static void Translation_ShouldBeOffsetComponentwise(Acceleration3 quantity, Vector3 translation)
    {
        Utility.QuantityTests.TransformTests.Translation_ShouldBeOffsetByBComponentwise(quantity, translation);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Acceleration3Dataset, Vector3Dataset>))]
    public static void Scaling_ShouldBeScaledComponentwise(Acceleration3 quantity, Vector3 scale)
    {
        Utility.QuantityTests.TransformTests.Scaling_ShouldBeScaledByBComponentwise(quantity, scale);
    }
}
