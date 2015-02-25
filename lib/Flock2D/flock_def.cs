using System;
using Agent;
using Vector;

namespace Flock{

	public class Flock2D{

		Random rnd = new Random(Guid.NewGuid().GetHashCode());

		public Agent2D[] Elements; // Arreglo con los elementos del flock
		public double[,] Dists; // Matriz con las distancias del flock
		public int[,] Adj; // Matriz de adjacencia geometrica

// =============================================================================================

		public Flock2D(int n, double l, double v, int k){

			Elements = new Agent2D[n];

			Dists = new double[n,n];
			Adj = new int[n,n];

			for (int i = 0; i < n; i++)
			{
				Elements[i] = new Agent2D(l, v, k);

				for (int j = 0; j < Elements[i].Links.Length; j++) //generar enlaces con otros agentes arbitrarios
				{
					Elements[i].Links[j] = rnd.Next(0, n);
				}
			}
		}

// =============================================================================================
		public void SetMatrix(double r){

			Array.Clear(Adj,0,Adj.Length);
			Array.Clear(Dists,0,Dists.Length);

			for (int i = 0; i < Elements.Length; i++){

				for (int j = i+1 ; j < Elements.Length ; j++){

					double d = Elements[i].Pos.Dist(Elements[j].Pos);

					Dists[i,j] = d;
					Dists[j,i] = d;

					if ( d <= r){
						Adj[i,j] = 1;
						Adj[j,i] = 1;
					}
				}
			}

		}

// =============================================================================================

		public double[] GetAngsIN(){

			int n = Elements.Length;
			int k = Elements [0].Links.Length;

			var angs = new double[n];

			if(k > 0){

				for(int i = 0; i < n; i++){

					var vecProm = new Vector2D (Elements[i].Vel);

					for (int j = 0; j < k; j++) {
						vecProm = vecProm + Elements [j].Vel;
					}
					vecProm = vecProm * (1 / k);

					angs [i] = Elements [i].Vel.Ang (vecProm);
				}
			}

			return angs;
		}

// =============================================================================================

		public double[] GetAngsGeom(){

			int n = Elements.Length;

			var angs = new double[n];

			for(int i = 0; i < n; i++){

				double k = 0;

				var vecProm = new Vector2D (Elements[i].Vel);

				for (int j = 0; j < n; j++) {
					if (Adj [i, j] == 1) {
						vecProm = vecProm + Elements [j].Vel;
						k += 1.0;
					}
				}

				if (k > 0) {
					vecProm = vecProm * (1 / k);
					angs [i] = Elements [i].Vel.Ang (vecProm);
				}
			}

			return angs;
		}

// =============================================================================================

		public void AlignVels(double w, double eta){ 
			// Actualiza las velocidades usando la matriz de adjacencia

			var angsIN = GetAngsIN ();
			var angsGeom = GetAngsGeom ();

			// Analysis disable once LocalVariableHidesMember
			var rnd = new MyRand();

			for(int i = 0; i < Elements.Length ; i ++ ){

				double ang = w * angsGeom [i] + (1 - w) * angsIN [i] + eta * (rnd.NextSimDouble (Math.PI));

				Elements[i].Vel.Rotate(ang);
			}

		}

// =============================================================================================

		public void Update(double dt, double w, double eta, double r){

			SetMatrix(r);
			AlignVels(w,eta);

			foreach(Agent2D agent in Elements){
				agent.Move(dt);
			}
		}

// =============================================================================================
	}
}

