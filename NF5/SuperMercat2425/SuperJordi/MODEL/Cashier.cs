using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperJordi.MODEL
{
	public class Cashier : Person
	{
		#region Attributes
		private DateTime joiningDate;
		#endregion

		#region Properties
		public int YearsOfService { get => (DateTime.Now - joiningDate).Days / 365; }
		public override double GetRating => Math.Round(totalInvoiced * 0.1, 2) + YearsOfService;
		#endregion

		#region Constructors
		public Cashier(string id, string fullName, DateTime? joiningDate) : base(id, fullName)
		{
			this.joiningDate = joiningDate ?? DateTime.Today;
		}
		#endregion

		#region Methods
		public override void AddPoints(int points)
		{
			this.points += (YearsOfService + 1) * points;
		}
		#endregion

		#region Overrides
		public override string ToString()
		{
			return $"DNI/NIE->{this.id} NOM->{((fulName.ToString().Length >= 30) ? FullName.ToString().Substring(0, 28) + "." : FullName),-30} RATING ->{GetRating,-6} ANTIGUITAT ->{YearsOfService,-2} VENDES ->{totalInvoiced,-8} PUNTS->{points,-6} {base.ToString()}";
		}
		#endregion

	}
}
