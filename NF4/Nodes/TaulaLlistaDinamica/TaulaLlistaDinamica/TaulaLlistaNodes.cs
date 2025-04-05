using System.Collections;
using System.Text;

namespace TaulaLlistaDinamica
{
	/// <summary>
	/// Classe que implementa una taula dinàmica mitjançant una llista enllaçada de nodes.
	/// </summary>
	/// <typeparam name="T">Tipus dels elements que conté la taula.</typeparam>
	public class TaulaLlistaNodes<T> : IEnumerable<T>, ICollection<T>, IList<T>, ICloneable
	{
		#region Atributs
		private Node head;
		private Node tail;
		private int nElem;
		#endregion

		#region Classe Node
		/// <summary>
		/// Classe privada que representa un node de la llista enllaçada.
		/// </summary>
		private class Node
		{
			//Per a les classes del Model els atributs queden abstractes i s'implementen com a propietats.
			//public T Data { get; set; }
			//public Node Next { get; set; }

			// T as private member data type.
			private T data;
			private Node next;
			// T as return type of property.
			public Node Next
			{
				get => next;
				set => next = value;
			}
			public T Data
			{
				get { return data; }
				set { data = value; }
			}
			public Node(T t)
			{
				Next = null;
				Data = t;
			}

			public override string ToString() => Data.ToString();

			public override bool Equals(object? obj)
			{

				bool result = true;

				if (obj == null || obj is not Node otherNode)
					result = false;
				else
					result = Data.Equals(otherNode.Data);
				return result;
				//return obj is Node otherNode && Data.Equals(otherNode.Data);
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Constructor per defecte. Crea una taula buida.
		/// </summary>
		public TaulaLlistaNodes() => nElem = 0;

		/// <summary>
		/// Constructor amb un sol element inicial.
		/// </summary>
		/// <param name="item">Element inicial a afegir.</param>
		public TaulaLlistaNodes(T item)
		{
			head = new Node(item);
			tail = head;
			nElem = 1;
		}

		/// <summary>
		/// Constructor a partir d'una col·lecció enumerable.
		/// </summary>
		/// <param name="llistaEnumerable">Col·lecció d'elements a afegir.</param>
		public TaulaLlistaNodes(IEnumerable<T> llistaEnumerable)
		{
			nElem = 0;
			foreach (T item in llistaEnumerable)
			{
				if (item is ICloneable clon)
					Add((T)clon.Clone());
				else
					Add(item);
			}
		}
		#endregion

		#region Propietats
		/// <summary>
		/// Indica si la taula està buida.
		/// </summary>
		public bool Empty => Count == 0;
		#endregion

		#region Mètodes públics

		/// <summary>
		/// Afegeix una col·lecció d'elements a la taula.
		/// </summary>
		/// <param name="collection">Col·lecció d'elements a afegir.</param>
		public void AddRange(IEnumerable<T> collection)
		{
			foreach (T item in collection)
			{
				if (item is ICloneable clon)
					Add((T)clon.Clone());
				else
					Add(item);
			}
		}
		#endregion

		#region IEnumerable<T>
		/// <summary>
		/// Retorna l'enumerador de la col·lecció.
		/// </summary>
		/// <returns>Enumerador de la col·lecció.</returns>

		//public IEnumerator<T> GetEnumerator() => new EnumeradoraNodes(head);
		public IEnumerator<T> GetEnumerator()
		{
			//Node aux = head;
			//while (aux != null)
			//{
			//	yield return aux.Data;
			//	aux = aux.Next;
			//}
			return new EnumeradoraNodes(head);
		}
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		/// <summary>
		/// Retorna l'enumerador de la col·lecció.
		/// </summary>
		private class EnumeradoraNodes : IEnumerator<T>
		{
			private Node actual;
			private Node inicial;
			private bool iniciat = false;

			public EnumeradoraNodes(Node head) => inicial = head;

			public T Current => actual.Data;
			object IEnumerator.Current => Current;

			public bool MoveNext()
			{
				if (!iniciat)
				{
					actual = inicial;
					iniciat = true;
				}
				else
				{
					if (actual != null) actual = actual.Next;
				}
				return actual != null;
			}

			public void Reset()
			{
				actual = null;
				iniciat = false;
			}

			public void Dispose() { }
		}
		#endregion

		#region ICollection<T>
		/// <summary>
		/// Retorna el nombre d'elements actuals a la taula.
		/// </summary>
		public int Count => nElem;

		/// <summary>
		/// Indica si la col·lecció és només de lectura.
		/// </summary>
		public bool IsReadOnly => false;

		/// <summary>
		/// Afegeix un element al final de la taula.
		/// </summary>
		/// <param name="item">Element a afegir.</param>
		/// <exception cref="NotSupportedException">Si la taula és només de lectura.</exception>
		public void Add(T item)
		{
			if (IsReadOnly)
				throw new NotSupportedException("TaulaLlista és READ-ONLY");

			Node nou = new Node(item);
			if (tail == null)
				head = nou;
			else
				tail.Next = nou;

			tail = nou;
			nElem++;
		}

		/// <summary>
		/// Indica si un element es troba a la taula.
		/// </summary>
		/// <param name="item">Element a buscar.</param>
		/// <returns>Cert si l'element hi és, fals altrament.</returns>
		public bool Contains(T item)
		{
			bool trobat = false;
			Node aux = head;
			while (aux != null && !trobat)
			{
				if (aux.Data.Equals(item))
					trobat = true;
				else
					aux = aux.Next;
			}
			return trobat;
		}

		/// <summary>
		/// Copia el contingut de la taula a un array, començant a un índex concret.
		/// </summary>
		/// <param name="array">Array de destinació.</param>
		/// <param name="index">Índex inicial a l'array.</param>
		/// <exception cref="ArgumentNullException">Si l'array és null.</exception>
		/// <exception cref="IndexOutOfRangeException">Si l'índex està fora dels límits de l'array.</exception>
		/// <exception cref="ArgumentException">Si no hi ha prou espai a l'array per copiar-hi els elements.</exception>
		public void CopyTo(T[] array, int index)
		{
			if (array == null)
				throw new ArgumentNullException("L'array no pot ser null");

			if (index < 0 || index >= array.Length)
				throw new IndexOutOfRangeException("Índex fora dels límits de l'array");

			if (Count + index > array.Length)
				throw new ArgumentException("No hi ha prou espai a l'array per copiar els elements");

			Node aux = head;
			while (aux != null)
			{
				array[index++] = aux.Data;
				aux = aux.Next;
			}
		}

		/// <summary>
		/// Elimina un element de la taula.
		/// </summary>
		/// <param name="element">Element a eliminar.</param>
		/// <returns>Cert si s'ha eliminat, fals si no s'ha trobat.</returns>
		/// <exception cref="NotSupportedException">Si la taula és només de lectura.</exception>
		public bool Remove(T element)
		{
			if (IsReadOnly) throw new NotSupportedException("TaulaLlista és READ-ONLY");

			bool trobat = false;
			Node anterior = null;
			Node actual = head;

			while (actual != null && !trobat)
			{
				if (actual.Data.Equals(element))
				{
					trobat = true;
					if (anterior == null) // primer element (canviem el cap)
						head = actual.Next;
					else
						anterior.Next = actual.Next;

					if (actual == tail) // últim element (tambe te en compte el cas d'un sol element)
						tail = anterior;

					nElem--;
				}
				else
				{
					anterior = actual;
					actual = actual.Next;
				}
			}

			return trobat;
		}
		#region versions de Remove
		//public bool Remove(T element)
		//{
		//	if (this.IsReadOnly) throw new NotSupportedException("TaulaLlista és READ-ONLY");

		//	bool trobat = false;
		//	Node anterior = head;
		//	Node actual = null;

		//	if (head == null) trobat = false;  //Si no hi ha cap node, trobat és fals
		//	else if (head.Data.Equals(element))   //Si hem d'eliminar el primer element
		//	{
		//		trobat = true;
		//		if (tail == head)
		//		{ //Si només hi ha un node, el cap i la cua són el mateix
		//			tail = null;
		//			head = null;
		//		}
		//		else
		//			head = head.Next; //Si hi ha més d'un node, el cap es desplaça al següent
		//	}
		//	else  //2 o més nodes
		//	{
		//		anterior = head; //sabem que no es el primer o hagues entrat en l'if anterior
		//		actual = head.Next;
		//		trobat = false;
		//		while (actual != null && !trobat)
		//		{
		//			if (actual.Data.Equals(element))
		//			{
		//				trobat = true;
		//				if (actual.Next == null) //l'hem trobat com a darrer element de la llista
		//				{
		//					tail = anterior;
		//					tail.Next = null;
		//				}
		//				else  // hem trobat actual tenint un node anterior i un de posterior
		//				{
		//					anterior.Next = actual.Next;
		//					actual.Next = null;
		//				}
		//			}
		//			else  //actual no és el que busquem i ens desplacem al seguent
		//			{
		//				anterior = actual;
		//				actual = actual.Next;
		//			}

		//		}
		//	}
		//	if (trobat) nElem--;
		//	return trobat;
		//}
		//public bool Remove(T item)
		//{
		//	if (IsReadOnly)
		//		throw new NotSupportedException("TaulaLlistaNodes is Read-Only");

		//	bool result = false;

		//	if (Contains(item))
		//	{
		//		RemoveAt(IndexOf(item));
		//		result = true;
		//	}

		//	return result;
		//}
		#endregion

		/// <summary>
		/// Buida completament la taula.
		/// </summary>
		public void Clear()
		{
			head = null;
			tail = null;
			nElem = 0;
		}

		#endregion

		#region IList<T>
		private Node GetNodeIndex(int index)
		{
			if (index < 0 || index >= nElem)
				throw new IndexOutOfRangeException("The index given is out of the limits of the TaulaLlistaNodes");

			Node aux = head;
			for (int i = 0; i < index; i++)
				aux = aux.Next;

			return aux;
		}

		/// <summary>
		/// Permet accedir a un element genèric T mitjançant un índex.
		/// </summary>
		/// <param name="index">Índex de l'element.</param>
		/// <returns>Element a la posició indicada.</returns>
		/// <exception cref="IndexOutOfRangeException">Si l'índex està fora dels límits.</exception>
		public T this[int index]
		{
			get => GetNodeIndex(index).Data;
			set => GetNodeIndex(index).Data = value;
		}

		/// <summary>
		/// Retorna la posició d'un element a la taula.
		/// </summary>
		/// <param name="item">element generic T que busquem</param>
		/// <returns>index on hem troblat l'element o -1 en cas de no trobar-lo</returns>
		public int IndexOf(T item)
		{
			Node aux = head;
			int pos = -1;
			bool trobat = false;
			int i = 0;
			while (!trobat && aux != null)
			{
				if (aux.Data.Equals(item))
					trobat = true;
				else
				{
					i++;
					aux = aux.Next;
				}
			}
			if (trobat) pos = i;
			return pos;
		}
		/// <summary>
		/// Insereix un element a la posició indicada dins la llista.
		/// </summary>
		/// <param name="index">Índex on s'ha d'inserir l'element (començant per 0).</param>
		/// <param name="item">L'element que es vol inserir.</param>
		/// <exception cref="NotSupportedException">Llançada si la col·lecció és de només lectura.</exception>
		/// <exception cref="ArgumentOutOfRangeException">Llançada si l’índex és fora dels límits permesos.</exception>
		public void Insert(int index, T item)
		{
			if (IsReadOnly)
				throw new NotSupportedException("TaulaLlista és de només lectura");

			if (index < 0 || index > nElem)
				throw new ArgumentOutOfRangeException(nameof(index), "L'índex està fora dels límits de la TaulaLlista");

			Node nou = new Node(item);
			if (head == null) // Si no hi ha cap node, el nou node es el primer i últim
			{
				head = nou;
				tail = nou;
			}
			else if (index == 0) // Si el nou node es el primer, el cap es el nou node
			{
				nou.Next = head;
				head = nou;
			}
			else if (index == nElem) // Si el nou node es l'últim, la cua es el nou node
			{
				//this.Add(item); //si sabem que es l'últim, podem fer servir el mètode Add
				tail.Next = nou;
				tail = nou;
			}
			else // Si el nou node es un node intermig, busquem la posició i l'inserim
			{
				Node aux = head; //creem un node per anar iterant fins trobar la posició
				for (int i = 0; i < index - 1; i++)
					aux = aux.Next;
				nou.Next = aux.Next;
				aux.Next = nou;
			}
			nElem++;
		}
		/// <summary>
		/// Elimina l’element en la posició indicada de la llista.
		/// </summary>
		/// <param name="index">Índex de l’element que es vol eliminar.</param>
		/// <exception cref="NotSupportedException">Llançada si la col·lecció és de només lectura.</exception>
		/// <exception cref="ArgumentOutOfRangeException">Llançada si l’índex és fora dels límits de la llista.</exception>
		public void RemoveAt(int index)
		{
			if (this.IsReadOnly) throw new NotSupportedException("TaulaLlista és READ-ONLY");
			T valor = this[index];
			Remove(valor);
		}
		#region versions de RemoveAt
		//public void RemoveAt(int index)
		//{
		//	if (IsReadOnly)
		//		throw new NotSupportedException("TaulaLlista és de només lectura");

		//	if (index < 0 || index >= nElem)
		//		throw new ArgumentOutOfRangeException(nameof(index), "Índex fora de rang");

		//	// Tractament especial si eliminem el primer
		//	if (index == 0)
		//	{
		//		head = head.Next;
		//		if (nElem == 1)
		//			tail = null;
		//	}
		//	else
		//	{
		//		Node anterior = GetNodeIndex(index - 1);
		//		Node actual = anterior.Next;

		//		anterior.Next = actual.Next;

		//		if (actual == tail)
		//			tail = anterior;
		//	}
		//	nElem--;
		//}
		#endregion

		#endregion

		#region Clone
		object ICloneable.Clone() => Clone();

		/// <summary>
		/// Retorna una còpia profunda de la taula.
		/// </summary>
		/// <returns>Nova instància de TaulaLlistaNodes amb els mateixos valors.</returns>
		public TaulaLlistaNodes<T> Clone() => new TaulaLlistaNodes<T>(this);

		#endregion

		#region Metodes Override
		/// <summary>
		/// confeccionem la cadena de text que representa la taula
		/// </summary>
		/// <returns>Cadena de text que representa la taula.</returns>
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("[");
			Node aux = head;
			while (aux != null)
			{
				sb.Append(aux.Data.ToString());
				aux = aux.Next;
				if (aux != null) sb.Append(", ");
			}
			sb.Append("]");
			return sb.ToString();
		}
		/// <summary>
		/// Retorna true si la taulaLlista de nodes és igual a l'objecte passat com a paràmetre.
		/// </summary>
		/// <returns>True si són iguals, false altrament.</returns>
		public override bool Equals(object? obj)
		{
			bool result = true;
			if (obj == null || obj is not TaulaLlistaNodes<T> otherList || Count != otherList.Count)
				result = false;
			else
			{
				Node aux1 = head;
				Node aux2 = otherList.head;
				while (aux1 != null && aux2 != null && result)
				{
					if (!aux1.Data.Equals(aux2.Data)) result = false;
					aux1 = aux1.Next;
					aux2 = aux2.Next;
				}
			}
			return result;
		}
		#endregion

		#region Metodes Ordenar
		/// <summary>
		/// Ordena la taula en ordre ascendent segons el metode CompareTo de l'element T.
		/// </summary>
		/// <exception cref="NotSupportedException">TaulaLlista és READ-ONLY</exception>
		public void Sort()
		{
			if (this.IsReadOnly) throw new NotSupportedException("TaulaLlista és READ-ONLY");
			T[] array = new T[nElem];
			CopyTo(array, 0);
			Array.Sort(array);
			Clear();
			AddRange(array);
		}
		/// <summary>
		/// Ordena la taula en ordre ascendent segons el comparador passat com a paràmetre.
		/// </summary>
		/// <exception cref="NotSupportedException">TaulaLlista és READ-ONLY</exception>
		public void Sort(IComparer<T> comparer)
		{
			if (this.IsReadOnly) throw new NotSupportedException("TaulaLlista és READ-ONLY");
			T[] array = new T[nElem];
			CopyTo(array, 0);
			Array.Sort(array, comparer);
			Clear();
			AddRange(array);
		}
		#endregion
	}
}
