namespace SharpMeasures.Tests.Unhandled4Cases;

using Xunit;

public class PureMagnitude
{
    private static Scalar Target(Unhandled4 vector) => vector.PureMagnitude();

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4))]
    public void MatchComponentsMagnitude(Unhandled4 vector)
    {
        var expected = vector.Components.Magnitude();

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
