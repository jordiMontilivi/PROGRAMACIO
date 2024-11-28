namespace ex01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //variables
            int numAct, numAnt, numAnt2;
            bool sumaAnt = false;
            const string path = @"..\..\..\..\FITXERS FINESTRES\";
            StreamReader sr = new StreamReader(path + "NUMEROS2.txt");
            string linia;

            //Valors entrada
            numAnt2 = Convert.ToInt32(sr.ReadLine());
            numAnt = Convert.ToInt32(sr.ReadLine());
            linia = sr.ReadLine();
            while (linia != null && !sumaAnt)
            {
                numAct = Convert.ToInt32(linia);
                if (numAct == numAnt + numAnt2)
                    sumaAnt = true;
                //següent valor
                else
                {
                    numAnt2 = numAnt;
                    numAnt = numAct;
                    linia = sr.ReadLine();
                }
            }
            sr.Close();
            //Mostrar resultats
            if (sumaAnt)
                Console.WriteLine($"Si ha trobat");
            else
                Console.WriteLine("No ha trobat");
        }
    }
}
