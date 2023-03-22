namespace SharpMeasures.Tests.Unhandled3Cases;

using Xunit;

public class AsIVectorQuantity_Magnitude
{
    private static Scalar Target(Unhandled3 vector)
    {
        return execute(vector);

        static Scalar execute(IVectorQuantity vector) => vector.Magnitude();
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3))]
    public void MatchPureMagnitude(Unhandled3 vector)
    {
        var expected = vector.PureMagnitude();

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
