namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class Operator_Cast_FromScalar
{
    private static Unhandled Target(Scalar magnitude) => (Unhandled)magnitude;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandledScalar))]
    public void Valid_MatchFromScalar(Scalar magnitude)
    {
        var expected = Unhandled.FromScalar(magnitude);

        var actual = Target(magnitude);

        Assert.Equal(expected, actual);
    }
}
