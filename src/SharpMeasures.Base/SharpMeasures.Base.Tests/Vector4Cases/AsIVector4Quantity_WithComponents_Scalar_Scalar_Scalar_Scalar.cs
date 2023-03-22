namespace SharpMeasures.Tests.Vector4Cases;

using Xunit;

public class AsIVector4Quantity_WithComponents_Scalar_Scalar_Scalar_Scalar
{
    private static Vector4 Target(Scalar x, Scalar y, Scalar z, Scalar w)
    {
        return execute<Vector4>();

        TVector execute<TVector>() where TVector : IVector4Quantity<TVector> => TVector.WithComponents(x, y, z, w);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidScalarTuple))]
    public void MatchConstructor(Scalar x, Scalar y, Scalar z, Scalar w)
    {
        Vector4 expected = new(x, y, z, w);

        var actual = Target(x, y, z, w);

        Assert.Equal(expected, actual);
    }
}
