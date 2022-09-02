namespace SharpMeasures.Generators.Scalars.Pipelines.Conversions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.SourceBuilding;
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
        private NewlineSeparationHandler SeparationHandler { get; }

        private DataModel Data { get; }

        private InterfaceCollector InterfaceCollector { get; }

        private Composer(DataModel data)
        {
            Data = data;

            SeparationHandler = new(Builder);

            InterfaceCollector = InterfaceCollector.Delayed(Builder);
        }

        private void Compose()
        {
            StaticBuilding.AppendHeaderAndDirectives(Builder);

            NamespaceBuilding.AppendNamespace(Builder, Data.Scalar.Namespace);

            Builder.Append(Data.Scalar.ComposeDeclaration());

            InterfaceCollector.MarkInsertionPoint();

            BlockBuilding.AppendBlock(Builder, ComposeTypeBlock, originalIndentationLevel: 0, initialNewLine: true);

            InterfaceCollector.InsertInterfacesOnNewLines(new Indentation(1));
        }

        private string Retrieve()
        {
            return Builder.ToString();
        }

        private void ComposeTypeBlock(Indentation indentation)
        {
            SeparationHandler.MarkUnncecessary();

            foreach (var definition in Data.Conversions)
            {
                foreach (var scalar in definition.Quantities)
                {
                    AppendInstanceConversion(indentation, scalar);
                }
            }

            foreach (var definition in Data.Conversions)
            {
                if (definition.CastOperatorBehaviour is ConversionOperatorBehaviour.None)
                {
                    continue;
                }

                Action<Indentation, NamedType> composer = definition.CastOperatorBehaviour switch
                {
                    ConversionOperatorBehaviour.Explicit => AppendExplicitOperatorConversion,
                    ConversionOperatorBehaviour.Implicit => AppendImplicitOperatorConversion,
                    _ => throw new NotSupportedException("Invalid cast operation")
                };

                foreach (var scalar in definition.Quantities)
                {
                    composer(indentation, scalar);
                }
            }
        }

        private void AppendInstanceConversion(Indentation indentation, NamedType scalar)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.Documentation.Conversion(scalar));
            Builder.AppendLine($"{indentation}public {scalar.FullyQualifiedName} As{scalar.Name} => new(Magnitude);");
        }

        private void AppendExplicitOperatorConversion(Indentation indentation, NamedType scalar)
            => AppendOperatorConversion(indentation, scalar, "explicit");

        private void AppendImplicitOperatorConversion(Indentation indentation, NamedType scalar)
            => AppendOperatorConversion(indentation, scalar, "implicit");

        private void AppendOperatorConversion(Indentation indentation, NamedType scalar, string behaviour)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.Documentation.CastConversion(scalar));

            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, $"public static {behaviour} operator {scalar.Name}", "new(x.Magnitude)", (Data.Scalar.AsNamedType(), "x"));
        }

        private void AppendDocumentation(Indentation indentation, string text)
        {
            DocumentationBuilding.AppendDocumentation(Builder, indentation, text);
        }
    }
}
