namespace cwiczenia3;

public class GasContainer(double maxCapacity, double pressure) : Container("G", maxCapacity), IHazardNotifier
{
    public double Pressure { get; set; } = pressure;
    
    public void NotifyHazard(string serialNumber, string message)
    {
        Console.WriteLine($"[ALERT] Kontener {serialNumber}: {message}");
    }
    
    public override void UnloadCargo()
    {
        CargoMass *= 0.05;
    }
    
    public override void LoadCargo(double mass)
    {
        if (mass > MaxCapacity)
        {
            NotifyHazard(SerialNumber, "Za duży załadunek");
            throw new OverfillException("Przekroczono maksymalną ładowność");
        }
        base.LoadCargo(mass);
    }
}