namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class FromScalar
{
    private static Unhandled Target(Scalar magnitude) => Unhandled.FromScalar(magnitude);

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandledScalar))]
    public void Valid_MatchConstructor(Scalar magnitude)
    {
        Unhandled expected = new(magnitude);

        var actual = Target(magnitude);

        Assert.Equal(expected, actual);
    }
}
