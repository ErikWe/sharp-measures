﻿namespace SharpMeasures.Generators.Scalars.Pipelines.DimensionalEquivalence;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.SourceBuilding;

using System;
using System.Text;
using System.Linq;

internal static class Execution
{
    public static void Execute(SourceProductionContext context, DataModel data)
    {
        if (data.DimensionalEquivalences.EquivalentQuantities.Any() is false)
        {
            return;
        }

        string source = Composer.Compose(context, data);

        context.AddSource($"{data.Scalar.Name}_DimensionalEquivalence.g.cs", SourceText.From(source, Encoding.UTF8));
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
        private UsingsCollector UsingsCollector { get; }

        private DataModel Data { get; }

        private Composer(SourceProductionContext context, DataModel data)
        {
            Context = context;
            Data = data;

            UsingsCollector = UsingsCollector.Delayed(Builder, data.Scalar.Namespace);
        }

        private void Compose()
        {
            StaticBuilding.AppendAutoGeneratedHeader(Builder);
            StaticBuilding.AppendNullableDirective(Builder);

            UsingsCollector.MarkInsertionPoint();

            NamespaceBuilding.AppendNamespace(Builder, Data.Scalar.Namespace);

            Builder.Append(Data.Scalar.ComposeDeclaration());

            BlockBuilding.AppendBlock(Builder, ComposeTypeBlock, originalIndentationLevel: 0);

            UsingsCollector.InsertUsings();
        }

        private string Retrieve()
        {
            return Builder.ToString();
        }

        private void ComposeTypeBlock(Indentation indentation)
        {
            foreach (ScalarInterface quantity in Data.DimensionalEquivalences.EquivalentQuantities)
            {
                ComposeInstanceConversion(quantity, indentation);
                Builder.AppendLine();
            }

            foreach (ScalarInterface quantity in Data.DimensionalEquivalences.EquivalentQuantitiesWithImplicitCast)
            {
                ComposeOperatorConversion(quantity, indentation, "implicit");
                Builder.AppendLine();
            }

            foreach (ScalarInterface quantity in Data.DimensionalEquivalences.EquivalentQuantitiesWithExplicitCast)
            {
                ComposeOperatorConversion(quantity, indentation, "explicit");
                Builder.AppendLine();
            }

            SourceBuildingUtility.RemoveOneNewLine(Builder);
        }

        private void ComposeInstanceConversion(ScalarInterface quantity, Indentation indentation)
        {
            UsingsCollector.AddUsing(quantity.ScalarType.Namespace);

            AppendDocumentation(indentation, ScalarDocumentationTags.DimensionallyEquivalentTo(quantity.ScalarType.Name));
            Builder.AppendLine($"{indentation}public {quantity.ScalarType.Name} As{quantity.ScalarType.Name} => new(Magnitude);");
        }

        private void ComposeOperatorConversion(ScalarInterface quantity, Indentation indentation, string behaviour)
        {
            AppendDocumentation(indentation, ScalarDocumentationTags.Operators.DimensionallyEquivalentTo(quantity.ScalarType.Name));
            Builder.AppendLine($"{indentation}public static {behaviour} operator {quantity.ScalarType.Name}({Data.Scalar.Name} x) => new(x.Magnitude);");
        }

        private void AppendDocumentation(Indentation indentation, string tag)
        {
            DocumentationBuilding.AppendDocumentation(Context, Builder, Data.Documentation, indentation, tag);
        }
    }
}
