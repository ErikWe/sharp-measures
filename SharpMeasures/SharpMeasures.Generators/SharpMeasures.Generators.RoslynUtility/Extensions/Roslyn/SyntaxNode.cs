namespace SharpMeasures.Generators;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

using System;
using System.Collections.Generic;
using System.Linq;

public static partial class RoslynUtilityExtensions
{
    public static bool IsNotKind(this SyntaxNode node, SyntaxKind kind) => node.IsKind(kind) is false;

    public static TNode? GetFirstChildOfType<TNode>(this SyntaxNode node) where TNode : SyntaxNode
    {
        return node.GetChildrenOfType<TNode>().FirstOrDefault();
    }

    public static TNode? GetFirstChildOfKind<TNode>(this SyntaxNode node, SyntaxKind kind) where TNode : SyntaxNode
    {
        foreach (TNode child in node.GetChildrenOfType<TNode>())
        {
            if (child.IsKind(kind))
            {
                return child;
            }
        }

        return null;
    }

    public static IEnumerable<TNode> GetChildrenOfType<TNode>(this SyntaxNode node) where TNode : SyntaxNode
    {
        if (node is null)
        {
            throw new ArgumentNullException(nameof(node));
        }

        foreach (SyntaxNode childNode in node.DescendantNodes())
        {
            if (childNode is TNode matchingNode)
            {
                yield return matchingNode;
            }
        }
    }
}
