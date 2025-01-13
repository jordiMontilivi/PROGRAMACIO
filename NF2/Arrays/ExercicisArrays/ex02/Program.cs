namespace ex02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] valors = GenerarTaula(5);
            MostrarTaula(valors);
        }
        static int[] GenerarTaula(int num)
        {
            Random rnd = new Random();
            int[] result = new int[num];
            for (int i = 0; i < num; i++)
                result[i] = rnd.Next(-100, 101);

            return result;
        }
        static void MostrarTaula(int[] ints)
        {
            for (int i = 0; i < ints.Length; i++)
                Console.WriteLine($"index {i}: {ints[i]}");
        }
    }
}
