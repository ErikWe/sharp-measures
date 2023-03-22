namespace SharpMeasures.Tests.Vector3Cases;

using Xunit;

public class Operator_Multiply_Vector3_Unhandled
{
    private static Unhandled3 Target(Vector3 a, Unhandled b) => a * b;

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3_ValidUnhandled))]
    public void MatchMethod(Vector3 a, Unhandled b)
    {
        var expected = Vector3.Multiply(a, b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
