using System.Collections;
using System.Text;

namespace CuaDinamica
{
	/// <summary>
	/// Classe que implementa una cua de manera dinàmica mitjançant una llista enllaçada de Nodes.
	/// La cua és una estructura FIFO (First In First Out)
	/// Estarà formada per una sèrie de nodes enllaçats entre ells amb tipus genèrics T.
	/// Important: Per la condició de ser simplement enllaçada, només es pot accedir al primer node de la cua (head) i al darrer (tail)
	/// i iterar del primer cap a l'últim. head -> ... -> tail
	/// Per tant inserim elements (Enqueue) per el tail i treiem elements (Dequeue) per el head 
	/// </summary>
	/// <typeparam name="T">Tipus dels elements que conté la Cua (element genèric T)</typeparam>
	public class Cua<T> : IEnumerable<T>
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
		/// Constructor per defecte. Crea una Cua buida.
		/// </summary>
		public Cua() => nElem = 0;

		/// <summary>
		/// Constructor amb un sol element inici.
		/// </summary>
		/// <param name="item">Element inici a afegir.</param>
		public Cua(T item)
		{
			head = new Node(item);
			tail = head;
			nElem = 1;
		}

		/// <summary>
		/// Constructor a partir d'una col·lecció enumerable.
		/// </summary>
		/// <param name="llistaEnumerable">Col·lecció d'elements a afegir.</param>
		public Cua(IEnumerable<T> llistaEnumerable)
		{
			nElem = 0;
			foreach (T item in llistaEnumerable)
			{
				if (item is ICloneable clon)
					Enqueue((T)clon.Clone());
				else
					Enqueue(item);
			}
		}
		#endregion

		#region propietats
		/// <summary>
		/// Indica si la Cua està buida.
		/// </summary>
		public bool IsEmpty => Count == 0;

		/// <summary>
		/// Retorna el nombre d'elements actuals a la Cua.
		/// </summary>
		public int Count => nElem;

		/// <summary>
		/// Indica si la Cua és només de lectura.
		/// </summary>
		public bool IsReadOnly => false;
		#endregion

		#region Metodes Publics
		/// <summary>
		/// esborra tots els elements de la cua.
		/// </summary>
		public void Clear()
		{
			head = null;
			tail = null;
			nElem = 0;
		}

		/// <summary>
		/// Comprova si l'element passat per paràmetre es troba a la cua.
		/// </summary>
		/// <param name="item">Element a buscar a la cua</param>
		public bool Contains(T item)
		{
			bool trobat = false;
			Node actual = head;
			while (actual != null && !trobat)
			{
				if (actual.Data.Equals(item))
					trobat = true;
				actual = actual.Next;
			}
			return trobat;
		}
		/// <summary>
		/// Afegir un element a la cua a la posició del tail.
		/// </summary>
		/// <param name="item">Element a afegir a la cua de tipus genèric T.</param>
		public void Enqueue(T item)
		{
			Node nou = new Node(item);
			if (IsEmpty)
				head = nou;
			else
				tail.Next = nou;

			tail = nou;
			nElem++;
		}
		/// <summary>
		/// Mirem l'element que es troba al cap de la cua (head) el que li toca sortir segons estructura FIFO.
		/// </summary>
		/// <returns>Retorna l'element que està preparat per sortir de la Cua</returns>
		/// <exception cref="InvalidOperationException">Cua Buida</exception>
		public T Peek()
		{
			if (IsEmpty) throw new InvalidOperationException("CUA BUIDA. NO HI HA CAP ELEMENT");

			return head.Data;
		}
		/// <summary>
		/// En cas que hi hagi element per desencuar es guarda en el paràmetre de sortida
		/// Si la cua és buida, es posa el item per defecte de T al paràmetre de sortida
		/// L'element mai es desencua
		/// </summary>
		/// <param name="item">element que tocaria ser desencuat</param>
		/// <returns>true si la cua no està buida i per tant, el "try" ha estat exitós</returns>
		public bool TryPeek(out T item)
		{
			if (IsEmpty) item = default(T);
			else item = head.Data;
			return !IsEmpty;
		}

		/// <summary>
		/// Desencuem l'element que es troba al cap de la cua (head) el que li toca sortir segons estructura FIFO.
		/// </summary>
		/// <returns>Retorna l'element que es troba en head</returns>
		/// <exception cref="InvalidOperationException">Cua Buida</exception>
		public T Dequeue()
		{
			if (this.IsEmpty) throw new InvalidOperationException("CUA BUIDA. NO ES POT DESENCUAR CAP ELEMENT");
			T item = head.Data;
			if (head == tail) // Si només hi ha un element
				tail = null;
			head = head.Next; // Desenllacem el node que es desencua
			nElem--;
			return item;
		}

		/// <summary>
		/// Treiem de la Cua l'element indicat per paràmetre. Similar a un Remove(T item) de les llistes.
		/// </summary>
		/// <param name="item">Lelement que volem treure de la Cua</param>
		/// <returns>Tornarem true si hem trobat i tret l'element i false en cas contrari</returns>
		public bool Leave(T item)
		{
			if (IsEmpty) throw new InvalidOperationException("CUA BUIDA. NO POT SORTIR CAP ELEMENT");

			Node actual = head;
			Node anterior = null;
			bool trobat = false;
			while (actual != null && !trobat)
			{
				if (actual.Data.Equals(item))
					trobat = true;
				else
				{
					anterior = actual;
					actual = actual.Next;
				}
			}
			if (trobat)
			{
				if (actual == head) // Si és el primer element
					head = actual.Next;
				else if (actual == tail) // Si és l'últim element
					tail = anterior;
				else anterior.Next = actual.Next; // Si no és el primer ni l'últim
			}
			nElem--;
			return trobat;
		}
		#endregion

		#region IEnumerable<T>
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<T> GetEnumerator() => new EnumeratorCua(head);

		private class EnumeratorCua : IEnumerator<T>
		{
			Node actual;
			Node inici;

			public T Current => actual.Data;
			object IEnumerator.Current => Current;

			public EnumeratorCua(Node head)
			{
				inici = head;
			}

			public void Dispose() { }
			public bool MoveNext()
			{
				if (actual == null)
					actual = inici;
				else
					actual = actual.Next;

				return actual != null;
			}
			public void Reset()
			{
				actual = null;
			}
		}
		#endregion

		#region Metodes Override
		/// <summary>
		/// Retrorna una cadena de text amb els elements de la cua.
		/// </summary>
		/// <returns>Cadena de text amb els elements de la cua</returns>
		public override string ToString()
		{
			StringBuilder cuaTxt = new StringBuilder("[");
			Node actual = head;
			while (actual != null)
				cuaTxt.Append($"{actual.Data}, ");
			cuaTxt.Remove(cuaTxt.Length - 2, 2); // Eliminem la coma i l'espai
			cuaTxt.Append("]");
			return cuaTxt.ToString();
		}
		/// <summary>
		/// Compara dues cues per veure si són iguals.
		/// </summary>
		/// <param name="obj">Un objecte de tipus Cua</param>
		/// <returns>true en cas que les dues cues siguen iguals o false en cas contrari</returns>
		public override bool Equals(object? obj)
		{
			bool res = true;
			if (obj == null || obj is not Cua<T> other || Count != other.Count)
				res = false;
			else
			{
				Node actual = head;
				Node otherActual = other.head;
				while (actual != null && otherActual != null)
				{
					if (!actual.Data.Equals(otherActual.Data))
						res = false;
					actual = actual.Next;
					otherActual = otherActual.Next;
				}
			}
			return res;
		}
		#endregion
	}
}
