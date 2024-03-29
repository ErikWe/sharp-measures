# SharpMeasures

SharpMeasures is an easily extended C# library that aims to encourage explicit usage of physical quantities and units of measurement during development. This is done by providing concrete types, such as `Time`, `UnitOfLength`, and `Acceleration3` - together with a custom-built source generator. While SharpMeasures is mainly designed as a treatment for *primitive obsession*, common matematical operations are also implemented, allowing quantities to be derived from other quantities - as demonstrated below.

```csharp
Displacement3 displacement = (0, 1.5, -4) * Distance.OneMetre;
Time time = 0.5 * Time.OneSecond;

Velocity3 velocity = displacement / time;

Console.WriteLine(velocity); // "(0, 3, -8) [m∙s⁻¹]"
Console.WriteLine(velocity.KilometresPerHour); // ~ "(0, 10.8, -28.8)"
```

See [GitHub](https://github.com/ErikWe/sharp-measures) for more information.

### Source Generator

The source generator used to implement SharpMeasures allows you to easily extend the existing set of quantities, or make new units and quantities from scratch:

```csharp
using SharpMeasures.Generators;

[SpecializedScalarQuantity(typeof(Height))]
public partial class Altitude { }
```

This source generator can be found [here](https://www.nuget.org/packages/SharpMeasures.Generators/).

### Extensions

Soem additional, more situational, units and quantities can be found here:

- [SharpMeasures.Astronomy](https://www.nuget.org/packages/SharpMeasures.Astronomy/)