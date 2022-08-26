namespace SharpMeasures.Generators.Vectors.Pipelines.Vector.Conversions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.SourceBuilding;
using SharpMeasures.Generators.Utility;

using System;
using System.Linq;
using System.Text;

internal static class Execution
{
    public static void Execute(SourceProductionContext context, DataModel data)
    {
        string source = Composer.Compose(data);

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
            return Builder.ToString();
        }

        private void ComposeTypeBlock(Indentation indentation)
        {
            SeparationHandler.MarkUnncecessary();

            foreach (var convertibleVectorGroup in Data.Conversions.SelectMany(static (x) => x.Vectors))
            {
                if (Data.VectorPopulation.Groups.TryGetValue(convertibleVectorGroup, out var group))
                {
                    if (group.MembersByDimension.TryGetValue(Data.Dimension, out var correspondingMember))
                    {
                        AppendInstanceConversions(correspondingMember, indentation);
                    }
                }
            }

            foreach (var convertibleVectorGroups in Data.Conversions)
            {
                if (convertibleVectorGroups.CastOperatorBehaviour is ConversionOperatorBehaviour.None)
                {
                    continue;
                }

                Action<NamedType, Indentation> composer = convertibleVectorGroups.CastOperatorBehaviour switch
                {
                    ConversionOperatorBehaviour.Explicit => AppendExplicitOperatorConversion,
                    ConversionOperatorBehaviour.Implicit => AppendImplicitOperatorConversion,
                    _ => throw new NotSupportedException("Invalid cast operation")
                };

                foreach (var convertibleVectorGroup in convertibleVectorGroups.Vectors)
                {
                    if (Data.VectorPopulation.Groups.TryGetValue(convertibleVectorGroup, out var group))
                    {
                        if (group.MembersByDimension.TryGetValue(Data.Dimension, out var correspondingMember))
                        {
                            composer(correspondingMember, indentation);
                        }
                    }
                }
            }
        }

        private void AppendInstanceConversions(NamedType vectorGroupMember, Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.Documentation.Conversion(vectorGroupMember));
            Builder.AppendLine($"{indentation}public {vectorGroupMember.FullyQualifiedName} As{vectorGroupMember.Name} => new(Components);");
        }

        private void AppendExplicitOperatorConversion(NamedType vectorGroupMember, Indentation indentation)
            => AppendOperatorConversion(vectorGroupMember, indentation, "explicit");

        private void AppendImplicitOperatorConversion(NamedType vectorGroupMember, Indentation indentation)
            => AppendOperatorConversion(vectorGroupMember, indentation, "implicit");

        private void AppendOperatorConversion(NamedType vectorGroupMember, Indentation indentation, string behaviour)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.Documentation.CastConversion(vectorGroupMember));

            var methodNameAndModifiers = $"public static {behaviour} operator {vectorGroupMember.Name}";
            var expression = "new(a.Components)";
            var parameters = new[] { (Data.Vector.AsNamedType(), "a") };

            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, parameters);
        }

        private void AppendDocumentation(Indentation indentation, string text)
        {
            DocumentationBuilding.AppendDocumentation(Builder, indentation, text);
        }
    }
}
