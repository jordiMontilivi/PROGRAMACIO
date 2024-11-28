namespace ex02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //variables
            int numAct, numAnt;
            const string path = @"..\..\..\..\FITXERS FINESTRES\";
            bool creixent = true;
            string fitxer;
            string linia;

            Console.Write("Escriu el nom d'un fitxer: ");
            fitxer = Console.ReadLine() + ".txt";

            if (File.Exists(path + fitxer))
            {
                StreamReader sr = new StreamReader(path + fitxer);

                //Valors entrada 
                //Correctament hem de controlar que el fitxer no estigui buit o tindrem errors
                numAnt = Convert.ToInt32(sr.ReadLine());
                linia = sr.ReadLine();
                numAct = Convert.ToInt32(linia);

                while (linia != null && !creixent)
                {
                    numAct = Convert.ToInt32(linia);
                    if (numAnt >= numAct)
                        creixent = false;
                    else
                    {
                        numAnt = numAct;
                        linia = sr.ReadLine();
                    }
                }
                if(creixent)
                    Console.WriteLine($"La seqüència de valors és CREIXENT");
                else
                    Console.WriteLine($"La seqüència de valors NO ES CREIXENT");
            }
            else
                Console.WriteLine($"No s'ha trobat el fitxer {fitxer} a la ruta {path}");
        }
    }
}
