namespace SharpMeasures.Tests.Vector4Cases;

using Xunit;

public class Dot_Vector4_Vector4
{
    private static Scalar Target(Vector4 a, Vector4 b) => Vector4.Dot(a, b);

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4_ValidVector4))]
    public void MatchInstanceMethod(Vector4 a, Vector4 b)
    {
        var expected = a.Dot(b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
