# Installation

SharpMeasures.Generators has a soft dependency on [SharpMeasures.Base](https://www.nuget.org/pacakages/SharpMeasures.Base/). The recommended `.csproj` looks like this:

``` XML
<ItemGroup>
    <PackageReference Include="SharpMeasures.Base" />
    <PackageReference Include="SharpMeasures.Generators" PrivateAssets="all" />
</ItemGroup>
```

Without [SharpMeasures.Base](https://www.nuget.org/pacakages/SharpMeasures.Base/), the generated files will not compile.