namespace SharpMeasures.Generators.Vectors.SourceBuilding.Vectors.Conversions;

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

        if (source.Length is 0)
        {
            return;
        }

        context.AddSource($"{data.Value.Vector.QualifiedName}.Conversions.g.cs", SourceText.From(source, Encoding.UTF8));
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

        private bool AnyConversions { get; set; }

        private Composer(DataModel data)
        {
            Data = data;

            SeparationHandler = new(Builder);
        }

        private void Compose()
        {
            StaticBuilding.AppendHeaderAndDirectives(Builder, Data.SourceBuildingContext.HeaderContentLevel);

            NamespaceBuilding.AppendNamespace(Builder, Data.Vector.Namespace);

            Builder.Append(Data.Vector.ComposeDeclaration());

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

            AppendMethodConversions(indentation);
            AppendOperatorConversions(indentation);

            AnyConversions = Builder.Length > initialLength;
        }

        private void AppendMethodConversions(Indentation indentation)
        {
            HashSet<NamedType> implementedOutgoingConversions = new();
            HashSet<NamedType> implementedIngoingConversions = new();

            foreach (var conversionDefinition in Data.Conversions)
            {
                appendMethodConversions(conversionDefinition);
            }

            if (Data.Group is null && Data.VectorPopulation.Vectors.TryGetValue(Data.Vector.AsNamedType(), out var vector) && vector.OriginalQuantity is not null)
            {
                recursivelyAppendVectorSpecializationMethodConversion(vector.OriginalQuantity.Value);
            }

            if (Data.Group is not null && Data.VectorPopulation.Groups.TryGetValue(Data.Group.Value, out var group) && group.OriginalQuantity is not null)
            {
                recursivelyAppendGroupSpecializationMethodConversion(group.OriginalQuantity.Value);
            }

            foreach (var conversionDefinition in Data.InheritedConversions)
            {
                appendMethodConversions(conversionDefinition);
            }

            void appendMethodConversions(IConvertibleQuantity definition)
            {
                foreach (var quantity in definition.Quantities)
                {
                    if (Data.VectorPopulation.Vectors.ContainsKey(quantity))
                    {
                        appendMethodConversionForQuantity(quantity);
                    }

                    if (Data.VectorPopulation.Groups.TryGetValue(quantity, out var group) && group.MembersByDimension.TryGetValue(Data.Dimension, out var correspondingMember))
                    {
                        appendMethodConversionForQuantity(correspondingMember);
                    }
                }

                void appendMethodConversionForQuantity(NamedType quantity)
                {
                    if (definition.ConversionDirection is QuantityConversionDirection.Onedirectional or QuantityConversionDirection.Bidirectional)
                    {
                        if (implementedOutgoingConversions.Add(quantity))
                        {
                            AppendOutgoingMethodConversion(indentation, quantity);
                        }
                    }

                    if (definition.ConversionDirection is QuantityConversionDirection.Antidirectional or QuantityConversionDirection.Bidirectional)
                    {
                        if (implementedIngoingConversions.Add(quantity))
                        {
                            AppendIngoingMethodConversion(indentation, quantity);
                        }
                    }
                }
            }

            void recursivelyAppendVectorSpecializationMethodConversion(NamedType originalQuantity)
            {
                if (implementedOutgoingConversions.Add(originalQuantity))
                {
                    AppendOutgoingMethodConversion(indentation, originalQuantity);
                }

                if (implementedIngoingConversions.Add(originalQuantity))
                {
                    AppendIngoingMethodConversion(indentation, originalQuantity);
                }

                if (Data.VectorPopulation.Vectors.TryGetValue(originalQuantity, out var vector) && vector.OriginalQuantity is not null)
                {
                    recursivelyAppendVectorSpecializationMethodConversion(vector.OriginalQuantity!.Value);
                }
            }

            void recursivelyAppendGroupSpecializationMethodConversion(NamedType originalGroupName)
            {
                if (Data.VectorPopulation.Groups.TryGetValue(originalGroupName, out var originalGroup) is false)
                {
                    return;
                }

                if (originalGroup.MembersByDimension.TryGetValue(Data.Dimension, out var member))
                {
                    if (implementedOutgoingConversions.Add(member))
                    {
                        AppendOutgoingMethodConversion(indentation, member);
                    }

                    if (implementedIngoingConversions.Add(member))
                    {
                        AppendIngoingMethodConversion(indentation, member);
                    }
                }

                if (originalGroup.OriginalQuantity is not null)
                {
                    recursivelyAppendGroupSpecializationMethodConversion(originalGroup.OriginalQuantity.Value);
                }
            }
        }

        private void AppendOutgoingMethodConversion(Indentation indentation, NamedType vector)
        {
            if (vector == Data.Vector.AsNamedType())
            {
                return;
            }

            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.Conversion(vector));
            Builder.AppendLine($"{indentation}public {vector.FullyQualifiedName} As{vector.Name} => new(Components);");
        }

        private void AppendIngoingMethodConversion(Indentation indentation, NamedType vector)
        {
            if (vector == Data.Vector.AsNamedType())
            {
                return;
            }

            var parameterName = SourceBuildingUtility.ToParameterName(vector.Name);

            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.AntidirectionalConversion(vector));

            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, $"public {Data.Vector.Name} From", $"new({parameterName}.Components)", (vector, parameterName));
        }

        private void AppendOperatorConversions(Indentation indentation)
        {
            HashSet<NamedType> implementedOutgoingConversions = new();
            HashSet<NamedType> implementedIngoingConversions = new();

            foreach (var conversionDefinition in Data.Conversions)
            {
                appendOperatorConversion(conversionDefinition);
            }

            if (Data.Group is null && Data.VectorPopulation.Vectors.TryGetValue(Data.Vector.AsNamedType(), out var vector) && vector.OriginalQuantity is not null)
            {
                recursivelyAppendVectorSpecializationOperatorConversion(vector.OriginalQuantity.Value);
            }

            if (Data.Group is not null && Data.VectorPopulation.Groups.TryGetValue(Data.Group.Value, out var group) && group.OriginalQuantity is not null)
            {
                recursivelyAppendGroupSpecializationOperatorConversion(group.OriginalQuantity.Value);
            }

            foreach (var conversionDefinition in Data.InheritedConversions)
            {
                appendOperatorConversion(conversionDefinition);
            }

            void appendOperatorConversion(IConvertibleQuantity definition)
            {
                var behaviour = definition.CastOperatorBehaviour is ConversionOperatorBehaviour.None ? string.Empty : GetConversionBehaviourText(definition.CastOperatorBehaviour);

                foreach (var quantity in definition.Quantities)
                {
                    if (Data.VectorPopulation.Vectors.ContainsKey(quantity))
                    {
                        appendOperatorConversionForQuantity(quantity);
                    }

                    if (Data.VectorPopulation.Groups.TryGetValue(quantity, out var group) && group.MembersByDimension.TryGetValue(Data.Dimension, out var correspondingMember))
                    {
                        appendOperatorConversionForQuantity(correspondingMember);
                    }
                }

                void appendOperatorConversionForQuantity(NamedType quantity)
                {
                    if (definition.ConversionDirection is QuantityConversionDirection.Onedirectional or QuantityConversionDirection.Bidirectional)
                    {
                        if (implementedOutgoingConversions.Add(quantity) && definition.CastOperatorBehaviour is not ConversionOperatorBehaviour.None)
                        {
                            AppendOutgoingOperatorConversion(indentation, quantity, behaviour);
                        }
                    }

                    if (definition.ConversionDirection is QuantityConversionDirection.Antidirectional or QuantityConversionDirection.Bidirectional)
                    {
                        if (implementedIngoingConversions.Add(quantity) && definition.CastOperatorBehaviour is not ConversionOperatorBehaviour.None)
                        {
                            AppendIngoingOperatorConversion(indentation, quantity, behaviour);
                        }
                    }
                }
            }

            void recursivelyAppendVectorSpecializationOperatorConversion(NamedType originalQuantity)
            {
                if (implementedOutgoingConversions.Add(originalQuantity) && Data.SpecializationBackwardsBehaviour is not ConversionOperatorBehaviour.None)
                {
                    var behaviour = GetConversionBehaviourText(Data.SpecializationBackwardsBehaviour);

                    AppendOutgoingOperatorConversion(indentation, originalQuantity, behaviour);
                }

                if (implementedIngoingConversions.Add(originalQuantity) && Data.SpecializationForwardsBehaviour is not ConversionOperatorBehaviour.None)
                {
                    var behaviour = GetConversionBehaviourText(Data.SpecializationForwardsBehaviour);

                    AppendIngoingOperatorConversion(indentation, originalQuantity, behaviour);
                }

                if (Data.VectorPopulation.Vectors.TryGetValue(originalQuantity, out var vector) && vector.OriginalQuantity is not null)
                {
                    recursivelyAppendVectorSpecializationOperatorConversion(vector.OriginalQuantity!.Value);
                }
            }

            void recursivelyAppendGroupSpecializationOperatorConversion(NamedType originalGroupName)
            {
                if (Data.VectorPopulation.Groups.TryGetValue(originalGroupName, out var originalGroup) is false)
                {
                    return;
                }

                if (originalGroup.MembersByDimension.TryGetValue(Data.Dimension, out var correspondingMember))
                {
                    if (implementedOutgoingConversions.Add(correspondingMember) && Data.SpecializationBackwardsBehaviour is not ConversionOperatorBehaviour.None)
                    {
                        var behaviour = GetConversionBehaviourText(Data.SpecializationBackwardsBehaviour);

                        AppendOutgoingOperatorConversion(indentation, correspondingMember, behaviour);
                    }

                    if (implementedIngoingConversions.Add(correspondingMember) && Data.SpecializationForwardsBehaviour is not ConversionOperatorBehaviour.None)
                    {
                        var behaviour = GetConversionBehaviourText(Data.SpecializationForwardsBehaviour);

                        AppendIngoingOperatorConversion(indentation, correspondingMember, behaviour);
                    }
                }

                if (originalGroup.OriginalQuantity is not null)
                {
                    recursivelyAppendGroupSpecializationOperatorConversion(originalGroup.OriginalQuantity.Value);
                }
            }
        }

        private void AppendOutgoingOperatorConversion(Indentation indentation, NamedType vector, string behaviour)
        {
            if (vector == Data.Vector.AsNamedType())
            {
                return;
            }

            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.CastConversion(vector));

            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, $"public static {behaviour} operator {vector.Name}", "new(a.Components)", (Data.Vector.AsNamedType(), "a"));
        }

        private void AppendIngoingOperatorConversion(Indentation indentation, NamedType vector, string behaviour)
        {
            if (vector == Data.Vector.AsNamedType())
            {
                return;
            }

            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.AntidirectionalCastConversion(vector));

            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, $"public static {behaviour} operator {Data.Vector.Name}", "new(a.Components)", (vector, "a"));
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
