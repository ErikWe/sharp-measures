namespace SharpMeasures.Tests.Vector4Cases;

using Xunit;

public class Divide_Vector4_Unhandled
{
    private static Unhandled4 Target(Vector4 a, Unhandled b) => Vector4.Divide(a, b);

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4_ValidUnhandled))]
    public void MatchInstanceMethod(Vector4 a, Unhandled b)
    {
        var expected = a.DivideBy(b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
