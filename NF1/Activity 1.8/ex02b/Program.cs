namespace ex02b
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //variables
            int numAct, numAnt;
            const string path = @"..\..\..\..\FITXERS FINESTRES\";
            string fitxer;
            string linia;

            Console.Write("Escriu el nom d'un fitxer: ");
            fitxer = Console.ReadLine() + ".txt";

            if (File.Exists(path + fitxer))
            {
                StreamReader sr = new StreamReader(path + fitxer);

                //Valors entrada
                numAnt = Convert.ToInt32(sr.ReadLine());
                linia = sr.ReadLine();

                while (linia != null && (numAct = Convert.ToInt32(linia)) < numAct)
                {
                    numAnt = numAct;
                    linia = sr.ReadLine();
                }
                if (linia == null)
                    Console.WriteLine($"La seqüència de valors és CREIXENT");
                else
                    Console.WriteLine($"La seqüència de valors NO ES CREIXENT");
            }
            else
                Console.WriteLine($"No s'ha trobat el fitxer {fitxer} a la ruta {path}");
        }
    }
}
