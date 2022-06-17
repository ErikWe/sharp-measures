namespace SharpMeasures.Generators.Quantities.Utility;

/// <summary>Contains common notation for producing the name for multiples of a constant, based on the name of the constant itself.</summary>
public static class ConstantMultiplesCodes
{
    /// <summary>Prepends "In" to the name of the constant.</summary>
    /// <remarks>For example; the name "</remarks>
    public const string InConstant = "In[*]";

    /// <summary>Prepends "In" and appends "Multiples" to the name of the constant.</summary>
    /// <remarks>For example; a constant "StandardGravity" would have the plural "InStandardGravityMultiples".</remarks>
    public const string InConstantMultiplies = "In[*]Multiples";

    /// <summary>Prepends "InMultiplesOf" to the name of the constant.</summary>
    /// <remarks>For example; a constant "StandardGravity" would have the plural "InMultiplesOfStandardGravity".</remarks>
    public const string InMultiplesOfConstant = "InMultiplesOf[*]";

    /// <summary>Prepends "In" and appends "s" to the name of the constant.</summary>
    /// <remarks>For example; a constant "PlanckLength" would have the plural "InPlanckLengths".</remarks>
    public const string InConstants = "In[*]s";

    /// <summary>Prepends "In" and appends "es" to the name of the constant.</summary>
    /// <remarks>For example; a constant "PlanckMass" would have the plural "InPlanckMasses".</remarks>
    public const string InConstantes = "In[*]es";

    /// <summary>Appends "Multiples" to the name of the constant.</summary>
    /// <remarks>For example; a constant "StandardGravity" would have the plural "StandardGravityMultiples".</remarks>
    public const string ConstantMultiples = "[*]Multiples";

    /// <summary>Prepends "MultiplesOf" to the name of the constant.</summary>
    /// <remarks>For example; a constant "StandardGravity" would have the plural "MultiplesOfStandardGravity".</remarks>
    public const string MultiplesOfConstant = "MultiplesOf[*]";
}
