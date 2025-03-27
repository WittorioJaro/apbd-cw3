namespace cwiczenia3;

public class ContainerShip
{
    public List<Container> Containers { get; private set; }
    public double MaxSpeed { get; private set; } 
    public int MaxContainerCount { get; private set; }
    public double MaxWeight { get; private set; }
    public string Name { get; private set; }
    
    public ContainerShip(string name, double maxSpeed, int maxContainerCount, double maxWeight)
    {
        Name = name;
        MaxSpeed = maxSpeed;
        MaxContainerCount = maxContainerCount;
        MaxWeight = maxWeight;
        Containers = new List<Container>();
    }
    
    public void LoadContainer(Container container)
    {
        if (Containers.Count >= MaxContainerCount)
            throw new Exception($"Nie można załadować kontenera {container.SerialNumber}. Statek {Name} osiągnął limit kontenerów ({MaxContainerCount}).");
        
        double currentWeight = CalculateTotalWeight();
        double newContainerWeight = container.CargoMass + container.OwnWeight;
        
        if (currentWeight + newContainerWeight > MaxWeight)
            throw new Exception($"Nie można załadować kontenera {container.SerialNumber}. Przekroczono maksymalną wagę.");
        
        Containers.Add(container);
    }
    
    public void LoadContainers(List<Container> containers)
    {
        if (Containers.Count + containers.Count > MaxContainerCount)
            throw new Exception($"Nie można załadować {containers.Count} kontenerów. Przekroczono limit ({MaxContainerCount}).");
        
        double currentWeight = CalculateTotalWeight();
        double newContainersWeight = containers.Sum(c => c.CargoMass + c.OwnWeight);
        
        if (currentWeight + newContainersWeight > MaxWeight)
            throw new Exception("Nie można załadować kontenerów. Przekroczono maksymalną wagę.");
        
        Containers.AddRange(containers);
    }
    
    public void RemoveContainer(string serialNumber)
    {
        Container container = FindContainer(serialNumber);
        Containers.Remove(container);
    }
    
    public void UnloadContainer(string serialNumber)
    {
        Container container = FindContainer(serialNumber);
        container.UnloadCargo();
    }
    
    public void ReplaceContainer(string serialNumber, Container newContainer)
    {
        int index = Containers.FindIndex(c => c.SerialNumber == serialNumber);
        if (index == -1)
            throw new Exception($"Kontener o numerze {serialNumber} nie znajduje się na statku {Name}.");
        
        Container oldContainer = Containers[index];
        Containers[index] = newContainer;
        
        if (CalculateTotalWeight() > MaxWeight)
        {
            Containers[index] = oldContainer;
            throw new Exception("Nie można wymienić kontenera. Przekroczono maksymalną wagę.");
        }
    }
    
    public void TransferContainerTo(string serialNumber, ContainerShip destinationShip)
    {
        Container container = FindContainer(serialNumber);
        
        destinationShip.LoadContainer(container);
        
        Containers.Remove(container);
    }
    
    public void PrintContainerInfo(string serialNumber)
    {
        Container container = FindContainer(serialNumber);
        
        Console.WriteLine($"Kontener {container.SerialNumber}:");
        Console.WriteLine($"Typ: {container.GetType().Name}");
        Console.WriteLine($"Masa ładunku: {container.CargoMass}");
        Console.WriteLine($"Waga własna: {container.OwnWeight}");
        Console.WriteLine($"Maksymalna pojemność: {container.MaxCapacity}");
        
        if (container is LiquidContainer liquidContainer)
        {
            Console.WriteLine($"Niebezpieczny: {liquidContainer.IsDangerous}");
        }
        else if (container is GasContainer gasContainer)
        {
            Console.WriteLine($"Ciśnienie: {gasContainer.Pressure}");
        }
        else if (container is CoolingContainer coolingContainer)
        {
            Console.WriteLine($"Typ produktu: {coolingContainer.ProductType}");
            Console.WriteLine($"Wymagana temperatura: {coolingContainer.RequiredTemperature}");
            Console.WriteLine($"Aktualna temperatura: {coolingContainer.CurrentTemperature}");
        }
    }
    public void PrintShipInfo()
    {
        Console.WriteLine($"Statek: {Name}");
        Console.WriteLine($"Maksymalna prędkość: {MaxSpeed} węzłów");
        Console.WriteLine($"Kontenery: {Containers.Count}/{MaxContainerCount}");
        Console.WriteLine($"Aktualna waga: {CalculateTotalWeight()}/{MaxWeight} ton");
        
        Console.WriteLine("\nZaładowane kontenery:");
        foreach (var container in Containers)
        {
            Console.WriteLine($"- {container.SerialNumber} (Typ: {container.GetType().Name}, Ładunek: {container.CargoMass})");
        }
    }
    
    private Container FindContainer(string serialNumber)
    {
        Container? container = Containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
        if (container == null)
            throw new Exception($"Kontener o numerze {serialNumber} nie znajduje się na statku {Name}.");
        return container;
    }
    
    private double CalculateTotalWeight()
    {
        return Containers.Sum(c => c.CargoMass + c.OwnWeight);
    }
    
    public static Container CreateContainer(string type, double maxCapacity, params object[] additionalParams)
    {
        return type.ToUpper() switch
        {
            "LIQUID" => new LiquidContainer(maxCapacity, (bool)additionalParams[0]),
            "GAS" => new GasContainer(maxCapacity, (double)additionalParams[0]),
            "COOLING" => new CoolingContainer(maxCapacity, (string)additionalParams[0], 
                                            (double)additionalParams[1], (double)additionalParams[2]),
            _ => throw new ArgumentException($"Nieznany typ kontenera: {type}")
        };
    }
}