namespace SharpMeasures.Tests.Vector4Cases;

using Xunit;

public class Equals_Vector4_Vector4
{
    private static bool Target(Vector4 lhs, Vector4 rhs) => Vector4.Equals(lhs, rhs);

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4_ValidVector4))]
    public void Valid_MatchInstanceMethod(Vector4 lhs, Vector4 rhs)
    {
        var expected = lhs.Equals(rhs);

        var actual = Target(lhs, rhs);

        Assert.Equal(expected, actual);
    }
}
