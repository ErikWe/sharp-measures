# SharpMeasures [![NuGet version (SharpMeasures)](https://img.shields.io/nuget/v/SharpMeasures.svg?style=plastic)](https://www.nuget.org/packages/SharpMeasures/)

SharpMeasures is a C# tool that aims to simplify proper usage of physical quantities and units of measurement during development. This is done by providing concrete types, such as `Time`, `UnitOfLength`, and `Acceleration3`. Some common matematical operations are also implemented, allowing quantities to easily be derived from other quantities.

```csharp
Displacement3 displacement = (0, 1.5, -4) * Length.OneMetre;
Time time = 0.5 * Time.OneSecond;

Velocity3 velocity = displacement / time;

Console.WriteLine(velocity); // "(0, 3, -8) [m/s]"
Console.WriteLine(velocity.InUnit(UnitOfSpeed.KilometresPerHour)); // "(0, 10.8, -28.8)"
```

## Source Generator [![NuGet version (SharpMeasures.Generators)](https://img.shields.io/nuget/v/SharpMeasures.Generators.svg?style=plastic)](https://www.nuget.org/packages/SharpMeasures.Generators/)

SharpMeasures is implemented as a source generator, which means that it's trivial to extend the existing set of types, or to make your own set from scratch.

```csharp
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[SharpMeasuresScalar(typeof(UnitOfTime))]
public partial class Time { }

[DerivedQuantity("1 / {0}", typeof(Time))]
[SharpMeasuresScalar(typeof(UnitOfFrequency))]
public partial class Frequency { }

[FixedUnitInstance("Second", "[*]s")]
[ScaledUnitInstance("Minute", "[*]s", "Second", 60)]
[SharpMeasuresUnit(typeof(Time))]
public partial class UnitOfTime { }

[DerivedUnitInstance("Hertz", "[*]", new[] { "Second" })]
[DerivableUnit("1 / {0}", typeof(UnitOfTime))]
[SharpMeasuresUnit(typeof(Frequency))]
public partial class UnitOfFrequency { }
```

## Similar Projects

   - [QuantityTypes](https://github.com/QuantityTypes/QuantityTypes)
   - [UnitsNet](https://github.com/angularsen/UnitsNet)
