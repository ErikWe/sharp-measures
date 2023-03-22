namespace SharpMeasures.Tests.Vector3Cases;

using Xunit;

public class Operator_Equality
{
    private static bool Target(Vector3 lhs, Vector3 rhs) => lhs == rhs;

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3_ValidVector3))]
    public void Valid_MatchEqualsMethod(Vector3 lhs, Vector3 rhs)
    {
        var expected = lhs.Equals(rhs);

        var actual = Target(lhs, rhs);

        Assert.Equal(expected, actual);
    }
}
