namespace ex01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Variables
            int n;
            Random rnd = new Random();
            int[] ints;

            // Valors entrada
            Console.Write("Escriu el valor d'n: ");
            n = Convert.ToInt32(Console.ReadLine());

            ints = new int[n];

            // Algorisme
            for (int i = 0; i < n; i++) 
                ints[i] = rnd.Next(-100, 101);

            // Mostrar Array
            for (int i = 0; i < n; i++)
                Console.WriteLine($"index {i}: {ints[i]}");

        }
    }
}
