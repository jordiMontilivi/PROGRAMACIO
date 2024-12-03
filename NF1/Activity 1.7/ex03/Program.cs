namespace ex03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Trobar si hi ha algun numero parell i en quina posició el trobem

            //Variables
            const string path = @"..\..\..\..\FITXERS PER CERQUES\";
            StreamReader fitxer = new StreamReader(path + "NUMEROS.txt");
            string linia;
            int  valor, cont = 1;

            //Llegir fitxer;
            //Agafem el primer valor dels possibles repetits
            linia = fitxer.ReadLine();
            valor = Convert.ToInt32(linia);

            //Mentre no trobat i no final
            while (linia != null && valor %2 != 0)
            {
                valor = Convert.ToInt32(linia);
                linia = fitxer.ReadLine();
                cont++;
            }
            fitxer.Close();
            if (valor % 2 == 0)
                Console.WriteLine($"Hem trobat un numero parell {valor} a la posició {cont}");
            else
                Console.WriteLine($"No hem trobat cap numero parell i hem recorregut tot el fitxer {cont} elements");
        }
    }
}
