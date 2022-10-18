namespace SharpMeasures.Tests.UnitConversions;

using SharpMeasures;

using System.Collections.Generic;

using Xunit;

public class TemperatureConversions
{
    [Theory]
    [MemberData(nameof(EquivalentTemperatures))]
    public void FromKelvin(double kelvin, double celsius, double rankine, double fahrenheit)
    {
        var temperature = new Temperature(kelvin, UnitOfTemperature.Kelvin);

        Assert.Equal(kelvin, temperature.Kelvin, 10);
        Assert.Equal(celsius, temperature.Celsius, 10);
        Assert.Equal(rankine, temperature.Rankine, 10);
        Assert.Equal(fahrenheit, temperature.Fahrenheit, 10);
    }

    [Theory]
    [MemberData(nameof(EquivalentTemperatures))]
    public void FromCelsius(double kelvin, double celsius, double rankine, double fahrenheit)
    {
        var temperature = new Temperature(celsius, UnitOfTemperature.Celsius);

        Assert.Equal(kelvin, temperature.Kelvin, 10);
        Assert.Equal(celsius, temperature.Celsius, 10);
        Assert.Equal(rankine, temperature.Rankine, 10);
        Assert.Equal(fahrenheit, temperature.Fahrenheit, 10);
    }

    [Theory]
    [MemberData(nameof(EquivalentTemperatures))]
    public void FromFahrenheit(double kelvin, double celsius, double rankine, double fahrenheit)
    {
        var temperature = new Temperature(fahrenheit, UnitOfTemperature.Fahrenheit);

        Assert.Equal(kelvin, temperature.Kelvin, 10);
        Assert.Equal(celsius, temperature.Celsius, 10);
        Assert.Equal(rankine, temperature.Rankine, 10);
        Assert.Equal(fahrenheit, temperature.Fahrenheit, 10);
    }

    public static IEnumerable<object[]> EquivalentTemperatures() => new object[][]
    {
        new object[] { 0, -273.15, 0, -459.67 },
        new object[] { 273.15, 0, 491.67, 32 },
        new object[] { 373.45, 100.3, 672.21, 212.54 },
        new object[] { -10, -283.15, -18, -477.67 }
    };
}
