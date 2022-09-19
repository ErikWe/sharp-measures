namespace SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

using SharpMeasures.Generators.Scalars;

using System;
using System.Collections.Generic;

public sealed class VectorOperatorDerivationSearcher
{
    public static IReadOnlyList<OperatorDerivation> GetDerivations(NamedType quantity, string expression, IReadOnlyList<NamedType> signature, DerivationOperatorImplementation operatorImplementation, IResolvedScalarPopulation scalarPopulation)
    {
        var firstElementIsVector = scalarPopulation.Scalars.ContainsKey(signature[0]) is false;

        var searcher = new VectorOperatorDerivationSearcher(quantity, expression, signature, operatorImplementation, firstElementIsVector);

        return searcher.GetDerivations();
    }

    private NamedType Quantity { get; }

    private string Expression { get; }
    private IReadOnlyList<NamedType> Signature { get; }
    private DerivationOperatorImplementation OperatorImplementation { get; }
    private bool FirstElementIsVector { get; }

    private OperatorType OriginalOperatorType { get; }

    private VectorOperatorDerivationSearcher(NamedType quantity, string expression, IReadOnlyList<NamedType> signature, DerivationOperatorImplementation operatorImplementation, bool firstElementIsVector)
    {
        Quantity = quantity;

        Expression = expression;
        Signature = signature;
        OperatorImplementation = operatorImplementation;
        FirstElementIsVector = firstElementIsVector;

        if (OperatorImplementation is DerivationOperatorImplementation.None)
        {
            return;
        }

        OriginalOperatorType = GetOperatorType(Expression);
    }

    private IReadOnlyList<OperatorDerivation> GetDerivations()
    {
        if (OperatorImplementation is DerivationOperatorImplementation.None)
        {
            return Array.Empty<OperatorDerivation>();
        }

        return GetQuantityDerivations();
    }

    private IReadOnlyList<OperatorDerivation> GetQuantityDerivations()
    {
        bool orderedExpression = Expression.IndexOf("0", StringComparison.InvariantCulture) < Expression.IndexOf("1", StringComparison.InvariantCulture);

        List<OperatorDerivation> derivations = new(3);

        if (OriginalOperatorType is OperatorType.Addition)
        {
            if (ShouldImplementLHS)
            {
                derivations.Add(new OperatorDerivation(Signature[0], OperatorType.Subtraction, Quantity, Signature[1]));
                derivations.Add(new OperatorDerivation(Signature[1], OperatorType.Subtraction, Quantity, Signature[0]));
            }

            return derivations;
        }

        if (OriginalOperatorType is OperatorType.Subtraction)
        {
            if (orderedExpression)
            {
                if (ShouldImplementLHS)
                {
                    derivations.Add(new OperatorDerivation(Signature[0], OperatorType.Addition, Quantity, Signature[1]));
                }

                if (ShouldImplementRHS(Signature[1]))
                {
                    derivations.Add(new OperatorDerivation(Signature[0], OperatorType.Addition, Signature[1], Quantity));
                }

                if (ShouldImplementRHS(Signature[0]))
                {
                    derivations.Add(new OperatorDerivation(Signature[1], OriginalOperatorType, Signature[0], Quantity));
                }

                return derivations;
            }

            if (ShouldImplementLHS)
            {
                derivations.Add(new OperatorDerivation(Signature[1], OperatorType.Addition, Quantity, Signature[0]));
            }

            if (ShouldImplementRHS(Signature[0]))
            {
                derivations.Add(new OperatorDerivation(Signature[1], OperatorType.Addition, Signature[0], Quantity));
            }

            if (ShouldImplementRHS(Signature[1]))
            {
                derivations.Add(new OperatorDerivation(Signature[0], OriginalOperatorType, Signature[1], Quantity));
            }

            return derivations;
        }

        if (OriginalOperatorType is OperatorType.Multiplication)
        {
            if (ShouldImplementLHS)
            {
                if (FirstElementIsVector)
                {
                    derivations.Add(new OperatorDerivation(Signature[0], OperatorType.Division, Quantity, Signature[1]));

                    return derivations;
                }

                derivations.Add(new OperatorDerivation(Signature[1], OperatorType.Division, Quantity, Signature[0]));

                return derivations;
            }
        }

        if (OriginalOperatorType is OperatorType.Division)
        {
            if (orderedExpression)
            {
                if (ShouldImplementLHS)
                {
                    derivations.Add(new OperatorDerivation(Signature[0], OperatorType.Multiplication, Quantity, Signature[1]));
                }

                if (ShouldImplementRHS(Signature[1]))
                {
                    derivations.Add(new OperatorDerivation(Signature[0], OperatorType.Multiplication, Signature[1], Quantity));
                }

                return derivations;
            }

            if (ShouldImplementLHS)
            {
                derivations.Add(new OperatorDerivation(Signature[1], OperatorType.Multiplication, Quantity, Signature[0]));
            }

            if (ShouldImplementRHS(Signature[0]))
            {
                derivations.Add(new OperatorDerivation(Signature[1], OperatorType.Multiplication, Signature[0], Quantity));
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

    private bool ShouldImplementLHS => OperatorImplementation is DerivationOperatorImplementation.All or DerivationOperatorImplementation.LeftHandSide or DerivationOperatorImplementation.Suitable;
    private bool ShouldImplementRHS(NamedType lhs) => OperatorImplementation is DerivationOperatorImplementation.All or DerivationOperatorImplementation.RightHandSide || OperatorImplementation is DerivationOperatorImplementation.Suitable && lhs.Assembly != Quantity.Assembly;
}
