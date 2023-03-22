namespace SharpMeasures.Tests.Vector3Cases;

using Xunit;

public class Operator_Multiply_Scalar_Vector3
{
    private static Vector3 Target(Scalar a, Vector3 b) => a * b;

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3_ValidScalar))]
    public void MatchInstanceMethod(Vector3 b, Scalar a)
    {
        var expected = Vector3.Multiply(b, a);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
