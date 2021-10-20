
using System;
using System.Collections.Generic;
namespace DeepSpace
{

	class Estrategia
	{
		/* Calcula y retorna un texto con la distancia del camino que existe entre el planeta 
		del Bot y la raíz */
		public String Consulta1(ArbolGeneral<Planeta> arbol)
		{
			/*Se supone que el arbol pasado como parametro corresponde al nodo raiz*/
			Cola<ArbolGeneral<Planeta>> c = new Cola<ArbolGeneral<Planeta>>();
			ArbolGeneral<Planeta> arbolAux;
			int contNiv = 0, nivel = 0;
			c.encolar(arbol);
			c.encolar(null);
			while (!c.esVacia())
			{
				arbolAux = c.desencolar();
				if (arbolAux == null)
				{
					contNiv++;
					if (!c.esVacia())
						c.encolar(null);
				}
				if (arbolAux != null && arbolAux.getDatoRaiz().EsPlanetaDeLaIA())
					nivel = contNiv;
				else
				{
					if (arbolAux != null && arbolAux.getHijos().Count != 0)
					{
						foreach (var hijo in arbolAux.getHijos())
							c.encolar(hijo);
					}
				}
			}
			return nivel.ToString();
		}

		/* Retorna un texto con el listado de los planetas ubicados en todos los 
		descendientes del nodo que contiene al planeta del Bot */
		public String Consulta2(ArbolGeneral<Planeta> arbol)
		{
			Cola<ArbolGeneral<Planeta>> c = new Cola<ArbolGeneral<Planeta>>();
			ArbolGeneral<Planeta> arbolAux;
			string cadena = " ";
			c.encolar(arbol);
			c.encolar(null);
			while (!c.esVacia())
			{
				arbolAux = c.desencolar();
				if (arbolAux == null)
				{
					if (!c.esVacia())
						c.encolar(null);
				}
				if (arbolAux != null && arbolAux.getDatoRaiz().EsPlanetaDeLaIA())
				{
					c.ResetCola();
					foreach (var hijos in arbolAux.getHijos())
					{
						foreach (var hijosAux in hijos.getHijos())
						{
							c.encolar(hijosAux);
						}
					}
					break;
				}
				else
				{
					if (arbolAux != null && arbolAux.getHijos().Count != 0)
					{
						foreach (var hijo in arbolAux.getHijos())
							c.encolar(hijo);
					}
				}
			}
			while (!c.esVacia())
			{
				arbolAux = c.desencolar();
				cadena += arbolAux.porNiveles();
			}
			return cadena;
		}

		/*Calcula y retorna en un texto la población total y promedio por cada nivel del árbol*/
		public String Consulta3(ArbolGeneral<Planeta> arbol)
		{
			/*Se calcula la población total por cada nivel del árbol*/
			/*Una vez obtenida la población total por cada nivel, se calcula el promedio de 
			 población del árbol*/
			/*Este método retorna la población por cada nivel, la población total del árbol
			 y el promedio de población del mismo.
			El promedio se calcula como: (suma de cantidad de nodos por nivel) / cantidad de niveles*/
			Cola<ArbolGeneral<Planeta>> c = new Cola<ArbolGeneral<Planeta>>();
			ArbolGeneral<Planeta> arbolAux;
			int contNiv = 0, cantNod = 0, contNodosNivel = 0;
			string cadena = "";
			c.encolar(arbol);
			c.encolar(null);
			while (!c.esVacia())
			{
				arbolAux = c.desencolar();
				if (arbolAux == null)
				{
					contNiv++;
					cadena += "[" + "Nivel " + contNiv + " ---> Población: " + cantNod + "]" + "\n";
					contNodosNivel += cantNod;
					if (!c.esVacia())
						c.encolar(null);
					cantNod = 0;
				}
				if (arbolAux != null && arbolAux.getHijos().Count == 0)
				{
					cantNod++;
				}
				if (arbolAux != null && arbolAux.getHijos().Count != 0)
				{
					cantNod++;
					foreach (var hijo in arbolAux.getHijos())
						c.encolar(hijo);
				}
			}
			double prom = contNodosNivel / contNiv;
			return cadena += "\nPoblación total del árbol: " + contNodosNivel + "\n" + "Promedio de población por nivel: " + prom;
		}
		private static Movimiento mov;
		private static int j = 0, x = 0;
		private static List<ArbolGeneral<Planeta>> lista = new List<ArbolGeneral<Planeta>>();


        public Movimiento CalcularMovimiento(ArbolGeneral<Planeta> arbol)
        {
            Cola<ArbolGeneral<Planeta>> c = new Cola<ArbolGeneral<Planeta>>();
            ArbolGeneral<Planeta> arbolAux;
            c.encolar(arbol);
            
            while (!c.esVacia())
            {
                arbolAux = c.desencolar();
				if (arbolAux.getDatoRaiz().EsPlanetaDeLaIA())
				{

					lista = RecorridoHijosBot(arbolAux, new List<ArbolGeneral<Planeta>>());
                    while (j < lista.Count - 1)
                    {
						if (!lista[j + 1].getDatoRaiz().EsPlanetaDeLaIA())
                        {
							if (!lista[j + 1].getDatoRaiz().EsPlanetaDeLaIA() && lista[j + 1].esHoja())
                            {
                                mov = new Movimiento(lista[j].getDatoRaiz(), lista[j + 1].getDatoRaiz());
                                j += 1;
                                return mov;
                            }
                            mov = new Movimiento(lista[0].getDatoRaiz(), lista[j + 1].getDatoRaiz());
                            j += 1;
                            return mov;
                        }
                    }
                    break;
                }
                else
                {
                    if (arbolAux.getHijos().Count != 0)
                        foreach (var hijo in arbolAux.getHijos())
                            c.encolar(hijo);
                }
            }
			int t = (lista.Count - 1) - x;
			while(t > 0)
            {
				if(lista[t].esHoja())
                {
					mov = new Movimiento(lista[t].getDatoRaiz(), lista[t - 1].getDatoRaiz());
					x += 1;
					return mov;
                }
				mov = new Movimiento(lista[t].getDatoRaiz(), lista[0].getDatoRaiz());
				x += 1;
				return mov;
            }
			return null;
        }
        private List<ArbolGeneral<Planeta>> RecorridoHijosBot(ArbolGeneral<Planeta> arbol, List<ArbolGeneral<Planeta>> lista)
		{
			List<ArbolGeneral<Planeta>> listaAux = null;
			lista.Add(arbol);
			if (arbol.esHoja())
				return lista;
			else
			{
				foreach (var item in arbol.getHijos())
					listaAux = RecorridoHijosBot(item, lista);
				if (listaAux != null)
					return listaAux;
			}
			return null;
		}
	}
}
