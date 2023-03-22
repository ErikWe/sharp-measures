namespace SharpMeasures.Tests.Unhandled3Cases;

using Xunit;

public class PureMagnitude
{
    private static Scalar Target(Unhandled3 vector) => vector.PureMagnitude();

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3))]
    public void MatchComponentsMagnitude(Unhandled3 vector)
    {
        var expected = vector.Components.Magnitude();

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
