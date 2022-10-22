# SharpMeasures [![NuGet version (SharpMeasures)](https://img.shields.io/nuget/v/SharpMeasures.svg?style=plastic)](https://www.nuget.org/packages/SharpMeasures/) ![GitHub](https://img.shields.io/github/license/ErikWe/sharp-measures?style=plastic)

SharpMeasures is an easily extended C# tool that aims to encourage explicit usage of physical quantities and units of measurement during development. This is done by providing concrete types, such as `Time`, `UnitOfLength`, and `Acceleration3` - together with a custom-built source generator. While SharpMeasures is mainly designed as a treatment for *primitive obsession*, common matematical operations are also implemented, allowing quantities to be derived from other quantities - as demonstrated below.

```csharp
Displacement3 displacement = (0, 1.5, -4) * Distance.OneMetre;
Time time = 0.5 * Time.OneSecond;

Velocity3 velocity = displacement / time;

Console.WriteLine(velocity); // "(0, 3, -8) [m∙s⁻¹]"
Console.WriteLine(velocity.KilometresPerHour); // ~ "(0, 10.8, -28.8)"
```

### Extendability

SharpMeasures is implemented entirely using a source generator, which means that it's trivial to extend the existing set of quantities and units. It is especially simple to construct specialized forms of existing quantities - for example a quantity `Altitude` as a specialized form of `Height`. Read more about the generator [here](SharpMeasures.Generators/Documentation/README.md).

Some extensions can be found here:

- [SharpMeasures.Astronomy](https://www.nuget.org/packages/SharpMeasures.Astronomy/) [![NuGet version (SharpMeasures.Generators)](https://img.shields.io/nuget/v/SharpMeasures.Astronomy.svg?style=plastic)](https://www.nuget.org/packages/SharpMeasures.Astronomy/) ![GitHub](https://img.shields.io/github/license/ErikWe/sharp-measures?style=plastic)

### Limitations

Dimensional analysis is not performed when evaluating mathematical expressions, which means that mathematical operations require all involved quantities (even intermediate) to be explicitly implemented in SharpMeasures. If this is not the case, a catch-all quantity `Unhandled` will be the result of the operation - which will need to be explicitly converted to the desired type. Currently, the set of supported operations is fairly limited - so this issue could be encountered relatively frequently.

## Source Generator [![NuGet version (SharpMeasures.Generators)](https://img.shields.io/nuget/v/SharpMeasures.Generators.svg?style=plastic)](https://www.nuget.org/packages/SharpMeasures.Generators/) ![GitHub](https://img.shields.io/github/license/ErikWe/sharp-measures?style=plastic)

All quantities and units of SharpMeasures are generated by a source generator. This means that it's trivial to extend the existing set of quantities and units, or to make your own set entirely from scratch.

### Specialized Types

The goal is for SharpMeasures to include only the most common quantites. However, you might prefer your method signature to consist of `Altitude` rather than the terribly ambiguous `Height`. This can easily be accomplished using the source generator:

```csharp
using SharpMeasures.Generators;

[SpecializedScalarQuantity(typeof(Height))]
public partial class Altitude { }
```

This means that `Altitude` will borrow the definition from `Height` - including units, operations, and constants. Cast operators ensure that `Altitude` can implicitly be used in place of `Height` - while the reverse action would require an explicit cast.

> Caveat: Any functionality manually added to `Height` will not be present in `Altitude`. Custom functionality that should be passed on to specialized types need to be defined using `QuantityProcessAttribute`.

### New Types

The source generator can also be used to generate entirely new quantities and units:

```csharp
using SharpMeasures.Generators;

[QuantityOperation(typeof(Area), typeof(Length), OperatorType.Multiplication)]
[ScalarQuantity(typeof(UnitOfLength))]
public partial class Length { }

[ScalarQuantity(typeof(UnitOfArea), DefaultUnit = "SquareMetre", DefaultSymbol = "m²")]
public partial class Area { }

[FixedUnitInstance("Metre", "[*]s")]
[ScaledUnitInstance("Foot", "Feet", "Metre", 0.3048)]
[Unit(typeof(Length))]
public partial class UnitOfLength { }

[DerivedUnitInstance("SquareMetre", "[*]", new[] { "Metre" })]
[DerivableUnit("{0} * {0}", typeof(UnitOfLength))]
[Unit(typeof(Area))]
public partial class UnitOfArea { }
```

The above declarations would make the following code valid:

```csharp
Length length = 41.9 * Length.OneFoot;
Area area = length * length;

Console.WriteLine(area); // "163.10150605439995 [m²]"
```

### XML Documentation Injection

XML documentation for generated members can easily be injected using a simple syntax. The following could apply to `UnitOfArea.SquareMetre`:

```
# UnitInstance_SquareMetre
<summary>The SI unit, representing { <see cref="UnitOfLength.Metre"/>² }.</summary>
/#
```

[Read more](SharpMeasures.Generators/Documentation/DocumentationInjection.md) about XML documentation injection.

### Documentation

Documentation regarding the source generator can be found [here](SharpMeasures.Generators/Documentation/README.md).

## Similar Projects

   - [QuantityTypes](https://github.com/QuantityTypes/QuantityTypes)
   - [UnitsNet](https://github.com/angularsen/UnitsNet)
