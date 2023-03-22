namespace SharpMeasures.Tests.Vector3Cases;

using Xunit;

public class Equals_Vector3_Vector3
{
    private static bool Target(Vector3 lhs, Vector3 rhs) => Vector3.Equals(lhs, rhs);

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3_ValidVector3))]
    public void Valid_MatchInstanceMethod(Vector3 lhs, Vector3 rhs)
    {
        var expected = lhs.Equals(rhs);

        var actual = Target(lhs, rhs);

        Assert.Equal(expected, actual);
    }
}
