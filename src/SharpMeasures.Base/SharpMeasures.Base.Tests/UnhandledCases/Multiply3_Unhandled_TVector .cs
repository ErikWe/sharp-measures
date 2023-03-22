﻿namespace SharpMeasures.Tests.UnhandledCases;

using System;

using Xunit;

public class Multiply3_Unhandled_TVector
{
    private static Unhandled3 Target<TVector>(Unhandled a, TVector b) where TVector : IVector3Quantity<TVector> => Unhandled.Multiply3(a, b);

    [Fact]
    public void Null_ArgumentNullException()
    {
        var a = Datasets.GetValidUnhandled();

        var b = Datasets.GetNullVector3Quantity();

        AnyError_TException<ArgumentNullException, ReferenceVector3Quantity>(a, b);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled_ValidVector3))]
    public void MatchInstanceMethod(Unhandled a, Vector3 b)
    {
        var expected = a.Multiply(b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException, TVector>(Unhandled a, TVector b) where TException : Exception where TVector : IVector3Quantity<TVector>
    {
        var exception = Record.Exception(() => Target(a, b));

        Assert.IsType<TException>(exception);
    }
}
