namespace SharpMeasures.Tests.Vector4Cases;

using Xunit;

public class Operator_Divide_Vector4_Unhandled
{
    private static Unhandled4 Target(Vector4 a, Unhandled b) => a / b;

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4_ValidUnhandled))]
    public void MatchInstanceMethod(Vector4 a, Unhandled b)
    {
        var expected = Vector4.Divide(a, b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
