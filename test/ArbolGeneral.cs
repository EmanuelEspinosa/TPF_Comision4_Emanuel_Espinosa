using System;
using System.Collections.Generic;

namespace DeepSpace
{
	public class ArbolGeneral<T>
	{
		
		private T dato;
		private List<ArbolGeneral<T>> hijos = new List<ArbolGeneral<T>>();

		public ArbolGeneral(T dato) {
			this.dato = dato;
		}
	
		public T getDatoRaiz() {
			return this.dato;
		}
	
		public List<ArbolGeneral<T>> getHijos() {
			return hijos;
		}
	
		public void agregarHijo(ArbolGeneral<T> hijo) {
			this.getHijos().Add(hijo);
		}
	
		public void eliminarHijo(ArbolGeneral<T> hijo) {
			this.getHijos().Remove(hijo);
		}
	
		public bool esHoja() {
			return this.getHijos().Count == 0;
		}
	
		public int altura() {
			return 0;
		}
	
		
		public int nivel(T dato) {
			return 0;
		}

		public string porNiveles()
		{
			string cadena = "";
			Cola<ArbolGeneral<T>> c = new Cola<ArbolGeneral<T>>();
			ArbolGeneral<T> arbolAux;
			c.encolar(this);

			while (!c.esVacia())
			{
				arbolAux = c.desencolar();
				cadena += "[" + arbolAux.getDatoRaiz() + "]" + " ";

				foreach (var hijos in arbolAux.getHijos())
				{
					c.encolar(hijos);
				}
			}
			return cadena;
		}

	}
}