namespace SharpMeasures;

using SharpMeasures.Generators.Units;

[FixedUnitInstance("Kelvin", "[*]")]
[BiasedUnitInstance("Celsius", "[*]", "Kelvin", -273.15)]
[ScaledUnitInstance("Rankine", "[*]", "Kelvin", 5d / 9)]
[BiasedUnitInstance("Fahrenheit", "[*]", "Rankine", -459.67)]
[SharpMeasuresUnit(typeof(TemperatureDifference), BiasTerm = true)]
public readonly partial record struct UnitOfTemperature { }
