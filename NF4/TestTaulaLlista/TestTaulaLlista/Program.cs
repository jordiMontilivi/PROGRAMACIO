namespace TestTaulaLlista
{
    class Program
    {
        static void Main(string[] args)
        {
            //ProvaAmbEnter();
            //ProvaAmbString();
            ProvaAmbProducte();
        }

        static void ProvaAmbEnter()
        {
            Console.WriteLine("\nProva amb int");
            TaulaLlista<int> llista = new TaulaLlista<int>();
            try
            {
                llista.Add(10);
                llista.Add(20);
                llista.Add(30);
                Console.WriteLine("Contingut: " + string.Join(", ", llista.ToArray()));
                Console.WriteLine("Índex de 20: " + llista.IndexOf(20));
                llista.Remove(20);
                Console.WriteLine("Després d'eliminar 20: " + string.Join(", ", llista.ToArray()));
                Console.WriteLine("Element a la posició 1: " + llista[1]);
                llista.Insert(1, 25);
                Console.WriteLine("Després d'inserir 25: " + string.Join(", ", llista.ToArray()));
                llista.RemoveAt(0);
                Console.WriteLine("Després d'eliminar el primer element: " + string.Join(", ", llista.ToArray()));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        static void ProvaAmbString()
        {
            Console.WriteLine("\nProva amb string");
            TaulaLlista<string> llista = new TaulaLlista<string>();
            try
            {
                llista.Add("Hola");
                llista.Add("Món");
                llista.Add("Test");
                Console.WriteLine("Contingut: " + string.Join(", ", llista.ToArray()));
                Console.WriteLine("Índex de 'Món': " + llista.IndexOf("Món"));
                llista.Remove("Hola");
                Console.WriteLine("Després d'eliminar 'Hola': " + string.Join(", ", llista.ToArray()));
                llista.Insert(0, "Benvingut");
                Console.WriteLine("Després d'inserir 'Benvingut': " + string.Join(", ", llista.ToArray()));

                Console.WriteLine("Intentant accedir a un índex fora de rang...");
                try { Console.WriteLine(llista[10]); } catch (Exception ex) { Console.WriteLine("Error esperat: " + ex.Message); }

                Console.WriteLine("Intentant eliminar un element inexistent...");
                Console.WriteLine("Eliminació de 'Inexistent': " + llista.Remove("Inexistent"));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        static void ProvaAmbProducte()
        {
            Console.WriteLine("\nTEST TAULALLISTA<PRODUCTE>");
			Console.WriteLine("_____________________________\n\n");
			TaulaLlista<Producte> llista = new TaulaLlista<Producte>();
            try
            {
                //ELEMENTS BASE
                int i;
                Producte p1 = new Producte(0, "Llapis", 0.50, 100, "Escolar");
                Producte p2 = new Producte(1, "Goma", 0.75, 50, "Escolar");
                Producte p3 = new Producte(2, "Bolígraf", 1.20, 75, "Escolar");
                Producte p4 = new Producte(3, "Rotulador", 1.5, 50);
                Producte p5 = new Producte(4, "Compàs", 4.4, 15);

                //AFEGIM ELEMENTS
                llista.Add(p1);
                llista.Add(p2);
                llista.Add(p3);
                llista.Add(p4);
                llista.Add(p5);

                //Console.WriteLine("ICollection<T>");

                Console.WriteLine("ADD\n");

                Console.WriteLine("Contingut:");
                //Indica quants elements tenim
                Console.WriteLine($"Afegits {llista.Count} productes");
                i = 0;
                foreach (var p in llista){ Console.WriteLine($"index {i++} -> {p}");}
                Console.WriteLine("________________________________________________________________________\n\n");

                //Busquem index
                Console.WriteLine("CONTAINS\n");

                Console.WriteLine($"La llista conté {p2.Nom} -> {llista.Contains(p2)}");
                if (llista.Contains(p2))
                    Console.WriteLine($"La llista conté el producte {p2}");
                else
                    Console.WriteLine($"La llista no conté el producte {p2}");
                Console.WriteLine();

                //busquem si la llista conté un altre producte amb el mateix index
                Console.WriteLine($"La llista conté {new Producte(2, "Grapadora", 3.20, 25).Nom} -> {llista.Contains(new Producte(2, "Grapadora", 3.20, 25))}");
                if (llista.Contains(new Producte(2, "Grapadora", 3.20, 25)))
                    Console.WriteLine($"La llista conté el producte {new Producte(2, "Grapadora", 3.20, 25)}");
                else
                    Console.WriteLine($"La llista no conté el producte {new Producte(2, "Grapadora", 3.20, 25)}");
                Console.WriteLine($"Com el {llista[new Producte(2, "Grapadora", 3.20, 25).Id].Nom} té el mateix id ({new Producte(2, "Grapadora", 3.20, 25).Id}) que {new Producte(2, "Grapadora", 3.20, 25).Nom} diem que si la conté");
                Console.WriteLine($"({llista[new Producte(2, "Grapadora", 3.20, 25).Id]}) --> ({new Producte(2, "Grapadora", 3.20, 25)})");
                Console.WriteLine();

                //La llista no conté la Grapadora amb id 5 ja que es un nou producte
                Console.WriteLine($"La llista conté {new Producte(5, "Grapadora", 3.20, 25).Nom} -> {llista.Contains(new Producte(5, "Grapadora", 3.20, 25))}");
                if (llista.Contains(new Producte(5, "Grapadora", 3.20, 25)))
                    Console.WriteLine($"La llista conté el producte {new Producte(5, "Grapadora", 3.20, 25)}");
                else
                    Console.WriteLine($"La llista no conté el producte {new Producte(5, "Grapadora", 3.20, 25)}");
                Console.WriteLine("________________________________________________________________________\n\n");

                //index
                Console.WriteLine("INDEXOF\n");
                Console.WriteLine($"index del producte {p2.Nom}: " + llista.IndexOf(p2));
                //Mateix exemple d'abans amb Grapadora amb index de Boligraf
                Console.WriteLine($"index del producte {llista[2].Nom}: " + llista.IndexOf(new Producte(2, "Grapadora", 3.20, 25))); // No és boligraf, però com només mira index retornarà 2
                try
                {
                    Console.WriteLine($"index del producte {new Producte(5, "Grapadora", 3.20, 25).Nom}: " + llista.IndexOf(new Producte(5, "Grapadora", 3.20, 25)));
                } // index Producte no existent
                catch (Exception ex) { Console.WriteLine("Error ", ex.Message); }
                Console.WriteLine("________________________________________________________________________\n\n");

                //Eliminem Elements
                Console.WriteLine("REMOVE\n");

                Console.WriteLine($"Eliminant de producte ({p1.Nom}) -> {llista.Remove(p1)}");
                Console.WriteLine($"Després d'eliminar {p1.Nom}:");
                Console.WriteLine($"Tenim {llista.Count} Productes");
                i = 0;
                foreach (var p in llista) { Console.WriteLine($"index {i++} -> {p}"); }
                Console.WriteLine();
                
                Console.WriteLine("Intentant eliminar un producte inexistent...");
                Producte p6 = new Producte(6, "Tisores", 2.50, 20);
                try
                {
                    Console.WriteLine($"Eliminant de producte inexistent ({p6.Nom}) -> {llista.Remove(p6)}");
                    Console.WriteLine($"Tenim {llista.Count} Productes");
                    llista.Remove(new Producte(9, "Grapadora", 3.20, 25));
                } // Eliminem producte no existent
                catch (Exception ex) { Console.WriteLine("Error ", ex.Message); }
                Console.WriteLine("________________________________________________________________________\n\n");

				//Eliminem Elements per posició
				Console.WriteLine("REMOVEAT\n");

                Producte aux = llista[2]; //guardem producte posició 2 auxiliar
				llista.RemoveAt(2);
				Console.WriteLine($"Eliminem element a la llista en posició {aux.Id} {aux.Nom}");
                Console.WriteLine($"Tenim {llista.Count} Productes");
                i = 0;
                foreach (var p in llista) { Console.WriteLine($"index {i++} -> {p}"); }

                Console.WriteLine("Eliminem el producte a la posició -1");
                try { llista.RemoveAt(-1); } // Eliminem producte posició no valida
                catch (Exception ex) { 
                    Console.WriteLine("Error ", ex.Message);
                    Console.WriteLine($"Tenim {llista.Count} Productes");
                }
                Console.WriteLine("________________________________________________________________________\n\n");

                //Inserim elements
                Console.WriteLine("INSERT\n");

                llista.Insert(0, new Producte(7, "Regle", 1.25, 30));
                Console.WriteLine($"Després d'inserir {llista[0].Nom} : en la posició {llista.IndexOf(new Producte(7, "Regle", 1.25, 30))}");
                i = 0;
                foreach (var p in llista) { Console.WriteLine($"index {i++} -> {p}"); }

                Console.WriteLine($"Inserim {new Producte(8, "Grapadora", 3.20, 25).Nom} en -1");
                try { llista.Insert(-1, new Producte(8, "Grapadora", 3.20, 25)); } // Inserim producte posició no valida
                catch (Exception ex) { Console.WriteLine("Error ", ex.Message); }
                Console.WriteLine("________________________________________________________________________\n\n");

                //Utilitzem iterador (this)
                Console.WriteLine("THIS\n");

                Console.WriteLine($"Accedim a l'element de la segona posició... {llista[2].Nom}");
                Console.WriteLine($"index 2 {llista[2]}");

                Console.WriteLine($"Intentant accedir a un índex fora de rang... index {llista.Count + 5}");
                try { Console.WriteLine(llista[llista.Count + 5]); } // Posició fora de rang
                catch (Exception ex) { Console.WriteLine("Error esperat: " + ex.Message); }
                Console.WriteLine("________________________________________________________________________\n\n");

                //Clonem la llista
                Console.WriteLine("CLONE\n");
                TaulaLlista<Producte> llistaClonada = (TaulaLlista<Producte>)llista.Clone();
                Console.WriteLine("Mostrem llista Clonada");
                i = 0;
                foreach (var p in llistaClonada) { Console.WriteLine($"index {i++} -> {p}"); }
                Console.WriteLine("________________________________________________________________________\n\n");

                //Esborrem la llista
                llista.Clear();
                //llista.Add(new Producte(9, "estoig", 3.50, 100, "Escolar"));
                Console.WriteLine("Mostrem llista");
                i = 0;
                foreach (var p in llista) { Console.WriteLine($"index {i++} -> {p}"); }
                Console.WriteLine();
                
                Console.WriteLine("Mostrem llista Clonada");
                i = 0;
                foreach (var p in llistaClonada) { Console.WriteLine($"index {i++} -> {p}"); }
                Console.WriteLine("________________________________________________________________________\n\n");

				//Provem els mètodes d'Ordenació
				Console.WriteLine("Tornem a crear una llista de Productes\n");
				llista.Add(p1);
				llista.Add(p2);
				llista.Add(p3);
				llista.Add(p4);
				llista.Add(p5);
                llista.Add(new Producte(5, "Grapadora", 3.20, 25, "Institut"));
				llista.Add(new Producte(6, "Tisores", 2.50, 20, "Institut"));
				llista.Add(new Producte(7, "Regle", 1.25, 30, "Institut"));
				llista.Add(new Producte(8, "Estoig", 3.20, 25));
                llista.Add(new Producte(9, "Carpeta", 4.50, 15, "Institut"));
				Console.WriteLine("Mostrem llista\n");
				i = 0;
				foreach (var p in llista) { Console.WriteLine($"index {i++} -> {p}"); }
				Console.WriteLine("\nSORT\n");
				Console.WriteLine("Ordenem per id\n");
				llista.Sort();
				i = 0;
				foreach (var p in llista) { Console.WriteLine($"index {i++} -> {p}"); }
				Console.WriteLine("\nOrdenem per nom\n");
				llista.Sort(new Producte.ComparaNom());
				i = 0;
				foreach (var p in llista) { Console.WriteLine($"index {i++} -> {p}"); }
				Console.WriteLine("\nOrdenem per preu\n");
				llista.Sort(new Producte.ComparaPreu());
				i = 0;
				foreach (var p in llista) { Console.WriteLine($"index {i++} -> {p}"); }
				Console.WriteLine("________________________________________________________________________\n\n");
			}

			catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
