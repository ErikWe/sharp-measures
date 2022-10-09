namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfLength), Vector = typeof(LengthN), DefaultUnit = "Metre", DefaultSymbol = "m")]
public readonly partial record struct Length { }

[QuantityOperation(typeof(Volume), typeof(Area), OperatorType.Multiplication)]
[QuantityOperation(typeof(Volume), typeof(ArealFrequency), OperatorType.Division)]
[QuantityOperation(typeof(TimeSquared), typeof(Acceleration), OperatorType.Division)]
[QuantityOperation(typeof(Time), typeof(Pace), OperatorType.Multiplication)]
[QuantityOperation(typeof(Time), typeof(Speed), OperatorType.Division)]
[QuantityOperation(typeof(TemperatureDifference), typeof(TemperatureGradient), OperatorType.Multiplication)]
[QuantityOperation(typeof(Speed), typeof(Frequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(Speed), typeof(Time), OperatorType.Division)]
[QuantityOperation(typeof(SpecificVolume), typeof(ArealDensity), OperatorType.Division)]
[QuantityOperation(typeof(SpatialFrequency), typeof(ArealFrequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(SpatialFrequency), typeof(Area), OperatorType.Division)]
[QuantityOperation(typeof(SpatialFrequency), typeof(Scalar), OperatorType.Division, OperatorPosition.Right)]
[QuantityOperation(typeof(Scalar), typeof(SpatialFrequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(Mass), typeof(LinearDensity), OperatorType.Multiplication)]
[QuantityOperation(typeof(LinearDensity), typeof(ArealDensity), OperatorType.Multiplication)]
[QuantityOperation(typeof(Frequency), typeof(Absement), OperatorType.Division)]
[QuantityOperation(typeof(Energy), typeof(Force), OperatorType.Multiplication)]
[QuantityOperation(typeof(ArealFrequency), typeof(VolumetricFrequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(ArealFrequency), typeof(Volume), OperatorType.Division)]
[QuantityOperation(typeof(ArealDensity), typeof(Density), OperatorType.Multiplication)]
[QuantityOperation(typeof(ArealDensity), typeof(SpecificVolume), OperatorType.Division)]
[QuantityOperation(typeof(Area), typeof(Length), OperatorType.Multiplication)]
[QuantityOperation(typeof(Area), typeof(SpatialFrequency), OperatorType.Division)]
[QuantityOperation(typeof(Acceleration), typeof(FrequencyDrift), OperatorType.Multiplication)]
[QuantityOperation(typeof(Acceleration), typeof(TimeSquared), OperatorType.Division)]
[QuantityOperation(typeof(Absement), typeof(Time), OperatorType.Multiplication)]
[QuantityOperation(typeof(Absement), typeof(Frequency), OperatorType.Division)]
public readonly partial record struct Length { }

[QuantityProcess("Cube", typeof(Volume), "new(Magnitude * Magnitude * Magnitude)")]
[QuantityProcess("Square", typeof(Area), "new(Magnitude * Magnitude)")]
public readonly partial record struct Length { }
