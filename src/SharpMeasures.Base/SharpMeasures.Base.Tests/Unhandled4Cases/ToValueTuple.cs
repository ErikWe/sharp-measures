namespace SharpMeasures.Tests.Unhandled4Cases;

using Xunit;

public class ToValueTuple
{
    private static (Unhandled X, Unhandled Y, Unhandled Z, Unhandled W) Target(Unhandled4 vector) => vector.ToValueTuple();

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4))]
    public void MatchX(Unhandled4 vector)
    {
        var expected = vector.X;

        var actual = Target(vector).X;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4))]
    public void MatchY(Unhandled4 vector)
    {
        var expected = vector.Y;

        var actual = Target(vector).Y;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4))]
    public void MatchZ(Unhandled4 vector)
    {
        var expected = vector.Z;

        var actual = Target(vector).Z;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4))]
    public void MatchW(Unhandled4 vector)
    {
        var expected = vector.W;

        var actual = Target(vector).W;

        Assert.Equal(expected, actual);
    }
}
