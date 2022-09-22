namespace SharpMeasures.Generators.Scalars.Pipelines.Conversions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.SourceBuilding;

using System;
using System.Collections.Generic;
using System.Text;

internal static class Execution
{
    public static void Execute(SourceProductionContext context, Optional<DataModel> data)
    {
        if (context.CancellationToken.IsCancellationRequested || data.HasValue is false)
        {
            return;
        }

        string source = Composer.Compose(data.Value);

        context.AddSource($"{data.Value.Scalar.QualifiedName}.Conversions.g.cs", SourceText.From(source, Encoding.UTF8));
    }

    private sealed class Composer
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

        private Composer(DataModel data)
        {
            Data = data;

            SeparationHandler = new(Builder);
        }

        private void Compose()
        {
            StaticBuilding.AppendHeaderAndDirectives(Builder);

            NamespaceBuilding.AppendNamespace(Builder, Data.Scalar.Namespace);

            Builder.Append(Data.Scalar.ComposeDeclaration());

            BlockBuilding.AppendBlock(Builder, ComposeTypeBlock, originalIndentationLevel: 0, initialNewLine: true);
        }

        private string Retrieve() => Builder.ToString();

        private void ComposeTypeBlock(Indentation indentation)
        {
            SeparationHandler.MarkUnncecessary();

            AppendMethodConversions(indentation);
            AppendOperatorConversions(indentation);
        }

        private void AppendMethodConversions(Indentation indentation)
        {
            HashSet<NamedType> implementedOutgoingConversions = new();
            HashSet<NamedType> implementedIngoingConversions = new();

            foreach (var conversionDefinition in Data.Conversions)
            {
                appendMethodConversions(conversionDefinition);
            }

            if (Data.ScalarPopulation.Scalars.TryGetValue(Data.Scalar.AsNamedType(), out var scalar) && scalar.OriginalQuantity is not null)
            {
                recursivelyAppendSpecializationMethodConversion(scalar.OriginalQuantity.Value);
            }

            foreach (var conversionDefinition in Data.InheritedConversions)
            {
                appendMethodConversions(conversionDefinition);
            }

            void appendMethodConversions(IConvertibleQuantity definition)
            {
                foreach (var scalar in definition.Quantities)
                {
                    if (definition.ConversionDirection is QuantityConversionDirection.Onedirectional or QuantityConversionDirection.Bidirectional)
                    {
                        if (implementedOutgoingConversions.Add(scalar))
                        {
                            AppendOutgoingMethodConversion(indentation, scalar);
                        }
                    }

                    if (definition.ConversionDirection is QuantityConversionDirection.Antidirectional or QuantityConversionDirection.Bidirectional)
                    {
                        if (implementedIngoingConversions.Add(scalar))
                        {
                            AppendIngoingMethodConversion(indentation, scalar);
                        }
                    }
                }
            }

            void recursivelyAppendSpecializationMethodConversion(NamedType originalQuantity)
            {
                if (implementedOutgoingConversions.Add(originalQuantity))
                {
                    AppendOutgoingMethodConversion(indentation, originalQuantity);
                }

                if (implementedIngoingConversions.Add(originalQuantity))
                {
                    AppendIngoingMethodConversion(indentation, originalQuantity);
                }

                if (Data.ScalarPopulation.Scalars.TryGetValue(originalQuantity, out var scalar) && scalar.OriginalQuantity is not null)
                {
                    recursivelyAppendSpecializationMethodConversion(scalar.OriginalQuantity!.Value);
                }
            }
        }

        private void AppendOutgoingMethodConversion(Indentation indentation, NamedType scalar)
        {
            if (scalar == Data.Scalar.AsNamedType())
            {
                return;
            }

            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.Documentation.Conversion(scalar));
            Builder.AppendLine($"{indentation}public {scalar.FullyQualifiedName} As{scalar.Name} => new(Magnitude);");
        }

        private void AppendIngoingMethodConversion(Indentation indentation, NamedType scalar)
        {
            if (scalar == Data.Scalar.AsNamedType())
            {
                return;
            }

            var parameterName = SourceBuildingUtility.ToParameterName(scalar.Name);

            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.Documentation.AntidirectionalConversion(scalar));

            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, $"public {Data.Scalar.Name} From", $"new({parameterName}.Magnitude)", (scalar, parameterName));
        }

        private void AppendOperatorConversions(Indentation indentation)
        {
            HashSet<NamedType> implementedOutgoingConversions = new();
            HashSet<NamedType> implementedIngoingConversions = new();

            foreach (var conversionDefinition in Data.Conversions)
            {
                appendOpertorConversion(conversionDefinition);
            }

            if (Data.ScalarPopulation.Scalars.TryGetValue(Data.Scalar.AsNamedType(), out var scalar) && scalar.OriginalQuantity is not null)
            {
                recursivelyAppendSpecializationOperatorConversion(scalar.OriginalQuantity.Value);
            }

            foreach (var conversionDefinition in Data.InheritedConversions)
            {
                appendOpertorConversion(conversionDefinition);
            }

            void appendOpertorConversion(IConvertibleQuantity definition)
            {
                var behaviour = definition.CastOperatorBehaviour is ConversionOperatorBehaviour.None ? string.Empty : GetConversionBehaviourText(definition.CastOperatorBehaviour);

                foreach (var scalar in definition.Quantities)
                {
                    if (definition.ConversionDirection is QuantityConversionDirection.Onedirectional or QuantityConversionDirection.Bidirectional)
                    {
                        if (implementedOutgoingConversions.Add(scalar) && definition.CastOperatorBehaviour is not ConversionOperatorBehaviour.None)
                        {
                            AppendOutgoingOperatorConversion(indentation, scalar, behaviour);
                        }
                    }

                    if (definition.ConversionDirection is QuantityConversionDirection.Antidirectional or QuantityConversionDirection.Bidirectional)
                    {
                        if (implementedIngoingConversions.Add(scalar) && definition.CastOperatorBehaviour is not ConversionOperatorBehaviour.None)
                        {
                            AppendIncomingOperatorConversion(indentation, scalar, behaviour);
                        }
                    }
                }
            }

            void recursivelyAppendSpecializationOperatorConversion(NamedType originalQuantity)
            {
                if (implementedOutgoingConversions.Add(originalQuantity) && Data.SpecializationBackwardsBehaviour is not ConversionOperatorBehaviour.None)
                {
                    var behaviour = GetConversionBehaviourText(Data.SpecializationBackwardsBehaviour);

                    AppendOutgoingOperatorConversion(indentation, originalQuantity, behaviour);
                }

                if (implementedIngoingConversions.Add(originalQuantity) && Data.SpecializationForwardsBehaviour is not ConversionOperatorBehaviour.None)
                {
                    var behaviour = GetConversionBehaviourText(Data.SpecializationForwardsBehaviour);

                    AppendIncomingOperatorConversion(indentation, originalQuantity, behaviour);
                }

                if (Data.ScalarPopulation.Scalars.TryGetValue(originalQuantity, out var scalar) && scalar.OriginalQuantity is not null)
                {
                    recursivelyAppendSpecializationOperatorConversion(scalar.OriginalQuantity!.Value);
                }
            }
        }

        private void AppendOutgoingOperatorConversion(Indentation indentation, NamedType scalar, string behaviour)
        {
            if (scalar == Data.Scalar.AsNamedType())
            {
                return;
            }

            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.Documentation.CastConversion(scalar));

            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, $"public static {behaviour} operator {scalar.Name}", "new(x.Magnitude)", (Data.Scalar.AsNamedType(), "x"));
        }

        private void AppendIncomingOperatorConversion(Indentation indentation, NamedType scalar, string behaviour)
        {
            if (scalar == Data.Scalar.AsNamedType())
            {
                return;
            }

            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.Documentation.AntidirectionalCastConversion(scalar));

            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, $"public static {behaviour} operator {Data.Scalar.Name}", "new(x.Magnitude)", (scalar, "x"));
        }

        private void AppendDocumentation(Indentation indentation, string text) => DocumentationBuilding.AppendDocumentation(Builder, indentation, text);

        private static string GetConversionBehaviourText(ConversionOperatorBehaviour behaviour) => behaviour switch
        {
            ConversionOperatorBehaviour.Explicit => "explicit",
            ConversionOperatorBehaviour.Implicit => "implicit",
            _ => throw new NotSupportedException("Invalid cast operation")
        };
    }
}
