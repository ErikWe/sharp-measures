namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class AsIScalarQuantity_WithMagnitude_Scalar
{
    private static Unhandled Target(Scalar magnitude)
    {
        return execute<Unhandled>(magnitude);

        static T execute<T>(Scalar magnitude) where T : IScalarQuantity<T> => T.WithMagnitude(magnitude);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandledScalar))]
    public void MatchConstructor(Scalar magnitude)
    {
        Unhandled expected = new(magnitude);

        var actual = Target(magnitude);

        Assert.Equal(expected, actual);
    }
}
