namespace SharpMeasures.Tests.Unhandled3Cases;

using Xunit;

public class Plus
{
    private static Unhandled3 Target(Unhandled3 vector) => vector.Plus();

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3))]
    public void EqualOriginal(Unhandled3 vector)
    {
        var actual = Target(vector);

        Assert.Equal(vector, actual);
    }
}
