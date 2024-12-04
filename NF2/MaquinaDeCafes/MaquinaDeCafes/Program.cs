namespace MaquinaDeCafes
{
    internal class Program
    {

        static void Main(string[] args)
        {
            ConsoleKeyInfo tecla;
            do
            {
                Console.Clear();
                MostrarMenu();

                Console.Write("\nENTRA UNA OPCIÓ: ");
                tecla = Console.ReadKey();

                switch (tecla.Key)
                {
                    case ConsoleKey.D1:
                        DoCafe();
                        break;
                    case ConsoleKey.D2:
                        DoTallat();
                        break;
                    case ConsoleKey.D3:
                        DoCafeLlet();
                        break;
                    case ConsoleKey.D0:
                        Console.WriteLine("\nHAS FINALITZAT EL PROGRAMA.");
                        MsgNextScreen("PREM UNA TECLA PER CONTINUAR");
                        break;
                    default:
                        Console.WriteLine("\nOPCIÓ NO VÀLIDA.");
                        MsgNextScreen("PREM UNA TECLA PER CONTINUAR");
                        break;
                }
            } while (tecla.Key != ConsoleKey.D0);
        }

        public static void DoCafe()
        {
            Console.WriteLine();
            Console.WriteLine("PREPARANT EL CAFÈ...");
            Thread.Sleep(2000);
            Console.WriteLine("EL CAFÈ JA ESTÀ LLEST.");
            Thread.Sleep(1000);
            MsgNextScreen("PREM UNA TECLA PER TORNAR AL MENÚ PRINCIPAL");
        }

        public static void DoTallat()
        {
            Console.WriteLine();
            Console.WriteLine("PREPARANT EL TALLAT...");
            Thread.Sleep(2000);
            Console.WriteLine("EL TALLAT JA ESTÀ LLEST.");
            Thread.Sleep(1000);
            MsgNextScreen("PREM UNA TECLA PER TORNAR AL MENÚ PRINCIPAL");
        }

        public static void DoCafeLlet()
        {
            Console.WriteLine();
            Console.WriteLine("PREPARANT EL CAFÈ AMB LLET...");
            Thread.Sleep(2000);
            Console.WriteLine("EL CAFÈ AMB LLET JA ESTÀ LLEST.");
            Thread.Sleep(1000);
            MsgNextScreen("PREM UNA TECLA PER TORNAR AL MENÚ PRINCIPAL");
        }

        public static void MostrarMenu()
        {
            Console.WriteLine("MAQUINA DE CAFÈS");
            Console.WriteLine("----------------");
            Console.WriteLine("1. CAFÈ");
            Console.WriteLine("2. TALLAT");
            Console.WriteLine("3. CAFE AMB LLET");
            Console.WriteLine("0. SORTIR");
        }

        public static void MsgNextScreen(string msg)
        {
            Console.WriteLine(msg);
            Console.ReadKey();
        }


    }
}