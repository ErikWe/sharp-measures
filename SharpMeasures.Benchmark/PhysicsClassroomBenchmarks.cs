namespace ErikWe.SharpMeasures.Benchmarks;

using BenchmarkDotNet.Attributes;

using System;

[MemoryDiagnoser]
public class PhysicsClassroomBenchmarks
{
    private readonly Random Random = new();

    // 4: https://www.physicsclassroom.com/class/1DKin/Lesson-6/Sample-Problems-and-Solutions
    [Benchmark(Baseline = true)]
    public double UsingPrimitive()
    {
        double velocity1 = Random.NextDouble();
        double velocity2 = Random.NextDouble();
        double elapsedTime = Random.NextDouble();

        double acceleration = (velocity2 - velocity1) / elapsedTime;

        double distance = velocity1 * elapsedTime + 0.5 * acceleration * elapsedTime * elapsedTime;

        return distance;
    }

    // 4: https://www.physicsclassroom.com/class/1DKin/Lesson-6/Sample-Problems-and-Solutions
    [Benchmark]
    public Quantities.Length UsingSharpMeasures()
    {
        Quantities.Speed velocity1 = Random.NextDouble() * Quantities.Speed.OneMetrePerSecond;
        Quantities.Speed velocity2 = Random.NextDouble() * Quantities.Speed.OneMetrePerSecond;
        Quantities.Time elapsedTime = Random.NextDouble() * Quantities.Time.OneSecond;

        Quantities.Acceleration acceleration = (velocity2 - velocity1) / elapsedTime;

        Quantities.Length distance = velocity1 * elapsedTime + 0.5 * acceleration * elapsedTime * elapsedTime;

        return distance;
    }

    // 4: https://www.physicsclassroom.com/class/1DKin/Lesson-6/Sample-Problems-and-Solutions
    [Benchmark]
    public QuantityTypes.Length UsingQuantityTypes()
    {
        QuantityTypes.Velocity velocity1 = Random.NextDouble() * QuantityTypes.Velocity.MetrePerSecond;
        QuantityTypes.Velocity velocity2 = Random.NextDouble() * QuantityTypes.Velocity.MetrePerSecond;
        QuantityTypes.Time elapsedTime = Random.NextDouble() * QuantityTypes.Time.Second;

        QuantityTypes.Acceleration acceleration = (velocity2 - velocity1) / elapsedTime;

        QuantityTypes.Length distance = velocity1 * elapsedTime + 0.5 * acceleration * elapsedTime * elapsedTime;

        return distance;
    }
}