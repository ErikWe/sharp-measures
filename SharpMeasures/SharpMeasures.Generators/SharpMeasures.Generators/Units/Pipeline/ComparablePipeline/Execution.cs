﻿namespace SharpMeasures.Generators.Units.Pipeline.ComparablePipeline;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.SourceBuilding;

using System;
using System.Text;

internal static class Execution
{
    public static void Execute(SourceProductionContext context, DataModel data)
    {
        string source = Composer.Compose(context, data);

        context.AddSource($"{data.Unit.Name}_Comparable.g.cs", SourceText.From(source, Encoding.UTF8));
    }

    private class Composer
    {
        public static string Compose(SourceProductionContext context, DataModel data)
        {
            Composer composer = new(context, data);
            composer.Compose();
            return composer.Retrieve();
        }

        private SourceProductionContext Context { get; }
        private StringBuilder Builder { get; } = new();

        private DataModel Data { get; }

        private Composer(SourceProductionContext context, DataModel data)
        {
            Context = context;
            Data = data;
        }

        private void Compose()
        {
            StaticBuilding.AppendAutoGeneratedHeader(Builder);
            StaticBuilding.AppendNullableDirective(Builder);

            NamespaceBuilding.AppendNamespace(Builder, Data.Unit.Namespace);

            UsingsBuilding.AppendUsings(Builder, new string[]
            {
            "System"
            });

            Builder.Append(Data.Unit.ComposeDeclaration());

            InterfaceBuilding.AppendInterfaceImplementation(Builder, new string[]
            {
            $"IComparable<{Data.Unit.Name}>"
            });

            BlockBuilding.AppendBlock(Builder, ComposeTypeBlock, originalIndentationLevel: 0);
        }

        private string Retrieve()
        {
            return Builder.ToString();
        }

        private void ComposeTypeBlock(StringBuilder source, Indentation indentation)
        {
            DocumentationBuilding.AppendDocumentation(Context, source, Data.Documentation, indentation, "CompareTo_SameType");
            source.Append($"{indentation}public int CompareTo({Data.Unit.Name} other) " +
                $"=> {Data.Quantity.Name}.CompareTo(other.{Data.Quantity.Name});{Environment.NewLine}");

            source.Append(Environment.NewLine);
            DocumentationBuilding.AppendDocumentation(Context, source, Data.Documentation, indentation, "Operator_LessThan_SameType");
            source.Append($"{indentation}public static bool operator <({Data.Unit.Name} x, {Data.Unit.Name} y) " +
                $"=> x.{Data.Quantity.Name} < y.{Data.Quantity.Name};{Environment.NewLine}");
            DocumentationBuilding.AppendDocumentation(Context, source, Data.Documentation, indentation, "Operator_GreaterThan_SameType");
            source.Append($"{indentation}public static bool operator >({Data.Unit.Name} x, {Data.Unit.Name} y) " +
                $"=> x.{Data.Quantity.Name} > y.{Data.Quantity.Name};{Environment.NewLine}");
            DocumentationBuilding.AppendDocumentation(Context, source, Data.Documentation, indentation, "Operator_LessThanOrEqual_SameType");
            source.Append($"{indentation}public static bool operator <=({Data.Unit.Name} x, {Data.Unit.Name} y) " +
                $"=> x.{Data.Quantity.Name} <= y.{Data.Quantity.Name};{Environment.NewLine}");
            DocumentationBuilding.AppendDocumentation(Context, source, Data.Documentation, indentation, "Operator_GreaterThanOrEqual_SameType");
            source.Append($"{indentation}public static bool operator >=({Data.Unit.Name} x, {Data.Unit.Name} y) " +
                $"=> x.{Data.Quantity.Name} >= y.{Data.Quantity.Name};");
        }
    }
}
