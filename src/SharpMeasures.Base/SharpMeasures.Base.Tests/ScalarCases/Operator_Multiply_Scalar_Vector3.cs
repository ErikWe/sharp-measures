namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class Operator_Multiply_Scalar_Vector3
{
    private static Vector3 Target(Scalar a, (Scalar, Scalar, Scalar) b) => a * b;

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar_ValidVector3))]
    public void MatchMethod(Scalar a, Vector3 b)
    {
        var expected = Scalar.Multiply(a, b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
