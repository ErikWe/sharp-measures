namespace SharpMeasures.Tests.Unhandled2Cases;

using Xunit;

public class Deconstruct
{
    private static (Unhandled X, Unhandled Y) Target(Unhandled2 vector)
    {
        vector.Deconstruct(out var x, out var y);

        return (x, y);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2))]
    public void MatchX(Unhandled2 vector)
    {
        var expected = vector.X;

        var actual = Target(vector).X;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2))]
    public void MatchY(Unhandled2 vector)
    {
        var expected = vector.Y;

        var actual = Target(vector).Y;

        Assert.Equal(expected, actual);
    }
}
