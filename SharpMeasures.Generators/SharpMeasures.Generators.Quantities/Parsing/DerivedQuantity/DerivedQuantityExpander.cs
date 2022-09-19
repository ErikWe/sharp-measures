namespace SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

using Microsoft.CodeAnalysis;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Vectors;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

public interface IDerivedQuantityExpandingDiagnostics
{
    public abstract Diagnostic? MalformedExpression(IDerivedQuantity definition);
    public abstract Diagnostic? IncompatibleQuantities(IDerivedQuantity definition);
    public abstract Diagnostic? DerivationUnexpectedlyResultsInScalar(IDerivedQuantity definition);
    public abstract Diagnostic? DerivationUnexpectedlyResultsInVector(IDerivedQuantity definition);
    public abstract Diagnostic? DerivationResultsInUnexpectedDimension(IDerivedQuantity definition, NamedType vector);
}

public sealed class DerivedQuantityExpander
{
    public static IOptionalWithDiagnostics<DerivedQuantityDefinition> Expand(NamedType resultingQuantity, IDerivedQuantity derivation, IVectorPopulation vectorPopulation, IDerivedQuantityExpandingDiagnostics diagnostics)
    {
        DerivedQuantityExpander expander = new(resultingQuantity, derivation, vectorPopulation, diagnostics);

        return expander.Expand();
    }

    private NamedType ResultingQuantity { get; }
    private IDerivedQuantity OriginalDerivation { get; }

    private IVectorPopulation VectorPopulation { get; }

    private IDerivedQuantityExpandingDiagnostics Diagnostics { get; }

    private IOptionalWithDiagnostics<INode> Root { get; }
    private QuantityType ResultingType { get; }
    private Dictionary<int, INode> LeafNodesByIndex { get; }
    private List<int> GroupIndices { get; }

    public DerivedQuantityExpander(NamedType resultingQuantity, IDerivedQuantity originalDerivation, IVectorPopulation vectorPopulation, IDerivedQuantityExpandingDiagnostics diagnostics)
    {
        ResultingQuantity = resultingQuantity;
        OriginalDerivation = originalDerivation;

        VectorPopulation = vectorPopulation;

        Diagnostics = diagnostics;

        GroupIndices = GetGroupIndices();
        LeafNodesByIndex = BuildLeafNodes();
        Root = BuildTree(OriginalDerivation.Expression.Replace(" ", string.Empty));

        if (Root.HasResult)
        {
            Root.Result.ReduceChoices();
        }

        ResultingType = Root.HasResult ? Root.Result.ResultingType() : QuantityType.None;
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

                leafs.Add(i, new GroupNode(i, groupMembers.GroupMembersByDimension.Transform(static (member) => member.Type.AsNamedType()), dimensions));

                continue;
            }

            if (VectorPopulation.VectorBases.TryGetValue(OriginalDerivation.Signature[i], out var vector))
            {
                leafs.Add(i, new VectorNode(i, OriginalDerivation.Signature[i], vector.Definition.Dimension));

                continue;
            }

            if (VectorPopulation.GroupMembers.TryGetValue(OriginalDerivation.Signature[i], out var member))
            {
                leafs.Add(i, new VectorNode(i, OriginalDerivation.Signature[i], member.Definition.Dimension));

                continue;
            }

            leafs.Add(i, new ScalarNode(i, OriginalDerivation.Signature[i]));
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
            right = GetLastFactor(expression);

            if (right == expression)
            {
                if (right[0] is '{' && right[right.Length - 1] is '}')
                {
                    return OptionalWithDiagnostics.Result(LeafNodesByIndex[int.Parse(right.Substring(1, right.Length - 2), CultureInfo.InvariantCulture)]);
                }

                if (right is "1")
                {
                    return OptionalWithDiagnostics.Result(new ConstantNode() as INode);
                }

                return OptionalWithDiagnostics.Empty<INode>(Diagnostics.MalformedExpression(OriginalDerivation));
            }
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
                return expression.Substring(i + 1, expression.Length - i - 1);
            }
        }

        return expression;
    }

    private static string GetLastFactor(string expression)
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

            if (parenthesisLevel is 0 && expression[i] is '*' or '/' or '.' or 'x')
            {
                return expression.Substring(i + 1, expression.Length - i - 1);
            }
        }

        return expression;
    }

    private IOptionalWithDiagnostics<DerivedQuantityDefinition> Expand()
    {
        if (Root.LacksResult)
        {
            return Root.AsEmptyOptional<DerivedQuantityDefinition>();
        }

        if (ResultingType is QuantityType.None)
        {
            return OptionalWithDiagnostics.Empty<DerivedQuantityDefinition>(Diagnostics.IncompatibleQuantities(OriginalDerivation));
        }

        var validity = ValidateResultingQuantity();
        
        if (validity.IsInvalid)
        {
            return validity.AsEmptyOptional<DerivedQuantityDefinition>();
        }

        List<ExpandedDerivedQuantity> scalarResults = new();
        Dictionary<int, List<ExpandedDerivedQuantity>> vectorResults = new();

        var trees = RecursivelyLimitGroups();

        HashSet<ReadOnlyEquatableList<NamedType>> detectedSignatures = new();

        foreach (var tree in trees)
        {
            var expression = tree.ComposeExpression();

            if (expression[0] is '(' && expression[expression.Length - 1] is ')')
            {
                expression = expression.Substring(1, expression.Length - 2);
            }

            var signature = new NamedType[OriginalDerivation.Signature.Count];
            tree.FillSignature(signature);

            if (detectedSignatures.Add(signature.AsReadOnlyEquatable()) is false)
            {
                continue;
            }

            ExpandedDerivedQuantity resolvedDerivation = new(expression, signature);

            if (ResultingType is QuantityType.Scalar)
            {
                scalarResults.Add(resolvedDerivation);

                continue;
            }

            var resultingDimension = tree.ResultingDimensions().Single();

            if (vectorResults.TryGetValue(resultingDimension, out var specificVectorResults))
            {
                specificVectorResults.Add(resolvedDerivation);

                continue;
            }

            vectorResults.Add(resultingDimension, new List<ExpandedDerivedQuantity>());

            vectorResults[resultingDimension].Add(resolvedDerivation);
        }

        if (scalarResults.Count is 0 && vectorResults.Values.SelectMany(static (list) => list).Any() is false)
        {
            return OptionalWithDiagnostics.Empty<DerivedQuantityDefinition>(Diagnostics.IncompatibleQuantities(OriginalDerivation));
        }

        DerivedQuantityDefinition expandedDefinition = new(OriginalDerivation.Expression, OriginalDerivation.Signature, OriginalDerivation.OperatorImplementation, OriginalDerivation.Permutations, scalarResults, vectorResults.Transform(static (list) => list as IReadOnlyList<IExpandedDerivedQuantity>), OriginalDerivation.Locations);

        return validity.Transform(expandedDefinition);
    }

    private IValidityWithDiagnostics ValidateResultingQuantity()
    {
        if (VectorPopulation.VectorBases.TryGetValue(ResultingQuantity, out var vectorBase))
        {
            if (ResultingType is not QuantityType.Vector)
            {
                return ValidityWithDiagnostics.Invalid(Diagnostics.DerivationUnexpectedlyResultsInScalar(OriginalDerivation));
            }

            if (Root.Result.ResultingDimensions().Where((dimension) => dimension == vectorBase.Definition.Dimension).Any() is false)
            {
                return ValidityWithDiagnostics.Invalid(Diagnostics.DerivationResultsInUnexpectedDimension(OriginalDerivation, ResultingQuantity));
            }

            return ValidityWithDiagnostics.Valid;
        }

        if (VectorPopulation.GroupMembers.TryGetValue(ResultingQuantity, out var member))
        {
            if (ResultingType is not QuantityType.Vector)
            {
                return ValidityWithDiagnostics.Invalid(Diagnostics.DerivationUnexpectedlyResultsInScalar(OriginalDerivation));
            }

            if (Root.Result.ResultingDimensions().Where((dimension) => dimension == member.Definition.Dimension).Any() is false)
            {
                return ValidityWithDiagnostics.Invalid(Diagnostics.DerivationResultsInUnexpectedDimension(OriginalDerivation, ResultingQuantity));
            }

            return ValidityWithDiagnostics.Valid;
        }

        if (VectorPopulation.GroupMembersByGroup.TryGetValue(ResultingQuantity, out var members))
        {
            if (ResultingType is not QuantityType.Vector)
            {
                return ValidityWithDiagnostics.Invalid(Diagnostics.DerivationUnexpectedlyResultsInScalar(OriginalDerivation));
            }

            if (new HashSet<int>(members.GroupMembersByDimension.Keys).Intersect(Root.Result.ResultingDimensions()).Any() is false)
            {
                return ValidityWithDiagnostics.Invalid(Diagnostics.DerivationResultsInUnexpectedDimension(OriginalDerivation, ResultingQuantity));
            }

            return ValidityWithDiagnostics.Valid;
        }

        return ValidityWithDiagnostics.Conditional(ResultingType is QuantityType.Scalar, () => Diagnostics.DerivationUnexpectedlyResultsInVector(OriginalDerivation));
    }

    private IEnumerable<INode> RecursivelyLimitGroups()
    {
        if (Root.LacksResult)
        {
            return Array.Empty<INode>();
        }

        if (GroupIndices.Count is 0)
        {
            return new[] { Root.Result };
        }

        return recurse(Root.Result, 0);

        IEnumerable<INode> recurse(INode root, int fromIndex)
        {
            var signatureIndex = GroupIndices[fromIndex];
            var dimensions = LeafNodesByIndex[signatureIndex].ResultingDimensions();

            foreach (var dimension in dimensions)
            {
                INode tree = dimensions.Count > 0 ? root.DeepCopy() : root;
                tree.LimitGroupWithIndex(signatureIndex, dimension);

                if (fromIndex == GroupIndices.Count - 1)
                {
                    if (tree.Verify())
                    {
                        yield return tree;
                    }

                    continue;
                }

                foreach (var result in recurse(tree, fromIndex + 1))
                {
                    yield return result;
                }
            }
        }
    }

    private interface INode
    {
        public abstract QuantityType ResultingType();
        public abstract IReadOnlyList<int> ResultingDimensions();

        public abstract INode DeepCopy();
        public abstract void ReduceChoices();
        public abstract void LimitResult(IReadOnlyList<int> dimensions);
        public abstract void LimitGroupWithIndex(int index, int dimension);

        public abstract bool Verify();
        public abstract string ComposeExpression();
        public abstract void FillSignature(IList<NamedType> signature);
    }

    private sealed class GroupNode : INode
    {
        private int SignatureIndex { get; }
        private IReadOnlyDictionary<int, NamedType> GroupPopulation { get; }

        private List<int> Dimensions { get; set; }

        public GroupNode(int signatureIndex, IReadOnlyDictionary<int, NamedType> groupPopulation, List<int> dimensions)
        {
            SignatureIndex = signatureIndex;
            GroupPopulation = groupPopulation;

            Dimensions = dimensions;
        }

        QuantityType INode.ResultingType() => QuantityType.Vector;
        IReadOnlyList<int> INode.ResultingDimensions() => Dimensions;

        public INode DeepCopy()
        {
            List<int> newList = new(Dimensions.Count);

            newList.AddRange(Dimensions);

            return new GroupNode(SignatureIndex, GroupPopulation, newList);
        }

        void INode.ReduceChoices() { }
        public void LimitResult(IReadOnlyList<int> dimensions) => Dimensions = new HashSet<int>(Dimensions).Intersect(dimensions).ToList();

        public void LimitGroupWithIndex(int index, int dimension)
        {
            if (SignatureIndex == index)
            {
                Dimensions = new() { dimension };
            }
        }

        bool INode.Verify() => true;
        public string ComposeExpression() => $$"""{{{SignatureIndex}}}""";
        public void FillSignature(IList<NamedType> signature) => signature[SignatureIndex] = GroupPopulation[Dimensions.Single()];
    }

    private sealed class VectorNode : INode
    {
        private int SignatureIndex { get; }
        private NamedType SignatureType { get; } 

        private int Dimension { get; set; }
        
        public VectorNode(int signatureIndex, NamedType signatureType, int dimension)
        {
            SignatureIndex = signatureIndex;
            SignatureType = signatureType;

            Dimension = dimension;
        }

        QuantityType INode.ResultingType() => QuantityType.Vector;
        IReadOnlyList<int> INode.ResultingDimensions() => new[] { Dimension };

        public INode DeepCopy() => new VectorNode(SignatureIndex, SignatureType, Dimension);
        void INode.ReduceChoices() { }
        void INode.LimitResult(IReadOnlyList<int> dimensions) { }
        void INode.LimitGroupWithIndex(int index, int dimension) { }

        bool INode.Verify() => true;
        public string ComposeExpression() => $$"""{{{SignatureIndex}}}""";
        public void FillSignature(IList<NamedType> signature) => signature[SignatureIndex] = SignatureType;
    }

    private sealed class ScalarNode : INode
    {
        private int SignatureIndex { get; }
        private NamedType SignatureType { get; }

        public ScalarNode(int signatureIndex, NamedType signatureType)
        {
            SignatureIndex = signatureIndex;
            SignatureType = signatureType;
        }

        QuantityType INode.ResultingType() => QuantityType.Scalar;
        IReadOnlyList<int> INode.ResultingDimensions() => Array.Empty<int>();

        public INode DeepCopy() => new ScalarNode(SignatureIndex, SignatureType);
        void INode.ReduceChoices() { }
        void INode.LimitResult(IReadOnlyList<int> dimensions) { }
        void INode.LimitGroupWithIndex(int index, int dimension) { }

        bool INode.Verify() => true;
        public string ComposeExpression() => $$"""{{{SignatureIndex}}}""";
        public void FillSignature(IList<NamedType> signature) => signature[SignatureIndex] = SignatureType;
    }

    private sealed class ConstantNode : INode
    {
        QuantityType INode.ResultingType() => QuantityType.Scalar;
        IReadOnlyList<int> INode.ResultingDimensions() => Array.Empty<int>();

        INode INode.DeepCopy() => this;
        void INode.ReduceChoices() { }
        void INode.LimitResult(IReadOnlyList<int> dimensions) { }
        void INode.LimitGroupWithIndex(int index, int dimension) { }

        bool INode.Verify() => true;
        string INode.ComposeExpression() => "1";
        void INode.FillSignature(IList<NamedType> signature) { }
    }

    private sealed class AdditionNode : INode
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

        public void LimitGroupWithIndex(int index, int dimension)
        {
            Left.LimitGroupWithIndex(index, dimension);
            Right.LimitGroupWithIndex(index, dimension);
        }

        bool INode.Verify()
        {
            if (ResultingType() is QuantityType.Scalar)
            {
                return true;
            }

            return Left.ResultingDimensions().Single() == Right.ResultingDimensions().Single();
        }

        public string ComposeExpression() => $"({Left.ComposeExpression()} + {Right.ComposeExpression()})";

        public void FillSignature(IList<NamedType> signature)
        {
            Left.FillSignature(signature);
            Right.FillSignature(signature);
        }
    }

    private sealed class SubtractionNode : INode
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

        public void LimitGroupWithIndex(int index, int dimension)
        {
            Left.LimitGroupWithIndex(index, dimension);
            Right.LimitGroupWithIndex(index, dimension);
        }

        bool INode.Verify()
        {
            if (ResultingType() is QuantityType.Scalar)
            {
                return true;
            }

            return Left.ResultingDimensions().Single() == Right.ResultingDimensions().Single();
        }

        public string ComposeExpression() => $"({Left.ComposeExpression()} - {Right.ComposeExpression()})";

        public void FillSignature(IList<NamedType> signature)
        {
            Left.FillSignature(signature);
            Right.FillSignature(signature);
        }
    }

    private sealed class MultplicationNode : INode
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

        public void LimitGroupWithIndex(int index, int dimension)
        {
            Left.LimitGroupWithIndex(index, dimension);
            Right.LimitGroupWithIndex(index, dimension);
        }

        bool INode.Verify() => true;

        public string ComposeExpression() => $"({Left.ComposeExpression()} * {Right.ComposeExpression()})";

        public void FillSignature(IList<NamedType> signature)
        {
            Left.FillSignature(signature);
            Right.FillSignature(signature);
        }
    }

    private sealed class DivisionNode : INode
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

        public void LimitGroupWithIndex(int index, int dimension)
        {
            Left.LimitGroupWithIndex(index, dimension);
            Right.LimitGroupWithIndex(index, dimension);
        }

        bool INode.Verify() => true;

        public string ComposeExpression() => $"({Left.ComposeExpression()} / {Right.ComposeExpression()})";

        public void FillSignature(IList<NamedType> signature)
        {
            Left.FillSignature(signature);
            Right.FillSignature(signature);
        }
    }

    private sealed class DotNode : INode
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

        void INode.LimitResult(IReadOnlyList<int> dimensions) { }

        public void LimitGroupWithIndex(int index, int dimension)
        {
            Left.LimitGroupWithIndex(index, dimension);
            Right.LimitGroupWithIndex(index, dimension);
        }

        bool INode.Verify() => Left.ResultingDimensions().Single() == Right.ResultingDimensions().Single();

        public string ComposeExpression()
        {
            int dimension = Left.ResultingDimensions().Single();

            return $"PureScalarMaths.Dot{dimension}({Left.ComposeExpression()}, {Right.ComposeExpression()})";
        }

        public void FillSignature(IList<NamedType> signature)
        {
            Left.FillSignature(signature);
            Right.FillSignature(signature);
        }
    }

    private sealed class CrossNode : INode
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

        public void LimitGroupWithIndex(int index, int dimension)
        {
            Left.LimitGroupWithIndex(index, dimension);
            Right.LimitGroupWithIndex(index, dimension);
        }

        bool INode.Verify() => Left.ResultingDimensions().Single() is 3 && Right.ResultingDimensions().Single() is 3;

        public string ComposeExpression()
        {
            return $"PureVector3Maths.Cross({Left.ComposeExpression()}, {Right.ComposeExpression()})";
        }

        public void FillSignature(IList<NamedType> signature)
        {
            Left.FillSignature(signature);
            Right.FillSignature(signature);
        }
    }

    private enum QuantityType { None, Scalar, Vector }
}
