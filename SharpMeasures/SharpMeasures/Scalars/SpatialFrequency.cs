namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfSpatialFrequency), DefaultUnit = "PerMetre", DefaultSymbol = "m⁻¹")]
public readonly partial record struct SpatialFrequency { }

[QuantityOperation(typeof(VolumetricFrequency), typeof(ArealFrequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(VolumetricFrequency), typeof(Area), OperatorType.Division)]
[QuantityOperation(typeof(Time), typeof(Absement), OperatorType.Multiplication)]
[QuantityOperation(typeof(TemperatureGradient), typeof(TemperatureDifference), OperatorType.Multiplication)]
[QuantityOperation(typeof(Scalar), typeof(Length), OperatorType.Multiplication)]
[QuantityOperation(typeof(Pace), typeof(Time), OperatorType.Multiplication)]
[QuantityOperation(typeof(Pace), typeof(Frequency), OperatorType.Division)]
[QuantityOperation(typeof(LinearDensity), typeof(Mass), OperatorType.Multiplication)]
[QuantityOperation(typeof(Length), typeof(Area), OperatorType.Multiplication)]
[QuantityOperation(typeof(Length), typeof(Scalar), OperatorType.Division, OperatorPosition.Right)]
[QuantityOperation(typeof(Length), typeof(ArealFrequency), OperatorType.Division)]
[QuantityOperation(typeof(FrequencyDrift), typeof(Acceleration), OperatorType.Multiplication)]
[QuantityOperation(typeof(Frequency), typeof(Speed), OperatorType.Multiplication)]
[QuantityOperation(typeof(Frequency), typeof(Pace), OperatorType.Division)]
[QuantityOperation(typeof(Force), typeof(Energy), OperatorType.Multiplication)]
[QuantityOperation(typeof(Force), typeof(Torque), OperatorType.Multiplication)]
[QuantityOperation(typeof(Force), typeof(Work), OperatorType.Multiplication)]
[QuantityOperation(typeof(Density), typeof(ArealDensity), OperatorType.Multiplication)]
[QuantityOperation(typeof(ArealFrequency), typeof(SpatialFrequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(ArealFrequency), typeof(Length), OperatorType.Division)]
[QuantityOperation(typeof(ArealDensity), typeof(LinearDensity), OperatorType.Multiplication)]
[QuantityOperation(typeof(Area), typeof(Volume), OperatorType.Multiplication)]
[QuantityOperation(typeof(Area), typeof(VolumetricFrequency), OperatorType.Division)]
public readonly partial record struct SpatialFrequency { }

[QuantityProcess("Cube", typeof(VolumetricFrequency), "new(Magnitude * Magnitude * Magnitude)")]
[QuantityProcess("Square", typeof(ArealFrequency), "new(Magnitude * Magnitude)")]
public readonly partial record struct SpatialFrequency { }
