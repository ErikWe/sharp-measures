namespace SharpMeasures.Tests.Vector3Cases;

using Xunit;

public class Multiply_Vector3_Unhandled
{
    private static Unhandled3 Target(Vector3 a, Unhandled b) => Vector3.Multiply(a, b);

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3_ValidUnhandled))]
    public void MatchInstanceMethod(Vector3 a, Unhandled b)
    {
        var expected = a.Multiply(b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
