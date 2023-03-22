namespace SharpMeasures.Tests.Unhandled2Cases;

using Xunit;

public class GetHashCode
{
    private static int Target(Unhandled2 vector) => vector.GetHashCode();

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2))]
    public void Equal_Match(Unhandled2 vector)
    {
        Unhandled2 other = new(vector.X, vector.Y);

        var expected = Target(other);

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
