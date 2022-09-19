namespace SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

using System;
using System.Collections.Generic;

public sealed class ScalarOperatorDerivationSearcher
{
    public static IReadOnlyList<OperatorDerivation> GetDerivations(NamedType quantity, string expression, IReadOnlyList<NamedType> signature, DerivationOperatorImplementation operatorImplementation)
    {
        var searcher = new ScalarOperatorDerivationSearcher(quantity, expression, signature, operatorImplementation);

        return searcher.GetDerivations();
    }

    private NamedType Quantity { get; }

    private string Expression { get; }
    private IReadOnlyList<NamedType> Signature { get; }
    private DerivationOperatorImplementation OperatorImplementation { get; }

    private OperatorType OriginalOperatorType { get; }
    private OperatorType OppositeOperatorType { get; }

    private ScalarOperatorDerivationSearcher(NamedType quantity, string expression, IReadOnlyList<NamedType> signature, DerivationOperatorImplementation operatorImplementation)
    {
        Quantity = quantity;

        Expression = expression;
        Signature = signature;
        OperatorImplementation = operatorImplementation;

        if (OperatorImplementation is DerivationOperatorImplementation.None)
        {
            return;
        }

        OriginalOperatorType = GetOperatorType(Expression);
        OppositeOperatorType = GetOppositeOperatorType(OriginalOperatorType);
    }

    private IReadOnlyList<OperatorDerivation> GetDerivations()
    {
        if (OperatorImplementation is DerivationOperatorImplementation.None)
        {
            return Array.Empty<OperatorDerivation>();
        }

        if (Signature.Count is 1)
        {
            return GetSingleQuantityDerivations();
        }

        return GetDoubleQuantityDerivations();
    }

    private IReadOnlyList<OperatorDerivation> GetSingleQuantityDerivations()
    {
        List<OperatorDerivation> derivations = new(3);

        NamedType pureScalar = new("Scalar", "SharpMeasures", "SharpMeasures.Base", true);

        if (ShouldImplementLHS)
        {
            derivations.Add(new OperatorDerivation(pureScalar, OperatorType.Multiplication, Quantity, Signature[0]));
        }

        if (ShouldImplementRHS(Signature[0]))
        {
            derivations.Add(new OperatorDerivation(pureScalar, OperatorType.Multiplication, Signature[0], Quantity));
        }

        derivations.Add(new OperatorDerivation(Signature[0], OperatorType.Division, pureScalar, Quantity));

        return derivations;
    }

    private IReadOnlyList<OperatorDerivation> GetDoubleQuantityDerivations()
    {
        bool orderedExpression = Expression.IndexOf("0", StringComparison.InvariantCulture) < Expression.IndexOf("1", StringComparison.InvariantCulture);

        List<OperatorDerivation> derivations = new(3);

        if (OriginalOperatorType is OperatorType.Addition or OperatorType.Multiplication)
        {
            if (ShouldImplementLHS)
            {
                derivations.Add(new OperatorDerivation(Signature[0], OppositeOperatorType, Quantity, Signature[1]));
                derivations.Add(new OperatorDerivation(Signature[1], OppositeOperatorType, Quantity, Signature[0]));
            }

            return derivations;
        }

        if (OriginalOperatorType is OperatorType.Subtraction or OperatorType.Division)
        {
            if (orderedExpression)
            {
                if (ShouldImplementLHS)
                {
                    derivations.Add(new OperatorDerivation(Signature[0], OppositeOperatorType, Quantity, Signature[1]));
                }

                if (ShouldImplementRHS(Signature[1]))
                {
                    derivations.Add(new OperatorDerivation(Signature[0], OppositeOperatorType, Signature[1], Quantity));
                }

                if (ShouldImplementRHS(Signature[0]))
                {
                    derivations.Add(new OperatorDerivation(Signature[1], OriginalOperatorType, Signature[0], Quantity));
                }

                return derivations;
            }

            if (ShouldImplementLHS)
            {
                derivations.Add(new OperatorDerivation(Signature[1], OppositeOperatorType, Quantity, Signature[0]));
            }

            if (ShouldImplementRHS(Signature[0]))
            {
                derivations.Add(new OperatorDerivation(Signature[1], OppositeOperatorType, Signature[0], Quantity));
            }

            if (ShouldImplementRHS(Signature[1]))
            {
                derivations.Add(new OperatorDerivation(Signature[0], OriginalOperatorType, Signature[1], Quantity));
            }
        }

        return derivations;
    }

    private static OperatorType GetOperatorType(string expression)
    {
        if (expression.Contains("+"))
        {
            return OperatorType.Addition;
        }

        if (expression.Contains("-"))
        {
            return OperatorType.Subtraction;
        }

        if (expression.Contains("*"))
        {
            return OperatorType.Multiplication;
        }

        return OperatorType.Division;
    }

    private static OperatorType GetOppositeOperatorType(OperatorType originalOperatorType) => originalOperatorType switch
    {
        OperatorType.Addition => OperatorType.Subtraction,
        OperatorType.Subtraction => OperatorType.Addition,
        OperatorType.Multiplication => OperatorType.Division,
        OperatorType.Division => OperatorType.Multiplication,
        _ => throw new NotSupportedException($"Unrecognized {typeof(OperatorType).Name}: {originalOperatorType}")
    };

    private bool ShouldImplementLHS => OperatorImplementation is DerivationOperatorImplementation.All or DerivationOperatorImplementation.LeftHandSide or DerivationOperatorImplementation.Suitable;
    private bool ShouldImplementRHS(NamedType lhs) => OperatorImplementation is DerivationOperatorImplementation.All or DerivationOperatorImplementation.RightHandSide || OperatorImplementation is DerivationOperatorImplementation.Suitable && lhs.Assembly != Quantity.Assembly;
}
