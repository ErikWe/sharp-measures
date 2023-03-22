namespace SharpMeasures.Tests.Unhandled2Cases;

using Xunit;

public class PureSquaredMagnitude
{
    private static Scalar Target(Unhandled2 vector) => vector.PureSquaredMagnitude();

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2))]
    public void MatchComponentsSquaredMagnitude(Unhandled2 vector)
    {
        var expected = vector.Components.SquaredMagnitude();

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
