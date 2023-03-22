namespace SharpMeasures.Tests.Vector3Cases;

using Xunit;

public class AsIVector3Quantity_WithComponents_Scalar_Scalar_Scalar
{
    private static Vector3 Target(Scalar x, Scalar y, Scalar z)
    {
        return execute<Vector3>();

        TVector execute<TVector>() where TVector : IVector3Quantity<TVector> => TVector.WithComponents(x, y, z);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidScalarTuple))]
    public void MatchConstructor(Scalar x, Scalar y, Scalar z)
    {
        Vector3 expected = new(x, y, z);

        var actual = Target(x, y, z);

        Assert.Equal(expected, actual);
    }
}
