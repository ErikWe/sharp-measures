namespace SharpMeasures.Tests.Unhandled4Cases;

using Xunit;

public class Normalize
{
    private static Unhandled4 Target(Unhandled4 vector) => vector.Normalize();

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4))]
    public void MatchArithmetic(Unhandled4 vector)
    {
        var expected = vector / vector.Magnitude();

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
