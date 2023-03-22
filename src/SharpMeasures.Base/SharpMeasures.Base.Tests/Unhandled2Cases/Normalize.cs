namespace SharpMeasures.Tests.Unhandled2Cases;

using Xunit;

public class Normalize
{
    private static Unhandled2 Target(Unhandled2 vector) => vector.Normalize();

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2))]
    public void MatchArithmetic(Unhandled2 vector)
    {
        var expected = vector / vector.Magnitude();

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
