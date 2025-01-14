namespace ex06
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] array = { 2, 5, -8, 9, 1, 3, -5, 6, 24, 13 };
            int num = 3;
            Console.WriteLine(IndexOf(array, num));
            Console.WriteLine(IndexOf(GenerarTaula(10), 7));
        }
        public static int IndexOf(int[] t, int valor)
        {
            int index = 0;
            bool trobat = false;
            while(!trobat && index < t.Length)
            {
                if (t[index] == valor)
                    trobat = true;
                else
                    index++;

            }
            if (!trobat)
                index = -1;

            return index;   
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
