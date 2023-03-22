namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class AsIScalarQuantity_WithMagnitude_Scalar
{
    private static Scalar Target(Scalar magnitude)
    {
        return execute<Scalar>(magnitude);

        static T execute<T>(Scalar magnitude) where T : IScalarQuantity<T> => T.WithMagnitude(magnitude);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar))]
    public void EqualOriginal(Scalar scalar)
    {
        var actual = Target(scalar);

        Assert.Equal(scalar, actual);
    }
}
