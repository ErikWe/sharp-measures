namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.TemperatureTests;

using ErikWe.SharpMeasures.Units;

using System.Collections.Generic;

using Xunit;

public class CustomExamples
{
    [Theory]
    [MemberData(nameof(EquivalentUnitsOfTemperature))]
    public void ExamplesShouldBeEquivalent(UnitOfTemperature expected, UnitOfTemperature actual)
    {
        Assert.Equal(expected.TemperatureDifference.Magnitude, actual.TemperatureDifference.Magnitude, 2);
        Assert.Equal(expected.Offset, actual.Offset, 2);
    }

    public static IEnumerable<object[]> EquivalentUnitsOfTemperature()
    {
        yield return new object[] { UnitOfTemperature.Kelvin, UnitOfTemperature.Kelvin.OffsetBy(-10).OffsetBy(10) };
        yield return new object[] { UnitOfTemperature.Kelvin, UnitOfTemperature.Kelvin.ScaledBy(10).ScaledBy(0.1) };
        yield return new object[] { UnitOfTemperature.Kelvin, UnitOfTemperature.Kelvin.OffsetBy(-10).ScaledBy(10).ScaledBy(0.1).OffsetBy(10) };
        yield return new object[] { UnitOfTemperature.Kelvin, UnitOfTemperature.Kelvin.ScaledBy(10).OffsetBy(-10).OffsetBy(10).ScaledBy(0.1) };

        yield return new object[] { UnitOfTemperature.Celsius, UnitOfTemperature.Kelvin.OffsetBy(-273.15) };
        yield return new object[] { UnitOfTemperature.Rankine, UnitOfTemperature.Kelvin.ScaledBy(5d / 9) };
        yield return new object[] { UnitOfTemperature.Fahrenheit, UnitOfTemperature.Celsius.ScaledBy(5d / 9).OffsetBy(32) };
        yield return new object[] { UnitOfTemperature.Rankine, UnitOfTemperature.Celsius.ScaledBy(5d / 9).OffsetBy(491.67) };
    }
}
