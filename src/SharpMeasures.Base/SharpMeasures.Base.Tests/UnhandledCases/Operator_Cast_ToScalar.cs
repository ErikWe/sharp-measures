namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class Operator_Cast_ToScalar
{
    private static Scalar Target(Unhandled unhandled) => (Scalar)unhandled;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled))]
    public void Valid_MatchToScalar(Unhandled unhandled)
    {
        var expected = unhandled.ToScalar();

        var actual = Target(unhandled);

        Assert.Equal(expected, actual);
    }
}
