namespace SharpMeasures.Tests.Unhandled4Cases;

using Xunit;

public class Plus
{
    private static Unhandled4 Target(Unhandled4 vector) => vector.Plus();

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4))]
    public void EqualOriginal(Unhandled4 vector)
    {
        var actual = Target(vector);

        Assert.Equal(vector, actual);
    }
}
