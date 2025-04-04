using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipsOrdenacions.Model
{
	public class Equip : IComparable<Equip>
	{
		#region Atributs
		private string nom;
		private int golsF;
		private int golsC;
		private int punts;
		#endregion

		#region Propietats
		public string Nom
		{
			get => nom;
			set => nom = value;
		}
		public int GolsF
		{
			get => golsF; 
			set => golsF = value; 
		}
		public int GolsC
		{
			get { return golsC; }
			set { golsC = value; }
		}
		public int Punts
		{
			get { return punts; }
			set { punts = value; }
		}
		#endregion

		#region Constructors
		public Equip() : this("", 0, 0, 0)
		{}
		public Equip(string nom, int golsF, int golsC, int punts)
		{
			this.nom = nom;
			this.golsF = golsF;
			this.golsC = golsC;
			this.punts = punts;
		}
		public Equip(string csv)
		{
			string[] valors;
			valors = csv.Split(';');

			if (valors.Length != 4)
				throw new ArgumentException("El CSV ha de tenir 4 arguments");

			nom = valors[0];
			golsF = Convert.ToInt32(valors[1]);
			golsC = Convert.ToInt32(valors[2]);
			punts = Convert.ToInt32(valors[3]);
		}
		#endregion

		#region Metodes Object
		public override string ToString()
		{
			return $"{nom};{golsF};{golsC};{punts}";
		}
		public override bool Equals(object? other)
		{
			bool res = true;
			Equip otherEquip;

			if (other == null || other is not Equip)
				res = false;
			else
			{
				otherEquip = other as Equip;
				if (otherEquip.Nom != nom)
					res = false;
			}

			return res;
		}
		#endregion

		#region Metodes de Comparacio i nested classes
		public int CompareTo(Equip? other)
		{
			int res;
			if (other == null)
				res = 1;
			else
				res = nom.CompareTo(other.Nom);
			return res;
		}

		//Nested Class per a les comparacions Personalitzades
		public class ComparadorGolsFavor : IComparer<Equip>
		{
			public int Compare(Equip? e1, Equip? e2)
			{
				int res;
				if (e1 == null && e2 == null)
					res = 0;
				else if (e1 == null)
					res = -1;
				else if (e2 == null)
					res = 1;
				else
					res = e1.GolsF.CompareTo(e2.GolsF); //Si volem ordre descendent haurem de fer un Reverse
				// Es pot invertir ordre dels comparadors si es volen els gols a favor en ordre descendent
				//res = e2.GolsF.CompareTo(e1.GolsF);

				return res;
			}
		}
		public class ComparadorGolsContra : IComparer<Equip>
		{
			public int Compare(Equip? e1, Equip? e2)
			{
				int res;
				if (e1 == null && e2 == null)
					res = 0;
				else if (e1 == null)
					res = -1;
				else if (e2 == null)
					res = 1;
				else
					res = e1.GolsC.CompareTo(e2.GolsC);

				return res;
			}
		}
		public class ComparadorPunts : IComparer<Equip>
		{
			public int Compare(Equip? e1, Equip? e2)
			{
				int res;
				if (e1 == null && e2 == null)
					res = 0;
				else if (e1 == null)
					res = -1;
				else if (e2 == null)
					res = 1;
				else
					// Si volem ordre descendent haurem de fer un Reverse o invertir comparació
					res = e1.Punts.CompareTo(e2.Punts);

				return res;
			}
		}
		#endregion
	}
}
