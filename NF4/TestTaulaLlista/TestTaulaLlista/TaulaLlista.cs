using System.Collections;
using System.Text;

namespace TestTaulaLlista
{
	/// <summary>
	/// Creem una classe TaulaLlista que simuli una llista d'elements (List) de tipus genèric T List<T>
	/// <typeparamref name="T"/> és el tipus d'objecte que contindrà la TaulaLlista </typeparamref>	
	/// </summary>
	/// <interface>IEnumerable<T>, ICloneable, ICollection<T>, IList<T></interface>
	public class TaulaLlista<T> : IEnumerable<T>, ICloneable, ICollection<T>, IList<T>
	{
		#region Atributs
		private T[] dades;
		private int nElem;
		const int MIDAXDEFECTE = 10;
		#endregion

		#region Constructors
		/// <summary>
		/// Constructor per defecte. Crea una TaulaLlista de mida MIDAXDEFECTE
		/// </summary>
		public TaulaLlista() : this(MIDAXDEFECTE)
		{ }
		/// <summary>
		/// Constructor que inicialitza la TaulaLlista amb un array de mida tamany
		/// </summary>
		/// <param name="tamany">Inicialitzem l'array de dades amb una mida inicial tamany</param>
		public TaulaLlista(int tamany)
		{
			dades = new T[tamany];
			nElem = 0;
		}
		/// <summary>
		/// Constructor que inicialitza la TaulaLlista amb una llista de tipus IEnumerable
		/// </summary>
		/// <param name="llista">Inicialitzem l'array de dades amb una llista genèrica (no sabem quina estructura és) de tipus IEnumerable</param>
		public TaulaLlista(IEnumerable<T> llista)
		{
			if (llista is null) throw new ArgumentNullException("La llista no pot ser null");
			dades = new T[llista.Count()];
			nElem = 0;
			foreach (T valor in llista) //recorrem la llista nova segons el seu iterador amb un foreach
			{
				if (valor is not null) // evitem inserir nulls (podem no fer aquesta comprovació i posar elements nulls)
				{
					dades[nElem] = valor;
					nElem++;
				}
			}
		}
		#endregion

		#region Interficie ICloneable
		/// <summary>
		/// Crea una còpia de la TaulaLlista amb els seus elements clonats si es possible, amb referències independent de la TaulaLlista original.
		/// </summary>
		/// <returns>Retorna aquesta còpia clonada</returns>
		public object Clone()
		{
			return Clonar();
		}
		/// <summary>
		/// creem un mètode privat que fa el deep clone de la TaulaLlista
		/// </summary>
		/// <returns>Retorna una còpia clonada de la TaulaLlista</returns>
		/// <remarks>Fem un deep clone. Si no fem el deep clone, els objectes que conté la TaulaLlista no es clonen</remarks>
		private TaulaLlista<T> Clonar()
		{   // La següent instrucció ni en broma!
			// TaulaLlista tCopiaShallow = (TaulaLlista)this.MemberwiseClone();

			// Fem un deep clone. Així sí.
			TaulaLlista<T> tCopia = new TaulaLlista<T>(dades.Length);
			tCopia.nElem = this.nElem;
			for (int i = 0; i < nElem; i++)
			{
				// Si el tipus és ICloneable, fem un clon de l'objecte
				// Forma antiga
				// if (dades[i] is ICloneable)
				// {
				// 	ICloneable clon = (ICloneable)dades[i];
				// 	tCopia.dades[i] = (T)clon.Clone();
				// }
				if (dades[i] is ICloneable clon) //fa un clon
					tCopia.dades[i] = (T)clon.Clone();
				else
					tCopia.dades[i] = dades[i];
			}
			return tCopia;
		}
		#endregion

		#region interficie IEnumerable
		/// <summary>
		/// crea un iterador que recorre la TaulaLlista 
		/// ens permet fer automàticament un foreach
		/// </summary>
		/// <returns> retorna una interfície IEnumerator<typeparamref name="T"/> que recorre la TaulaLlista</returns>
		public IEnumerator<T> GetEnumerator()
		{
			//foreach (T valor in this.dades) yield return valor; //ja que dades és un array
			//for (int i = 0; i < nElem; i++) yield return dades[i]; //podem usar el yield return

			return new ElMeuEnumerator(this.dades, this.nElem); //retornem un enumerador nested class
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
		/// <summary>
		/// Crea una classe enumeradora que recorre la TaulaLlista
		/// </summary>
		/// <interface>IEnumerator<T></interface>
		/// <exception cref="IndexOutOfRangeException"></exception>
		public class ElMeuEnumerator : IEnumerator<T>
		{
			private int limit;
			private int posicio;
			private T[] dades;
			public ElMeuEnumerator(T[] dades, int nElem)
			{
				this.dades = dades;
				this.posicio = -1;
				this.limit = nElem;
			}
			/// <summary>
			/// Propietat per a accedir al valor actual de l'iterador o llança una excepció si no hi ha valor actual
			/// </summary>
			/// <returns>retorna el valor actual de l'iterador</returns>
			public T Current
			{
				get
				{
					if (posicio == -1 || posicio >= limit) throw new IndexOutOfRangeException("OUT OF RANGE");
					return dades[posicio];
				}
			}
			object IEnumerator.Current
			{
				get { return this.Current; }
			}
			/// <summary>
			/// Dispose() allibera la memòria ocupada per l'iterador
			/// </summary>
			public void Dispose()
			{
				this.dades = null;
			}
			/// <summary>
			/// MoveNext() avança l'iterador a la següent posició 
			/// </summary>
			/// <returns>retorna true si estem dintre de l'array o false si no</returns>
			public bool MoveNext()
			{
				bool hiHaSeguent = true;
				posicio++;
				if (posicio == limit) hiHaSeguent = false;
				return hiHaSeguent;
			}
			/// <summary>
			/// Reset() torna l'iterador a la posició inicial
			/// </summary>
			public void Reset()
			{
				posicio = -1;
			}
		}
		#endregion

		#region Interficie ICollection
		/// <summary>
		/// Retorna el nombre d'elements de la TaulaLlista
		/// </summary>
		public int Count
		{
			get { return nElem; }
		}
		/// <summary>
		/// Retorna si la TaulaLlista es només lectura
		/// </summary>
		public bool IsReadOnly
		{
			get { return false; }
		}
		/// <summary>
		/// afegeix un element a la TaulaLlista en la primera posició lliure (nElem)
		/// </summary>
		/// <param name="valor">valor a afegir de tipus genèric T</param>
		/// <exception cref="NotSupportedException"></exception>
		public void Add(T valor)
		{
			if (valor is null) throw new NotSupportedException("NO PODEM INSERIR ELEMENTS NULLS");
			if (IsReadOnly) throw new NotSupportedException("Objecte de només lectura");
			if (nElem == dades.Length) DuplicaCapacitat();
			dades[nElem] = valor;
			nElem++;

		}
		/// <summary>
		/// Esborra tots els elements de la TaulaLlista
		/// </summary>
		/// <exception cref="NotSupportedException"></exception>
		public void Clear()
		{
			if (IsReadOnly) throw new NotSupportedException("Objecte de només lectura");
			for (int n = 0; n < nElem; n++) dades[n] = default(T);
			nElem = 0;
		}
		/// <summary>
		/// Ens diu si la TaulaLlista conté l'element passat com a paràmetre
		/// </summary>
		/// <param name="target">element a buscar de tipus genèric T</param>
		/// <returns>true si l'element existeix a la TaulaLlista, false si no</returns>
		public bool Contains(T target)
		{
			return IndexOf(target) != -1;
		}

		/// <summary>
		/// A partir de l'index arrayIndex, copia en l'array de tipus T (prèviament creat), TOTS els elements de la TaulaLlista.
		/// És imprescindible que a partir de la posició arrayIndex hi càpiguen tots els elements de la TaulaLlista
		/// </summary>
		/// <param name="array">array de tipus d'elements genèric T a emplenar</param>
		/// <param name="arrayIndex">index inicial en el que comencem a inserir els elements</param>
		/// <exception cref="ArgumentNullException">Array no conté un un heap de dades</exception>
		/// <exception cref="ArgumentException">Array massa petit per al total d'elements de la taulallista</exception>
		/// <exception cref="ArgumentOutOfRangeException">L'index ha de ser >=0</exception>"
		public void CopyTo(T[] array, int arrayIndex)
		{
			if (array is null) throw new ArgumentNullException("L'array d'entrada ha d'estar creat");
			if (array.Length - arrayIndex < Count) throw new ArgumentException("Array massa petit per al total d'elements de la taulallista");
			if (arrayIndex < 0) throw new ArgumentOutOfRangeException("L'index ha de ser >=0");
			for (int i = 0; i < Count; i++) array[i + arrayIndex] = dades[i]; //en aquest cas un for és suficient ja que recorrem la taulallista desde l'index 0 fins a Count-1
			
			/* Si la iteració amb la nostra llista té alguna peculiaritat podem utilitzar un iterador foreach
			 * foreach (T valor in dades)
			 * {
			 * array[arrayIndex] = valor;
			 * arrayIndex++;
			 * }
			 */

		}
		/// <summary>
		/// Esborra un element de la TaulaLlista a partir de l'objecte passat com a paràmetre
		/// </summary>
		/// <param name="item">element a esborrar de tipus genèric T</param>
		/// <returns>Retorna true si hem pogut esborrar l'element i false si no existeix</returns>
		public bool Remove(T item)
		{
			bool trobat = true;
			int posicio = IndexOf(item);
			if (posicio != -1)
				RemoveAt(posicio);
			else trobat = false;
			return trobat;
		}
		#endregion

		#region Interficie IList
		/// <summary>
		/// Propietat Indexador, ens permet accedir a l'element de l'index tant per retornar-lo com per modificar-lo
		/// ens permet utilitzar la classe TaulaLlista com una llista d'elements
		/// </summary>
		/// <param name="idx">Index de l'element al que volem accedir</param>
		/// <returns>retorna l'element de l'index si fem un get</returns>
		/// <exception cref="IndexOutOfRangeException">Fora de Rang</exception>
		/// <exception cref="NotSupportedException">Només de Lectura</exception>
		/// <exception cref="ArgumentNullException">No podem assignar valors nulls</exception>
		public T this[int idx]

		{
			get
			{
				if (idx < 0 || idx >= nElem) throw new IndexOutOfRangeException(idx + " FORA DE RANG [" + 0 + "," + (nElem - 1) + "]");
				return dades[idx];
			}
			set
			{
				if (IsReadOnly) throw new NotSupportedException("Només Lectura");
				if (idx < 0 || idx >= nElem) throw new IndexOutOfRangeException(idx + " FORA DE RANG [" + 0 + "," + (nElem - 1) + "]");
				if (value is null) throw new ArgumentNullException("NO PODEM ASSIGNAR VALORS NULLS"); //excepció no obligatòria
				dades[idx] = value;
			}
		}
		/// <summary>
		/// Obtenim l'index de l'element genèric T passat com a paràmetre
		/// </summary>
		/// <param name="target">element a buscar de tipus genèric T</param>
		/// <returns>retorna l'index de l'element passat com a paràmetre</returns>
		public int IndexOf(T target)
		{
			bool trobat = false;
			int i = 0;
			while (!trobat && i < nElem)
			{
				trobat = object.Equals(dades[i], target);
				if (!trobat) i++;
			}
			if (!trobat) i = -1;
			return i;
		}

		/// <summary>
		/// Insereix un element a la TaulaLlista a la posició indicada per l'index
		/// </summary>
		/// <param name="posicio">Posició on vols afegir un element</param>
		/// <param name="valor">Element genèric T que volem afegir</param>
		/// <exception cref="NotSupportedException"></exception>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="IndexOutOfRangeException"></exception>
		public void Insert(int posicio, T valor)
		{
			if (IsReadOnly) throw new NotSupportedException("Només Lectura");
			if (valor is null) throw new ArgumentNullException("NO PODEM INSERIR VALORS NULLS");
			if (posicio > nElem) throw new IndexOutOfRangeException("No es pot inserir més enllà de la darrera posició vàlida");
			if (posicio < 0) throw new IndexOutOfRangeException("No es pot inserir en posicions negatives");
			else if (posicio == nElem) Add(valor);
			else
			{
				if (nElem == dades.Length) DuplicaCapacitat();
				for (int i = nElem; i > posicio + 1; i--) dades[i] = dades[i - 1];
				dades[posicio] = valor;
				nElem++;
			}
		}
		/// <summary>
		/// Esborra un element de la TaulaLlista a partir de la seva posició
		/// </summary>
		/// <param name="posicio">posició de la que volem eliminar l'element</param>
		/// <exception cref="NotSupportedException"></exception>
		/// <exception cref="IndexOutOfRangeException"></exception>
		public void RemoveAt(int posicio)
		{
			T nou;
			if (IsReadOnly) throw new NotSupportedException("Només Lectura");
			if ((posicio < 0) || (posicio >= nElem))
				throw new IndexOutOfRangeException("Índex fora de rang");
			else
			{
				nou = dades[posicio];
				for (int i = posicio; i <= nElem - 2; i++)
				{
					dades[i] = dades[i + 1];
				}
				dades[nElem - 1] = default(T);
				nElem--;

			}
		}

		#endregion

		#region altres metodes privats
		/// <summary>
		/// duplica la capacitat de l'array de dades en cas que no hi càpiguen més elements
		///</summary>
		private void DuplicaCapacitat()
		{
			T[] tTemp = new T[dades.Length * 2];
			for (int i = 0; i < dades.Length; i++) tTemp[i] = dades[i];
			dades = tTemp;
		}
		#endregion

		#region altres propietats que no son de les interficies
		public bool IsFull
		{
			get { return this.nElem == Capacity; }
		}
		public int Capacity
		{
			get { return this.dades.Length; }
		}
		#endregion

		#region altres metodes publics que no son de les interficies
		/// <summary>
		/// passem la TaulaLlista a un array de tipus T de mida Count per a no tenir espais buits
		/// ho fem amb un for ja que la nostra TaulaLlista simula un List i per tant comença a l'index 0 fins index Count-1
		/// </summary>
		/// <returns>retorna un array de tipus T amb els elements de la TaulaLlista</returns>
		public T[] ToArray()
		{
			T[] arrayOut = null;
			if (nElem != 0)
			{
				arrayOut = new T[Count];
				for (int i = 0; i < nElem; i++)
					arrayOut[i] = dades[i];
			}
			return arrayOut;
		}
		/// <summary>
		/// Fem el mateix que ToArray() però utilitzant un iterador de tipus IEnumerator retornat per GetEnumerator()
		/// mentre que MoveNext() sigui true emplenant l'arrayOut amb el valor actual (Current) de l'iterador
		/// </summary>
		/// <returns>retorna un array de tipus T amb els elements de la TaulaLlista</returns>
		public T[] ToArray2()
		{
			T[] final = new T[Count];
			int i = 0;
			IEnumerator<T> it = this.GetEnumerator();
			while (it.MoveNext())
			{
				final[i] = it.Current;
				i++;
			}
			return final;
		}
		/// <summary>
		/// Fem el mateix que ToArray() però utilitzant un iterador automàtic utilitzant el foreach
		/// </summary>
		/// <returns>retorna un array de tipus T amb els elements de la TaulaLlista</returns>
		public T[] ToArray3()
		{
			T[] final = new T[Count];
			int i = 0;
			foreach (T valor in this)
			{
				final[i] = valor;
				i++;
			}
			return final;
		}
		/// <summary>
		/// Busquem l'última aparició de l'element genèric T passat com a paràmetre
		/// </summary>
		/// <param name="target">element a buscar de tipus genèric T</param>
		/// <returns>retorna l'index de l'última aparició de l'element passat com a paràmetre</returns>
		public int LastIndexOf(T target)
		{
			bool trobat = false;
			int i = nElem - 1;
			while (!trobat && i >= 0)
			{
				trobat = object.Equals(dades[i], target);
				if (!trobat) i--;
			}
			if (!trobat) i = -1;
			return i;
		}
		/// <summary>
		/// intercanviem els valors dels index de donant la volta als valors de l'array
		/// minimitzant el nombre d'iteracions (fem la meitat) ja que amb un auxiliar canviem l'últim valor amb el primer i el primer amb l'últim...
		/// </summary>
		public void Reverse()
		{
			T aux;
			for (int i = 0; i < nElem / 2; i++)
			{
				aux = dades[i];
				dades[i] = dades[nElem - i - 1];
				dades[nElem - i - 1] = aux;
			}
		}
		/// <summary>
		/// Ordenem la TaulaLlista amb el mètode Sort de la classe Array
		/// </summary>
		public void Sort()
		{
			Array.Sort(dades);
		}
		/// <summary>
		/// Ordenem la TaulaLlista amb el mètode Sort de la classe Array amb un comparador
		/// </summary>
		public void Sort(IComparer<T> comparador)
		{
			Array.Sort(dades, comparador);
		}

		#endregion

		#region sobreescriptures d'Object
		/// <summary>
		/// Retorna una cadena de text que representa la TaulaLlista
		/// sobreescriu el mètode ToString() de la classe Object
		/// </summary>
		/// <returns>Retorna una cadena de text que representa la TaulaLlista</returns>
		public override string ToString()
		{
			StringBuilder sOut = new StringBuilder("[ ");
			for (int i = 0; i < nElem - 1; i++)
			{
				sOut.Append(this.dades[i].ToString() + " , \n");
			}
			sOut.Append(this.dades[nElem - 1].ToString() + " ]");
			return sOut.ToString();
		}
		/// <summary>
		/// direm que dos taulallistes son iguals si tenen el mateix nombre d'elements i els mateixos valors als mateixos index
		/// </summary>
		/// <param name="obj">objecte obj de la classe pare Object a comparar</param>
		/// <returns>true si son iguals, false si no</returns>
		public override bool Equals(object obj)
		{
			bool iguals = false;
			if (obj is null) iguals = this is null;
			else if (obj is TaulaLlista<T>)
			{
				TaulaLlista<T> tParam = (TaulaLlista<T>)obj;
				iguals = tParam.Count == this.Count;
				if (iguals)
				{
					int i = 0;
					while (iguals && i < Count)
					{
						iguals = object.Equals(this.dades[i], tParam.dades[i]);
						if (iguals) i++;
					}
				}
			}
			return iguals;
		}
		#endregion

	}
}
