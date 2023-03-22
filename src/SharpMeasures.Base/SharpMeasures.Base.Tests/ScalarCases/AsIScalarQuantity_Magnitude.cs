namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class AsIScalarQuantity_Magnitude
{
    private static Scalar Target(Scalar scalar)
    {
        return execute(scalar);

        static Scalar execute(IScalarQuantity quantity) => quantity.Magnitude;
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar))]
    public void EqualOriginal(Scalar scalar)
    {
        var actual = Target(scalar);

        Assert.Equal(scalar, actual);
    }
}
