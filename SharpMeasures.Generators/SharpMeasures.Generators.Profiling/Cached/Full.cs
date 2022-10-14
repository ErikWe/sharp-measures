namespace SharpMeasures.Generators.Profiling.Cached;

using SharpMeasures.Generators.DriverUtility;

using System.Threading.Tasks;

internal static class Full
{
    public static async Task AddingQuantityProcess()
    {
        var driver = await ExistingSolutionDriver.Build(ProjectPath.Path + @"\..\..\SharpMeasures\SharpMeasures.sln").ConfigureAwait(false);

        driver.ApplyChange(ProjectPath.Path + @"\..\..\SharpMeasures\SharpMeasures\Scalars\Angle.cs", AddingQuantityProcessText);
    }

    private static string AddingQuantityProcessText => """
        namespace SharpMeasures;

        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfAngle), Vector = typeof(AngleN), DefaultUnit = "Radian", DefaultSymbol = "rad")]
        public readonly partial record struct Angle { }

        [QuantityOperation(typeof(TimeSquared), typeof(AngularAcceleration), OperatorType.Division)]
        [QuantityOperation(typeof(Time), typeof(AngularSpeed), OperatorType.Division)]
        [QuantityOperation(typeof(SolidAngle), typeof(Angle), OperatorType.Multiplication)]
        [QuantityOperation(typeof(SolidAngle), typeof(AngularFrequency), OperatorType.Division)]
        [QuantityOperation(typeof(AngularSpeed), typeof(Frequency), OperatorType.Multiplication)]
        [QuantityOperation(typeof(AngularSpeed), typeof(Time), OperatorType.Division)]
        [QuantityOperation(typeof(AngularFrequency), typeof(Scalar), OperatorType.Division, OperatorPosition.Right)]
        [QuantityOperation(typeof(AngularFrequency), typeof(SolidAngle), OperatorType.Division)]
        [QuantityOperation(typeof(AngularAcceleration), typeof(FrequencyDrift), OperatorType.Multiplication)]
        [QuantityOperation(typeof(AngularAcceleration), typeof(TimeSquared), OperatorType.Division)]
        public readonly partial record struct Angle { }

        [QuantityProcess("Square2", typeof(SolidAngle), "new(Magnitude * Magnitude)")]
        [QuantityProcess("Square", typeof(SolidAngle), "new(Magnitude * Magnitude)")]
        public readonly partial record struct Angle { }
        """;
}
