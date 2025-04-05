using System.Collections;

namespace TestPila
{
	/// <summary>
	/// Implementació genèrica d'una pila (estructura LIFO) amb capacitat fixa.
	/// </summary>
	public class Pila<T> : ICollection<T>, IEnumerable<T>, ICloneable
	{
		#region Atributs
		private int top;
		private T[] dades;
		private const int DEFAULT_SIZE = 5;
		#endregion

		#region Propietats
		/// <summary>
		/// Capacitat màxima de la pila. No s'amplia automàticament.
		/// </summary>
		public int Capacity => dades.Length;

		/// <summary>
		/// Indica si la pila està plena.
		/// </summary>
		public bool IsFull => Count == Capacity;

		/// <summary>
		/// Indica si la pila està buida.
		/// </summary>
		public bool IsEmpty => Count == 0;

		/// <summary>
		/// Nombre d'elements a la pila.
		/// </summary>
		public int Count => top + 1;

		/// <summary>
		/// Indica si la pila és només de lectura.
		/// </summary>
		public bool IsReadOnly => false;

		/// <summary>
		/// Indexador per accedir als elements per posició.
		/// </summary>
		/// <param name="index">Índex de l'element a accedir.</param>
		/// <returns>Valor de l'element.</returns>
		public T this[int index]
		{
			get
			{
				if (index < 0 || index >= Count)
					throw new IndexOutOfRangeException("Índex fora dels límits de la pila");
				return dades[index];
			}
		}
		#endregion

		#region Constructors
		public Pila() : this(DEFAULT_SIZE) { }

		public Pila(int mida)
		{
			top = -1;
			dades = new T[mida];
		}

		public Pila(IEnumerable<T> estructuraDades)
		{
			top = -1;
			int count = 0;

			foreach (T valor in estructuraDades)
			{
				Push(valor);
				count++;
			}

			dades = new T[count * 2];
		}
		#endregion

		#region Mètodes Pila
		/// <summary>
		/// Treu i retorna l'element del cim de la pila.
		/// </summary>
		public T Pop()
		{
			if (IsReadOnly)
				throw new InvalidOperationException("La pila és de només lectura");

			if (IsEmpty)
				throw new InvalidOperationException("No hi ha elements a la pila");

			return dades[top--]; //Recordar que primer fa dades[top] i després decrementa top
		}

		/// <summary>
		/// Retorna l'element del cim sense treure'l.
		/// </summary>
		public T Peek()
		{
			if (IsEmpty)
				throw new InvalidOperationException("No hi ha elements a la pila");

			return dades[top];
		}

		/// <summary>
		/// Afegeix un nou element al cim de la pila.
		/// </summary>
		public void Push(T item)
		{
			if (IsReadOnly)
				throw new InvalidOperationException("La pila és de només lectura");

			if (IsFull)
				throw new StackOverflowException("La pila és plena");

			dades[++top] = item; // Recordar que primer incrementa top i després assigna
		}

		/// <summary>
		/// Garanteix que la pila tingui una capacitat mínima, en cas de ja tenir-la no fa res.
		/// </summary>
		public void EnsureCapacity(int novaCapacitat)
		{
			if (novaCapacitat > Capacity)
			{
				T[] resultat = new T[novaCapacitat];
				CopyTo(resultat, 0);
				dades = resultat;
			}
		}

		/// <summary>
		/// Retorna els elements de la pila en un nou array.
		/// </summary>
		public T[] ToArray()
		{
			T[] resultat = new T[Count];
			CopyTo(resultat, 0);
			return resultat;
		}
		#endregion

		#region ICollection<T>
		/// <summary>
		/// Add no te massa sentit en una pila, però ho implementem per a complir amb la interfície, simplement fa un Push.
		/// </summary>
		/// <param name="item">Element a afegir.</param>
		public void Add(T item) => Push(item);

		/// <summary>
		/// Elimina tots els elements de la pila.
		/// </summary>
		public void Clear()
		{
			if (IsReadOnly)
				throw new InvalidOperationException("La pila és de només lectura");

			top = -1;
			// Opcional: si volem netejar la memòria, podem fer:
			//for (int i = 0; i < Count; i++)
			//	dades[i] = default(T);
		}

		/// <summary>
		/// Comprova si la pila conté un element.
		/// </summary>
		/// <param name="item">Element a buscar.</param>
		/// <returns>True si l'element es troba a la pila, false en cas contrari.</returns>
		public bool Contains(T item)
		{
			for (int i = 0; i <= top; i++)
				if (this[i].Equals(item))
					return true;
			return false;
		}

		/// <summary>
		/// Copia els elements de la pila a un array determinat començant des de l'índex especificat.
		/// </summary>
		/// <param name="array">Array de destí.</param>
		/// <param name="arrayIndex">Índex d'inici a l'array de destí.</param>
		public void CopyTo(T[] array, int arrayIndex)
		{
			if (array == null)
				throw new ArgumentNullException(nameof(array));

			if (arrayIndex < 0 || arrayIndex >= array.Length)
				throw new ArgumentOutOfRangeException(nameof(arrayIndex));

			if (arrayIndex + Count > array.Length)
				throw new ArgumentException("No hi ha prou espai a l'array de destinació");

			for (int i = 0; i <= top; i++)
			{
				array[arrayIndex + i] = dades[i] is ICloneable c ? (T)c.Clone() : dades[i];
			}
		}

		/// <summary>
		/// Elimina l'element especificat de la pila.
		/// </summary>
		/// <param name="item">Element a eliminar.</param>
		/// <returns>True si l'element ha estat eliminat, false en cas contrari.</returns>
		public bool Remove(T item)
		{
			if (IsReadOnly)
				throw new InvalidOperationException("La pila és de només lectura");

			if (!Contains(item)) return false;

			while (!this[top].Equals(item))
				Pop();
			Pop();

			return true;
		}
		#endregion

		#region IEnumerable<T>
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		/// <summary>
		/// Genera un enumerador per a la pila.
		/// </summary>
		/// <returns>Un IEnumerator<T> per a la pila.</returns>
		public IEnumerator<T> GetEnumerator() => new ClassEnumerator(this);

		/// <summary>
		/// Classe interna per a l'enumerador de la pila.
		/// </summary>
		public class ClassEnumerator : IEnumerator<T>
		{
			private Pila<T> pila;
			private int index;

			public ClassEnumerator(Pila<T> pila)
			{
				this.pila = pila.Clone();
				index = pila.top + 1;
			}

			public T Current => pila.dades[index];

			object IEnumerator.Current => Current;

			public bool MoveNext()
			{
				index--;
				return index >= 0;
			}

			public void Reset() => index = pila.top + 1;

			public void Dispose() { }
		}
		#endregion

		#region ICloneable
		/// <summary>
		/// Retorna un hash code per a la pila.
		/// </summary>
		/// <returns>retorna una Pila<T></returns>
		public Pila<T> Clone()
		{
			Pila<T> nova = new Pila<T>(Capacity);
			for (int i = 0; i <= top; i++)
			{
				if (dades[i] is ICloneable clonable)
					nova.dades[i] = (T)clonable.Clone();
				else
					nova.dades[i] = dades[i];
			}
			nova.top = this.top;
			return nova;
		}
		/// <summary>
		/// estructura que permet clonar la pila retorna un objecte Object o un objecte Pila<T>
		/// </summary>
		/// <returns>Retorna un objecte Pila<T> clonat </returns>
		object ICloneable.Clone() => this.Clone();
		#endregion

		#region Overrides
		/// <summary>
		/// separa els elements de la pila amb punts i coma.
		/// </summary>
		/// <returns>Cadena amb els elements de la pila separats per punts i coma.</returns>
		public override string ToString()
		{
			// Retorna els elements de la pila com una cadena separada per punts i coma.
			return string.Join(";", this.ToArray());
		}
		/// <summary>
		/// Compara la pila amb un altre objecte.
		/// </summary>
		/// <param name="obj">Element a comparar de tipus Object</param>
		/// <returns>True si obj es un objecte de tipus pila i cada valor coincideix amb la nostra pila, false en cas contrari</returns>
		public override bool Equals(object? obj)
		{
			if (obj is null || obj is not Pila<T> altraPila || altraPila.Count != this.Count)
				return false;

			for (int i = 0; i <= top; i++)
				if (!this[i].Equals(altraPila[i]))
					return false;

			return true;
		}
		#endregion

		#region Mètodes alternatius ToArray
		/// <summary>
		/// Versió alternativa de ToArray amb enumerador explícit.
		/// </summary>
		public T[] ToArray2()
		{
			T[] resultat = new T[Count];
			int i = 0;
			IEnumerator<T> it = GetEnumerator();
			while (it.MoveNext())
			{
				resultat[i++] = it.Current;
			}
			return resultat;
		}

		/// <summary>
		/// Versió alternativa de ToArray amb foreach.
		/// </summary>
		public T[] ToArray3()
		{
			T[] resultat = new T[Count];
			int i = 0;
			foreach (T item in this)
			{
				resultat[i++] = item;
			}
			return resultat;
		}
		#endregion
	}

}
