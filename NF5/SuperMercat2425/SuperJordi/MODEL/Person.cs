using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperJordi.MODEL
{
	public abstract class Person : IComparable<Person>
	{
		#region Attributes
		protected string id;
		protected string fulName;
		protected int points;
		protected double totalInvoiced;
		protected bool isActive;
		#endregion

		#region Properties
		public abstract double GetRating { get; }
		public string FullName { get => fulName; }
		public bool Active
		{
			get => isActive;
			set => isActive = value;
		}
		#endregion

		#region Constructors
		protected Person(string id, string fullName, int points)
		{
			this.id = id;
			this.fulName = fullName;
			this.points = points;
			this.totalInvoiced = 0;
			this.isActive = false;
		}
		public Person(string id, string fullName): this(id, fullName, 0)
		{ }
		#endregion

		#region Methods
		public abstract void AddPoints(int points);
		public void AddInvoicedAmout(double amount)
		{
			this.totalInvoiced += amount;
		}
		#endregion

		#region Overrides
		public override string ToString()
		{
			return $"DISPONIBLE -> { (isActive ? 'S' : 'N') }";
		}
		public override bool Equals(object? obj)
		{
			return (obj is Person person && obj is not null) ? this.id == person.id : false;
			//if (obj is Person person)
			//{
			//	return this.id == person.id;
			//}
			//return false;
		}
		#endregion

		#region Interfaces
		// les oredenacions sempre han de ser de menor a major per defecte,
		// però en aquest cas ens demana que el més gran sigui el primer
		// per tant, el que fem és invertir l'ordre de la comparació ???
		public int CompareTo(Person? other)
		{
			if (other is null) return 1;
			if (other.GetRating == this.GetRating) return this.id.CompareTo(other.id);
			return other.GetRating.CompareTo(this.GetRating);
		}
		#endregion

	}
}
