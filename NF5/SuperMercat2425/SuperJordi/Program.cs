using SuperJordi.MODEL;
using System.Net.WebSockets;

namespace SuperJordi
{
	internal class Program
	{
		public const string PATH = @"../../../DADES/";

        public static void MostrarMenu()
        {
            Console.WriteLine("1- UN CLIENT ENTRA AL SUPER I OMPLE EL SEU CARRO DE LA COMPRA");
            Console.WriteLine("2- AFEGIR UN ARTICLE A UN CARRO DE LA COMPRA");
            Console.WriteLine("3- UN CARRO PASSA A CUA DE CAIXA (CHECKIN)");
            Console.WriteLine("4- CHECKOUT DE CUA TRIADA PER L'USUARI");
            Console.WriteLine("5- OBRIR SEGÜENT CUA DISPONIBLE");
            Console.WriteLine("6- INFO CUES");
            Console.WriteLine("7- CLIENTS VOLTANT PEL SUPERMERCAT");
            Console.WriteLine("8- LLISTAR CLIENTS PER RATING (DESCENDENT)");
            Console.WriteLine("9- LLISTAR ARTICLES PER STOCK (DE  - A  +)");
            Console.WriteLine("A- CLOSE QUEUE");
            Console.WriteLine("B- CLOSE ALL QUEUES");
            Console.WriteLine("0- EXIT");
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

			SuperMarket super = new SuperMarket("HIPERCAR", "C/Barna 99", PATH + "CASHIERS.TXT", PATH + "CUSTOMERS.TXT", PATH + "GROCERIES.TXT", 2);
			//
			Dictionary<Customer, ShoppingCart> carrosPassejant = new Dictionary<Customer, ShoppingCart>();

			ConsoleKeyInfo tecla;
			do
			{
				Console.Clear(); Console.WriteLine("\x1b[3J");
				MostrarMenu();
				tecla = Console.ReadKey();
				switch (tecla.Key)
				{
					case ConsoleKey.D1:
						DoNewShoppingCart(carrosPassejant, super);
						break;

					case ConsoleKey.D2:
						DoAfegirUnArticleAlCarro(carrosPassejant, super);
						MsgNextScreen("PRESS ANY KEY 2 CONTINUE");
						break;

					case ConsoleKey.D3:
						DoCheckIn(carrosPassejant, super);
						MsgNextScreen("PRESS ANY KEY 2 CONTINUE");
						break;

					case ConsoleKey.D4:
						if (DoCheckOut(super)) Console.WriteLine("BYE BYE. HOPE 2 SEE YOU AGAIN!");
						else Console.WriteLine("NO S'HA POGUT TANCAR CAP COMPRA");
						MsgNextScreen("PRESS ANY KEY 2 CONTINUE");
						break;

					case ConsoleKey.D5:
						DoOpenCua(super);
						MsgNextScreen("PRESS ANY KEY 2 CONTINUE");
						break;

					case ConsoleKey.D6:
						DoInfoCues(super);
						MsgNextScreen("PRESS ANY KEY 2 CONTINUE");
						break;

					case ConsoleKey.D7:
						DoClientsComprant(carrosPassejant);
						MsgNextScreen("PRESS ANY KEY 2 CONTINUE");

						break;
					case ConsoleKey.D8:
						DoListCustomers(super);
						MsgNextScreen("PRESS ANY KEY 2 CONTINUE");
						break;

                    case ConsoleKey.D9:
                        SortedSet<Item> articlesOrdenatsPerEstoc = super.GetItemsByStock();
                        DoListArticlesByStock("LLISTAT D'ARTICLES - DATA " + DateTime.Now, articlesOrdenatsPerEstoc);
                        MsgNextScreen("PRESS ANY KEY 2 CONTINUE");
                        break;

                    case ConsoleKey.A:
                        int line;
                        Console.Write($"LINIA A TANCAR (1 a {super.MAXLines}) ?");
                        line = Convert.ToInt32(Console.ReadLine());
                        DoCloseLine(super, line);
                        MsgNextScreen("PRESS ANY KEY 2 CONTINUE");
                        break;

                    case ConsoleKey.B:
                        DoCloseLine(super);
                        MsgNextScreen("PRESS ANY KEY 2 CONTINUE");
                        break;

                    case ConsoleKey.D0:
                        MsgNextScreen("PRESS ANY KEY 2 EXIT");
                        break;
                    default:
                        MsgNextScreen("Error. Prem una tecla per tornar al menú...");
                        break;
                }

			} while (tecla.Key != ConsoleKey.D0);


		}
		//OPCIO 1 - Entra un nou client i se li assigna un carro de la compra. S'omple el carro de la compra
		/// <summary>
		/// Crea un nou carro de la compra assignat a un Customer inactiu
		/// L'omple d'articles aleatòriament 
		/// i l'afegeix als carros que estan passejant pel super
		/// </summary>
		/// <param name="carros">Llista de carros que encara no han entrat a cap 
		/// cua de pagament</param>
		/// <param name="super">necessari per poder seleccionar un client inactiu</param>
		public static void DoNewShoppingCart(Dictionary<Customer, ShoppingCart> carros, SuperMarket super)
		{
			Console.Clear(); Console.WriteLine("\x1b[3J");
			ShoppingCart novaCompra = new ShoppingCart((Customer)super.GetAvailableCustomer(), DateTime.Now);
			novaCompra.AddAllRandomly(super.Warehouse);
			carros.Add(novaCompra.Customer, novaCompra);
			Console.WriteLine("NOU CARRO DE LA COMPRA ACTIU DINS EL SUPER: ");
			Console.WriteLine(novaCompra);
			MsgNextScreen("PREM UNA TECLA PER ANAR AL MENÚ PRINCIPAL");
		}

		//OPCIO 2 - AFEGIR UN ARTICLE ALEATORI A UN CARRO DE LA COMPRA ALEATORI DELS QUE ESTAN VOLTANT PEL SUPER
		/// <summary>
		/// Dels carros que van passejant pel super, 
		/// es selecciona un carro a l'atzar i un article a l'atzar
		/// i s'afegeix al carro de la compra
		/// amb una quantitat d'unitats determinada
		/// Cal mostrar el carro seleccionat abans i després d'afegir l'article.
		/// </summary>
		/// <param name="carros">Llista de carros que encara no han entrat a cap 
		/// cua de pagament</param>
		/// <param name="super">necessari per poder seleccionar un article del magatzem</param>

		public static void DoAfegirUnArticleAlCarro(Dictionary<Customer, ShoppingCart> carros, SuperMarket super)
		{
			Console.Clear(); Console.WriteLine("\x1b[3J");
			//Seleccionem carro
			if (carros.Count == 0) Console.WriteLine("NO HI HA CAP CLIENT PENDENT D'ANAR A UNA CUA");
			else
			{
				//Seleccionem carro
				Random r = new Random();
				int nCarro = r.Next(carros.Count);
				ShoppingCart carroSeleccionat = carros.ElementAt(nCarro).Value;

				Console.WriteLine("CARRO ABANS D'AFEGIR ARTICLE -->");
				Console.WriteLine(carroSeleccionat);

				//Seleccionem article
				int nArticle = r.Next(super.Warehouse.Count);
				Item item = super.Warehouse.ElementAt(nArticle).Value;
				carroSeleccionat.AddOne(item, r.Next(1, 6));

				Console.WriteLine("\nCARRO DESPRES D'AFEGIR ARTICLE -->");
				Console.WriteLine(carroSeleccionat);
				MsgNextScreen("PREM UNA TECLA PER ANAR AL MENÚ PRINCIPAL");
			}
		}

		// OPCIO 3 : Un dels carros que van pululant pel super s'encua a una cua activa
		// La selecció del carro i de la cua és aleatòria
		/// <summary>
		/// Agafem un dels carros passejant (random) i l'encuem a una de les cues actives
		/// de pagament.
		/// També hem d'eliminar el carro seleccionat de la llista de carros que passejen 
		/// Si no hi ha cap carro passejant o no hi ha cap linia activa, cal donar missatge 
		/// </summary>
		/// <param name="carros">Llista de carros que encara no han entrat a cap 
		/// cua de pagament</param>
		/// <param name="super">necessari per poder encuar un carro a una linia de caixa</param>
		public static void DoCheckIn(Dictionary<Customer, ShoppingCart> carros, SuperMarket super)
		{
			Console.Clear(); Console.WriteLine("\x1b[3J");
			if (carros.Count == 0) { Console.WriteLine("NO HI HA CAP CARRO PENDENT D'ANAR A LA CUA"); }
			else if (super.ActiveLines == 0) { Console.WriteLine("NO HI HA CAP CUA ACTIVA"); }
			else
			{
				//Seleccionem un carro dels que estan "pululant" pel super
				Random r = new Random();
				int nCarro = r.Next(carros.Count);
				ShoppingCart carroSeleccionat = carros.ElementAt(nCarro).Value;
				Console.WriteLine($"PASSA A CUA DE CAIXA EL SEGÜENT CARRO -->\n {carroSeleccionat}");

				//Seleccionem la cua 
				int numCuaSeleccionada = r.Next(1, super.MAXLines + 1);
				while (super.GetCheckOutLine(numCuaSeleccionada) is null)
					numCuaSeleccionada = r.Next(1, super.MAXLines + 1);

				super.JoinTheQueue(carroSeleccionat, numCuaSeleccionada);
				carros.Remove(carroSeleccionat.Customer); //El carro deixa d'estar "pululant" pel super
				Console.WriteLine($"ESTAT ACTUAL DE LA CUA {numCuaSeleccionada}:\n{super.GetCheckOutLine(numCuaSeleccionada)}");

			}
		}

		// OPCIO 4 - CHECK OUT D'UNA CUA TRIADA PER L'USUARI
		/// <summary>
		/// Es demana per teclat una cua de les actives (1..MAXLINES)
		/// i es fa el checkout del ShoppingCart que toqui
		/// Si no hi ha cap carro a la cua triada, es dona un missatge
		/// </summary>
		/// <param name="super">necessari per fer el checkout</param>
		/// <returns>true si s'ha pogut fer el checkout. False en cas contrari</returns>
		public static bool DoCheckOut(SuperMarket super)
		{
			bool fet = false;
			int numCua;
			Console.Write($"CUA PER FER CHECKOUT (1 a {super.MAXLines} ?");
			numCua = Convert.ToInt32(Console.ReadLine());
			
			fet = super.CheckOut(numCua);

			if (fet) Console.WriteLine($"UN CLIENT AMB CARRO DE COMPRA DESENCUAT DE LINIA {numCua}");
			else Console.WriteLine($"CAP CLIENT AMB CARRO DE COMPRA PRESENT A LA LINIA {numCua}");
			return fet;
		}

		// OPCIO 5 : Obrir la següent cua disponible (si n'hi ha)
		/// <summary>
		/// En cas que hi hagin cues disponibles per obrir, s'obre la 
		/// següent linia disponible
		/// </summary>
		/// <param name="super"></param>
		/// <returns>true si s'ha pogut obrir la cua</returns>
		public static bool DoOpenCua(SuperMarket super)
		{
			bool fet = true;
			Console.Clear(); Console.WriteLine("\x1b[3J");
			if (super.ActiveLines < super.MAXLines)
			{
				int linia = 1;
				while (super.GetCheckOutLine(linia) is not null)
					linia++;
				super.OpenCheckOutLine(linia);
				Console.WriteLine("OBERTA CUA: \n" + super.GetCheckOutLine(linia));
			}
			else
			{
				fet = false;
				Console.WriteLine("TOTES LES CUES ESTAN OBERTES. IMPOSSIBLE OBRIR-NE UNA DE NOVA");
			}
			return fet;
		}

		//OPCIO 6 : Llistar les cues actives
		/// <summary>
		/// Es llisten totes les cues actives des de l'array de cues.
		/// Apretar una tecla després de cada cua activa
		/// </summary>
		/// <param name="super"></param>

		public static void DoInfoCues(SuperMarket super)
		{
			Console.Clear(); Console.WriteLine("\x1b[3J");
			Console.WriteLine("INFORMACIÓ DE LES CUES ACTIVES");
			for (int i = 1; i <= super.MAXLines; i++)
			{
				CheckOutLine line = super.GetCheckOutLine(i);
				if (line is not null)
				{
					Console.WriteLine(line);
					MsgNextScreen("PREM UNA TECLA PER CONTINUAR");
				}
			}

		}

		// OPCIO 7 - Mostrem tots els carros de la compra que estan voltant
		// pel super però encara no han anat a cap cua per pagar
		/// <summary>
		/// Es mostren tots els carros que no estan en cap cua.
		/// Cal apretar una tecla després de cada carro
		/// </summary>
		/// <param name="carros"></param>
		public static void DoClientsComprant(Dictionary<Customer, ShoppingCart> carros)
		{
			Console.Clear(); Console.WriteLine("\x1b[3J");
			Console.WriteLine("CARROS VOLTANT PEL SUPER (PENDENTS D'ANAR A PAGAR): ");
			foreach (ShoppingCart cart in carros.Values)
			{
				Console.WriteLine(cart);
				MsgNextScreen("PREM UNA TECLA PER CONTINUAR");
			}

		}

		//OPCIO 8 : LListat de clients per rating
		/// <summary>
		/// Cal llistar tots els clients de més a menys rating
		/// Per poder veure bé el llistat, primer heu de fer uns quants
		/// checkouts i un cop fets, fer el llistat. Aleshores els
		/// clients que han comprat tindran ratings diferents de 0
		/// Jo he fet una sentencia linq per solucionar aquest apartat
		/// </summary>
		/// <param name="super"></param>
		public static void DoListCustomers(SuperMarket super)
		{

			Console.Clear(); Console.WriteLine("\x1b[3J");
			int i = 0;

			// Consulta linq per ordenar els clients per rating
			//var llistaPerRating = super.Customers.Values.ToList().OrderByDescending(c => c.GetRating);

			SortedSet<Person> llistaPerRating = new SortedSet<Person>(super.Customers.Values);

			foreach (Customer c in llistaPerRating)
			{
				Console.WriteLine(c);
				i++;
				if (i % 20 == 0) { MsgNextScreen("PREM UNA TECLA PER CONTINUAR"); }
			}

		}

		// OPCIO 9
		/// <summary>
		/// Llistar de menys a més estoc, tots els articles del magatzem
		/// </summary>
		/// <param name="header">Text de capçalera del llistat</param>
		/// <param name="items">articles que ja vindran preparats en la ordenació desitjada</param>
		public static void DoListArticlesByStock(String header, SortedSet<Item> items)
		{
			Console.WriteLine(header);
			foreach (Item i in items)
			{
				Console.WriteLine(i);
			}
		}

		// Metode per mostrar el llistat d'articles per estoc, sense trampes
		public static void DoListArticlesByStock(String header, SuperMarket super)
		{
			SortedSet<Item> items = super.GetItemsByStock();
			DoListArticlesByStock(header, items);
		}

        // OPCIO A : Tancar cua. Només si no hi ha cap client
        // tanca una cua activa sense carritos
        // DoCloseQueue
        public static void DoCloseLine(SuperMarket super, int number) 
        {
            CheckOutLine line = super.GetCheckOutLine(number);
            if (line is not null && line.Empty)
            {
                super.RemoveCheckOutLine(number);
                Console.WriteLine($"ARA LA LINIA NUMERO {number} JA ESTÀ INACTIVA");
            }
            else Console.WriteLine("NO ES POT ELIMINAR LA CUA PQ. HI HA CLIENTS PENDENTS DE PAGAR");
        }

        // OPCIO B : Tancar cua. Només si no hi ha cap client
        // tanca totes les cues actives sense carritos
        // DoCloseQueue
        public static void DoCloseLine(SuperMarket super)
        {
            for(int i = 1; i <= super.MAXLines; i++)
                DoCloseLine(super, i);
        }

		public static void MsgNextScreen(string msg)
		{
			Console.WriteLine(msg);
			Console.ReadKey();
			Console.Write("\r");
		}

		// NO USAT
		public static void DoListWarehouse(String header, SortedDictionary<int, Item> warehouse)
		{
			Console.WriteLine(header);
			foreach (KeyValuePair<int, Item> kvp in warehouse)
			{
				Console.WriteLine(kvp.Value);
			}
		}
		//NO USAT

		public static void DoListPeople(String header, Dictionary<string, Person> persones)
		{
			Console.Clear(); Console.WriteLine("\x1b[3J");

			ConsoleKeyInfo tipus;
			Console.Write("ACTIUS NOMÉS (A) o TOTS (ALTRE TECLA) ?");
			tipus = Console.ReadKey();

			Console.WriteLine(header);
			foreach (KeyValuePair<string, Person> kvp in persones)
			{
				if (tipus.Key != ConsoleKey.A || (tipus.Key == ConsoleKey.A && kvp.Value.Active))
					Console.WriteLine(kvp.Value);
			}

		}
	}
}
