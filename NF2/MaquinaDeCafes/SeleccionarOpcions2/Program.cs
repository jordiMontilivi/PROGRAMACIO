namespace SeleccionarOpcions2
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

                switch (tecla.Key)
                {
                    case ConsoleKey.D1:
                        Console.WriteLine("\nHAS SELECCIONAT LA TECLA 1");
                        break;
                    case ConsoleKey.D2:
                        Console.WriteLine("\nHAS SELECCIONAT LA TECLA 2");
                        break;
                    case ConsoleKey.D3:
                        Console.WriteLine("\nHAS SELECCIONAT LA TECLA 3");
                        break;
                    case ConsoleKey.D0:
                        Console.WriteLine("\nHAS FINALITZAT EL PROGRAMA.");
                        break;
                    default:
                        Console.WriteLine("\nOPCIÓ NO VÀLIDA.");
                        break;
                }
            } while (tecla.Key != ConsoleKey.D0);
        }
    }
}