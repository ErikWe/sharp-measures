#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.AngularMomentum3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class TransformTests
{
    [Theory]
    [ClassData(typeof(AngularMomentum3Dataset))]
    public void Transform_Identity_ShouldBeUnmodified(AngularMomentum3 quantity)
    {
        Utility.QuantityTests.TransformTests.Identity_ShouldBeUnaltered(quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<AngularMomentum3Dataset, Vector3Dataset>))]
    public static void Translation_ShouldBeOffsetComponentwise(AngularMomentum3 quantity, Vector3 translation)
    {
        Utility.QuantityTests.TransformTests.Translation_ShouldBeOffsetByBComponentwise(quantity, translation);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<AngularMomentum3Dataset, Vector3Dataset>))]
    public static void Scaling_ShouldBeScaledComponentwise(AngularMomentum3 quantity, Vector3 scale)
    {
        Utility.QuantityTests.TransformTests.Scaling_ShouldBeScaledByBComponentwise(quantity, scale);
    }
}
