using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTaulaLlista
{
    public class Producte : ICloneable, IComparable<Producte>
    {
        private int id;
        private string nom;
        private double preu;
        private int quantitat;
        private string categoria;

        public Producte() : this(-1, "", 0, 0, "UNDEFINED") { }

        public Producte(int id, string nom, double preu, int quantitat) :
            this(id, nom, preu, quantitat, "UNDEFINED")
        { }

        public Producte(int id, string nom, double preu, int quantitat, string categoria)
        {
            this.id = id;
            this.nom = nom;
            this.preu = preu;
            this.quantitat = quantitat;
            this.categoria = categoria;
        }

        //IMPLEMETAR PROPIETATS I MÈTODES
        //Propietats
        public int Id { get { return id; } }
        public string Nom { get { return nom; } set { nom = value; } }
        //public string Nom { get; set; }
        public double Preu
        {
            get { return preu; }
            set
            {
                if (value < 0)
                    throw new Exception("EL PREU DEL PRODUCTE NO POT SER INFERIOR A ZERO.");
                else
                    preu = value;
            }
        }
        public int Quantitat
        {
            get { return quantitat; }
            set
            {
                if (value < 0) throw new Exception("LA QUANTITAT DEL PRODUCTE NO POT SER INFERIOR A ZERO.");
                else quantitat = value;
            }
        }
        public string Categoria { get => nom; set => nom = value; }

        public override string ToString()
        {
            return $"{id};{nom};{preu};{quantitat};{categoria}";
        }
        public override bool Equals(object? obj)
        {
            if (obj is null || obj is not Producte) return false;
            else
            {
                Producte producte = obj as Producte;
                return this.Id.Equals(producte.Id);
            }
        }
        public object Clone()
        {
            Producte nou = new Producte(id, nom, preu, quantitat, categoria);
            return nou;
        }

        public int CompareTo(Producte? other)
        {
            if (other is null) return 1;
            else
            {
                if (this.id > other.id) return 1;
                else if (this.id < other.id) return -1;
                else return 0;
            }
        }
        public class ComparaNom : IComparer<Producte>
        {
            public int Compare(Producte? p1, Producte? p2)
            {
                if (p1 is null) return -1;
                else if (p2 is null) return 1;
                else return p1.nom.CompareTo(p2.nom);
            }
        }
        public class ComparaPreu : IComparer<Producte>
        {
            public int Compare(Producte? p1, Producte? p2)
            {
                if (p1 is null) return -1;
                else if (p2 is null) return 1;
                else return p1.preu.CompareTo(p2.preu);
            }
        }
    }
}
