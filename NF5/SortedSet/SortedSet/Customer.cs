using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortedSet
{
	public class Customer : Person
	{
		#region Attributes
		private int? fidelityCard;
		#endregion

		#region Properties
		public override double GetRating => Math.Round(totalInvoiced * 0.02, 2);
		#endregion

		#region Constructors
		public Customer(string id, string fullName, int? fidelityCard) : base(id, fullName)
		{
			this.fidelityCard = fidelityCard;
		}
		#endregion

		#region Methods
		public override void AddPoints(int points)
		{
			if (fidelityCard is not null) base.points += points;
		}
		#endregion

		#region Overrides
		public override string ToString()
		{
			return $"DNI/NIE->{this.id} NOM->{this.FullName,-35} RATING ->{GetRating,-8} VENDES ->{this.totalInvoiced,-8}€ PUNTS->{points,-6} {base.ToString()}"; ;
		}
		#endregion

	}
}
