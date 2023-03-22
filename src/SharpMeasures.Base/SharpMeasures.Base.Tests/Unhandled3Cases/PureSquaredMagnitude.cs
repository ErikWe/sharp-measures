namespace SharpMeasures.Tests.Unhandled3Cases;

using Xunit;

public class PureSquaredMagnitude
{
    private static Scalar Target(Unhandled3 vector) => vector.PureSquaredMagnitude();

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3))]
    public void MatchComponentsSquaredMagnitude(Unhandled3 vector)
    {
        var expected = vector.Components.SquaredMagnitude();

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
