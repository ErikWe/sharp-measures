#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SpecificAngularMomentum3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class TransformTests
{
    [Theory]
    [ClassData(typeof(SpecificAngularMomentum3Dataset))]
    public void Transform_Identity_ShouldBeUnmodified(SpecificAngularMomentum3 quantity)
    {
        Utility.QuantityTests.TransformTests.Identity_ShouldBeUnaltered(quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<SpecificAngularMomentum3Dataset, Vector3Dataset>))]
    public static void Translation_ShouldBeOffsetComponentwise(SpecificAngularMomentum3 quantity, Vector3 translation)
    {
        Utility.QuantityTests.TransformTests.Translation_ShouldBeOffsetByBComponentwise(quantity, translation);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<SpecificAngularMomentum3Dataset, Vector3Dataset>))]
    public static void Scaling_ShouldBeScaledComponentwise(SpecificAngularMomentum3 quantity, Vector3 scale)
    {
        Utility.QuantityTests.TransformTests.Scaling_ShouldBeScaledByBComponentwise(quantity, scale);
    }
}
