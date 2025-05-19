namespace proves
{
    public class Program
    {
        static void Main(string[] args)
        {
			// Crear un objeto de la clase Cercle
			Cercle cercle = new Cercle();
			Console.WriteLine(cercle.Saludar());
			Console.WriteLine(cercle.Saludar2());
			Figura c2 = new Cercle();
			Console.WriteLine(c2.Saludar());
			Console.WriteLine(c2.Saludar2());
		}
    }
    public class Figura
	{
		public double Area { get; set; }
		public double Perimetre { get; set; }

        public string Saludar()
		{
			return "Hola desde la classe figura";
		}
		public virtual string Saludar2()
		{
			return "Hola desde la classe figura virtual";
		}
	}
	public class Cercle : Figura
	{
		public double Radio { get; set; }
		public new string Saludar()
		{
			return "Hola desde la classe cercle";
		}
		public override string Saludar2()
		{
			return "Hola desde la classe cercle virtual";
		}
	}


}
