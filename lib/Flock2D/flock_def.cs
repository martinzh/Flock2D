using System;
using Agent;
using Vector;

namespace Flock{

	public class Flock2D{

		// elements --> arreglo con los agentes
		// N --> numero de miembros
		Random rnd = new Random(Guid.NewGuid().GetHashCode());
		public Agent2D[] elements;
		public double[,] dists; // Matriz con las distancias del flock
		public Vector2D[] vels; // Matriz con las posiciones del flock
		public Vector2D[] pos; // Matriz con las velocidades del flock
		public int[,] adj; // Matriz de adjacencia geometrica

// =============================================================================================

		public Flock2D(int N, double L, double V, int k, double r){

			elements = new Agent2D[N];
			vels = new Vector2D[N];
			pos = new Vector2D[N];

			dists = new double[N,N];
			adj = new int[N,N];

			for (int i = 0; i < N; i++)
			{
				elements[i] = new Agent2D(L, V, k, r);
				vels [i] = new Vector2D ();
				for (int j = 0; j < elements[i].links.Length; j++) //generar enlaces con otros agentes arbitrarios
				{
					elements[i].links[j] = rnd.Next(0, N);
				}
			}
		}

// =============================================================================================
		 public void SetMatrix(){


			Array.Clear(adj,0,adj.Length);
			Array.Clear(dists,0,dists.Length);

//			Array.Clear(vels,0,vels.Length);

//			Console.WriteLine (vels.Length);

//			Console.WriteLine ("limpia");

//			for(int i = 0; i < elements.Length; i ++){
//				for (int j = 0; j < elements.Length; j++){
//
//					Console.Write("{0}\t",adj[i,j]);
//				}
//				Console.WriteLine();
//			}
//
//			Console.WriteLine();
//			Console.WriteLine();

//			for(int i = 0; i < vels.Length; i ++){
//
//				Console.Write("{0}\t",vels[i].Display());
//			}
//
//			Console.WriteLine();
//
//			Console.WriteLine();
//			Console.WriteLine();


			for (int i = 0; i < elements.Length; i++){

				vels [i] = elements[i].vel;
				pos [i] = elements[i].pos;

				for (int j = elements.Length - 1 ; j > i ; j--){

					double d = elements[i].pos.Dist(elements[j].pos);

					dists[i,j] = d;
					dists[j,i] = d;

					if ( d > 0 && d <= elements[i].r){
						adj[i,j] = 1;
						adj[j,i] = 1;
					}
				}
			}

//			Console.WriteLine ("llena");
//
//			for(int i = 0; i < elements.Length; i ++){
//				for (int j = 0; j < elements.Length; j++){
//
//					Console.Write("{0}\t",adj[i,j]);
//				}
//				Console.WriteLine();
//			}
//
//			Console.WriteLine();
//			Console.WriteLine();
//
//			for(int i = 0; i < elements.Length; i ++){
//				for (int j = 0; j < elements.Length; j++){
//
//					Console.Write("{0}\t",dists[i,j]);
//				}
//				Console.WriteLine();
//			}
//
//			Console.WriteLine();
//			Console.WriteLine();

//			for(int i = 0; i < vels.Length; i ++){
//
//				Console.Write("{0}\t",vels[i].Display());
//			}
//
//			Console.WriteLine();
//
//			Console.WriteLine();
//			Console.WriteLine();
		}

// =============================================================================================

		public void AlignVels(double pg, double hg, double ht){ 
			// Actualiza las velocidades usando la matriz de adjacencia

			MyRand rnd = new MyRand();

			for(int i = 0; i < elements.Length ; i ++ ){

				// se inicializan los vectores promedio con la de la particula

				Vector2D prom_geom = new Vector2D (vels[i]); 
				Vector2D prom_topo = new Vector2D (vels[i]);

				double NG = 1;
				double NT = (double)elements[i].links.Length;

				double ang_geom = 0, ang_topo = 0;

				for (int j = 0; j < elements.Length; j ++) {
					prom_geom = prom_geom + ((double)adj[i,j])*vels[j];
					NG += adj[i,j];
				}

				prom_geom = (1/NG) * prom_geom; // velocidad promedio geometrica

				//ang_geom =  pg * ( vels[i].Ang(prom_geom) + hg*(rnd.NextSimDouble(Math.PI)) );
				ang_geom =  pg * ( vels[i].Ang(prom_geom) );

				if (NT != 0 ) {
					foreach (int x in elements[i].links){
						// Console.WriteLine(x);
						prom_topo = prom_topo + vels[x];
					}
					prom_topo = (1/NT) * prom_topo; // velocidad promedio "topologica"
				}
	
				//ang_topo =  Math.Abs(1-pg)*(vels[i].Ang(prom_topo) + ht*(rnd.NextSimDouble(Math.PI)));
				ang_topo =  Math.Abs(1-pg)*(vels[i].Ang(prom_topo));
	
				//vels[i].Rotate(ang_geom + ang_topo );
				vels[i].Rotate(ang_geom + ang_topo + hg*(rnd.NextSimDouble(Math.PI)) );
			}

			for (int i = 0 ; i < elements.Length; i++ ) {
				elements[i].vel = vels[i];
			}
		}

// =============================================================================================

		public void Update(double dt, double pg, double hg, double ht){

			SetMatrix();
			AlignVels(pg,hg,ht);

			foreach(Agent2D agent in elements){
				agent.Move(dt);
				// agent.AlignTopo(elements, Math.Abs(1-pg), ht);
				// agent.AlignGeom(elements, pg, hg);
				// agent.AlignBoth(elements, pg, hg, ht);
			}
		}

// =============================================================================================
	}
}

