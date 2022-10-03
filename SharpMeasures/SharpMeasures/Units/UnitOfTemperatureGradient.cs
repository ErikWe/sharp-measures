namespace SharpMeasures;

using SharpMeasures.Generators;

[DerivedUnitInstance("KelvinPerMetre", "[*]", new[] { "Kelvin", "Metre" })]
[DerivedUnitInstance("CelsiusPerMetre", "[*]", new[] { "Celsius", "Metre" })]
[DerivedUnitInstance("RankinePerMetre", "[*]", new[] { "Rankine", "Metre" })]
[DerivedUnitInstance("FahrenheitPerMetre", "[*]", new[] { "Fahrenheit", "Metre" })]
[DerivableUnit("{0} / {1}", typeof(UnitOfTemperature), typeof(UnitOfLength))]
[Unit(typeof(TemperatureGradient))]
public readonly partial record struct UnitOfTemperatureGradient { }
