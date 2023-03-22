namespace SharpMeasures.Tests.Unhandled3Cases;

using Xunit;

public class GetHashCode
{
    private static int Target(Unhandled3 vector) => vector.GetHashCode();

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3))]
    public void Equal_Match(Unhandled3 vector)
    {
        Unhandled3 other = new(vector.X, vector.Y, vector.Z);

        var expected = Target(other);

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
