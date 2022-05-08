namespace Microsoft.CodeAnalysis.CSharp.Syntax;

using System;
using System.Collections.Generic;

public static partial class Extensions
{
    public static AttributeSyntax? GetAttributeOfType<TAttribute>(this AttributeListSyntax attributeList, SemanticModel semanticModel)
    {
        return GetAttributeOfType(attributeList, typeof(TAttribute), semanticModel);
    }

    public static AttributeSyntax? GetAttributeOfType(this AttributeListSyntax attributeList, Type attributeType, SemanticModel semanticModel)
    {
        if (attributeType is null)
        {
            throw new ArgumentNullException(nameof(attributeType));
        }

        return GetAttributeWithName(attributeList, attributeType.FullName, semanticModel);
    }

    public static AttributeSyntax? GetAttributeWithName(this AttributeListSyntax attributeList, string attributeName, SemanticModel semanticModel)
    {
        if (attributeList is null)
        {
            throw new ArgumentNullException(nameof(attributeList));
        }

        foreach (AttributeSyntax attributeSyntax in attributeList.Attributes)
        {
            if (attributeSyntax.IsDescribedAttributeName(attributeName, semanticModel))
            {
                return attributeSyntax;
            }
        }

        return null;
    }

    public static AttributeSyntax? GetFirstAttributeOfTypeIn(this AttributeListSyntax attributeList, IReadOnlyCollection<Type> attributeTypes,
        SemanticModel semanticModel)
    {
        if (attributeTypes is null)
        {
            throw new ArgumentNullException(nameof(attributeTypes));
        }

        string[] attributeNames = new string[attributeTypes.Count];

        int index = 0;
        foreach (Type attributeType in attributeTypes)
        {
            attributeNames[index++] = attributeType.FullName;
        }

        return GetFirstAttributeWithNameIn(attributeList, attributeNames, semanticModel);
    }

    public static AttributeSyntax? GetFirstAttributeOfTypeIn(this AttributeListSyntax attributeList, SemanticModel semanticModel, params Type[] attributeTypes)
    {
        return GetFirstAttributeOfTypeIn(attributeList, attributeTypes, semanticModel);
    }

    public static AttributeSyntax? GetFirstAttributeWithNameIn(this AttributeListSyntax attributeList, IReadOnlyCollection<string> attributeNames,
        SemanticModel semanticModel)
    {
        if (attributeNames is null)
        {
            throw new ArgumentNullException(nameof(attributeNames));
        }

        foreach (string attributeName in attributeNames)
        {
            if (GetAttributeWithName(attributeList, attributeName, semanticModel) is AttributeSyntax attributeSyntax)
            {
                return attributeSyntax;
            }
        }

        return null;
    }

    public static AttributeSyntax? GetFirstAttributeWithNameIn(this AttributeListSyntax attributeList, SemanticModel semanticModel, params string[] attributeNames)
    {
        return GetFirstAttributeWithNameIn(attributeList, attributeNames, semanticModel);
    }
}
