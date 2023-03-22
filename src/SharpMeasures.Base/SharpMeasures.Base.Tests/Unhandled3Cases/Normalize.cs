namespace SharpMeasures.Tests.Unhandled3Cases;

using Xunit;

public class Normalize
{
    private static Unhandled3 Target(Unhandled3 vector) => vector.Normalize();

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3))]
    public void MatchArithmetic(Unhandled3 vector)
    {
        var expected = vector / vector.Magnitude();

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
