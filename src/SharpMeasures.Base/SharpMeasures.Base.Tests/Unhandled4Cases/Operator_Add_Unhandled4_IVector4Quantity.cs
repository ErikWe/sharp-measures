﻿namespace SharpMeasures.Tests.Unhandled4Cases;

using System;

using Xunit;

public class Operator_Add_Unhandled4_IVector4Quantity
{
    private static Unhandled4 Target(Unhandled4 a, IVector4Quantity b) => a + b;

    [Fact]
    public void Null_ArgumentNullException()
    {
        var a = Datasets.GetValidUnhandled4();
        var b = Datasets.GetNullVector4Quantity();

        AnyError_TException<ArgumentNullException>(a, b);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4_ValidUnhandled4))]
    public void Valid_MatchMethod(Unhandled4 a, Unhandled4 b)
    {
        var expected = Unhandled4.Add(a, b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException>(Unhandled4 a, IVector4Quantity b) where TException : Exception
    {
        var exception = Record.Exception(() => Target(a, b));

        Assert.IsType<TException>(exception);
    }
}
