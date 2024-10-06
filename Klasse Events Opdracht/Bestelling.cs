using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klasse_Events_Opdracht
{

    internal class Bestelling<T>
    {
        //generator
        private static int Counter = 0;
        public int Id { get; private set; }
        public T Item { get; set; }
        public DateTime Datum { get; set; }
        public int Aantal { get; set; }
        public string AbonnementPeriode { get; set; }

        // constructor
        public Bestelling(T item, int aantal, string abonnementPeriode = null)
        {
            Id = ++Counter;
            Item = item;
            Aantal = aantal;
            Datum = DateTime.Now;
            AbonnementPeriode = abonnementPeriode;
        }

        // tuple
        public Tuple<string, int, decimal> Bestel()
        {
            if (Item is Boek boek)
            {
                decimal totalePrijs = boek.Prijs * Aantal;

                OnBestellingGeplaatst(new BestellingEventArgs(Id, boek.Isbn, Aantal, totalePrijs));
                return Tuple.Create(boek.Isbn, Aantal, totalePrijs);
            }
            throw new InvalidOperationException("Item moet van type Boek zijn.");
        }

        //event
        public event EventHandler<BestellingEventArgs> BestellingGeplaatst;


        protected virtual void OnBestellingGeplaatst(BestellingEventArgs e)
        {
            BestellingGeplaatst?.Invoke(this, e);
        }

        public override string ToString()
        {
            if (Item is Boek boek)
            {
                return $"Bestelling ID: {Id}, ISBN: {boek.Isbn}, Naam: {boek.Naam}, Aantal: {Aantal}, Totale Prijs: €{boek.Prijs * Aantal}";
            }
            return $"Bestelling ID: {Id}, Item: {Item.GetType().Name}, Aantal: {Aantal}";
        }
    }
    public class BestellingEventArgs : EventArgs
    {
        public int BestellingId { get; }
        public string Isbn { get; }
        public int Aantal { get; }
        public decimal TotalePrijs { get; }

        public BestellingEventArgs(int bestellingId, string isbn, int aantal, decimal totalePrijs)
        {
            BestellingId = bestellingId;
            Isbn = isbn;
            Aantal = aantal;
            TotalePrijs = totalePrijs;
        }
    }
}