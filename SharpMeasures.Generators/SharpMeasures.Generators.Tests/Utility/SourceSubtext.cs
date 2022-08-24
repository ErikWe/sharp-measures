namespace SharpMeasures.Generators.Tests.Utility;

public record struct SourceSubtext
{
    internal static SourceSubtext Covered(string text, string prefix = "", string postfix = "") => new($"{prefix}{text}{postfix}", new(text, prefix, postfix));
    internal static SourceSubtext AsTypeof(string text, string prefix = "", string postfix = "") => new($"{prefix}typeof({text}){postfix}", new(text, $"{prefix}typeof(", $"){postfix}"));
    internal static SourceSubtext FromTypeof(string text, string prefix = "", string postfix = "") => new($"{prefix}{text}{postfix}", SourceLocationContext.AsTypeof(text, prefix, postfix));

    public string Text { get; init; }

    internal SourceLocationContext Context { get; init; }

    internal SourceSubtext(string text, SourceLocationContext context)
    {
        Text = text;

        Context = context;
    }

    public override string ToString() => Text;
}
