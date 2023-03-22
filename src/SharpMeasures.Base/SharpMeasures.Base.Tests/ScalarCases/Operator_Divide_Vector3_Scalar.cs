namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class Operator_Divide_Vector3_Scalar
{
    private static Vector3 Target((Scalar, Scalar, Scalar) a, Scalar b) => a / b;

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar_ValidVector3))]
    public void MatchMethod(Scalar b, Vector3 a)
    {
        var expected = Vector3.Divide(a, b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
