namespace arrayTony
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] noms = new string[10] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" };
            string[] cognoms = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" };
            Employee[] empleats = new Employee[10];
            Random rnd = new Random();
            for (int i = 0; i < noms.Length; i++)
            {
                empleats[i] = new Employee(200 + i, noms[i], cognoms[i], rnd.Next(2000, 2501));
            }
        }
    }
}
