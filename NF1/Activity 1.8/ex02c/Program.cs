namespace ex02c
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //variables
            int numAct, numAnt;
            const string path = @"..\..\..\..\FITXERS FINESTRES\";
            bool creixent = true;
            string fitxer = "merda";
            string linia;
            char resposta = 's';
            char opcio = '0';

            while (resposta == 's')
            {
                Console.WriteLine("Fitxers");
                Console.WriteLine("1.- Creixent");
                Console.WriteLine("2.- No Creixent");
                Console.Write("Tria un fitxer: ");
                opcio = Console.ReadKey().KeyChar;
                Console.WriteLine();
                if (opcio == '1')
                    fitxer = "CREIXENT.TXT";
                else if (opcio == '2')
                    fitxer = "NO_CREIXENT.TXT";
                else
                    Console.WriteLine("No has triat una opcio valida");

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
                    if (creixent)
                        Console.WriteLine($"La seqüència de valors és CREIXENT");
                    else
                        Console.WriteLine($"La seqüència de valors NO ES CREIXENT");
                }
                else
                    Console.WriteLine($"No s'ha trobat el fitxer {fitxer} a la ruta {path}");

                Console.Write("Vols llegir un altre fitxer: ");
                resposta = Console.ReadKey().KeyChar;
                Console.Clear();
            }
        }
    }
}
