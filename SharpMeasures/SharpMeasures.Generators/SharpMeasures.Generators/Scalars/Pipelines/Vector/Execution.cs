﻿namespace SharpMeasures.Generators.Scalars.Pipelines.Vectors;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.SourceBuilding;
using SharpMeasures.Generators.Vectors;
using SharpMeasures.Generators.Vectors.Pipelines;

using System;
using System.Collections.Generic;
using System.Text;

internal static class Execution
{
    public static void Execute(SourceProductionContext context, DataModel data)
    {
        string source = Composer.Compose(context, data);

        context.AddSource($"{data.Scalar.Name}_Vectors.g.cs", SourceText.From(source, Encoding.UTF8));
    }

    private class Composer
    {
        public static string Compose(SourceProductionContext context, DataModel data)
        {
            Composer composer = new(context, data);
            composer.Compose();
            composer.ReportDiagnostics();
            return composer.Retrieve();
        }

        private SourceProductionContext Context { get; }
        private StringBuilder Builder { get; } = new();

        private DataModel Data { get; }

        private UsingsCollector UsingsCollector { get; }
        private List<Diagnostic> Diagnostics { get; } = new();

        private static VectorTextBuilder ScalarTupleText { get; } = ConstantTexts.Builders.Lower.Scalar;

        private Composer(SourceProductionContext context, DataModel data)
        {
            Context = context;
            Data = data;

            UsingsCollector = UsingsCollector.Delayed(Builder, data.Scalar.Namespace);
            UsingsCollector.AddUsing("SharpMeasures");
        }

        private void Compose()
        {
            StaticBuilding.AppendAutoGeneratedHeader(Builder);
            StaticBuilding.AppendNullableDirective(Builder);

            NamespaceBuilding.AppendNamespace(Builder, Data.Scalar.Namespace);

            UsingsCollector.MarkInsertionPoint();

            Builder.Append(Data.Scalar.ComposeDeclaration());

            BlockBuilding.AppendBlock(Builder, ComposeTypeBlock, originalIndentationLevel: 0);

            UsingsCollector.InsertUsings();
        }

        private void ReportDiagnostics()
        {
            Context.ReportDiagnostics(Diagnostics);
        }

        private string Retrieve()
        {
            return Builder.ToString();
        }

        private void ComposeTypeBlock(Indentation indentation)
        {
            foreach (VectorInterface vector in Data.VectorCollection.Vectors)
            {
                UsingsCollector.AddUsing(vector.VectorType.Namespace);

                ComposeForVector(indentation, vector);
                Builder.AppendLine();
            }
        }

        private void ComposeForVector(Indentation indentation, VectorInterface vector)
        {
            string scalarTuple = $"({ScalarTupleText.GetText(vector.Dimension)}";

            AppendDocumentation(indentation, ScalarDocumentationTags.Vectors.Multiply_Vector(vector.Dimension));
            Builder.AppendLine($"{indentation}public {vector.VectorType.Name} Multiply(Vector{vector.Dimension} factor) => new(Magnitude.Value * factor);");

            AppendDocumentation(indentation, ScalarDocumentationTags.Vectors.Multiply_ScalarTuple(vector.Dimension));
            Builder.AppendLine($"{indentation}public {vector.VectorType.Name} Multiply({scalarTuple} components) => new(Magnitude * components);");

            Builder.AppendLine();

            AppendDocumentation(indentation, ScalarDocumentationTags.Vectors.Operators.Multiply_Vector_LHS(vector.Dimension));
            Builder.AppendLine($"{indentation}public static {vector.VectorType.Name} operator *({Data.Scalar.Name} x, Vector{vector.Dimension} y) => new(x.Magnitude.Value * y);");

            AppendDocumentation(indentation, ScalarDocumentationTags.Vectors.Operators.Multiply_Vector_RHS(vector.Dimension));
            Builder.AppendLine($"{indentation}public static {vector.VectorType.Name} operator *(Vector{vector.Dimension} x, {Data.Scalar.Name} y) => new(x * y.Magnitude.Value);");

            AppendDocumentation(indentation, ScalarDocumentationTags.Vectors.Operators.Multiply_ScalarTuple_LHS(vector.Dimension));
            Builder.AppendLine($"{indentation}public static {vector.VectorType.Name} operator *({Data.Scalar.Name} x, {scalarTuple} y) => new(x.Magnitude * y);");

            AppendDocumentation(indentation, ScalarDocumentationTags.Vectors.Operators.Multiply_ScalarTuple_RHS(vector.Dimension));
            Builder.AppendLine($"{indentation}public static {vector.VectorType.Name} operator *({scalarTuple} x, {Data.Scalar.Name} y) => new(x * y.Magnitude);");
        }

        private void AppendDocumentation(Indentation indentation, string tag)
        {
            DocumentationBuilding.AppendDocumentation(Context, Builder, Data.Documentation, indentation, tag);
        }
    }
}
