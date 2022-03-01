namespace ErikWe.SharpMeasures.Tests.Utility;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public static class AssertExtra
{
    public static void AssertEqualMagnitudes(IScalarQuantity expected, IScalarQuantity actual)
    {
        Assert.Equal(Rounding.ToSignificantDigits(expected.Magnitude, 5), Rounding.ToSignificantDigits(actual.Magnitude, 5), 2);
    }

    public static void AssertEqualMagnitudes<T>(IScalarQuantity expected, IEnumerable<T> actuals)
        where T : IScalarQuantity
    {
        foreach (IScalarQuantity actual in actuals)
        {
            AssertEqualMagnitudes(expected, actual);
        }
    }

    public static void AssertEqualComponents((double x, double y, double z) expected, (double x, double y, double z) actual)
    {
        Assert.Equal(Rounding.ToSignificantDigits(expected.x, 5), Rounding.ToSignificantDigits(actual.x, 5), 2);
        Assert.Equal(Rounding.ToSignificantDigits(expected.y, 5), Rounding.ToSignificantDigits(actual.y, 5), 2);
        Assert.Equal(Rounding.ToSignificantDigits(expected.z, 5), Rounding.ToSignificantDigits(actual.z, 5), 2);
    }

    public static void AssertEqualComponents(IVector3Quantity expected, (double x, double y, double z) actual)
        => AssertEqualComponents((expected.X, expected.Y, expected.Z), actual);
    public static void AssertEqualComponents((double x, double y, double z) expected, IVector3Quantity actual)
        => AssertEqualComponents(expected, (actual.X, actual.Y, actual.Z));
    public static void AssertEqualComponents(IVector3Quantity expected, IVector3Quantity actual)
        => AssertEqualComponents((expected.X, expected.Y, expected.Z), (actual.X, actual.Y, actual.Z));
    public static void AssertEqualComponents(Vector3 expected, (double x, double y, double z) actual) => AssertEqualComponents(expected.ToValueTuple(), actual);
    public static void AssertEqualComponents((double x, double y, double z) expected, Vector3 actual) => AssertEqualComponents(expected, actual.ToValueTuple());
    public static void AssertEqualComponents(Vector3 expected, IVector3Quantity actual) => AssertEqualComponents(expected.ToValueTuple(), actual);
    public static void AssertEqualComponents(IVector3Quantity expected, Vector3 actual) => AssertEqualComponents(expected, actual.ToValueTuple());
    public static void AssertEqualComponents(Vector3 expected, Vector3 actual) => AssertEqualComponents(expected.ToValueTuple(), actual.ToValueTuple());

    public static void AssertEqualComponents((double x, double y, double z) expected, IEnumerable<(double x, double y, double z)> actuals)
    {
        foreach ((double x, double y, double z) actual in actuals)
        {
            AssertEqualComponents(expected, actual);
        }
    }

    public static void AssertEqualComponents(IVector3Quantity expected, IEnumerable<(double x, double y, double z)> actuals)
    {
        foreach ((double x, double y, double z) actual in actuals)
        {
            AssertEqualComponents(expected, actual);
        }
    }

    public static void AssertEqualComponents<T>((double x, double y, double z) expected, IEnumerable<T> actuals)
        where T : IVector3Quantity
    {
        foreach (IVector3Quantity actual in actuals)
        {
            AssertEqualComponents(expected, actual);
        }
    }

    public static void AssertEqualComponents<T>(IVector3Quantity expected, IEnumerable<T> actuals)
        where T : IVector3Quantity
    {
        foreach (IVector3Quantity actual in actuals)
        {
            AssertEqualComponents(expected, actual);
        }
    }

    public static void AssertEqualComponents(IVector3Quantity expected, IEnumerable<Vector3> actuals)
    {
        foreach (Vector3 actual in actuals)
        {
            AssertEqualComponents(expected, actual);
        }
    }

    public static void AssertEqualComponents(Vector3 expected, IEnumerable<(double x, double y, double z)> actuals)
    {
        foreach ((double x, double y, double z) actual in actuals)
        {
            AssertEqualComponents(expected, actual);
        }
    }

    public static void AssertEqualComponents((double x, double y, double z) expected, IEnumerable<Vector3> actuals)
    {
        foreach (Vector3 actual in actuals)
        {
            AssertEqualComponents(expected, actual);
        }
    }

    public static void AssertEqualComponents<T>(Vector3 expected, IEnumerable<T> actuals)
        where T : IVector3Quantity
    {
        foreach (IVector3Quantity actual in actuals)
        {
            AssertEqualComponents(expected, actual);
        }
    }

    public static void AssertEqualComponents(Vector3 expected, IEnumerable<Vector3> actuals)
    {
        foreach (Vector3 actual in actuals)
        {
            AssertEqualComponents(expected, actual);
        }
    }
}