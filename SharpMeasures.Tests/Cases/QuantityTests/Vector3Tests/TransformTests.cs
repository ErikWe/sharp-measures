namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Vector3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class TransformTests
{
    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void Transform_Identity_ShouldBeUnmodified(Vector3 vector)
    {
        Utility.QuantityTests.TransformTests.Identity_ShouldBeUnaltered(vector);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, Vector3Dataset>))]
    public static void Translation_ShouldBeOffsetComponentwise(Vector3 vector, Vector3 translation)
    {
        Utility.QuantityTests.TransformTests.Translation_ShouldBeOffsetByBComponentwise(vector, translation);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, Vector3Dataset>))]
    public static void Scaling_ShouldBeScaledComponentwise(Vector3 vector, Vector3 scale)
    {
        Utility.QuantityTests.TransformTests.Scaling_ShouldBeScaledByBComponentwise(vector, scale);
    }
}
