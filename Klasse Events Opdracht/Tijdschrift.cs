using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klasse_Events_Opdracht
{
    internal class Tijdschrift : Boek
    {
        // enum
        public enum VerschijningsPeriode { Dagelijks, Wekelijks, Maandelijks }
        public VerschijningsPeriode Periode { get; set; }

        // constructor
        public Tijdschrift(string isbn, string naam, string uitgever, decimal prijs, VerschijningsPeriode periode)
            : base(isbn, naam, uitgever, prijs)
        {
            Periode = periode;
        }

        // tostring
        public override string ToString()
        {
            return base.ToString() + $", Verschijningsperiode: {Periode}";
        }

        public void Lees()
        {
            base.Lees();
            Console.WriteLine("Select de verschijningsperiode (0 = Dagelijks, 1 = Wekelijks, 2 = Maandelijks): ");
            Periode = (VerschijningsPeriode)Enum.Parse(typeof(VerschijningsPeriode), Console.ReadLine());
        }
    }

}
