namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class AsIScalarQuantity_Magnitude
{
    private static Scalar Target(Unhandled unhandled)
    {
        return execute(unhandled);

        static Scalar execute(IScalarQuantity quantity) => quantity.Magnitude;
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled))]
    public void MatchMagnitude(Unhandled unhandled)
    {
        var expected = unhandled.Magnitude;

        var actual = Target(unhandled);

        Assert.Equal(expected, actual);
    }
}
