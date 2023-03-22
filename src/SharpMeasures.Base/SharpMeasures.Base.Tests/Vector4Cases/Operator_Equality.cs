namespace SharpMeasures.Tests.Vector4Cases;

using Xunit;

public class Operator_Equality
{
    private static bool Target(Vector4 lhs, Vector4 rhs) => lhs == rhs;

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4_ValidVector4))]
    public void Valid_MatchEqualsMethod(Vector4 lhs, Vector4 rhs)
    {
        var expected = lhs.Equals(rhs);

        var actual = Target(lhs, rhs);

        Assert.Equal(expected, actual);
    }
}
