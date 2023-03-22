namespace SharpMeasures.Tests.Vector3Cases;

using Xunit;

public class Operator_Remainder_Vector3_Scalar
{
    private static Vector3 Target(Vector3 a, Scalar b) => a % b;

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3_ValidScalar))]
    public void MatchMethod(Vector3 a, Scalar b)
    {
        var expected = Vector3.Remainder(a, b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
