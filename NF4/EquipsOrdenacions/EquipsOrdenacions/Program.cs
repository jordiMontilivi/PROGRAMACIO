using EquipsOrdenacions.Model;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace EquipsOrdenacions
{
	internal class Program
	{
		//int espais = (Console.WindowWidth - "╔══════════════════════════════════════════╗".Length) / 2 + 5;
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
		public static int MostrarMenu()
		{
			int espaisMenu;
			Centrar("╔══════════════════════════════════════════╗");
			Centrar("║              PREMIER LEAGE               ║");
			Centrar("╠══════════════════════════════════════════╣");
			Centrar("║                                          ║");
			Centrar("║    1. LLISTAT ALFABÈTIC                  ║");
			Centrar("║    2. LLISTAT PER CLASSIFICACIÓ          ║");
			Centrar("║    3. LLISTAT PER GOLS MARCATS           ║");
			Centrar("║    4. LLISTAT PER GOLS ENCAIXATS         ║");
			Centrar("║                                          ║");
			Centrar("║    0. EXIT                               ║");
			espaisMenu = Centrar("╚══════════════════════════════════════════╝");
			Console.WriteLine();
			return espaisMenu;
		}

		static public void Opcions(TaulaLlista<Equip> equips)
		{
			int espaisMenu;
			ConsoleKeyInfo option;
			Console.Clear();
			do
			{
				espaisMenu = MostrarMenu();
				option = Console.ReadKey();

				switch (option.Key)
				{
					case ConsoleKey.D1:
						equips.Sort();
						MostrarEquips(equips, espaisMenu, "Mostrar Ordenat Alfabèticament");
						Final(espaisMenu);
						break;
					case ConsoleKey.D2:
						equips.Sort(new Equip.ComparadorPunts());
						equips.Reverse();
						MostrarEquips(equips, espaisMenu, "Mostrar Ordenat Per Punts");
						Final(espaisMenu);
						break;
					case ConsoleKey.D3:
						equips.Sort(new Equip.ComparadorGolsFavor());
						equips.Reverse();
						MostrarEquips(equips, espaisMenu, "Mostrar Ordenat Per Gols a Favor");
						Final(espaisMenu);
						break;
					case ConsoleKey.D4:
						equips.Sort(new Equip.ComparadorGolsContra());
						MostrarEquips(equips, espaisMenu, "Mostrar Ordenat Per Gols en Contra");
						Final(espaisMenu);
						break;
					case ConsoleKey.D0:
						for (int i = 5; i >= 0; i--)
						{
							Centrar($"Sortint en {i} segons", espaisMenu);
							Thread.Sleep(1000);
						}
						break;
					default:
						Centrar($"Opcio no valida", espaisMenu);
						Final(espaisMenu);
						break;
				}
			} while (option.Key != ConsoleKey.D0);
		}
		static public void MostrarEquips(TaulaLlista<Equip> equips, int espais, string titol)
		{
			int index = 1;
			Console.Clear();
			Console.WriteLine();
			Centrar(titol, espais);
			Console.WriteLine("\n");
			foreach (Equip equip in equips)
				Centrar($"{index++}. {equip}", espais);
		}
		static void Final(int espais)
		{
			Console.WriteLine();
			Centrar("Prem una tecla per continuar: ", espais);
			Console.ReadKey();
			Console.Clear();
		}
		static int Centrar(string text)
		{
			int espais = (Console.WindowWidth - text.Length) / 2;
			Console.WriteLine($"{new string(' ', espais)}{text}");
			return espais;
		}
		static void Centrar(string text, int espais)
		{
			Console.WriteLine($"{new string(' ', espais)}{text}");
		}
	}
}
