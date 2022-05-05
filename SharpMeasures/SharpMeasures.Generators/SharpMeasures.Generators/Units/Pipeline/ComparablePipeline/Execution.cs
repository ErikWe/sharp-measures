﻿namespace SharpMeasures.Generators.Units.Pipeline.ComparablePipeline;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.SourceBuilding;

using System.Text;

internal static class Execution
{
    public static void Execute(SourceProductionContext context, Stage4.Result result)
    {
        string source = Compose(context, result);

        context.AddSource($"{result.TypeDefinition.Name.Name}_Comparable.g.cs", SourceText.From(source, Encoding.UTF8));
    }

    private static string Compose(SourceProductionContext context, Stage4.Result data)
    {
        StringBuilder source = new();

        string unitName = data.TypeDefinition.Name.Name;
        string quantityName = data.Quantity.Name;

        StaticBuilding.AppendAutoGeneratedHeader(source);
        StaticBuilding.AppendNullableDirective(source);

        NamespaceBuilding.AppendNamespace(source, data.TypeDefinition.Name.NameSpace);

        UsingsBuilding.AppendUsings(source, new string[]
        {
            "System"
        });

        source.Append(data.TypeDefinition.ComposeDeclaration());

        InterfaceBuilding.AppendInterfaceImplementation(source, new string[]
        {
            $"IComparable<{unitName}>"
        });

        BlockBuilding.AppendBlock(source, typeBlock, originalIndentationLevel: 0);

        void typeBlock(StringBuilder source, Indentation indentation)
        {
            DocumentationBuilding.AppendDocumentation(context, source, data.Documentation, indentation, "CompareToSame");

            source.Append($@"{indentation}public int CompareTo({unitName} other) => {quantityName}.CompareTo(other.{quantityName});

{indentation}/// <summary>Determines whether the <see cref=""Quantities.Length""/> represented by <paramref name=""x""/> is
{indentation}/// less than that of <paramref name=""y""/>.</summary>
{indentation}/// <param name=""x"">The operator determines whether the <see cref=""Quantities.Length""/> represented by this <see cref=""UnitOfLength""/> is
{indentation}/// less than that of <paramref name=""y""/>.</param>
{indentation}/// <param name=""y"">The operator determines whether the <see cref=""Quantities.Length""/> represented by <paramref name=""x""/> is
{indentation}/// less than that of this <see cref=""UnitOfLength""/>.</param>
{indentation}public static bool operator <(UnitOfLength x, UnitOfLength y) => x.Length < y.Length;

{indentation}/// <summary>Determines whether the <see cref=""Quantities.Length""/> represented by <paramref name=""x""/> is
{indentation}/// greater than that of <paramref name=""y""/>.</summary>
{indentation}/// <param name=""x"">The operator determines whether the <see cref=""Quantities.Length""/> represented by this <see cref=""UnitOfLength""/> is
{indentation}/// greater than that of <paramref name=""y""/>.</param>
{indentation}/// <param name=""y"">The operator determines whether the <see cref=""Quantities.Length""/> represented by <paramref name=""x""/> is
{indentation}/// greater than that of this <see cref=""UnitOfLength""/>.</param>
{indentation}public static bool operator >(UnitOfLength x, UnitOfLength y) => x.Length > y.Length;

{indentation}/// <summary>Determines whether the <see cref=""Quantities.Length""/> represented by <paramref name=""x""/> is
{indentation}/// less than or equal to that of <paramref name=""y""/>.</summary>
{indentation}/// <param name=""x"">The method determines whether the <see cref=""Quantities.Length""/> represented by this <see cref=""UnitOfLength""/> is
{indentation}/// less than or equal to that of <paramref name=""y""/>.</param>
{indentation}/// <param name=""y"">The method determines whether the <see cref=""Quantities.Length""/> represented by <paramref name=""x""/> is
{indentation}/// less than or equal to that of this <see cref=""UnitOfLength""/>.</param>
{indentation}public static bool operator <=(UnitOfLength x, UnitOfLength y) => x.Length <= y.Length;

{indentation}/// <summary>Determines whether the <see cref=""Quantities.Length""/> represented by <paramref name=""x""/> is
{indentation}/// greater than or equal to that of <paramref name=""y""/>.</summary>
{indentation}/// <param name=""x"">The method determines whether the <see cref=""Quantities.Length""/> represented by this <see cref=""UnitOfLength""/> is
{indentation}/// greater than or equal to that of <paramref name=""y""/>.</param>
{indentation}/// <param name=""y"">The method determines whether the <see cref=""Quantities.Length""/> represented by <paramref name=""x""/> is
{indentation}/// greater than or equal to that of this <see cref=""UnitOfLength""/>.</param>
{indentation}public static bool operator >=(UnitOfLength x, UnitOfLength y) => x.Length >= y.Length;");
        }

        return source.ToString();
    }
}
