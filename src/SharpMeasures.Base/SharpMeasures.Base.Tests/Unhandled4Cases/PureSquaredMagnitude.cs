namespace SharpMeasures.Tests.Unhandled4Cases;

using Xunit;

public class PureSquaredMagnitude
{
    private static Scalar Target(Unhandled4 vector) => vector.PureSquaredMagnitude();

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4))]
    public void MatchComponentsSquaredMagnitude(Unhandled4 vector)
    {
        var expected = vector.Components.SquaredMagnitude();

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
