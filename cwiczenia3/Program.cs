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
        
        LiquidContainer liquid2 = (LiquidContainer)ContainerShip.CreateContainer("LIQUID", 120, true);
        liquid2.OwnWeight = 25;
        liquid2.LoadCargo(50);

        GasContainer gas2 = (GasContainer)ContainerShip.CreateContainer("GAS", 150, 4.5);
        gas2.OwnWeight = 40;
        gas2.LoadCargo(100);

        Console.WriteLine("\n=== Testowanie LoadContainers ===");
        List<Container> containersToLoad = new List<Container> { liquid2, gas2 };
        try {
            ship1.LoadContainers(containersToLoad);
            Console.WriteLine("Pomyślnie załadowano wiele kontenerów na statek ship1");
        }
        catch (Exception ex) {
            Console.WriteLine($"Błąd ładowania kontenerów: {ex.Message}");
        }

        Console.WriteLine("\n=== Testowanie PrintContainerInfo ===");
        ship1.PrintContainerInfo(liquid1.SerialNumber);

        Console.WriteLine("\n=== Testowanie UnloadContainer ===");
        Console.WriteLine($"Przed rozładunkiem: Kontener {liquid1.SerialNumber} ładunek: {liquid1.CargoMass}");
        ship1.UnloadContainer(liquid1.SerialNumber);
        Console.WriteLine($"Po rozładunku: Kontener {liquid1.SerialNumber} ładunek: {liquid1.CargoMass}");

        Console.WriteLine("\n=== Testowanie ReplaceContainer ===");
        CoolingContainer cooling2 = (CoolingContainer)ContainerShip.CreateContainer("COOLING", 250, "Nabiał", 2.0, 1.0);
        cooling2.OwnWeight = 55;
        cooling2.LoadCargo(200);
        
        try {
            ship1.ReplaceContainer(gas2.SerialNumber, cooling2);
            Console.WriteLine($"Pomyślnie wymieniono {gas2.SerialNumber} na {cooling2.SerialNumber}");
        }
        catch (Exception ex) {
            Console.WriteLine($"Błąd wymiany kontenera: {ex.Message}");
        }

        Console.WriteLine("\n=== Testowanie RemoveContainer ===");
        Console.WriteLine($"Przed usunięciem: Liczba kontenerów na statku ship1: {ship1.Containers.Count}");
        ship1.RemoveContainer(liquid2.SerialNumber);
        Console.WriteLine($"Po usunięciu: Liczba kontenerów na statku ship1: {ship1.Containers.Count}");

        Console.WriteLine("\n=== Końcowe Informacje o Statkach ===");
        ship1.PrintShipInfo();
        Console.WriteLine();
        ship2.PrintShipInfo();
    }