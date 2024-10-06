using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klasse_Events_Opdracht
{

    internal class Boek
    {
        // Eigenschappen
        public string Isbn { get; set; }
        public string Naam { get; set; }
        public string Uitgever { get; set; }

        private decimal prijs;

        public decimal Prijs
        {
            get { return prijs; }
            set
            {
                // prijs tussen 5 en 50 euro
                if (value < 5 || value > 50)
                {
                    throw new ArgumentException("Prijs moet tussen 5 en 50 euro liggen.");
                }
                prijs = value;
            }
        }

        // constructor
        public Boek(string isbn, string naam, string uitgever, decimal prijs)
        {
            Isbn = isbn;
            Naam = naam;
            Uitgever = uitgever;
            Prijs = prijs; 
        }

        // tostring
        public override string ToString()
        {
            return $"Boek: {Naam}, ISBN: {Isbn}, Uitgever: {Uitgever}, Prijs: €{Prijs}";
        }

        public void Lees()
        {
            Console.WriteLine("Voer de gegevens van het boek in:");
            Console.Write("ISBN: ");
            Isbn = Console.ReadLine();
            Console.Write("Naam: ");
            Naam = Console.ReadLine();
            Console.Write("Uitgever: ");
            Uitgever = Console.ReadLine();
            Console.Write("Prijs: ");
            Prijs = decimal.Parse(Console.ReadLine());
        }
    }

}
