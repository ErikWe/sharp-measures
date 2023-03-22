namespace SharpMeasures.Tests.Unhandled3Cases;

using Xunit;

public class ToValueTuple
{
    private static (Unhandled X, Unhandled Y, Unhandled Z) Target(Unhandled3 vector) => vector.ToValueTuple();

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3))]
    public void MatchX(Unhandled3 vector)
    {
        var expected = vector.X;

        var actual = Target(vector).X;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3))]
    public void MatchY(Unhandled3 vector)
    {
        var expected = vector.Y;

        var actual = Target(vector).Y;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3))]
    public void MatchZ(Unhandled3 vector)
    {
        var expected = vector.Z;

        var actual = Target(vector).Z;

        Assert.Equal(expected, actual);
    }
}
