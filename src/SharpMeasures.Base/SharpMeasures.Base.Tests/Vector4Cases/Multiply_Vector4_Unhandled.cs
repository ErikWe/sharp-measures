namespace SharpMeasures.Tests.Vector4Cases;

using Xunit;

public class Multiply_Vector4_Unhandled
{
    private static Unhandled4 Target(Vector4 a, Unhandled b) => Vector4.Multiply(a, b);

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4_ValidUnhandled))]
    public void MatchInstanceMethod(Vector4 a, Unhandled b)
    {
        var expected = a.Multiply(b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
