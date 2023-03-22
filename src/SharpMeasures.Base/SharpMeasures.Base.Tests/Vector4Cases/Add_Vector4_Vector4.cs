namespace SharpMeasures.Tests.Vector4Cases;

using Xunit;

public class Add_Vector4_Vector4
{
    private static Vector4 Target(Vector4 a, Vector4 b) => Vector4.Add(a, b);

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4_ValidVector4))]
    public void MatchInstanceMethod(Vector4 a, Vector4 b)
    {
        var expected = a.Add(b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
