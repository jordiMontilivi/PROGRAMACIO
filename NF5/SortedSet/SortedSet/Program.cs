namespace SortedSet
{
	public class Program
	{
		static void Main(string[] args)
		{
			//string fileName = "GROCERIES.txt";
			//SortedDictionary<int, Item> inventari = LoadWarehouse(fileName);
			//Console.WriteLine("Dictionary elements:");
			//foreach (KeyValuePair<int, Item> number in inventari)
			//	Console.WriteLine(number.Value);

			//Console.WriteLine("*****************************************************");
			//Console.WriteLine();
			//Console.WriteLine("SortedSet elements by stock:");
			//SortedSet<Item> items = GetItemsByStock(inventari);
			//foreach (Item item in items)
			//	Console.WriteLine(item);

			Dictionary<string, Person> customers = LoadCustomers("CUSTOMERS.txt");
			Console.WriteLine("Dictionary elements:");
			DoListCustomers(customers);
		}

		// Mostrar persones d'un supermercat
		public static Dictionary<string, Person> LoadCustomers(string fileName)
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

		public static void DoListCustomers(Dictionary<string, Person> customers)
		{

			Console.Clear();
			int i = 0;
			Random rnd = new Random();
			foreach (Customer customer in customers.Values)
				customer.AddInvoicedAmout(rnd.Next(0, 1000)); // Afegim un import a cada client
			

			Console.WriteLine("__________________________________________________");

			//List<Person> llistaPerRating = new List<Person>(customers.Values);
			//llistaPerRating.Sort();
			Console.WriteLine();
			SortedSet<Person> llistaPerRating = new SortedSet<Person>(customers.Values);

			Console.WriteLine($"Tenim {llistaPerRating.Count} clients");
			foreach (Customer c in llistaPerRating)
			{
				Console.WriteLine(c);
				i++;
				if (i % 20 == 0) { MsgNextScreen("PREM UNA TECLA PER CONTINUAR"); }
			}

		}
		public static void MsgNextScreen(string msg)
		{
			Console.WriteLine(msg);
			Console.ReadKey();
		}


		// Mostrar elements Items
		public static SortedSet<Item> GetItemsByStock(SortedDictionary<int, Item> warehouse)
		{
			// SortedSet<Item> items = new SortedSet<Item>(warehouse.Values, new Item.ComparatorStock());
			SortedSet<Item> items = new SortedSet<Item>(new Item.ComparatorStock());
			foreach (Item item in warehouse.Values)
			{
				items.Add(item);

			}
			return items;
		}
		public static SortedDictionary<int, Item> LoadWarehouse(string fileName)
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
						switch (data[2])
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
						double stock = random.Next(30, 300);
						int minStock = random.Next(5, 30);
						Item item = new Item(code, data[0], Convert.ToDouble(data[3]), (Item.Category)Convert.ToInt32(data[1]), pack, stock, minStock);
						inventari.Add(code, item);
					}
					code++;
				}
			}
			return inventari;
		}
	}
}
