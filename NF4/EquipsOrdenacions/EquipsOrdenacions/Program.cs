using EquipsOrdenacions.Model;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace EquipsOrdenacions
{
	internal class Program
	{
		static int espais; // variable global per centrar el text
		static void Main(string[] args)
		{
			TaulaLlista<Equip> equips = new TaulaLlista<Equip>();
			string linia;
			//Amb el using no cal un close del fitxer al final, és més net
			using (StreamReader sR = new StreamReader("../../../../Equips.csv"))
			{
				while ((linia = sR.ReadLine()) != null)
					equips.Add(new Equip(linia));
			}
			Opcions(equips);
		}
		public static void MostrarMenu()
		{
			Console.WriteLine("\n");
			CentrarMenu("╔══════════════════════════════════════════╗");
			CentrarMenu("║              PREMIER LEAGE               ║");
			CentrarMenu("╠══════════════════════════════════════════╣");
			CentrarMenu("║                                          ║");
			CentrarMenu("║    1. LLISTAT ALFABÈTIC                  ║");
			CentrarMenu("║    2. LLISTAT PER CLASSIFICACIÓ          ║");
			CentrarMenu("║    3. LLISTAT PER GOLS MARCATS           ║");
			CentrarMenu("║    4. LLISTAT PER GOLS ENCAIXATS         ║");
			CentrarMenu("║                                          ║");
			CentrarMenu("║    0. EXIT                               ║");
			CentrarMenu("╚══════════════════════════════════════════╝");
			Console.WriteLine();
		}

		static public void Opcions(TaulaLlista<Equip> equips)
		{
			ConsoleKeyInfo option;
			Console.Clear();
			do
			{
				MostrarMenu();
				Console.Write($"{new string(' ', espais)}"); // per tenir el cursor a la mateixa columna
				option = Console.ReadKey();
				switch (option.Key)
				{
					case ConsoleKey.D1:
						equips.Sort();
						MostrarEquips(equips, espais, "Mostrar Ordenat Alfabèticament");
						Final();
						break;
					case ConsoleKey.D2:
						equips.Sort(new Equip.ComparadorPunts());
						equips.Reverse();
						MostrarEquips(equips, espais, "Mostrar Ordenat Per Punts");
						Final();
						break;
					case ConsoleKey.D3:
						equips.Sort(new Equip.ComparadorGolsFavor());
						equips.Reverse();
						MostrarEquips(equips, espais, "Mostrar Ordenat Per Gols a Favor");
						Final();
						break;
					case ConsoleKey.D4:
						equips.Sort(new Equip.ComparadorGolsContra());
						MostrarEquips(equips, espais, "Mostrar Ordenat Per Gols en Contra");
						Final();
						break;
					case ConsoleKey.D0:
						Console.Write("\r");
						for (int i = 5; i >= 0; i--)
						{
							Console.Write($"{new string(' ', espais)} Sortint en {i} segons \r");
							Thread.Sleep(1000);
						}
						Console.Clear();
						break;
					default:
						Console.WriteLine();
						Centrar($"Opcio no valida");
						Final();
						break;
				}
			} while (option.Key != ConsoleKey.D0);
		}
		static public void MostrarEquips(TaulaLlista<Equip> equips, int espais, string titol)
		{
			int index = 1;
			Console.Clear();
			Console.WriteLine("\n");
			Centrar(titol);
			Console.WriteLine("\n");
			Centrar($"    {Equip.ArreglarString("Equip")}{"GF".PadRight(4, ' ')}{"GC".PadRight(4, ' ')}{"PT".PadRight(3, ' ')}");
			Centrar($"{new string('_', 24)}{new string('_', 4)}{new string('_', 4)}{new string('_', 3)}");
			foreach (Equip equip in equips)
				Centrar($"{$"{index++}. ".PadLeft(4, ' ')}{equip.ToString('B')}"); //Centrar($"{index++:00}. {equip.ToString('B')}");
			Centrar($"{new string('_', 24)}{new string('_', 4)}{new string('_', 4)}{new string('_', 3)}");
		}
		static void Final()
		{
			Console.WriteLine();
			Centrar("Prem una tecla per continuar: ");
			Console.Write($"{new string(' ', espais)}");
			Console.ReadKey();
			Console.Clear();
		}
		static void CentrarMenu(string text)
		{
			espais = (Console.WindowWidth - text.Length) / 2 + 5; //+5 per la barra lateral
			int espaisMenu = (Console.WindowWidth - text.Length) / 2;
			Console.WriteLine($"{new string(' ', espaisMenu)}{text}");
		}
		static void Centrar(string text) => Console.WriteLine($"{new string(' ', espais)}{text}");
		//{
		//	Console.WriteLine($"{new string(' ', espais)}{text}");
		//}
	}
}
