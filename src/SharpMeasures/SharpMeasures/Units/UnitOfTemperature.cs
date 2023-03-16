namespace SharpMeasures;

using SharpMeasures.Generators;

[FixedUnitInstance("Kelvin", "[*]")]
[BiasedUnitInstance("Celsius", "[*]", "Kelvin", -273.15)]
[ScaledUnitInstance("Rankine", "[*]", "Kelvin", 5d / 9)]
[BiasedUnitInstance("Fahrenheit", "[*]", "Rankine", -459.67)]
[Unit(typeof(TemperatureDifference), BiasTerm = true)]
public readonly partial record struct UnitOfTemperature { }
