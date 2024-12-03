namespace GamingCenter
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
                tecla = Console.ReadKey();
                switch (tecla.Key)
                {
                    case ConsoleKey.D1:
                        DoJugarDaus();
                        break;
                    case ConsoleKey.D2:
                        DoJugarCaraOCreu();
                        break;
                    case ConsoleKey.D0:
                        MsgNextScreen("PRESS ANY KEY 2 EXIT");

                        break;
                    default:
                        MsgNextScreen("Error. Prem una tecla per tornar al menú...");
                        break;
                }

            } while (tecla.Key != ConsoleKey.D0);
        }

        /// <summary>
        /// mostra un missatge donat per el paràmetre msg i fa apretar una tecla
        /// </summary>
        /// <param name="msg">missatge a mostrar</param>
        public static void MsgNextScreen(string msg)
        {
            Console.WriteLine(msg);
            Console.ReadKey();
        }
        /// <summary>
        /// Fa triar el nombre de cares d'un dau a l'usuari i li fa predir el llençament 
        /// d'un dau. Informa si ho encerta o no.
        /// </summary>
        public static void DoJugarDaus()
        {
            int nCostats;
            int tiradaAleatoria, prediccioJugador;
            Random r = new Random();
            Console.Clear();
            Console.WriteLine("PRIMER DEFINIM ELS COSTATS DEL DAU ENTRE 2 i 10:");
            nCostats = LlegirEnterEntre(2, 10);
            Console.WriteLine($"EL DAU TÉ {nCostats} COSTATS NUMERATS DEL 1 AL {nCostats}");
            MsgNextScreen("PREM UNA TECLA PER COMENÇAR A JUGAR");
            tiradaAleatoria = r.Next(1, nCostats + 1);
            Console.WriteLine($"JA HE TIRAT EL DAU. QUIN NUMERO CREUS QUE HA SORTIT? ");
            prediccioJugador = LlegirEnterEntre(1, nCostats);
            if (prediccioJugador == tiradaAleatoria)
            {
                Console.WriteLine($"HAS ENCERTAT. HAVIA SORTIT {tiradaAleatoria}");
            }
            else
            {
                Console.WriteLine($"HAS FALLAT. HAVIA SORTIT {tiradaAleatoria} I TU HAS DIT {prediccioJugador}");
            }
            MsgNextScreen("PREM UNA TECLA PER ANAR AL MENU PRINCIPAL");
        }
        /// <summary>
        /// Demana per teclat un valor entre min i max inclosos , retornant el valor entrat
        /// </summary>
        /// <param name="min">valor mínim acceptat </param>
        /// <param name="max">valor màxim acceptat</param>
        /// <returns>un valor triat per l'usuari que sempre estarà entre [min,max]</returns>
        public static int LlegirEnterEntre(int min, int max)
        {
            int n;

            Console.Write($"ENTRA UN VALOR ENTRE {min} i {max}-->");
            n = Convert.ToInt32(Console.ReadLine());
            while (n < min || n > max)
            {
                Console.WriteLine("ERROR!");
                Console.Write($"ENTRA UN VALOR ENTRE {min} i {max}");
                n = Convert.ToInt32(Console.ReadLine());
            }
            return n;
        }
        /// <summary>
        /// Tira una moneda a l'atzar i li demana a l'usuari pel valor que ha sortit
        /// informant si ha encertat o ha fallat
        /// Per simplicitat demanem que l'usuari entri 0 per cara o 1 per creu.
        /// </summary>
        public static void DoJugarCaraOCreu()
        {
            Console.Clear();
            Random r = new Random(); string resultatAleatori;
            int tiradaAleatoria = r.Next(2);  // Considerem 0 = cara, 1 = creu
            int prediccioJugador;
            Console.WriteLine("JA HE TIRAT UNA MONEDA. TU QUE CREUS QUE HA SORTIT ? ENTRA 0 PER CARA o 1 PER CREU");
            prediccioJugador = Convert.ToInt32(Console.ReadLine());

            if (tiradaAleatoria == 0) resultatAleatori = "CARA";
            else resultatAleatori = "CREU";

            if (prediccioJugador == tiradaAleatoria)
            {
                Console.WriteLine($"HAS ENCERTAT. HAVIA SORTIT {resultatAleatori}");
            }
            else
            {
                Console.WriteLine($"HAS FALLAT. HAVIA SORTIT {resultatAleatori} I TU HAS PREDIT JUST EL CONTRARI");
            }
            MsgNextScreen("PREM UNA TECLA PER TORNAR AL MENÚ PRINCIPAL");
        }



        /// <summary>
        /// Mostra les opcions del nostre menú
        /// </summary>
        public static void MostrarMenu()
        {
            Console.WriteLine("1- JUGAR A ENCERTAR TIRADA D'UN DAU D'UN NOMBRE DE COSTATS DEFINIT PEL JUGADOR");
            Console.WriteLine("2- JUGAR A CARA O CREU");
            Console.WriteLine("0- EXIT");
        }
    }
}

