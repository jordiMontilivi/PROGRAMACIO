namespace HashSetEx
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HashSet<int> numbers = new HashSet<int>() { 1, 2, 3, 4, 5, 6, 3, 2, 4, 9, 2 };
			numbers.Add(1);

			Console.WriteLine("Veiem que no hi ha repetits");
			Console.WriteLine("Encara que no segueixen un ordre, la funcio per defecte del hash dels enters ens permet que ens retorne numeros simples en el mateix ordre insercio");
			foreach (int number in numbers)
			{
				Console.WriteLine(number);
			}
		}
    }
}
