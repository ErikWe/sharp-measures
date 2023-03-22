namespace SharpMeasures.Tests.Unhandled2Cases;

using Xunit;

public class Plus
{
    private static Unhandled2 Target(Unhandled2 vector) => vector.Plus();

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2))]
    public void EqualOriginal(Unhandled2 vector)
    {
        var actual = Target(vector);

        Assert.Equal(vector, actual);
    }
}
