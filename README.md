# SharpMeasures [![NuGet version (SharpMeasures)](https://img.shields.io/nuget/v/SharpMeasures.svg?style=plastic)](https://www.nuget.org/packages/SharpMeasures/)

SharpMeasures is a C# tool that aims to simplify proper usage of physical quantities and units of measurement during development. This is done by providing concrete types, such as `Time`, `UnitOfLength`, and `Acceleration3`. Common matematical operations are also implemented, allowing quantities to be derived from other quantities - as demonstrated below.

```csharp
Displacement3 displacement = (0, 1.5, -4) * Length.OneMetre;
Time time = 0.5 * Time.OneSecond;

Velocity3 velocity = displacement / time;

Console.WriteLine(velocity); // "(0, 3, -8) [m/s]"
Console.WriteLine(velocity.InUnit(UnitOfSpeed.KilometresPerHour)); // "(0, 10.8, -28.8)"
```

### Extendability

SharpMeasures is implemented entirely using a source generator, which means that it's trivial to extend the existing set of quantities and units. It is especially simple to construct specialized forms of existing quantities - for example a quantity `Altitude` as a specialized form of `Length`. Using specialized types helps make your code even more expressive and accurate. Read more below.

### Functionality

SharpMeasures consists of two main components: quantities and units. A quantity represents a scalar magnitude (or multiple, for vectors), and is not expressed in any particular unit - rather, they are always expressed in a default unit (normally SI). A specific unit is only applied when constructing the quantity, or when accessing the magnitude of the quantity. Dimensional analysis is not performed when evaluating mathematical expressions, which means that mathematical operations require all involved quantities (even intermediate) to be explicitly implemented in SharpMeasures.

## Source Generator [![NuGet version (SharpMeasures.Generators)](https://img.shields.io/nuget/v/SharpMeasures.Generators.svg?style=plastic)](https://www.nuget.org/packages/SharpMeasures.Generators/)

All quantities and units of SharpMeasures are generated by a custom source generator. This means that it's trivial to extend the existing set of quantities and units, or to make your own set from scratch.

### Specialized Types

The goal is for `SharpMeasures` to include the most common quantites, and to be sufficient for most cases. However, you might prefer a method signature to consist of the more explicit `Altitude` rather than an ambiguous `Length`. The source generator can accomplish this, as shown below:

```csharp
using SharpMeasures.Generators.Scalars;

[SpecializedSharpMeasuresScalar(typeof(Length))] // <- note the [Specialized...] attribute
public partial class Altitude { }
```

This means that `Altitude` inherits all properties from `Length`, such as units, derivations, and constants. (Note: `Altitude` does not inherit from `Length` in the regular ´polymorphic´ sense, so this also works for structs.)

### New Types

The source generator can also be used to generate quantities and units ´from scratch´:

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

The declarations shown above would make the following code valid:

```csharp
Time time = 78 * Time.OneMinute;
Frequency frequency = 1 / time;

Console.WriteLine(frequency); // "0.769230769230769"
```

## Similar Projects

   - [QuantityTypes](https://github.com/QuantityTypes/QuantityTypes)
   - [UnitsNet](https://github.com/angularsen/UnitsNet)
