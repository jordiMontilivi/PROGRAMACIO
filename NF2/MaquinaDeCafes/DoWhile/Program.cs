namespace DoWhile
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleKeyInfo tecla;
            do
            {
                Console.Write("ENTRA UNA TECLA: ");
                tecla = Console.ReadKey();
                Console.WriteLine($"\nHAS ENTRAT LA TECLA {tecla.Key}");

            } while (tecla.Key != ConsoleKey.D0);
        }

    }
}