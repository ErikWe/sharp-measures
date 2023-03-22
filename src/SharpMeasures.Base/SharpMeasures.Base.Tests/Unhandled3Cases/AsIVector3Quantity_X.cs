namespace SharpMeasures.Tests.Unhandled3Cases;

using Xunit;

public class AsIVector3Quantity_X
{
    private static Scalar Target(Unhandled3 vector)
    {
        return execute(vector);

        static Scalar execute(IVector3Quantity vector) => vector.X;
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3))]
    public void MatchXMagnitude(Unhandled3 vector)
    {
        var expected = vector.X.Magnitude;

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
