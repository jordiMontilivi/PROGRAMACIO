namespace ex06b
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Variables
            //Versio 2. Rang amb finestres
            //la ventaja de fer-ho amb rang és que podem controlar més de dos missatges
            string missatgeAnt;
            int cont = 0, rang = 1;
            const string path = @"..\..\..\..\FITXERS FINESTRES\";
            string fitxer = "";
            string linia;
            char resposta = 's';
            char opcio = '0';

            while (resposta == 's')
            {
                Console.WriteLine("Fitxers");
                Console.WriteLine("1.- SOS_SI");
                Console.WriteLine("2.- SOS_NO");
                Console.Write("Tria un fitxer: ");

                opcio = Console.ReadKey().KeyChar;
                Console.WriteLine();

                if (opcio == '1')
                    fitxer = "SOS_SI.TXT";
                else if (opcio == '2')
                    fitxer = "SOS_NO.TXT";
                else
                    Console.WriteLine("No has triat una opcio valida");

                if (File.Exists(path + fitxer))
                {
                    StreamReader sr = new StreamReader(path + fitxer);

                    //Valors entrada 
                    //Correctament hem de controlar que el fitxer no estigui buit o tindrem errors
                    linia = sr.ReadLine();
                    if (linia == null)
                        Console.WriteLine("El fitxer està buit");
                    else
                    {
                        missatgeAnt = linia;
                        linia = sr.ReadLine();
                        while (linia != null)
                        {
                            if (missatgeAnt == linia && missatgeAnt == "... --- ...")
                                rang++;
                            else
                                rang = 1;
                            //segons aquesta condició, si el rang és 2, incrementem el contador
                            //si envia més de dos missatges de SOS consecutius considerem que es el mateix perill
                            if (rang == 2)
                                cont++;

                            missatgeAnt = linia;
                            linia = sr.ReadLine();
                        }
                        if (cont != 0)
                            Console.WriteLine($"El vaixell ha enviat {cont} vegades {rang} missatges de SOS consecutius");
                        else
                            Console.WriteLine($"El vaixell no ha enviat dos missatges de SOS consecutius");
                    }
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
