namespace SeleccionarOpcions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleKeyInfo tecla;
            do
            {
                Console.Write("ENTRA UNA TECLA DE 0 A 3: ");
                tecla = Console.ReadKey();

                if (tecla.Key == ConsoleKey.D1)
                {
                    Console.WriteLine("\nHAS SELECCIONAT LA TECLA 1");
                }
                else if (tecla.Key == ConsoleKey.D2)
                {
                    Console.WriteLine("\nHAS SELECCIONAT LA TECLA 2");
                }
                else if (tecla.Key == ConsoleKey.D3)
                {
                    Console.WriteLine("\nHAS SELECCIONAT LA TECLA 3");
                }
                else if (tecla.Key == ConsoleKey.D0)
                {
                    Console.WriteLine("\nHAS FINALITZAT EL PROGRAMA.");
                }
                else
                {
                    Console.WriteLine("\nOPCIÓ NO VÀLIDA.");
                }
            } while (tecla.Key != ConsoleKey.D0);
        }
    }
}