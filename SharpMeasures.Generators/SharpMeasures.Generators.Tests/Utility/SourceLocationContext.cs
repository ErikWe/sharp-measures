namespace SharpMeasures.Generators.Tests.Utility;

internal record struct SourceLocationContext
{
    public static SourceLocationContext Empty { get; } = new(string.Empty);

    public static SourceLocationContext AsTypeof(string target, string prefix = "", string postfix = "") => new(target, prefix: $"{prefix}typeof(", postfix: $"){postfix}");

    public string Target { get; init; }

    public string Prefix { get; init; }
    public string Postfix { get; init; }

    public SourceLocationContext(string target, string prefix = "", string postfix = "")
    {
        Target = target;

        Prefix = prefix;
        Postfix = postfix;
    }

    public SourceLocationContext With(string outerPrefix = "", string innerPrefix = "", string innerPostfix = "", string outerPostfix = "") => this with { Prefix = $"{outerPrefix}{Prefix}{innerPrefix}", Postfix = $"{innerPostfix}{Postfix}{outerPostfix}"};

    public override string ToString() => $"{Prefix}{Target}{Postfix}";
}
