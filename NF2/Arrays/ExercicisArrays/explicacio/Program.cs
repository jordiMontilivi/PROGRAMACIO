namespace explicacio
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] noms;
            //Console.WriteLine(noms[2]);
            noms = new string[10];
            Console.WriteLine("hola" + noms[2] + "adeu");
            Employee[] employees = new Employee[10];
            Console.WriteLine(employees[2]);
            const int N = 10;
            Employee e = new Employee(200, "Garcia", "Pepet", 2008, 0.1, DateTime.Now);
            Employee[] empleats = new Employee[N];
            for (int i = 0; i < empleats.Length; i++)
                empleats[i] = e;
            e.Salary = 5555;
            Console.WriteLine(empleats[0]);
            Console.WriteLine(empleats[empleats.Length - 1]);

            //4
            string[] firstNames = new string[10] { "Pau", "Ricard", "David", "Celeste", "Sharuti", "Iris", "Xesc", "Claudia", "Alex", "Gurleen" };
            string[] lastNames = new string[10] { "Crosas", "Ribas", "Garcia", "Flores", "Singh", "Lopez", "de la Rosa", "Parra", "Sola", "Singh" };

            Random rnd = new Random();
            for (int i = 0; i <= firstNames.Length; i++)
            {
                empleats[i] = new Employee(i + 200, firstNames[i], lastNames[i], rnd.Next(2000, 2501));
            }
        }
        //5
        public static Employee[] GenerarEmpleats(string[] lastNames, string[] firstNames, int idInicial)
        {
            Employee[] empleats = new Employee[lastNames.Length];
            Random rnd = new Random();
            for (int i = 0; i <= firstNames.Length; i++)
            {
                empleats[i] = new Employee(i + idInicial, firstNames[i], lastNames[i], rnd.Next(2000, 2501), rnd.Next(0, 3) / 10.0, DateTime.Today);
            }
            return empleats;
        }
        //6
        public static void ShowEmployees(Employee[] plantilla)
        {
            Console.WriteLine($"Tenim una plantilla amb {plantilla.Length} empleats");
            foreach (Employee e in plantilla)
                Console.WriteLine(e);
            //Fa el mateix que l'anterior però ens permet numerar de forma còmoda els empleats
            for (int i = 0;i < plantilla.Length;i++)
            {
                Console.WriteLine($"\nEmpleat {i + 1}");
                Console.WriteLine(plantilla[i]);
            }
        }
    }
}
