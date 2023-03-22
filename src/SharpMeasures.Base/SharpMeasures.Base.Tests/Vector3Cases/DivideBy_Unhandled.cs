namespace SharpMeasures.Tests.Vector3Cases;

using Xunit;

public class DivideBy_Unhandled
{
    private static Unhandled3 Target(Vector3 vector, Unhandled divisor) => vector.DivideBy(divisor);

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3_ValidUnhandled))]
    public void MatchXDivideBy(Vector3 vector, Unhandled divisor)
    {
        var expected = vector.X.DivideBy(divisor);

        var actual = Target(vector, divisor).X;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3_ValidUnhandled))]
    public void MatchYDivideBy(Vector3 vector, Unhandled divisor)
    {
        var expected = vector.Y.DivideBy(divisor);

        var actual = Target(vector, divisor).Y;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3_ValidUnhandled))]
    public void MatchZDivideBy(Vector3 vector, Unhandled divisor)
    {
        var expected = vector.Z.DivideBy(divisor);

        var actual = Target(vector, divisor).Z;

        Assert.Equal(expected, actual);
    }
}
