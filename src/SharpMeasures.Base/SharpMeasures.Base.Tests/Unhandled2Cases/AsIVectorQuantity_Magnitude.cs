namespace SharpMeasures.Tests.Unhandled2Cases;

using Xunit;

public class AsIVectorQuantity_Magnitude
{
    private static Scalar Target(Unhandled2 vector)
    {
        return execute(vector);

        static Scalar execute(IVectorQuantity vector) => vector.Magnitude();
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2))]
    public void MatchPureMagnitude(Unhandled2 vector)
    {
        var expected = vector.PureMagnitude();

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
