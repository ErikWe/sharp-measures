namespace SharpMeasures.Tests.Unhandled2Cases;

using Xunit;

public class PureMagnitude
{
    private static Scalar Target(Unhandled2 vector) => vector.PureMagnitude();

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2))]
    public void MatchComponentsMagnitude(Unhandled2 vector)
    {
        var expected = vector.Components.Magnitude();

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
