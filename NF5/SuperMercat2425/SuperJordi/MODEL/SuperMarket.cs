using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperJordi.MODEL
{
	public class SuperMarket
	{
		#region Attributes
		private string name;
		private string address;
		private const int MAXLINES = 5;
		private int activeLines;
		private CheckOutLine[] lines;
		private Dictionary<string, Person> staff;
		private Dictionary<string, Person> customers;
		private SortedDictionary<int, Item> warehouse;
		#endregion

		#region Properties
		public Dictionary<string, Person> Staff { get => staff; }
		public Dictionary<string, Person> Customers { get => customers; }
		public SortedDictionary<int, Item> Warehouse { get => warehouse; }
		public int ActiveLines { get => activeLines; }
		public int MAXLines { get => MAXLINES; }
		#endregion

        #region Constructors
        public SuperMarket(string name, string address, string fileCashiers, string fileCustomers, string fileItems, int activeLines)
		{
			this.name = name;
			this.address = address;
			this.activeLines = activeLines;
			lines = new CheckOutLine[MAXLINES];
			staff = LoadCashiers(fileCashiers);
			customers = LoadCustomers(fileCustomers);
			warehouse = LoadWarehouse(fileItems);
			for (int i = 1; i <= activeLines; i++)
				OpenCheckOutLine(i);
		}
		#endregion

		#region Private Methods
		private Dictionary<string, Person> LoadCustomers(string fileName)
		{
			Dictionary<string, Person> clients = new Dictionary<string, Person>();
			using (StreamReader sR = new StreamReader(fileName))
			{
				string line;
				while ((line = sR.ReadLine()) != null)
				{
					string[] data = line.Split(';');
					if (data.Length == 3)
					{
						Person customer = new Customer(data[0], data[1], data[2] != "" ? Convert.ToInt32(data[2]) : null);
						clients.Add(data[0], customer);
					}
				}
			}
			return clients;
		}
		private Dictionary<string, Person> LoadCashiers(string fileName)
		{
			Dictionary<string, Person> caixers = new Dictionary<string, Person>();
			using (StreamReader sR = new StreamReader(fileName))
			{
				string line;
				while ((line = sR.ReadLine()) != null)
				{
					string[] data = line.Split(';');
					if (data.Length == 4)
					{
						Person cashier = new Cashier(data[0], data[1], Convert.ToDateTime(data[3]));
						caixers.Add(data[0], cashier);
					}
				}
			}
			return caixers;
		}
		private SortedDictionary<int, Item> LoadWarehouse(string fileName) 
		{
			Random random = new Random();
			SortedDictionary<int, Item> inventari = new SortedDictionary<int, Item>();
			using (StreamReader sR = new StreamReader(fileName))
			{
				//suposem un codi autoincremental
				int code = 0;
				string line;
				while ((line = sR.ReadLine()) != null)
				{
					string[] data = line.Split(';');
					if (data.Length == 4)
					{
						Item.Packaging pack;
						switch(data[2])
						{
							case "U":
								pack = Item.Packaging.Unit;
								break;
							case "K":
								pack = Item.Packaging.Kg;
								break;
							case "P":
								pack = Item.Packaging.Package;
								break;
							default:
								pack = Item.Packaging.Unit;
								break;
						}
						double stock = random.Next(30,300);
						int minStock = random.Next(5, 30);
						Item item = new Item(code, data[0], Convert.ToDouble(data[3]), (Item.Category)Convert.ToInt32(data[1]), pack, stock, minStock);
						inventari.Add(code, item);
					}
					code++;
				}
			}
			return inventari;
		}
		#endregion

		#region Methods
		public SortedSet<Item> GetItemsByStock()
		{
			// SortedSet<Item> items = new SortedSet<Item>(warehouse.Values, new Item.ComparatorStock());
			SortedSet<Item> items = new SortedSet<Item>(new Item.ComparatorStock());
			foreach (Item item in warehouse.Values)
			{
				items.Add(item);

			}
			return items;
		}
		public Person GetAvailableCustomer()
		{
			// Podem fer una cerca amb un while i ElementsAt(i)
			Customer c = customers.Values.FirstOrDefault(p => !p.Active) as Customer;
			if (c is not null) c.Active = true;
			return c;
		}
		public Person GetAvailableCashier()
		{
			// Podem fer una cerca amb un while i ElementsAt(i)
			Person p = staff.Values.FirstOrDefault(p => !p.Active);
			if (p is not null) p.Active = true;
			return p;
		}
		public void OpenCheckOutLine(int line2Open)
		{
			int lineIndex = line2Open - 1;
			if (lineIndex >= 0 && lineIndex < MAXLINES && lines[lineIndex] is null)
			{
				Person cashier = GetAvailableCashier();
				if (cashier is not null)
				{
					lines[lineIndex] = new CheckOutLine(cashier, line2Open);
					activeLines++;
				}
			}
		}
		public CheckOutLine GetCheckOutLine(int lineNumber)
		{
			int lineIndex = lineNumber - 1;
			if (lineIndex >= 0 && lineIndex < MAXLINES && lines[lineIndex] is not null)
				return lines[lineIndex];
			else
				return null;
		}
		public bool JoinTheQueue(ShoppingCart cart, int lineNumber)
		{
			if(GetCheckOutLine(lineNumber) is not null)
				return GetCheckOutLine(lineNumber).CheckIn(cart);
			else
				return false;
		}
		
		// Afegit de la manega per fer el main
		public bool RemoveCheckOutLine(int lineNumber)
		{
            int lineIndex = lineNumber - 1;
            if (lineIndex >= 0 && lineIndex < MAXLINES && lines[lineIndex] is not null)
            {
                lines[lineIndex].IsActive = false;
                lines[lineIndex] = null;
                activeLines--;
                return true;
            }
            else
                return false;
        }

        public bool CheckOut(int lineNumber)
		{
			if (GetCheckOutLine(lineNumber) is not null)
				return GetCheckOutLine(lineNumber).CheckOut();
			else
				return false;
		}
		#endregion

	}
}
