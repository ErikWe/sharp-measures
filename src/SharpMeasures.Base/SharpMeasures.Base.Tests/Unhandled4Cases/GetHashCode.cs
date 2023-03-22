namespace SharpMeasures.Tests.Unhandled4Cases;

using Xunit;

public class GetHashCode
{
    private static int Target(Unhandled4 vector) => vector.GetHashCode();

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4))]
    public void Equal_Match(Unhandled4 vector)
    {
        Unhandled4 other = new(vector.X, vector.Y, vector.Z, vector.W);

        var expected = Target(other);

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
