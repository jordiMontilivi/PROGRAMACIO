using ClassesComuns;
namespace DictionaryEx2
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var exemples = new List<Pokemon>
			{
			new Pokemon(1, "Charmander", Pokemon.PokemonType.Fire, 5, 39, 52, 43, 65),
			new Pokemon(2, "Squirtle", Pokemon.PokemonType.Water, 5, 44, 48, 65, 43),
			new Pokemon(3, "Bulbasaur", Pokemon.PokemonType.Grass, 5, 45, 49, 49, 45),
			new Pokemon(4, "Pikachu", Pokemon.PokemonType.Electric, 5, 35, 55, 40, 90),
			new Pokemon(5, "Jigglypuff", Pokemon.PokemonType.Fairy, 5, 115, 45, 20, 20),
			new Pokemon(3, "FakeBulbasaur", Pokemon.PokemonType.Psychic, 20, 80, 80, 80, 80), // Mateix ID que Bulbasaur
            new Pokemon(2, "FakeSquirtle", Pokemon.PokemonType.Ice, 30, 70, 70, 70, 70),      // Mateix ID que Squirtle
            new Pokemon(6, "Machop", Pokemon.PokemonType.Fighting, 7, 70, 80, 50, 35),
			new Pokemon(7, "Onix", Pokemon.PokemonType.Rock, 10, 35, 45, 160, 70),
			new Pokemon(8, "Gengar", Pokemon.PokemonType.Psychic, 12, 60, 65, 60, 110)
			};


			Dictionary<int, Pokemon> pokemonsDict = new Dictionary<int, Pokemon>();
			foreach (var p in exemples)
			{
				if (!pokemonsDict.ContainsKey(p.Id))
					Console.WriteLine($"No s'ha pogut inserir (duplicat d'id): {p.Name} (Id {p.Id})");
				try { pokemonsDict.Add(p.Id, p); } // Intentem inserir-lo de totes formes
				catch (ArgumentException e)
				{
					Console.WriteLine($"No s'ha pogut inserir (duplicat d'id): {p.Name} (Id {p.Id})");
					Console.WriteLine(e.Message);
				}
			}
			Console.WriteLine("\n--- Pokémons dins del Diccionari (ordre no garantit) ---\n");
			foreach (KeyValuePair<int, Pokemon> elem in pokemonsDict)
				Console.WriteLine($"Key -> {elem.Key} | Value -> {elem.Value}");

			Console.WriteLine($"\nTotal de pokémons inserits: {pokemonsDict.Count}");
		}

	}
}
