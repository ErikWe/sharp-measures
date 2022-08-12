namespace SharpMeasures.Generators.Scalars.Pipelines.Conversions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.SourceBuilding;
using SharpMeasures.Generators.Unresolved.Scalars;
using SharpMeasures.Generators.Utility;

using System;
using System.Text;

internal static class Execution
{
    public static void Execute(SourceProductionContext context, DataModel data)
    {
        string source = Composer.Compose(data);

        context.AddSource($"{data.Scalar.Name}_Conversions.g.cs", SourceText.From(source, Encoding.UTF8));
    }

    private class Composer
    {
        public static string Compose(DataModel data)
        {
            Composer composer = new(data);
            composer.Compose();
            return composer.Retrieve();
        }

        private StringBuilder Builder { get; } = new();
        private UsingsCollector UsingsCollector { get; }
        private InterfaceCollector InterfaceCollector { get; }

        private DataModel Data { get; }

        private Composer(DataModel data)
        {
            Data = data;

            UsingsCollector = UsingsCollector.Delayed(Builder, data.Scalar.Namespace);
            InterfaceCollector = InterfaceCollector.Delayed(Builder);

            UsingsCollector.AddUsing("SharpMeasures.ScalarAbstractions");

            if (Data.Scalar.IsReferenceType)
            {
                UsingsCollector.AddUsing("System");
            }
        }

        private void Compose()
        {
            StaticBuilding.AppendHeaderAndDirectives(Builder);

            UsingsCollector.MarkInsertionPoint();

            NamespaceBuilding.AppendNamespace(Builder, Data.Scalar.Namespace);

            Builder.Append(Data.Scalar.ComposeDeclaration());

            InterfaceCollector.MarkInsertionPoint();

            BlockBuilding.AppendBlock(Builder, ComposeTypeBlock, originalIndentationLevel: 0);

            InterfaceCollector.InsertInterfacesOnNewLines(new Indentation(1));
            UsingsCollector.InsertUsings();
        }

        private string Retrieve()
        {
            return Builder.ToString();
        }

        private void ComposeTypeBlock(Indentation indentation)
        {
            foreach (IConvertibleScalar definition in Data.Conversions)
            {
                foreach (IUnresolvedScalarType scalar in definition.Scalars)
                {
                    ComposeInstanceConversion(indentation, scalar);
                    Builder.AppendLine();
                }
            }

            foreach (IConvertibleScalar definition in Data.Conversions)
            {
                if (definition.CastOperatorBehaviour is ConversionOperatorBehaviour.None)
                {
                    continue;
                }

                Action<Indentation, IUnresolvedScalarType> composer = definition.CastOperatorBehaviour switch
                {
                    ConversionOperatorBehaviour.Explicit => ComposeExplicitOperatorConversion,
                    ConversionOperatorBehaviour.Implicit => ComposeImplicitOperatorConversion,
                    _ => throw new NotSupportedException("Invalid cast operation")
                };

                foreach (IUnresolvedScalarType scalar in definition.Scalars)
                {
                    composer(indentation, scalar);
                    Builder.AppendLine();
                }
            }

            SourceBuildingUtility.RemoveOneNewLine(Builder);
        }

        private void ComposeInstanceConversion(Indentation indentation, IUnresolvedScalarType scalar)
        {
            AppendDocumentation(indentation, Data.Documentation.Conversion(scalar));
            Builder.AppendLine($"{indentation}public {scalar.Type.Name} As{scalar.Type.Name} => new(Magnitude);");
        }

        private void ComposeExplicitOperatorConversion(Indentation indentation, IUnresolvedScalarType scalar)
            => ComposeOperatorConversion(indentation, scalar, "explicit");

        private void ComposeImplicitOperatorConversion(Indentation indentation, IUnresolvedScalarType scalar)
            => ComposeOperatorConversion(indentation, scalar, "implicit");

        private void ComposeOperatorConversion(Indentation indentation, IUnresolvedScalarType scalar, string behaviour)
        {
            AppendDocumentation(indentation, Data.Documentation.CastConversion(scalar));

            if (Data.Scalar.IsReferenceType)
            {
                Builder.AppendLine($"""{indentation}/// <exception cref="ArgumentNullException"/>""");
            }

            Builder.Append($"{indentation}public static {behaviour} operator {scalar.Type.Name}({Data.Scalar.Name} x)");

            if (Data.Scalar.IsReferenceType)
            {
                Builder.AppendLine();

                Builder.AppendLine($$"""
                    {{indentation}}{
                    {{indentation.Increased}}ArgumentNullException.ThrowIfNull(x);

                    {{indentation.Increased}}return new(x.Magnitude);
                    {{indentation}}}
                    """);
            }
            else
            {
                Builder.AppendLine(" => new(x.Magnitude);");
            }
        }

        private void AppendDocumentation(Indentation indentation, string text)
        {
            DocumentationBuilding.AppendDocumentation(Builder, indentation, text);
        }
    }
}
