namespace cwiczenia3;

public abstract class Container
{
    public double CargoMass { get; set; }
    public int Height { get; set; }
    public double OwnWeight { get; set; }
    public int Depth { get; set; }
    public string SerialNumber { get; set; }
    public double MaxCapacity { get; set; }
    
    
    protected Container(string containerType, double maxCapacity)
    {
        this.MaxCapacity = maxCapacity;
        this.SerialNumber = GenerateSerialNumber(containerType);
    }
    
    private static int SerialCounter = 0;
    private string GenerateSerialNumber(string containerType)
    
    {
        SerialCounter++;
        return $"KON-{containerType}-{SerialCounter}";
    }
    
    public virtual void LoadCargo(double mass)
    {
        if (mass > MaxCapacity)
            throw new OverfillException("Przekroczono maksymalną ładowność");
        CargoMass = mass;
    }
    
    public virtual void UnloadCargo()
    {
        CargoMass = 0;
    }
}