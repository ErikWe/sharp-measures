namespace SharpMeasures.Generators.Scalars.Pipelines.Operations;

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

        context.AddSource($"{data.Value.Scalar.QualifiedName}.Operations.g.cs", SourceText.From(source, Encoding.UTF8));
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

        private InterfaceCollector InterfaceCollector { get; }

        private HashSet<(NamedType, OperatorType, NamedType)> ImplementedOperators { get; } = new();
        private HashSet<(string, NamedType)> ImplementedMethods { get; } = new();

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
            if (ImplementedOperators.Count is 0 && ImplementedMethods.Count is 0)
            {
                return string.Empty;
            }

            return Builder.ToString();
        }

        private void ComposeTypeBlock(Indentation indentation)
        {
            SeparationHandler.MarkUnncecessary();

            foreach (var operation in Data.Operations)
            {
                if (operation.Implementation is QuantityOperationImplementation.OperatorAndMethod or QuantityOperationImplementation.Method)
                {
                    AppendMethodOperation(indentation, operation);
                }
            }

            foreach (var operation in Data.Operations)
            {
                if (operation.Implementation is QuantityOperationImplementation.OperatorAndMethod or QuantityOperationImplementation.Operator)
                {
                    AppendOperatorOperation(indentation, operation);
                }
            }
        }

        private void AppendMethodOperation(Indentation indentation, IQuantityOperation operation)
        {
            foreach ((var result, var other) in GetQuantityCombinations(operation))
            {
                AppendMethodOperation(indentation, operation, result, other);
            }
        }

        private void AppendMethodOperation(Indentation indentation, IQuantityOperation operation, NamedType result, NamedType other)
        {
            var operatorSymbol = GetOperatorSymbol(operation.OperatorType);
            var parameterName = SourceBuildingUtility.ToParameterName(other.Name);

            if (ImplementedMethods.Add((operation.MethodName, other)))
            {
                SeparationHandler.AddIfNecessary();
                
                var methodNameAndModifiers = $"public {result.FullyQualifiedName} {operation.MethodName}";

                AppendDocumentation(indentation, Data.Documentation.OperationMethod(operation, other));
                StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, fullExpression(), new[] { (other, parameterName) });
            }

            if (operation.Mirror && operation.OperatorType is OperatorType.Subtraction or OperatorType.Division && ImplementedMethods.Add((operation.MirroredMethodName, other)))
            {
                SeparationHandler.AddIfNecessary();

                var mirroredMethodNameAndModifiers = $"public {result.FullyQualifiedName} {operation.MirroredMethodName}";

                AppendDocumentation(indentation, Data.Documentation.MirroredOperationMethod(operation, other));
                StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, mirroredMethodNameAndModifiers, mirroredExpression(), new[] { (other, parameterName) });
            }

            string fullExpression()
            {
                if (operation.Position is OperatorPosition.Left)
                {
                    return $"new(Magnitude {operatorSymbol} {otherExpression()})";
                }

                return $"new({otherExpression()} {operatorSymbol} Magnitude)";
            }

            string mirroredExpression()
            {
                if (operation.Position is OperatorPosition.Right)
                {
                    return $"new(Magnitude {operatorSymbol} {otherExpression()})";
                }

                return $"new({otherExpression()} {operatorSymbol} Magnitude)";
            }

            string otherExpression()
            {
                if (other.FullyQualifiedName is "global::SharpMeasures.Scalar" || other.FullyQualifiedName.StartsWith("global::SharpMeasures.Vector", StringComparison.InvariantCulture))
                {
                    return $"{parameterName}";
                }

                if (Data.ScalarPopulation.Scalars.ContainsKey(other))
                {
                    return $"{parameterName}.Magnitude";
                }

                return $"{parameterName}.Components";
            }
        }

        private void AppendOperatorOperation(Indentation indentation, IQuantityOperation operation)
        {
            foreach ((var result, var other) in GetQuantityCombinations(operation))
            {
                AppendOperatorOperation(indentation, operation, result, other);
            }
        }

        private void AppendOperatorOperation(Indentation indentation, IQuantityOperation operation, NamedType result, NamedType other)
        {
            var operatorSymbol = GetOperatorSymbol(operation.OperatorType);

            var methodNameAndModifiers = $"public static {result.FullyQualifiedName} operator {operatorSymbol}";

            if (operation.Position is OperatorPosition.Left || operation.Mirror && ImplementedOperators.Add((Data.Scalar.AsNamedType(), operation.OperatorType, other)))
            {
                var parameters = new[] { (Data.Scalar.AsNamedType(), "a"), (other, "b") };

                AppendDocumentation(indentation, Data.Documentation.OperationOperator(operation, other));
                StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, fullExpression(), parameters);
            }

            if (operation.Position is OperatorPosition.Right || operation.Mirror && ImplementedOperators.Add((other, operation.OperatorType, Data.Scalar.AsNamedType())))
            {
                var parameters = new[] { (other, "a"), (Data.Scalar.AsNamedType(), "b") };

                AppendDocumentation(indentation, Data.Documentation.MirroredOperationOperator(operation, other));
                StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, mirroredExpression(), parameters);
            }

            string fullExpression()
            {
                if (operation.Position is OperatorPosition.Left)
                {
                    return $"new(a.Magnitude {operatorSymbol} {otherExpression("b")})";
                }

                return $"new({otherExpression("a")} {operatorSymbol} b.Magnitude)";
            }

            string mirroredExpression()
            {
                if (operation.Position is OperatorPosition.Right)
                {
                    return $"new(a.Magnitude {operatorSymbol} {otherExpression("b")})";
                }

                return $"new({otherExpression("a")} {operatorSymbol} b.Magnitude)";
            }

            string otherExpression(string parameterName)
            {
                if (other.FullyQualifiedName is "global::SharpMeasures.Scalar" || other.FullyQualifiedName.StartsWith("global::SharpMeasures.Vector", StringComparison.InvariantCulture))
                {
                    return $"{parameterName}";
                }

                if (Data.ScalarPopulation.Scalars.ContainsKey(other))
                {
                    return $"{parameterName}.Magnitude";
                }

                return $"{parameterName}.Components";
            }
        }

        private static string GetOperatorSymbol(OperatorType operatorType) => operatorType switch
        {
            OperatorType.Addition => "+",
            OperatorType.Subtraction => "-",
            OperatorType.Multiplication => "*",
            OperatorType.Division => "/",
            _ => throw new NotSupportedException($"Invalid {typeof(OperatorType).Name}: {operatorType}")
        };

        private IEnumerable<(NamedType Result, NamedType Other)> GetQuantityCombinations(IQuantityOperation operation)
        {
            if (Data.ScalarPopulation.Scalars.ContainsKey(operation.Result))
            {
                return new[] { (operation.Result, operation.Other) };
            }

            if (Data.VectorPopulation.Vectors.TryGetValue(operation.Result, out var resultVector))
            {
                if (Data.VectorPopulation.Vectors.ContainsKey(operation.Other))
                {
                    return new[] { (operation.Result, operation.Other) };
                }

                if (Data.VectorPopulation.Groups.TryGetValue(operation.Other, out var otherGroup))
                {
                    if (otherGroup.MembersByDimension.TryGetValue(resultVector.Dimension, out var otherMember))
                    {
                        return new[] { (operation.Result, otherMember) };
                    }
                }
            }

            if (Data.VectorPopulation.Groups.TryGetValue(operation.Result, out var resultGroup))
            {
                if (Data.VectorPopulation.Vectors.TryGetValue(operation.Other, out var otherVector))
                {
                    if (resultGroup.MembersByDimension.TryGetValue(otherVector.Dimension, out var resultMember))
                    {
                        return new[] { (resultMember, operation.Other) };
                    }
                }

                if (Data.VectorPopulation.Groups.TryGetValue(operation.Other, out var otherGroup))
                {
                    foreach (var dimension in resultGroup.MembersByDimension.Keys)
                    {
                        if (otherGroup.MembersByDimension.TryGetValue(dimension, out var otherMember))
                        {
                            return new[] { (resultGroup.MembersByDimension[dimension], otherMember) };
                        }
                    }
                }
            }

            return Array.Empty<(NamedType, NamedType)>();
        }

        private void AppendDocumentation(Indentation indentation, string text) => DocumentationBuilding.AppendDocumentation(Builder, indentation, text);
    }
}
