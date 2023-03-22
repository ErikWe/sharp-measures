namespace SharpMeasures.Tests.Vector3Cases;

using Xunit;

public class Operator_Divide_Vector3_Unhandled
{
    private static Unhandled3 Target(Vector3 a, Unhandled b) => a / b;

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3_ValidUnhandled))]
    public void MatchInstanceMethod(Vector3 a, Unhandled b)
    {
        var expected = Vector3.Divide(a, b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
