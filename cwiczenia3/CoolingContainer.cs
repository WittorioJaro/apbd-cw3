namespace cwiczenia3;

public class CoolingContainer(double maxCapacity, string productType, double requiredTemperature, double currentTemperature) : Container("C", maxCapacity)
{
    public String ProductType { get; set; } = productType;
    public double RequiredTemperature { get; set; } = requiredTemperature;
    public double CurrentTemperature { get; set; } = currentTemperature;
    
    public void SetTemperature(double temperature)
    {
        if (temperature < RequiredTemperature)
            throw new Exception("Temperatura niższa niż wymagana");
        CurrentTemperature = temperature;
    }
    
    public bool IsCompatibleProduct(string product)
    {
        return product == ProductType;
    }
}