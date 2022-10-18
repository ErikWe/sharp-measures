# SharpMeasures.Generators

The source generator used to implement [SharpMeasures](https://www.nuget.org/packages/SharpMeasures/). Allows you to easily extend the existing set of quantities, or make new units and quantities from scratch:

```csharp
using SharpMeasures.Generators.Scalars;

[SpecializedScalarQuantity(typeof(Length))]
public partial class Altitude { }
```

See [GitHub](https://github.com/ErikWe/sharp-measures) for more information.