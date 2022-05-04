namespace Microsoft.CodeAnalysis;

using Microsoft.CodeAnalysis.CSharp;

using System;

public static partial class Extensions
{
    public static TNode? GetFirstChildOfType<TNode>(this SyntaxNode node) where TNode : SyntaxNode
    {
        if (node is null)
        {
            throw new ArgumentNullException(nameof(node));
        }

        foreach (SyntaxNode childNode in node.DescendantNodes())
        {
            if (childNode is TNode matchingNode)
            {
                return matchingNode;
            }
        }

        return null;
    }

    public static TNode? GetFirstChildOfKind<TNode>(this SyntaxNode node, SyntaxKind kind) where TNode : SyntaxNode
    {
        if (node is null)
        {
            throw new ArgumentNullException(nameof(node));
        }

        foreach (SyntaxNode childNode in node.DescendantNodes())
        {
            if (childNode is TNode matchingNode && matchingNode.IsKind(kind))
            {
                return matchingNode;
            }
        }

        return null;
    }
}
