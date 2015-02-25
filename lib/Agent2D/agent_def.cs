using System;
using Vector;

namespace Agent{

	public class Agent2D{

		// L --> tamanio de la caja (cubica) 
		// V --> vel maxima
		// k --> numero de interacciones

		public Vector2D Pos, Vel;
		public int[] Links;

// =============================================================================================

		public Agent2D(double l, double v, int k){

			var rnd = new MyRand();

			Pos = new Vector2D (rnd.NextSimDouble (l), rnd.NextSimDouble (l));
			Vel = new Vector2D (rnd.NextSimDouble (1), rnd.NextSimDouble (1));

			Vel = v * (1 / Vel.Norm ()) * Vel;  // velocidad con direccion aleatoria y magnitud unitaria

			// Console.WriteLine("pos = " + this.pos.ToString() + "  vel = " + this.vel.ToString() + " V = " + this.vel.Norm());

			Links = new int[k];
		}

// =============================================================================================

		public  void Move(double dt){
			Pos = Pos + dt*Vel;
		}
			
// =============================================================================================

		public void AlignTopo(Agent2D[] elements, double pt, double ht){ 

			const double EPSILON = 10e-8;
			// con respecto a su vecindad topologica

			var rnd = new MyRand();

			var prom = new Vector2D(Vel);   // se inicializa como la vel propia para considerarla en el prom

			// Console.WriteLine(prom.ToString());

			double N = (double)Links.Length;

			if (Math.Abs (N) > EPSILON ) {

				foreach (int x in Links) {
					// Console.WriteLine(x);
					prom = prom + elements[x].Vel;
				}
				prom = (1/N) * prom;
			}        

			if (Math.Abs (Vel.Norm ()) > EPSILON && Math.Abs (prom.Norm ()) > EPSILON) {

				double ang =  pt*(Vel.Ang(prom) + ht*(rnd.NextSimDouble(Math.PI)));

				Vel.Rotate(ang);
			} 
		}

// =============================================================================================

		public void AlignGeom(Agent2D[] elements, double pg, double hg, double r){ 

			// con respecto a su vecindad geometrica
			const double EPSILON = 10e-8;

			var rnd = new MyRand();

			var prom = new Vector2D();  
			// se inicializa como la vel propia para considerarla en el prom

			// Console.WriteLine(prom.ToString());

			double N = 0;

			foreach (Agent2D agent in elements) {

				double d = Pos.Dist(agent.Pos);

				// Console.WriteLine(d);

				if ( d > 0 && d <= r) {
					prom = prom + agent.Vel;
					N += 1;
				}
			}

			// Console.WriteLine("vel_i = " + vel.ToString() + " N =" + N);

			if(N > 0) {

				prom = (1/N) * prom;

				// Console.WriteLine(prom.ToString());

				if (Math.Abs (Vel.Norm ()) > EPSILON && Math.Abs (prom.Norm ()) > EPSILON) {

					double ang =  pg*( Vel.Ang(prom) + hg*(rnd.NextSimDouble(Math.PI)) );

					Vel.Rotate(ang);

				}
			}

			// Console.WriteLine("vel_f = " + vel.ToString());
		}

// =============================================================================================

		public void AlignBoth(Agent2D[] elements, double pg, double hg, double ht, double r){ 
			// con respecto a las dos vecindades

			const double EPSILON = 10e-8;

			var rnd = new MyRand();

			var prom_geom = new Vector2D(Vel);   // se inicializa como la vel propia para considerarla en el prom
			var prom_topo = new Vector2D(Vel);   // se inicializa como la vel propia para considerarla en el prom

			double ang_geom = 0, ang_topo = 0;

			// Console.WriteLine(prom.ToString());

			double NG = 1;

			foreach (Agent2D agent in elements) {

				double d = Pos.Dist(agent.Pos);

				// Console.WriteLine(d);

				if ( d > 0 && d <= r) {
					prom_geom = prom_geom + agent.Vel;
					NG += 1;
				}
			}

			prom_geom = (1/NG) * prom_geom;

			ang_geom =  pg * ( Vel.Ang(prom_geom) + hg*(rnd.NextSimDouble(Math.PI)) );

			double NT = (double)Links.Length;

			if (Math.Abs (NT) > EPSILON) {
				foreach (int x in Links) {
					// Console.WriteLine(x);
					prom_topo = prom_topo + elements [x].Vel;
				}
				prom_topo = (1 / NT) * prom_topo;
			}        

			ang_topo =  Math.Abs(1-pg)*(Vel.Ang(prom_topo) + ht*(rnd.NextSimDouble(Math.PI)));

			Vel.Rotate(ang_geom + ang_topo);

		}
// =============================================================================================

// =============================================================================================
	}
}

