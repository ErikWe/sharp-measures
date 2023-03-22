namespace SharpMeasures.Tests.Unhandled3Cases;

using Xunit;

public class AsIVector3Quantity_Z
{
    private static Scalar Target(Unhandled3 vector)
    {
        return execute(vector);

        static Scalar execute(IVector3Quantity vector) => vector.Z;
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3))]
    public void MatchZMagnitude(Unhandled3 vector)
    {
        var expected = vector.Z.Magnitude;

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
