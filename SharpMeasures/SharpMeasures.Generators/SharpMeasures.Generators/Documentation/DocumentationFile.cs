namespace SharpMeasures.Generators.Documentation;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

internal readonly record struct DocumentationFile
{
    public static DocumentationFile Empty => new(string.Empty, new ReadOnlyDictionary<string, string>(new Dictionary<string, string>()));

    public string Name { get; }
    private ReadOnlyDictionary<string, string> Content { get; }

    public DocumentationFile(string name, ReadOnlyDictionary<string, string> content)
    {
        Name = name;
        Content = content;
    }

    public string ResolveText(string text)
    {
        string originalText = text;

        MatchCollection matches = DocumentationParsing.MatchInvokations(text);

        foreach (Match match in matches)
        {
            string tag = match.Groups["tag"].Value;

            if (Content.TryGetValue(tag, out string tagText))
            {
                text = DocumentationParsing.ResolveInvokation(tag, text, tagText);
            }
        }

        if (text == originalText)
        {
            return text;
        }
        else
        {
            return ResolveText(text);
        }
    }
}
