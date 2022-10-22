# SharpMeasures.Generators

The source generator used to implement [SharpMeasures](https://www.nuget.org/packages/SharpMeasures/) - allowing you to easily extend the existing set of quantities, or make new units and quantities from scratch:

```csharp
using SharpMeasures.Generators;

[SpecializedScalarQuantity(typeof(Distance))]
public partial class Altitude { }
```

### Installation

SharpMeasures.Generators has a soft dependency on [SharpMeasures.Base](https://www.nuget.org/pacakages/SharpMeasures.Base/). The recommended `.csproj` looks like this:

``` XML
<ItemGroup>
    <PackageReference Include="SharpMeasures.Base" />
    <PackageReference Include="SharpMeasures.Generators" PrivateAssets="all" />
</ItemGroup>
```

See [GitHub](https://github.com/ErikWe/sharp-measures/tree/main/SharpMeasures.Generators/Documentation) for more information.