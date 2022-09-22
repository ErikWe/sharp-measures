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

SharpMeasures is implemented entirely using a source generator, which means that it's trivial to extend the existing set of quantities and units. It is especially simple to construct specialized forms of existing quantities - for example a quantity `Altitude` as a specialized form of `Length`. Using such specialized types helps make your code even more expressive and accurate, with minimal effort. Read more further down.

### Functionality

SharpMeasures consists of two main components: quantities and units. A quantity represents a scalar magnitude (or multiple, for vectors), and is not expressed in any particular unit - rather, they are always expressed in a default unit (typically SI units). A specific unit is only applied when constructing a quantity, or when accessing the magnitude of a quantity. Dimensional analysis is not performed when evaluating mathematical expressions, which means that mathematical operations require all involved quantities (even intermediate) to be explicitly implemented in SharpMeasures.

## Source Generator [![NuGet version (SharpMeasures.Generators)](https://img.shields.io/nuget/v/SharpMeasures.Generators.svg?style=plastic)](https://www.nuget.org/packages/SharpMeasures.Generators/)

All quantities and units of SharpMeasures are generated by a custom source generator. This means that it's trivial to extend the existing set of quantities and units, or to make your own set entirely from scratch.

### Specialized Types

The goal is for SharpMeasures to include the most common quantites, and to be sufficient for most cases. However, you might prefer your method signature to consist of the more explicit `Altitude` rather than an ambiguous `Length`. This can easily be accomplished using the source generator:

```csharp
using SharpMeasures.Generators.Scalars;

[SpecializedSharpMeasuresScalar(typeof(Length))] // <-- note the [Specialized...] attribute
public partial class Altitude { }
```

This means that `Altitude` will borrow the definition from `Length` - including units, derivations, and constants. Cast operators ensure that `Altitude` can implicitly be used in place of `Length` - while the reverse action would require an explicit cast.

### New Types

The source generator can also be used to generate entirely new quantities and units:

```csharp
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[DerivedQuantity("1 / {0}", typeof(Frequency))]
[SharpMeasuresScalar(typeof(UnitOfTime))] // <-- [Specialized...] attributes are no longer used
public partial class Time { }

[DerivedQuantity("1 / {0}", typeof(Time))]
[SharpMeasuresScalar(typeof(UnitOfFrequency), DefaultUnitInstanceName = "Hertz", DefaultUnitInstanceSymbol = "Hz")]
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

The above declarations would make the following code valid:

```csharp
Time time = 78 * Time.OneMinute;
Frequency frequency = 1 / time;

Console.WriteLine(frequency); // "0.769230769230769 [Hz]"
```

### Diagnostics

The source generator include a large set of diagnostics message, which helps make sure that the tool is used correctly:

<p align="center">
  <img src="https://user-images.githubusercontent.com/19408310/191651853-9f29e901-955f-437a-be21-12c371fafc25.png" />
</p>

## Similar Projects


   - [QuantityTypes](https://github.com/QuantityTypes/QuantityTypes)
   - [UnitsNet](https://github.com/angularsen/UnitsNet)
