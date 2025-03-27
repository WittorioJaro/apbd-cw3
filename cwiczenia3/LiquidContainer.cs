namespace cwiczenia3;

public class LiquidContainer(double maxCapacity, bool isDangerous) : Container("L", maxCapacity), IHazardNotifier
{
    public bool IsDangerous { get; set; } = isDangerous;

    public void LoadCargo(double mass)
    {
        double maxFill = isDangerous ? MaxCapacity * 0.5 : MaxCapacity * 0.9;
        if (mass > maxFill)
        {
            NotifyHazard(SerialNumber, "Za duży załadunek");
            throw new OverfillException("Przekroczono maksymalną ładowność");
        }
        base.LoadCargo(mass);
    }

    public void NotifyHazard(string serialNumber, string message)
    {
        Console.WriteLine($"[ALERT] Kontener {serialNumber}: {message}");
    }
}