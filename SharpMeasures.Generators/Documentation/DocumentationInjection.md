# Documentation Injection

The SharpMeasures source generator supports injection of XML documentation using a fairly simple custom syntax. For example, `UnitOfLength.Kilometre` can be documented using a file `UnitOfLength.doc.txt` with the following content:

```
# UnitInstance_Kilometre
<summary>Represents { 1 000 } <see cref="Metre"/>.</summary>
/#
```

# Including Files

The documentation files are added to a project as `Additional Files`, declared in the `.csproj`:

``` XML
<ItemGroup>
    <AdditionalFiles Include="Documentation/**/*.doc.txt" />
</ItemGroup>
```

A type `E` in the namespace `A.B.C.D` would require a file named `A.B.C.D.E.doc.txt` to be present in the project.

# Common Tags

The source generator looks for pre-determined tags when matching content with the members of a type. Some of the most common tags are listed here, [Figure-out-tag Mode](#figure-out-tag-mode) describes how to discover the tag related to any member. For unambiguous members, such as `Magnitude` on scalars, the name of the member will be the name of the tag.

- **Any Type**
  - **Header** Targets the type itself.
- **Units**
  - **UnitInstance_{x}** Targets the definition of the unit named {x}. Example: *UnitInstance_Metre*
  - **Derivation_{x}** Targets a unit derivation, where {x} lists the types comprising the signature, separated by underscores. Example: *Derivation_UnitOfLength_UnitOfTime*
- **Scalars**
  - **One_{x}** Targets the property decribing the value 1 in the unit named {x}. Example: *One_Metre*
- **Quantities**
  - **Constant_{x}** Targets the definition of the constant named {x}. Example: *Constant_PlanckLength*
  - **InUnit_{x}** Targets the property describing the magnitude in the unit name {x}. Example: *InUnit_Metre*
  - **InMultiplesOf_{x}** Targets the property describing the magnitude in multiples of the constant named {x}. Example: *InMultiplesOf_PlanckLength*
  - **As_{x}** Targets the property converting to the quantity named {x}. Example: *As_Distance*
  - **From_{x}** Targets the method converting from the quantity named {x}. Example: *From_Distance*

# Syntax

###### Defining a tag

```
# <tag>
<content>
/#
```

###### Inserting the content of another tag

```
#<tag>/#
```

###### Importing the content of another file `OtherFile.doc.txt`

```
# Requires: OtherFile
```

###### Defining the file as only for utility (suppressing some diagnostics)

```
# Utility: true
```

##### Contrived Example

The following setup would result in `Length.Metre` being documented with `<summary>The metre.</summary>`:

```
// Base.doc.txt

# Utility: true

# UnitInstance_Metre
<summary>#Text_For_Metre/#</summary>
/#
```

```
// Length.doc.txt

# Requires: Base

# Text_For_Metre
The metre.
/#
```

# Figure-out-tag Mode

The source generator can be [configured](#configuration) to output the tags related to each generated member, using the global AnalyzerConfig key `SharpMeasures_PrintDocumentationTags`. This will output the tags in the generated file, under `<sharpmeasures-tag>` in the XML documentation of each member. In Visual Studio 2022, generated files are found under `Solution Explorer > Project > Dependencies > Analyzers > SharpMeasures.Generators`.

# Configuration

The source generator can be configured using the global AnalyzerConfig. Read [Configuration.md](Configuration.md) for more details. The following configuration relates to documentation injection:

- `SharpMeasures_GenerateDocumentation` Dictates whether documentation is generated. The default is `true`.
- `SharpMeasures_DocumentationFileExtension` Dictates the extensions of documentation files. The default is `.doc.txt`.
- `SharpMeasures_PrintDocumentationTags` Enables [Figure-out-tag Mode](#figure-out-tag-mode). The default is `false`.
- `SharpMeasures_LimitOneErrorPerDocumentationFile` Dictates whether more than one diagnostics can be reported per documentation file. The default is `true`.