namespace ListEx
{
	internal class Program
	{
		static void Main(string[] args)
		{
			List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 3, 2, 4, 9, 2 };
			// Comprovem que admet repetits i indexos
			foreach (int number in numbers)
				Console.WriteLine(number);

			for(int i = 0; i < numbers.Count; i++)
				Console.WriteLine($"Index {i}: {numbers[i]}");


			// Treballem amb Predicats / funcions lambda
			List<int> evenNumbers = numbers.FindAll(n => n % 2 != 0).ToList();
			Console.WriteLine("Even Numbers:");
			foreach (int number in evenNumbers)
				Console.WriteLine(number);

			// Treiem els nombres repetits
			List<int> uniqueNumbers = new List<int>();
			foreach (int number in numbers)
			{
				if (!uniqueNumbers.Contains(number))
					uniqueNumbers.Add(number);
			}
			// Ja existeix un mètode que fa aixo
			List<int> uniqueNumbers2 = numbers.Distinct().ToList();
		}
	}
}
