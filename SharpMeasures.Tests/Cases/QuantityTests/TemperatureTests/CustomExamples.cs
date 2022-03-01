namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.TemperatureTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Units;

using System.Collections.Generic;

using Xunit;

public class CustomExamples
{
    [Theory]
    [MemberData(nameof(EquivalentTemperatures))]
    public void TemperaturesShouldBeEquivalent(Temperature expected, Temperature actual)
    {
        Assert.Equal(expected.Magnitude, actual.Magnitude, 2);
    }

    public static IEnumerable<object[]> EquivalentTemperatures()
    {
        yield return new object[] { Temperature.OneKelvin, new Temperature(1, UnitOfTemperature.Kelvin) };
        yield return new object[] { Temperature.OneKelvin, new Temperature(-272.15, UnitOfTemperature.Celsius) };
        yield return new object[] { Temperature.OneKelvin, new Temperature(-457.87, UnitOfTemperature.Fahrenheit) };
        yield return new object[] { Temperature.OneKelvin, new Temperature(1.8, UnitOfTemperature.Rankine) };

        UnitOfTemperature kiloCelsius = UnitOfTemperature.Celsius.WithPrefix(MetricPrefix.Kilo);

        yield return new object[] { new Temperature(0, UnitOfTemperature.Celsius), new Temperature(0, kiloCelsius) };
        yield return new object[] { new Temperature(1376, UnitOfTemperature.Celsius), new Temperature(1.376, kiloCelsius) };
        yield return new object[] { 1649.15 * Temperature.OneKelvin, new Temperature(1.376, kiloCelsius) };
        yield return new object[] { new Temperature(2508.8, UnitOfTemperature.Fahrenheit), new Temperature(1.376, kiloCelsius) };

        UnitOfTemperature offsetCelsius = UnitOfTemperature.Celsius.OffsetBy(1);

        yield return new object[] { new Temperature(0, UnitOfTemperature.Celsius), new Temperature(1, offsetCelsius) };
        yield return new object[] { new Temperature(2, UnitOfTemperature.Celsius), new Temperature(3, offsetCelsius) };

        UnitOfTemperature scaledCelcius = UnitOfTemperature.Celsius.ScaledBy(2);

        yield return new object[] { new Temperature(0, UnitOfTemperature.Celsius), new Temperature(0, scaledCelcius) };
        yield return new object[] { new Temperature(6, UnitOfTemperature.Celsius), new Temperature(3, scaledCelcius) };

        UnitOfTemperature offsetScaledCelcius = UnitOfTemperature.Celsius.OffsetBy(1).ScaledBy(2);

        yield return new object[] { new Temperature(-1, UnitOfTemperature.Celsius), new Temperature(0, offsetScaledCelcius) };
        yield return new object[] { new Temperature(5, UnitOfTemperature.Celsius), new Temperature(3, offsetScaledCelcius) };

        UnitOfTemperature scaledOffsetCelcius = UnitOfTemperature.Celsius.ScaledBy(2).OffsetBy(1);

        yield return new object[] { new Temperature(-2, UnitOfTemperature.Celsius), new Temperature(0, scaledOffsetCelcius) };
        yield return new object[] { new Temperature(4, UnitOfTemperature.Celsius), new Temperature(3, scaledOffsetCelcius) };

        UnitOfTemperature fahrenheit = UnitOfTemperature.Celsius.ScaledBy(5d / 9).OffsetBy(32);

        yield return new object[] { new Temperature(0, UnitOfTemperature.Fahrenheit), new Temperature(0, fahrenheit) };
        yield return new object[] { new Temperature(3, UnitOfTemperature.Fahrenheit), new Temperature(3, fahrenheit) };
    }
}
