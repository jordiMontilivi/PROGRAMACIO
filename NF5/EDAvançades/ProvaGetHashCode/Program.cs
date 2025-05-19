namespace ProvaGetHashCode
{
	internal class Program
	{
		static void Main(string[] args)
		{
			for (int i = 0; i < 100; i++)
			{
				Console.WriteLine($"HashCode: {i} -> {HashCode.Combine(i)}");
			}
			Console.WriteLine("==========================================");

			for (int i = 0; i < 100; i++)
			{
				Console.WriteLine($"HashCode: {i} -> {HashCode.Combine(i)}");
			}
			Console.WriteLine("==========================================");

			for (int i = 0; i < 100; i++)
			{
				Console.WriteLine($"HashCode: {i} -> {i.ToString().GetHashCode()}");
			}
		}
	}
}
