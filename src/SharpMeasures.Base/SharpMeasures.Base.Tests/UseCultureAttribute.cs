namespace SharpMeasures.Tests;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Threading;

using Xunit.Sdk;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public sealed class UseCultureAttribute : BeforeAfterTestAttribute
{
    public CultureInfo Culture { get; }
    public CultureInfo UICulture { get; }

    private CultureInfo OriginalCulture { get; set; } = null!;
    private CultureInfo OriginalUICulture { get; set; } = null!;

    [SuppressMessage("Design", "CA1019: Define accessors for attribute arguments")]
    public UseCultureAttribute(string culture) : this(culture, culture) { }

    [SuppressMessage("Design", "CA1019: Define accessors for attribute arguments")]
    public UseCultureAttribute(string culture, string uiCulture)
    {
        Culture = new(culture, false);
        UICulture = new(uiCulture, false);
    }

    public override void Before(MethodInfo methodUnderTest)
    {
        OriginalCulture = Thread.CurrentThread.CurrentCulture;
        OriginalUICulture = Thread.CurrentThread.CurrentUICulture;

        Thread.CurrentThread.CurrentCulture = Culture;
        Thread.CurrentThread.CurrentUICulture = UICulture;

        CultureInfo.CurrentCulture.ClearCachedData();
        CultureInfo.CurrentUICulture.ClearCachedData();
    }

    public override void After(MethodInfo methodUnderTest)
    {
        Thread.CurrentThread.CurrentCulture = OriginalCulture;
        Thread.CurrentThread.CurrentUICulture = OriginalUICulture;

        CultureInfo.CurrentCulture.ClearCachedData();
        CultureInfo.CurrentUICulture.ClearCachedData();
    }
}
