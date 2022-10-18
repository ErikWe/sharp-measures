# Configuration

This document describes how the SharpMeasures source generator can be configured - starting with the actual process, then listing the possible configurations.

### How-to

The source generator is configured using the [Global AnalyzerConfig](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/configuration-files). Here is a quick step-by-step:

1. Create a file `ProjectName.globalconfig`, with the content:

```
is_global = true
```

2. Add the file as a Global AnalyzerConfig file using `ProjectName.csproj`:

``` XML
<ItemGroup>
    <GlobalAnalyzerConfigFiles Include="ProjectName.globalconfig" />
</ItemGroup>
```

3. Configurations are added to `ProjectName.globalconfig` according to

```
<Key> = <value>
```

## Supported Configuration

The following keys can be used to configure the SharpMeasures source generator:

* [SharpMeasures_GeneratedFileHeaderContent](#generated-file-header-content)
* [SharpMeasures_AllowAttributeAliases](#allow-aliased-attributes)
* [SharpMeasures_GenerateDocumentation](#generate-documentation)
* [SharpMeasures_DocumentationFileExtension](#documentation-file-extension)
* [SharpMeasures_LimitOneErrorPerDocumentationFile](#documentation-file-diagnostics)
* [SharpMeasures_PrintDocumentationTags](#print-documentation-tags)

### Generated File Header Content

The key `SharpMeasures_GeneratedFileHeaderContent` is used to configure the content of the *"auto-generated-file-header"* present in each generated file. Expects a comma-separated list of items. The default value is `all`. The following items are supported:

|   Item  | Header content           |
|--------:|--------------------------|
|    none | No header is generated   |
|  header | A header is generated    |
|    tool | Name of the generator    |
| version | Version of the generator |
|    date | Current date             |
|    time | Current time             |
|     all | All of the above         |

### Allow Aliased Attributes

The key `SharpMeasures_AllowAttributeAliases` is used to configure whether the source generator considers aliases when looking for relevant attributes. This is a relatively expensive check, so expect the performance to degrade if enabled. Expects `true` or `false`, with the default value being `false`.

The following code would require `SharpMeasures_AllowAttributeAliases` set to `true`:

``` csharp
using Scalar = SharpMeasures.Generators.ScalarQuantityAttribute;

[Scalar(typeof(UnitOfLength))]
public partial class Length { }
```

### Generate Documentation

The key `SharpMeasures_GenerateDocumentation` is used to configure whether the generator outputs XML documentation for the generated types and members. Expects `true` or `false`, with the default value being `true`. Read [Documentation.md](DocumentationInjection.md) for more information.

### Documentation File Extension

The key `SharpMeasures_DocumentationFileExtension` is used to configure the extension used by documentation files. The default value is `.doc.txt`. Read [Documentation.md](DocumentationInjection.md) for more information.

### Documentation File Diagnostics

The key `SharpMeasures_LimitOneErrorPerDocumentationFile` is used to configure whether more than one diagnostics can be issued per documentation file. Expects `true` or `false`, with the default value being `true`. Read [Documentation.md](DocumentationInjection.md) for more information.

### Print Documentation Tags
The key `SharpMeasures_PrintDocumentationTags` is used to enable [Figure-out-tag Mode](DocumentationInjection.md#figure-out-tag-mode). Expects `true` or `false`, with the default value being `false`. Read [Documentation.md](DocumentationInjection.md) for more information.