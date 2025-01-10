namespace ex01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Employee e = new Employee(4, "Iker", "Lopez", 1000);
            Employee e2 = new(1, "Pau", "Crosas", 1400, 0.5, Convert.ToDateTime("01/05/1998"));
            Console.WriteLine(e.GetLastName());
            Console.WriteLine(e.LastName);

            e.SetCommission(0.1);
            e.Commission = 0.2;

            //Console.WriteLine(e.Salary);
            //e.Salary = 1000.40;
            //Console.WriteLine(e.Salary);
            //e.AnysTreballats();
            Console.WriteLine(e);
            Console.WriteLine(e.ToString("csv"));

            Console.WriteLine($"{e2} \nporta {e2.AnysTreballats()} anys a la empresa");
        }
    }
}
