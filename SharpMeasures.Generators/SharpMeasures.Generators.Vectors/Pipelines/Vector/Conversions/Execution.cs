namespace SharpMeasures.Generators.Vectors.Pipelines.Vector.Conversions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.SourceBuilding;

using System;
using System.Text;

internal static class Execution
{
    public static void Execute(SourceProductionContext context, DataModel data)
    {
        if (context.CancellationToken.IsCancellationRequested)
        {
            return;
        }

        string source = Composer.Compose(data);

        if (source.Length is 0)
        {
            return;
        }

        context.AddSource($"{data.Vector.Name}_Conversions.g.cs", SourceText.From(source, Encoding.UTF8));
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

        private bool AnyConversions { get; set; }

        private Composer(DataModel data)
        {
            Data = data;

            SeparationHandler = new(Builder);
        }

        private void Compose()
        {
            StaticBuilding.AppendHeaderAndDirectives(Builder);

            NamespaceBuilding.AppendNamespace(Builder, Data.Vector.Namespace);

            Builder.AppendLine(Data.Vector.ComposeDeclaration());

            BlockBuilding.AppendBlock(Builder, ComposeTypeBlock, originalIndentationLevel: 0, initialNewLine: true);
        }

        private string Retrieve()
        {
            if (AnyConversions)
            {
                return Builder.ToString();
            }

            return string.Empty;
        }

        private void ComposeTypeBlock(Indentation indentation)
        {
            var initialLength = Builder.Length;

            SeparationHandler.MarkUnncecessary();

            foreach (var conversionDefinition in Data.Conversions)
            {
                if (conversionDefinition.ConversionDirection is QuantityConversionDirection.Onedirectional or QuantityConversionDirection.Bidirectional)
                {
                    foreach (var vector in conversionDefinition.Quantities)
                    {
                        if (Data.VectorPopulation.Groups.TryGetValue(vector, out var group))
                        {
                            if (group.MembersByDimension.TryGetValue(Data.Dimension, out var correspondingMember))
                            {
                                AppendNormalMethodConversion(indentation, correspondingMember);
                            }
                        }

                        if (Data.VectorPopulation.Vectors.ContainsKey(vector))
                        {
                            AppendNormalMethodConversion(indentation, vector);
                        }
                    }
                }

                if (conversionDefinition.ConversionDirection is QuantityConversionDirection.Antidirectional or QuantityConversionDirection.Bidirectional)
                {
                    foreach (var vector in conversionDefinition.Quantities)
                    {
                        if (Data.VectorPopulation.Groups.TryGetValue(vector, out var group))
                        {
                            if (group.MembersByDimension.TryGetValue(Data.Dimension, out var correspondingMember))
                            {
                                AppendAntidirectionalMethodConversion(indentation, correspondingMember);
                            }
                        }

                        if (Data.VectorPopulation.Vectors.ContainsKey(vector))
                        {
                            AppendAntidirectionalMethodConversion(indentation, vector);
                        }
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
                        ConversionOperatorBehaviour.Explicit => (indentation, vector) => AppendNormalOperatorConversion(indentation, vector, "explicit"),
                        ConversionOperatorBehaviour.Implicit => (indentation, vector) => AppendAntidirectionalOperatorConversion(indentation, vector, "implicit"),
                        _ => throw new NotSupportedException("Invalid cast operation")
                    };

                    foreach (var vector in conversionDefinition.Quantities)
                    {
                        if (Data.VectorPopulation.Groups.TryGetValue(vector, out var group))
                        {
                            if (group.MembersByDimension.TryGetValue(Data.Dimension, out var correspondingMember))
                            {
                                composer(indentation, correspondingMember);
                            }
                        }

                        if (Data.VectorPopulation.Vectors.ContainsKey(vector))
                        {
                            composer(indentation, vector);
                        }
                    }
                }

                if (conversionDefinition.ConversionDirection is QuantityConversionDirection.Antidirectional or QuantityConversionDirection.Bidirectional)
                {
                    Action<Indentation, NamedType> composer = conversionDefinition.CastOperatorBehaviour switch
                    {
                        ConversionOperatorBehaviour.Explicit => (indentation, vector) => AppendAntidirectionalOperatorConversion(indentation, vector, "explicit"),
                        ConversionOperatorBehaviour.Implicit => (indentation, vector) => AppendAntidirectionalOperatorConversion(indentation, vector, "implicit"),
                        _ => throw new NotSupportedException("Invalid cast operation")
                    };

                    foreach (var vector in conversionDefinition.Quantities)
                    {
                        if (Data.VectorPopulation.Groups.TryGetValue(vector, out var group))
                        {
                            if (group.MembersByDimension.TryGetValue(Data.Dimension, out var correspondingMember))
                            {
                                composer(indentation, correspondingMember);
                            }
                        }

                        if (Data.VectorPopulation.Vectors.ContainsKey(vector))
                        {
                            composer(indentation, vector);
                        }
                    }
                }
            }

            AnyConversions = Builder.Length > initialLength;
        }

        private void AppendNormalMethodConversion(Indentation indentation, NamedType vector)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.Documentation.Conversion(vector));
            Builder.AppendLine($"{indentation}public {vector.FullyQualifiedName} As{vector.Name} => new(Components);");
        }

        private void AppendAntidirectionalMethodConversion(Indentation indentation, NamedType vector)
        {
            var parameterName = SourceBuildingUtility.ToParameterName(vector.Name);

            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.Documentation.AntidirectionalConversion(vector));

            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, $"public {Data.Vector.Name} From", $"new({parameterName}.Components)", (vector, parameterName));
        }

        private void AppendNormalOperatorConversion(Indentation indentation, NamedType vector, string behaviour)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.Documentation.CastConversion(vector));

            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, $"public static {behaviour} operator {vector.Name}", "new(a.Components)", (Data.Vector.AsNamedType(), "a"));
        }

        private void AppendAntidirectionalOperatorConversion(Indentation indentation, NamedType vector, string behaviour)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.Documentation.AntidirectionalCastConversion(vector));

            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, $"public static {behaviour} operator {Data.Vector.Name}", "new(a.Components)", (vector, "a"));
        }

        private void AppendDocumentation(Indentation indentation, string text)
        {
            DocumentationBuilding.AppendDocumentation(Builder, indentation, text);
        }
    }
}
