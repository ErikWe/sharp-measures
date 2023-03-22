namespace SharpMeasures.Tests.Unhandled4Cases;

using Xunit;

public class AsIVectorQuantity_Magnitude
{
    private static Scalar Target(Unhandled4 vector)
    {
        return execute(vector);

        static Scalar execute(IVectorQuantity vector) => vector.Magnitude();
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4))]
    public void MatchPureMagnitude(Unhandled4 vector)
    {
        var expected = vector.PureMagnitude();

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
