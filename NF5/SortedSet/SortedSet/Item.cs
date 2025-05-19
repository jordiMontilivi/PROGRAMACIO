using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortedSet
{

	public class Item : IComparable<Item>
	{
		#region Enums
		public enum Category
		{ BEVERAGE = 1, FRUITS, VEGETABLES, BREAD, MILK_AND_DERIVATIVES, GARDEN, MEAT, SWEETS, SAUCES, FROZEN, CLEANING, FISH, OTHER };

		public enum Packaging
		{ Unit, Kg, Package };

		#endregion

		#region Attributes
		private char currency;
		private int code;
		private string description;
		private bool onSale;
		private double price;
		private Category category;
		private Packaging packaging;
		private double stock;
		private int minStock;

		#endregion

		#region Properties
		public double Stock { get => stock; }
		public int MinStock { get => minStock; }
		public Packaging PackagingType { get => packaging; }
		public string Description { get => description; }
		public Category GetCategory { get => category; }
		public bool OnSale { get => onSale; }

		public double Price { get => Math.Round(onSale ? price * 0.9 : price, 2); }
		#endregion

		#region Constructors
		public Item(int code, string description, double price, Category category, Packaging packaging, double stock, int minStock)
		{
			this.currency = '\u20AC';
			this.code = code;
			this.description = description ?? throw new ArgumentNullException();
			this.price = price;
			this.category = category;
			this.packaging = packaging;
			this.stock = stock;
			this.minStock = minStock;
		}
		#endregion

		#region Methods
		public static void UpdateStock(Item item, double qty)
		{
			item.stock += qty;
		}
		#endregion

		#region Overrides
		public override string ToString()
		{
			string withDescount = onSale ? $"Y({Price}){currency}" : "N"; //no utilitzada
			return $"CODE->{this.code,3} DESCRIPTION->{this.description,-30} CATEGORY ->{this.category,-35} STOCK ->{this.stock,-4} MIN_STOCK->{this.minStock,-6} PRICE->{this.price,-6}{currency} ON SALE ->{(onSale ? $"Y({Price}){currency}" : "N")}";
		}
		#endregion

		#region Interfaces
		public int CompareTo(Item? other)
		{
			if (other is null) return 1;
			return this.code.CompareTo(other.code);
		}
		public class ComparatorStock : IComparer<Item>
		{
			public int Compare(Item? x, Item? y)
			{
				if (x is null) return 1;
				if (y is null) return -1;
				int result = x.Stock.CompareTo(y.Stock);
				if (result == 0)
					result = x.code.CompareTo(y.code); // Per evitar duplicats si el stock és igual
				return result;
			}
		}
		#endregion
	}
}
