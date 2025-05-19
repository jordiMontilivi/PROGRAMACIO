using System.Text;

namespace SuperJordi.MODEL
{
	public class ShoppingCart
	{
		#region Attributes
		private Dictionary<Item, double> shoppingList;
		private Customer customer;
		private DateTime dateOfPurchase;
		#endregion

		#region Properties
		// Segons la pràctica no hem de tenir en compte que les quantitats de la cistella siguen menors al magatzem
		public Dictionary<Item, double> ShoppingList { get => shoppingList; }
		public Customer Customer { get => customer; }
		public DateTime DateOfPurchase { get => dateOfPurchase; }
		#endregion

		#region Constructors
		public ShoppingCart(Customer customer, DateTime dateOfPurchase)
		{
			this.customer = customer;
			if (!this.customer.Active) this.customer.Active = true; //No deuria passar però per si de cas
			this.shoppingList = new Dictionary<Item, double>();
			//this.dateOfPurchase = DateTime.Now;
			this.dateOfPurchase = dateOfPurchase;
		}
		#endregion

		#region Methods
		// Segons especificacions podem afegir qty negatives però m'assegure de no deixar que la quantitat quedi negativa
		public void AddOne(Item item, double qty)
		{
			//if(itemKeyValue.PackagingType is not Item.Packaging.Kg) qty = (int)qty;
			if ((int)qty != qty && item.PackagingType is not Item.Packaging.Kg)
				throw new ArgumentException("No es pot afegir decimals a un producte que no es ven a granel");
			if (shoppingList.ContainsKey(item))
				shoppingList[item] += qty;
			else
				shoppingList.Add(item, qty);
			// Ens asegurem de que la quantitat no quedi negativa
			if (shoppingList[item] < 0) shoppingList[item] = 0;
		}
		public void AddAllRandomly(SortedDictionary<int, Item> warehouse)
		{
			Random random = new Random();
			int items = random.Next(1, 11);
			while (items > 0)
			{
				int code = random.Next(1, warehouse.Count + 1);
				// Per si hem esborrat algun producte del magatzem
				if (warehouse.ContainsKey(code))
				{
					Item item = warehouse[code];
					if (!shoppingList.ContainsKey(item))
					{
						double qty = random.Next(1, 6);
						AddOne(item, qty);
						items--;
					}
				}
			}
		}
		public int RawPointsObtainedAtCheckout(double totalInvoiced)
		{
			return (int)(totalInvoiced * 0.1);
		}
		public static double ProccessItems(ShoppingCart cart)
		{
			double totalInvoiced = 0;
			foreach (KeyValuePair<Item, double> itemKeyValue in cart.ShoppingList)
			{
				//double realQty = (itemKeyValue.Key.Stock >= itemKeyValue.Value) ? itemKeyValue.Value : itemKeyValue.Key.Stock;
				double realQty = itemKeyValue.Value;
				// Com que no tenim setters no podem modificar shoppingList, 
				if (itemKeyValue.Key.Stock < itemKeyValue.Value)
					realQty = itemKeyValue.Key.Stock;

				Item.UpdateStock(itemKeyValue.Key, -realQty);
				//cart.customer.AddInvoicedAmout(itemKeyValue.Key.Price * itemKeyValue.Value); //Hem d'afegir gastos als customers i caishers
				totalInvoiced += realQty * itemKeyValue.Key.Price;
			}
			return Math.Round(totalInvoiced, 2);
		}
		public override string ToString()
		{
			StringBuilder carrito = new StringBuilder("*********\nINFO CARRITO DE COMPRA CLIENT->" + customer.FullName + "\n");
			foreach (KeyValuePair<Item, double> kvp in shoppingList)
			{
				carrito.Append($"{((kvp.Key.Description.ToString().Length >= 20) ? kvp.Key.Description.ToString().Substring(0, 18) + "." : kvp.Key.Description),-20} - CAT-->{((kvp.Key.GetCategory.ToString().Length >= 15)?kvp.Key.GetCategory.ToString().Substring(0,13) + "." : kvp.Key.GetCategory),-15} - QTY-->{kvp.Value,-3} - UNIT PRICE-->{kvp.Key.Price,-6}");
				if (kvp.Key.OnSale) carrito.Append("€(*)\n");
				else carrito.Append("€\n");
			}
			carrito.Append("*****FI CARRITO COMPRA****\n");
			return carrito.ToString();
		}
		#endregion

	}
}
