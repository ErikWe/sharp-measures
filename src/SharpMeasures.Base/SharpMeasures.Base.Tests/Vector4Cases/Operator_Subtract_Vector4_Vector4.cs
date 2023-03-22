namespace SharpMeasures.Tests.Vector4Cases;

using Xunit;

public class Operator_Subtract_Vector4_Vector4
{
    private static Vector4 Target(Vector4 a, Vector4 b) => a - b;

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4_ValidVector4))]
    public void MatchMethod(Vector4 a, Vector4 b)
    {
        var expected = Vector4.Subtract(a, b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
