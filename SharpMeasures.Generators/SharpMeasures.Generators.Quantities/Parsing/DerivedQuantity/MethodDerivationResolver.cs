namespace SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Vectors;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

public interface IMethodDerivationResolutionDiagnostics
{
    public abstract Diagnostic MalformedExpression();
}

public class MethodDerivationResolver
{
    public static IEnumerable<ResolvedMethodDerivation> Resolve(IDerivedQuantity derivation, IVectorPopulation vectorPopulation, IMethodDerivationResolutionDiagnostics diagnostics)
    {
        MethodDerivationResolver resolver = new(derivation, vectorPopulation, diagnostics);

        return resolver.RecursivelyResolve();
    }

    private IDerivedQuantity OriginalDerivation { get; }

    private IVectorPopulation VectorPopulation { get; }

    private IMethodDerivationResolutionDiagnostics Diagnostics { get; }

    private IOptionalWithDiagnostics<INode> Root { get; }
    private Dictionary<int, INode> LeafNodesByIndex { get; }
    private List<int> GroupIndices { get; }

    public MethodDerivationResolver(IDerivedQuantity originalDerivation, IVectorPopulation vectorPopulation, IMethodDerivationResolutionDiagnostics diagnostics)
    {
        OriginalDerivation = originalDerivation;

        VectorPopulation = vectorPopulation;

        Diagnostics = diagnostics;

        GroupIndices = GetGroupIndices();
        LeafNodesByIndex = BuildLeafNodes();
        Root = BuildTree(OriginalDerivation.Expression.Replace(" ", string.Empty));

        Root.Result.ReduceChoices();
    }

    private List<int> GetGroupIndices()
    {
        List<int> groupIndices = new();

        for (int i = 0; i < OriginalDerivation.Signature.Count; i++)
        {
            if (VectorPopulation.Groups.ContainsKey(OriginalDerivation.Signature[i]))
            {
                groupIndices.Add(i);
            }
        }

        return groupIndices;
    }

    private Dictionary<int, INode> BuildLeafNodes()
    {
        Dictionary<int, INode> leafs = new(OriginalDerivation.Signature.Count);

        for (int i = 0; i < OriginalDerivation.Signature.Count; i++)
        {
            if (VectorPopulation.GroupMembersByGroup.TryGetValue(OriginalDerivation.Signature[i], out var groupMembers))
            {
                var dimensions = groupMembers.GroupMembersByDimension.Keys.ToList();

                leafs.Add(i, new VectorNode(i, dimensions));

                continue;
            }

            if (VectorPopulation.VectorBases.TryGetValue(OriginalDerivation.Signature[i], out var vector))
            {
                leafs.Add(i, new VectorNode(i, new List<int>() { vector.Definition.Dimension }));

                continue;
            }

            leafs.Add(i, new ScalarNode(i));
        }

        return leafs;
    }

    private IOptionalWithDiagnostics<INode> BuildTree(string expression)
    {
        if (expression[0] is '(' && expression[expression.Length - 1] is ')')
        {
            return BuildTree(expression.Substring(1, expression.Length - 2));
        }

        var right = GetLastTerm(expression);

        if (right == expression)
        {
            if (right[0] is '{' && right[right.Length - 1] is '}')
            {
                return OptionalWithDiagnostics.Result(LeafNodesByIndex[int.Parse(right.Substring(0, right.Length - 2), CultureInfo.InvariantCulture)]);
            }

            return OptionalWithDiagnostics.Empty<INode>(Diagnostics.MalformedExpression());
        }

        var rightNode = BuildTree(right);

        if (rightNode.LacksResult)
        {
            return rightNode;
        }

        var leftNode = BuildTree(expression.Substring(0, expression.Length - right.Length - 1));

        if (leftNode.LacksResult)
        {
            return leftNode;
        }

        var operatorSymbol = expression[expression.Length - right.Length - 1];

        INode node = operatorSymbol switch
        {
            '+' => new AdditionNode(leftNode.Result, rightNode.Result),
            '-' => new SubtractionNode(leftNode.Result, rightNode.Result),
            '*' => new MultplicationNode(leftNode.Result, rightNode.Result),
            '/' => new DivisionNode(leftNode.Result, rightNode.Result),
            '.' => new DotNode(leftNode.Result, rightNode.Result),
            'x' => new CrossNode(leftNode.Result, rightNode.Result),
            _ => throw new NotSupportedException($"Unsupported quantity derivation operator: \"{operatorSymbol}\"")
        };

        return OptionalWithDiagnostics.Result(node);
    }

    private static string GetLastTerm(string expression)
    {
        int parenthesisLevel = 0;

        for (int i = expression.Length - 1; i >= 0; i--)
        {
            if (expression[i] is ')')
            {
                parenthesisLevel += 1;

                continue;
            }

            if (expression[i] is '(')
            {
                parenthesisLevel -= 1;

                continue;
            }

            if (parenthesisLevel is 0 && expression[i] is '+' or '-')
            {
                return expression.Substring(i, expression.Length - i + 1);
            }
        }

        return expression;
    }

    private IEnumerable<ResolvedMethodDerivation> RecursivelyResolve()
    {
        // TODO: Recurse with Root.DeepCopy(), 
    }

    private interface INode
    {
        public abstract QuantityType ResultingType();
        public abstract IReadOnlyList<int> ResultingDimensions();

        public abstract INode DeepCopy();
        public abstract void ReduceChoices();
        public abstract void LimitResult(IReadOnlyList<int> dimensions);
        public abstract string ComposeText();
    }

    private class VectorNode : INode
    {
        private int SignatureIndex { get; }

        private List<int> Dimensions { get; set; }
        
        public VectorNode(int signatureIndex, List<int> dimensions)
        {
            SignatureIndex = signatureIndex;

            Dimensions = dimensions;
        }

        QuantityType INode.ResultingType() => QuantityType.Vector;

        IReadOnlyList<int> INode.ResultingDimensions() => Dimensions;

        public INode DeepCopy()
        {
            List<int> newList = new(Dimensions.Count);

            newList.AddRange(Dimensions);

            return new VectorNode(SignatureIndex, newList);
        }

        void INode.ReduceChoices() { }
        public void LimitResult(IReadOnlyList<int> dimensions) => Dimensions = new HashSet<int>(Dimensions).Intersect(dimensions).ToList();

        public string ComposeText() => $$"""{{{SignatureIndex}}}""";
    }

    private class ScalarNode : INode
    {
        private int SignatureIndex { get; }

        public ScalarNode(int signatureIndex)
        {
            SignatureIndex = signatureIndex;
        }

        QuantityType INode.ResultingType() => QuantityType.Scalar;
        IReadOnlyList<int> INode.ResultingDimensions() => Array.Empty<int>();

        public INode DeepCopy() => new ScalarNode(SignatureIndex);
        void INode.ReduceChoices() { }
        void INode.LimitResult(IReadOnlyList<int> _) { }

        public string ComposeText() => $$"""{{{SignatureIndex}}}""";
    }

    private class AdditionNode : INode
    {
        private INode Left { get; }
        private INode Right { get; }

        public AdditionNode(INode left, INode right)
        {
            Left = left;
            Right = right;
        }

        public QuantityType ResultingType()
        {
            var leftResult = Left.ResultingType();
            var rightResult = Right.ResultingType();

            if (leftResult is QuantityType.None || rightResult is QuantityType.None || leftResult != rightResult)
            {
                return QuantityType.None;
            }

            return leftResult;
        }

        public IReadOnlyList<int> ResultingDimensions() => new HashSet<int>(Left.ResultingDimensions()).Intersect(Right.ResultingDimensions()).ToList();

        public INode DeepCopy() => new AdditionNode(Left.DeepCopy(), Right.DeepCopy());

        public void ReduceChoices()
        {
            LimitResult(ResultingDimensions());

            Left.ReduceChoices();
            Right.ReduceChoices();
        }
        
        public void LimitResult(IReadOnlyList<int> dimensions)
        {
            Left.LimitResult(dimensions);
            Right.LimitResult(dimensions);
        }

        public string ComposeText() => $"({Left.ComposeText()}) + ({Right.ComposeText()})";
    }

    private class SubtractionNode : INode
    {
        private INode Left { get; }
        private INode Right { get; }

        public SubtractionNode(INode left, INode right)
        {
            Left = left;
            Right = right;
        }

        public QuantityType ResultingType()
        {
            var leftResult = Left.ResultingType();
            var rightResult = Right.ResultingType();

            if (leftResult is QuantityType.None || rightResult is QuantityType.None || leftResult != rightResult)
            {
                return QuantityType.None;
            }

            return leftResult;
        }

        public IReadOnlyList<int> ResultingDimensions() => new HashSet<int>(Left.ResultingDimensions()).Intersect(Right.ResultingDimensions()).ToList();

        public INode DeepCopy() => new SubtractionNode(Left.DeepCopy(), Right.DeepCopy());

        public void ReduceChoices()
        {
            LimitResult(ResultingDimensions());

            Left.ReduceChoices();
            Right.ReduceChoices();
        }

        public void LimitResult(IReadOnlyList<int> dimensions)
        {
            Left.LimitResult(dimensions);
            Right.LimitResult(dimensions);
        }

        public string ComposeText() => $"({Left.ComposeText()}) - ({Right.ComposeText()})";
    }

    private class MultplicationNode : INode
    {
        private INode Left { get; }
        private INode Right { get; }

        public MultplicationNode(INode left, INode right)
        {
            Left = left;
            Right = right;
        }

        public QuantityType ResultingType()
        {
            var leftResult = Left.ResultingType();
            var rightResult = Right.ResultingType();

            if (leftResult is QuantityType.None || rightResult is QuantityType.None || leftResult is QuantityType.Vector && rightResult is QuantityType.Vector)
            {
                return QuantityType.None;
            }

            if (leftResult is QuantityType.Vector || rightResult is QuantityType.Vector)
            {
                return QuantityType.Vector;
            }

            return QuantityType.Scalar;
        }

        public IReadOnlyList<int> ResultingDimensions() => new HashSet<int>(Left.ResultingDimensions()).Union(Right.ResultingDimensions()).ToList();

        public INode DeepCopy() => new MultplicationNode(Left.DeepCopy(), Right.DeepCopy());

        public void ReduceChoices()
        {
            Left.ReduceChoices();
            Right.ReduceChoices();
        }

        public void LimitResult(IReadOnlyList<int> dimensions)
        {
            Left.LimitResult(dimensions);
            Right.LimitResult(dimensions);
        }

        public string ComposeText() => $"({Left.ComposeText()}) * ({Right.ComposeText()})";
    }

    private class DivisionNode : INode
    {
        private INode Left { get; }
        private INode Right { get; }

        public DivisionNode(INode left, INode right)
        {
            Left = left;
            Right = right;
        }

        public QuantityType ResultingType()
        {
            var leftResult = Left.ResultingType();
            var rightResult = Right.ResultingType();

            if (leftResult is QuantityType.None || rightResult is QuantityType.None || rightResult is QuantityType.Vector)
            {
                return QuantityType.None;
            }

            return leftResult;
        }

        public IReadOnlyList<int> ResultingDimensions() => Left.ResultingDimensions();

        public INode DeepCopy() => new DivisionNode(Left.DeepCopy(), Right.DeepCopy());

        public void ReduceChoices()
        {
            Left.ReduceChoices();
            Right.ReduceChoices();
        }

        public void LimitResult(IReadOnlyList<int> dimensions)
        {
            Left.LimitResult(dimensions);
        }

        public string ComposeText() => $"({Left.ComposeText()}) / ({Right.ComposeText()})";
    }

    private class DotNode : INode
    {
        private INode Left { get; }
        private INode Right { get; }

        public DotNode(INode left, INode right)
        {
            Left = left;
            Right = right;
        }

        public QuantityType ResultingType()
        {
            var leftResult = Left.ResultingType();
            var rightResult = Right.ResultingType();

            if (leftResult is not QuantityType.Vector || rightResult is not QuantityType.Vector)
            {
                return QuantityType.None;
            }

            return QuantityType.Scalar;
        }

        IReadOnlyList<int> INode.ResultingDimensions() => Array.Empty<int>();

        public INode DeepCopy() => new DotNode(Left.DeepCopy(), Right.DeepCopy());

        public void ReduceChoices()
        {
            var commonDimensions = new HashSet<int>(Left.ResultingDimensions()).Intersect(Right.ResultingDimensions()).ToList();

            Left.LimitResult(commonDimensions);
            Right.LimitResult(commonDimensions);

            Left.ReduceChoices();
            Right.ReduceChoices();
        }

        void INode.LimitResult(IReadOnlyList<int> _) { }

        public string ComposeText()
        {
            int dimension = Left.ResultingDimensions().Single();

            return $"PureScalarMaths.Dot{dimension}({Left.ComposeText()}, {Right.ComposeText()})";
        }
    }

    private class CrossNode : INode
    {
        private INode Left { get; }
        private INode Right { get; }

        public CrossNode(INode left, INode right)
        {
            Left = left;
            Right = right;
        }

        public QuantityType ResultingType()
        {
            var leftResult = Left.ResultingType();
            var rightResult = Right.ResultingType();

            if (leftResult is not QuantityType.Vector || rightResult is not QuantityType.Vector)
            {
                return QuantityType.None;
            }

            return QuantityType.Vector;
        }

        IReadOnlyList<int> INode.ResultingDimensions() => new HashSet<int>(new[] { 3 }).Intersect(Left.ResultingDimensions()).Intersect(Right.ResultingDimensions()).ToList();

        public INode DeepCopy() => new CrossNode(Left.DeepCopy(), Right.DeepCopy());

        void INode.ReduceChoices() { }

        public void LimitResult(IReadOnlyList<int> dimensions)
        {
            if (dimensions.Contains(3))
            {
                return;
            }

            Left.LimitResult(dimensions);
            Right.LimitResult(dimensions);
        }

        public string ComposeText()
        {
            return $"PureVector3Maths.Cross({Left.ComposeText()}, {Right.ComposeText()})";
        }
    }

    private enum QuantityType { None, Scalar, Vector }
}
