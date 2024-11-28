namespace ex01b
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //variables
            int numAct = -1, numAnt, numAnt2;
            const string path = @"..\..\..\..\FITXERS FINESTRES\";
            StreamReader sr = new StreamReader(path + "NUMEROS1.txt");
            string linia;

            //Valors entrada
            linia = sr.ReadLine();
            if (linia != null)
            {
                numAnt2 = Convert.ToInt32(linia);
                linia = sr.ReadLine();
                if (linia != null)
                {
                    numAnt = Convert.ToInt32(linia);
                    linia = sr.ReadLine();
                    if (linia != null)
                    {
                        while (linia != null && (numAct = Convert.ToInt32(linia)) != numAnt + numAnt2)
                        {
                            numAct = Convert.ToInt32(linia);
                            numAnt2 = numAnt;
                            numAnt = numAct;
                            linia = sr.ReadLine();
                            //if(linia != null)
                            //    numAct = Convert.ToInt32(linia);
                        }
                        sr.Close();
                        //Mostrar resultats
                        if (numAct == numAnt + numAnt2)
                            Console.WriteLine($"Si ha trobat");
                        else
                            Console.WriteLine("No ha trobat");
                    }
                    else
                        Console.WriteLine("Nomes tenim 2 valors al fitxer");
                }
                else
                    Console.WriteLine("Nomes tenim un valor al fitxer");
            }
            else
                Console.WriteLine("No tenim cap valor al fitxer");
        }
    }
}
