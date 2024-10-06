using Klasse_Events_Opdracht;
using System;
using System.Collections.Generic;

using Klasse_Events_Opdracht;
using System;
using System.Collections.Generic;

class Program
{
    static List<Boek> boeken = new List<Boek>(); // List to store books
    static List<Tijdschrift> tijdschriften = new List<Tijdschrift>(); // List to store magazines
    static List<Bestelling<Boek>> bestellingen = new List<Bestelling<Boek>>(); // List to store orders

    static void Main(string[] args)
    {
        // 2 boek en 2 ts
        VoorbeeldData();

        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\n----- Hoofdmenu -----");
            Console.WriteLine("1. Voeg een boek toe");
            Console.WriteLine("2. Voeg een tijdschrift toe");
            Console.WriteLine("3. Plaats een bestelling");
            Console.WriteLine("4. Bekijk bestellingen");
            Console.WriteLine("5. Exit");
            Console.Write("Kies een optie: ");

            string keuze = Console.ReadLine();

            switch (keuze)
            {
                case "1":
                    VoegBoekToe();
                    break;
                case "2":
                    VoegTijdschriftToe();
                    break;
                case "3":
                    PlaatsBestelling();
                    break;
                case "4":
                    BekijkBestellingen();
                    break;
                case "5":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Ongeldige keuze, probeer het opnieuw.");
                    break;
            }
        }
    }

    static void VoorbeeldData()
    {
        // 2 boeken en 2 ts als voorbeeld
        Boek boek1 = new Boek("testBoek1ISBN", "testBoek1Naam", "testBoek1Uitgever", 10m);
        Boek boek2 = new Boek("testBoek2ISBN", "testBoek2Naam", "testBoek2Uitgever", 10m);
        Tijdschrift tijdschrift1 = new Tijdschrift("testTijdschrift1ISBN", "testTijdschrift1Naam", "testTijdschrift1Uitgever", 10m, Tijdschrift.VerschijningsPeriode.Wekelijks);
        Tijdschrift tijdschrift2 = new Tijdschrift("testTijdschrift2ISBN", "testTijdschrift2Naam", "testTijdschrift2Uitgever", 10m, Tijdschrift.VerschijningsPeriode.Maandelijks);

        // toevoegen
        boeken.Add(boek1);
        boeken.Add(boek2);
        tijdschriften.Add(tijdschrift1);
        tijdschriften.Add(tijdschrift2);

        // displayen
        ToonBoeken(boeken);
        ToonTijdschriften(tijdschriften);
    }

    static void ToonBoeken(List<Boek> boeken)
    {
        Console.WriteLine("\n----- Beschikbare Boeken -----");
        foreach (var boek in boeken)
        {
            Console.WriteLine(boek);
        }
    }

    static void ToonTijdschriften(List<Tijdschrift> tijdschriften)
    {
        Console.WriteLine("\n----- Beschikbare Tijdschriften -----");
        foreach (var tijdschrift in tijdschriften)
        {
            Console.WriteLine(tijdschrift);
        }
    }

    static void VoegBoekToe()
    {
        Console.Write("Voer ISBN in: ");
        string isbn = Console.ReadLine();

        Console.Write("Voer naam in: ");
        string naam = Console.ReadLine();

        Console.Write("Voer uitgever in: ");
        string uitgever = Console.ReadLine();

        decimal prijs = VraagPrijs();

        boeken.Add(new Boek(isbn, naam, uitgever, prijs));
        Console.WriteLine("Boek toegevoegd!");

        ToonBoeken(boeken);
    }

    static void VoegTijdschriftToe()
    {
        Console.Write("Voer ISBN in: ");
        string isbn = Console.ReadLine();

        Console.Write("Voer naam in: ");
        string naam = Console.ReadLine();

        Console.Write("Voer uitgever in: ");
        string uitgever = Console.ReadLine();

        decimal prijs = VraagPrijs();

        Console.Write("Selecteer de verschijningsperiode (0 = Dagelijks, 1 = Wekelijks, 2 = Maandelijks): ");
        int periodeInput = VraagVerschijningsPeriode();

        Tijdschrift.VerschijningsPeriode periode = (Tijdschrift.VerschijningsPeriode)periodeInput;
        tijdschriften.Add(new Tijdschrift(isbn, naam, uitgever, prijs, periode));
        Console.WriteLine("Tijdschrift toegevoegd!");

        ToonTijdschriften(tijdschriften);
    }

    static void PlaatsBestelling()
    {
        if (boeken.Count == 0)
        {
            Console.WriteLine("Geen boeken beschikbaar om te bestellen.");
            return;
        }

        Console.WriteLine("\nSelecteer een boek om te bestellen:");
        for (int i = 0; i < boeken.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {boeken[i]}");
        }

        int boekNummer = VraagBoekNummer();

        Boek geselecteerdBoek = boeken[boekNummer - 1];

        Console.Write("Voer aantal in: ");
        int aantal = VraagAantal();

        Bestelling<Boek> bestelling = new Bestelling<Boek>(geselecteerdBoek, aantal);

        bestelling.BestellingGeplaatst += (sender, e) =>
        {
            Console.WriteLine($"Bestelling geplaatst: ID = {e.BestellingId}, ISBN = {e.Isbn}, Aantal = {e.Aantal}, Totale Prijs = €{e.TotalePrijs}");
        };

        var orderInfo = bestelling.Bestel();
        bestellingen.Add(bestelling);
        Console.WriteLine($"Bestel Info: ISBN = {orderInfo.Item1}, Aantal = {orderInfo.Item2}, Totale Prijs = €{orderInfo.Item3}");
    }

    static void BekijkBestellingen()
    {
        if (bestellingen.Count == 0)
        {
            Console.WriteLine("Geen bestellingen geplaatst.");
            return;
        }

        Console.WriteLine("\n----- Bestellingen -----");
        foreach (var bestelling in bestellingen)
        {
            Console.WriteLine(bestelling.ToString());
        }
    }

    static decimal VraagPrijs()
    {
        decimal prijs;
        do
        {
            Console.Write("Voer prijs in (tussen 5 en 50 euro): ");
        } while (!decimal.TryParse(Console.ReadLine(), out prijs) || prijs < 5 || prijs > 50);

        return prijs;
    }

    static int VraagVerschijningsPeriode()
    {
        int periodeInput;
        do
        {
            Console.Write("Selecteer de verschijningsperiode (0 = Dagelijks, 1 = Wekelijks, 2 = Maandelijks): ");
        } while (!int.TryParse(Console.ReadLine(), out periodeInput) || periodeInput < 0 || periodeInput > 2);

        return periodeInput;
    }

    static int VraagBoekNummer()
    {
        int boekNummer;
        do
        {
            Console.Write("Kies een boeknummer: ");
        } while (!int.TryParse(Console.ReadLine(), out boekNummer) || boekNummer < 1 || boekNummer > boeken.Count);

        return boekNummer;
    }

    static int VraagAantal()
    {
        int aantal;
        do
        {
            Console.Write("Voer aantal in (moet groter zijn dan 0): ");
        } while (!int.TryParse(Console.ReadLine(), out aantal) || aantal < 1);

        return aantal;
    }
}
