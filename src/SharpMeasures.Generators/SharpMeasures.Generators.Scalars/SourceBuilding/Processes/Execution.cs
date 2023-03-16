namespace SharpMeasures.Generators.Scalars.SourceBuilding.Processes;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.SourceBuilding;

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

        var source = Composer.Compose(data.Value);

        if (source.Length is 0)
        {
            return;
        }

        context.AddSource($"{data.Value.Scalar.QualifiedName}.Processes.g.cs", SourceText.From(source, Encoding.UTF8));
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

        private HashSet<string> ImplementedProperties { get; } = new();
        private HashSet<string> ImplementedMethodNames { get; } = new();
        private HashSet<(string, IReadOnlyList<NamedType>)> ImplementedMethodSignatures { get; } = new();

        private Composer(DataModel data)
        {
            Data = data;

            SeparationHandler = new(Builder);
        }

        private void Compose()
        {
            StaticBuilding.AppendHeaderAndDirectives(Builder, Data.SourceBuildingContext.HeaderContent);

            NamespaceBuilding.AppendNamespace(Builder, Data.Scalar.Namespace);

            Builder.Append(Data.Scalar.ComposeDeclaration());

            BlockBuilding.AppendBlock(Builder, ComposeTypeBlock, originalIndentationLevel: 0, initialNewLine: true);
        }

        private string Retrieve()
        {
            return Builder.ToString();
        }

        private void ComposeTypeBlock(Indentation indentation)
        {
            SeparationHandler.MarkUnncecessary();

            foreach (var process in Data.Processes)
            {
                if (process.ImplementAsProperty)
                {
                    AppendProperty(indentation, process);
                }

                AppendMethod(indentation, process);
            }
        }

        private void AppendProperty(Indentation indentation, IQuantityProcess process)
        {
            if (ImplementedProperties.Add(process.Name) is false || ImplementedMethodNames.Contains(process.Name))
            {
                return;
            }

            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.Process(process));
            Builder.AppendLine($"{indentation}public {GetPotentialStaticKeyword(process)}{GetResultingType(process).FullyQualifiedName} {process.Name} => {process.Expression};");
        }

        private void AppendMethod(Indentation indentation, IQuantityProcess process)
        {
            if (ImplementedMethodSignatures.Add((process.Name, process.ParameterTypes)) is false || ImplementedProperties.Contains(process.Name))
            {
                return;
            }

            SeparationHandler.AddIfNecessary();

            ImplementedMethodNames.Add(process.Name);

            var methodNameAndModifiers = $"public {GetPotentialStaticKeyword(process)}{GetResultingType(process).FullyQualifiedName} {process.Name}";

            var parameters = new (NamedType, string)[process.ParameterTypes.Count];
            var parameterNames = GetParameterNames(process);

            for (var i = 0; i < parameters.Length; i++)
            {
                parameters[i] = (process.ParameterTypes[i], parameterNames[i]);
            }

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.Process(process));
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, process.Expression, parameters);
        }

        private static string GetPotentialStaticKeyword(IQuantityProcess process) => process.ImplementStatically ? "static " : string.Empty;
        private NamedType GetResultingType(IQuantityProcess process) => process.Result is null ? Data.Scalar.AsNamedType() : process.Result.Value;

        private static IReadOnlyList<string> GetParameterNames(IQuantityProcess process)
        {
            if (process.ParameterNames.Count == process.ParameterTypes.Count)
            {
                return process.ParameterNames;
            }

            Dictionary<string, int> counts = new();

            foreach (var parameterType in process.ParameterTypes)
            {
                countParameter(parameterType);
            }

            var parameterNames = new string[process.ParameterTypes.Count];

            var index = 0;

            foreach (var parameterType in process.ParameterTypes)
            {
                var name = SourceBuildingUtility.ToParameterName(parameterType.Name);
                name = appendParameterNumber(name, parameterType);

                parameterNames[index] = name;
                index += 1;
            }

            return parameterNames;

            void countParameter(NamedType parameterType)
            {
                if (counts.TryGetValue(parameterType.Name, out var count))
                {
                    counts[parameterType.Name] = count - 1;

                    return;
                }

                counts[parameterType.Name] = -1;
            }

            string appendParameterNumber(string text, NamedType parameterType)
            {
                var count = counts[parameterType.Name];

                if (count == -1)
                {
                    return text;
                }

                if (count < 0)
                {
                    counts[parameterType.Name] = 1;
                    return $"{text}1";
                }

                counts[parameterType.Name] += 1;
                return $"{text}{counts[parameterType.Name]}";
            }
        }

        private void AppendDocumentation(Indentation indentation, string text) => DocumentationBuilding.AppendDocumentation(Builder, indentation, text);
    }
}
