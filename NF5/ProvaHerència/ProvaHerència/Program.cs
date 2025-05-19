namespace ProvaHerència
{
    internal class Program
    {
        static void Main(string[] args)
        {
			Persona p1 = new Persona(1, "Pep");
			p1.Saludar();
			Console.WriteLine("-------------------------------------------------");
			Alumne a1 = new Alumne(2, "Anna", 8, "Matemàtiques");
			a1.Saludar();
			Console.WriteLine("-------------------------------------------------");
			Console.ReadKey();
			Persona p2 = new Alumne(3, "Joan", 9, "Física");
			p2.Saludar();
			Console.ReadKey();
		}
    }
    public class Persona
    {
        private int id;
        private string nom;
		//public int Id { get; set; }
		//public string Nom { get; set; }
		public Persona(int id, string nom)
		{
			this.id = id;
			this.nom = nom;
		}

		public void Saludar()
        {
            Console.WriteLine("Hola, sóc una persona");
			Console.WriteLine($"id: {id} i nom: {nom}");
        }
    }
    public class Alumne : Persona
    {
        private int notaMitjana;
		private string assignatura;

        public Alumne(int id, string nom, int notaMitjana, string assignatura) : base (id, nom)
		{
			this.notaMitjana = notaMitjana;
			this.assignatura = assignatura;
		}

		public void Saludar()
		{
			base.Saludar();
			Console.WriteLine("Hola, sóc un alumne");
			Console.WriteLine($"nota: {notaMitjana} i Assignatura: {assignatura}");
		}
	}
}
