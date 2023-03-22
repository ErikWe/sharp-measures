namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class Constructor
{
    private static Unhandled Target(Scalar magnitude) => new(magnitude);

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandledScalar))]
    public void Valid_MatchMagnitude(Scalar magnitude)
    {
        var actual = Target(magnitude).Magnitude;

        Assert.Equal(magnitude, actual);
    }
}
