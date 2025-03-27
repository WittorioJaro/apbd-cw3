using cwiczenia3;

{
        ContainerShip ship1 = new ContainerShip("Ocean Explorer", 25.5, 10, 1000);
        ContainerShip ship2 = new ContainerShip("Cargo Master", 20.0, 15, 1500);
        
        LiquidContainer liquid1 = (LiquidContainer)ContainerShip.CreateContainer("LIQUID", 100, false);
        liquid1.OwnWeight = 30;
        
        GasContainer gas1 = (GasContainer)ContainerShip.CreateContainer("GAS", 200, 5.5);
        gas1.OwnWeight = 50;
        
        CoolingContainer cooling1 = (CoolingContainer)ContainerShip.CreateContainer("COOLING", 300, "Frozen Fish", -5.0, -10.0);
        cooling1.OwnWeight = 60;
        
        try
        {
            liquid1.LoadCargo(80);
            gas1.LoadCargo(180);
            cooling1.LoadCargo(250);
            Console.WriteLine("Załadowano towary do kontenerów.");
        }
        catch (OverfillException ex)
        {
            Console.WriteLine($"Błąd ładowania: {ex.Message}");
        }
        
        try
        {
            ship1.LoadContainer(liquid1);
            ship1.LoadContainer(gas1);
            Console.WriteLine("Załadowano kontenery na statek 1.");
            
            ship2.LoadContainer(cooling1);
            Console.WriteLine("Załadowano kontenery na statek 2.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd: {ex.Message}");
        }
        
        Console.WriteLine("\n=== Informacje o statkach ===");
        ship1.PrintShipInfo();
        Console.WriteLine();
        ship2.PrintShipInfo();
        
        Console.WriteLine("\n=== Przenoszenie kontenera między statkami ===");
        try
        {
            ship1.TransferContainerTo(gas1.SerialNumber, ship2);
            Console.WriteLine("Kontener przeniesiony pomyślnie.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd przenoszenia: {ex.Message}");
        }
        
        Console.WriteLine("\n=== Zaktualizowane informacje o statkach ===");
        ship1.PrintShipInfo();
        Console.WriteLine();
        ship2.PrintShipInfo();
    }