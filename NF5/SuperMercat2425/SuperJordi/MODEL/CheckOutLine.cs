using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperJordi.MODEL
{
	/// <summary>
	/// El CheckOutLine representa una cua de compra d'un supermercat.
	/// té un número d'identificació
	/// amb un caixer/a al càrrec.
	/// un CheckOutLine pot tenir diversos carrets de la compra a la cua.
	/// ens indica si està activa o no.
	/// </summary>
	public class CheckOutLine
	{
		#region Attributes
		private int number;
		private Queue<ShoppingCart> queue;
		private Person cashier;
		private bool isActive;
        #endregion

        #region Properties
		public bool Empty => queue.Count == 0;
		public bool IsActive { get => isActive; set => isActive = value; }
        #endregion

        #region Constructors
        public CheckOutLine(Person responsible, int number)
		{
			this.number = number;
			this.queue = new Queue<ShoppingCart>();
			this.cashier = responsible;
			this.isActive = true;
		}
		#endregion

		#region Methods
		public bool CheckIn(ShoppingCart cart)
		{
			if (isActive)
				queue.Enqueue(cart);
			return isActive;
		}
		/// <summary>
		/// CheckOut processa el primer carret de la cua.
		/// comporova que estigui la cua activa i que hi hagi un carret a la cua.
		/// Processa tots els articles (items) del carret i calcula el total a pagar.
		/// Calcula els punts obtinguts i els afegeix al caixer/a i al client.
		/// Afegeix l'importCobrat i punts al caixer/a 
		/// Afegeix l'importPagar i punts al client.
		/// posa el client com a inactiu.
		/// </summary>
		/// <returns>true o false si hem pogut aconseguir el checkout </returns>
		public bool CheckOut() 
		{
			if (isActive && queue.Count > 0)
			{
				ShoppingCart cart = queue.Dequeue();
				double totalAmount = ShoppingCart.ProccessItems(cart);
				int points = cart.RawPointsObtainedAtCheckout(totalAmount);
				// Afegim l'import i punts al al caixer
				cashier.AddInvoicedAmout(totalAmount);
				cashier.AddPoints(points);

				// Afegim l'import i punts al client i el desactivem
				cart.Customer.AddInvoicedAmout(totalAmount);
				cart.Customer.AddPoints(points);
				cart.Customer.Active = false;

				return true;
			}
			return false;
		}
		public override string ToString()
		{
			StringBuilder info = new StringBuilder("NUMERO DE CAIXA -->" + this.number + "\n");
			info.Append("CAIXER/A AL CÀRREC -->" + this.cashier.FullName + "\n");
			if (queue.Count != 0)
			{
				foreach (ShoppingCart llistaCompra in queue)
				{
					info.Append(llistaCompra);
				}
			}
			else info.Append("CUA BUIDA\n");
			return info.ToString();
		}
		#endregion

	}
}
