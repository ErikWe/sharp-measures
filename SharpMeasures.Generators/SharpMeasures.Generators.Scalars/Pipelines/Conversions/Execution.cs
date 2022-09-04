namespace SharpMeasures.Generators.Scalars.Pipelines.Conversions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.SourceBuilding;

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

            foreach (var conversionDefinition in Data.Conversions)
            {
                if (conversionDefinition.ConversionDirection is QuantityConversionDirection.Onedirectional or QuantityConversionDirection.Bidirectional)
                {
                    foreach (var scalar in conversionDefinition.Quantities)
                    {
                        AppendNormalMethodConversion(indentation, scalar);
                    }
                }

                if (conversionDefinition.ConversionDirection is QuantityConversionDirection.Antidirectional or QuantityConversionDirection.Bidirectional)
                {
                    foreach (var scalar in conversionDefinition.Quantities)
                    {
                        AppendAntidirectionalMethodConversion(indentation, scalar);
                    }
                }
            }

            foreach (var conversionDefinition in Data.Conversions)
            {
                if (conversionDefinition.CastOperatorBehaviour is ConversionOperatorBehaviour.None)
                {
                    continue;
                }

                if (conversionDefinition.ConversionDirection is QuantityConversionDirection.Onedirectional or QuantityConversionDirection.Bidirectional)
                {
                    Action<Indentation, NamedType> composer = conversionDefinition.CastOperatorBehaviour switch
                    {
                        ConversionOperatorBehaviour.Explicit => (indentation, scalar) => AppendNormalOperatorConversion(indentation, scalar, "explicit"),
                        ConversionOperatorBehaviour.Implicit => (indentation, scalar) => AppendAntidirectionalOperatorConversion(indentation, scalar, "implicit"),
                        _ => throw new NotSupportedException("Invalid cast operation")
                    };

                    foreach (var scalar in conversionDefinition.Quantities)
                    {
                         composer(indentation, scalar);
                    }
                }

                if (conversionDefinition.ConversionDirection is QuantityConversionDirection.Antidirectional or QuantityConversionDirection.Bidirectional)
                {
                    Action<Indentation, NamedType> composer = conversionDefinition.CastOperatorBehaviour switch
                    {
                        ConversionOperatorBehaviour.Explicit => (indentation, scalar) => AppendAntidirectionalOperatorConversion(indentation, scalar, "explicit"),
                        ConversionOperatorBehaviour.Implicit => (indentation, scalar) => AppendAntidirectionalOperatorConversion(indentation, scalar, "implicit"),
                        _ => throw new NotSupportedException("Invalid cast operation")
                    };

                    foreach (var scalar in conversionDefinition.Quantities)
                    {
                        composer(indentation, scalar);
                    }
                }
            }
        }

        private void AppendNormalMethodConversion(Indentation indentation, NamedType scalar)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.Documentation.Conversion(scalar));
            Builder.AppendLine($"{indentation}public {scalar.FullyQualifiedName} As{scalar.Name} => new(Magnitude);");
        }

        private void AppendAntidirectionalMethodConversion(Indentation indentation, NamedType scalar)
        {
            var parameterName = SourceBuildingUtility.ToParameterName(scalar.Name);

            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.Documentation.AntidirectionalConversion(scalar));

            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, $"public {Data.Scalar.Name} From", $"new({parameterName}.Magnitude)", (scalar, parameterName));
        }

        private void AppendNormalOperatorConversion(Indentation indentation, NamedType scalar, string behaviour)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.Documentation.CastConversion(scalar));

            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, $"public static {behaviour} operator {scalar.Name}", "new(x.Magnitude)", (Data.Scalar.AsNamedType(), "x"));
        }

        private void AppendAntidirectionalOperatorConversion(Indentation indentation, NamedType scalar, string behaviour)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.Documentation.AntidirectionalCastConversion(scalar));

            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, $"public static {behaviour} operator {Data.Scalar.Name}", "new(x.Magnitude)", (scalar, "x"));
        }

        private void AppendDocumentation(Indentation indentation, string text)
        {
            DocumentationBuilding.AppendDocumentation(Builder, indentation, text);
        }
    }
}
