#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.OrbitalAngularVelocity3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class TransformTests
{
    [Theory]
    [ClassData(typeof(OrbitalAngularVelocity3Dataset))]
    public void Transform_Identity_ShouldBeUnmodified(OrbitalAngularVelocity3 quantity)
    {
        Utility.QuantityTests.TransformTests.Identity_ShouldBeUnaltered(quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<OrbitalAngularVelocity3Dataset, Vector3Dataset>))]
    public static void Translation_ShouldBeOffsetComponentwise(OrbitalAngularVelocity3 quantity, Vector3 translation)
    {
        Utility.QuantityTests.TransformTests.Translation_ShouldBeOffsetByBComponentwise(quantity, translation);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<OrbitalAngularVelocity3Dataset, Vector3Dataset>))]
    public static void Scaling_ShouldBeScaledComponentwise(OrbitalAngularVelocity3 quantity, Vector3 scale)
    {
        Utility.QuantityTests.TransformTests.Scaling_ShouldBeScaledByBComponentwise(quantity, scale);
    }
}
