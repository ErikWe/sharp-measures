# SharpMeasures [![NuGet version (SharpMeasures)](https://img.shields.io/nuget/v/SharpMeasures.svg?style=plastic)](https://www.nuget.org/packages/SharpMeasures/)

SharpMeasures is a C# tool that aims to simplify proper usage of physical quantities and units of measurement during development. This is done by providing concrete types, such as `Time`, `UnitOfLength`, and `Acceleration3`. Common appropriate matematical operations are also implemented, allowing quantities to easily be derived from other quantities.

```csharp
Displacement3 displacement = new Displacement3(0, 1.5, -4, UnitOfLength.Metre);
Time time = 0.5 * UnitOfTime.Second.Time;

Velocity3 velocity = displacement / time;

Console.WriteLine(velocity); // "(0, 3, -8) [m/s]"
Console.WriteLine(velocity.InUnit(UnitOfSpeed.KilometresPerHour)); // "(0, 10.8, -28.8)"
```

## Source Generator [![NuGet version (SharpMeasures.Generators)](https://img.shields.io/nuget/v/SharpMeasures.Generators.svg?style=plastic)](https://www.nuget.org/packages/SharpMeasures.Generators/)

SharpMeasures is implemented as a source generator, which means that it's trivial to make your own set of quantities and units from scratch. Make sure to also add [SharpMeasures.Base](https://www.nuget.org/packages/SharpMeasures.Base/) to the project.

```csharp
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[SharpMeasuresScalar(typeof(UnitOfLength))]
public partial class Length { }

[PrefixedUnitInstance("Kilometre", "[*]s", "Metre", MetricPrefixName.Kilo)]
[FixedUnitInstance("Metre", "[*]s")]
[SharpMeasuresUnit(typeof(Length))]
public partial class UnitOfLength { }
```

## Similar Projects

   - [UnitsNet](https://github.com/angularsen/UnitsNet)
