using System;
using Vector;

namespace Agent{

	public class Agent2D{

		// L --> tamanio de la caja (cubica) 
		// V --> vel maxima
		// k --> numero de interacciones

		public Vector2D pos, vel;
		public int[] links;
		public double r;

// =============================================================================================

		public Agent2D(double L, double V, int k, double r){

			MyRand rnd = new MyRand();

			this.pos = new Vector2D(rnd.NextSimDouble(L), rnd.NextSimDouble(L));
			this.vel = new Vector2D(rnd.NextSimDouble(1), rnd.NextSimDouble(1));

			this.vel = V * (1/this.vel.Norm()) * this.vel;  // velocidad con direccion aleatoria y magnitud unitaria

			// Console.WriteLine(this.vel.Norm());

			// Console.WriteLine("pos = " + this.pos.ToString() + "  vel = " + this.vel.ToString() + " V = " + this.vel.Norm());

			links = new int[k];

			this.r = r;
		}

// =============================================================================================

		public  void Move(double dt){
			pos = pos + dt*vel;
		}
			
// =============================================================================================

		public void AlignTopo(Agent2D[] elements, double pt, double ht){ 

			// con respecto a su vecindad topologica

			// Random rnd = new Random(Guid.NewGuid().GetHashCode());

			MyRand rnd = new MyRand();

			Vector2D prom = new Vector2D(vel);   // se inicializa como la vel propia para considerarla en el prom

			// Console.WriteLine(prom.ToString());

			// double N = (double)this.links.Length;
			double N = (double)links.Length;

			if (N != 0 ) {
				// foreach (int x in this.links) {
				foreach (int x in links) {
					// Console.WriteLine(x);
					prom = prom + elements[x].vel;
				}
				prom = (1/N) * prom;
			}        

			// Console.WriteLine(prom.ToString());

			// double nv = this.vel.Norm();
			// double np = prom.Norm();

			if (vel.Norm() != 0 && prom.Norm() != 0) {

				double ang =  pt*(vel.Ang(prom) + ht*(rnd.NextSimDouble(Math.PI)));

				vel.Rotate(ang);
			} 
		}

// =============================================================================================

		public void AlignGeom(Agent2D[] elements, double pg, double hg){ 

			// con respecto a su vecindad geometrica

			MyRand rnd = new MyRand();

			Vector2D prom = new Vector2D();   // se inicializa como la vel propia para considerarla en el prom

			// Console.WriteLine(prom.ToString());

			double N = 0;

			foreach (Agent2D agent in elements) {

				double d = pos.Dist(agent.pos);

				// Console.WriteLine(d);

				if ( d > 0 && d <= r) {
					prom = prom + agent.vel;
					N += 1;
				}
			}

			// Console.WriteLine("vel_i = " + vel.ToString() + " N =" + N);

			if(N > 0) {

				prom = (1/N) * prom;

				// Console.WriteLine(prom.ToString());

				// double nv = this.vel.Norm();
				// double np = prom.Norm();

				if ( vel.Norm() != 0 && prom.Norm() != 0) {

					double ang =  pg*( vel.Ang(prom) + hg*(rnd.NextSimDouble(Math.PI)) );

					vel.Rotate(ang);

				}
			}

			// Console.WriteLine("vel_f = " + vel.ToString());
		}

// =============================================================================================

		public void AlignBoth(Agent2D[] elements, double pg, double hg, double ht){ 
			// con respecto a las dos vecindades

			MyRand rnd = new MyRand();

			Vector2D prom_geom = new Vector2D(vel);   // se inicializa como la vel propia para considerarla en el prom
			Vector2D prom_topo = new Vector2D(vel);   // se inicializa como la vel propia para considerarla en el prom

			double ang_geom = 0, ang_topo = 0;

			// Console.WriteLine(prom.ToString());

			double NG = 1;

			foreach (Agent2D agent in elements) {

				double d = pos.Dist(agent.pos);

				// Console.WriteLine(d);

				if ( d > 0 && d <= r) {
					prom_geom = prom_geom + agent.vel;
					NG += 1;
				}
			}

			// Console.WriteLine("vel_i = " + vel.ToString() + " N =" + N);

			// if(NG > 0) {

			prom_geom = (1/NG) * prom_geom;

			// if ( vel.Norm() != 0 && prom_geom.Norm() != 0) {

			ang_geom =  pg * ( vel.Ang(prom_geom) + hg*(rnd.NextSimDouble(Math.PI)) );
			// }
			// }

			double NT = (double)links.Length;

			if (NT != 0 ) {
				foreach (int x in links) {
					// Console.WriteLine(x);
					prom_topo = prom_topo + elements[x].vel;
				}
				prom_topo = (1/NT) * prom_topo;
			}        

			// if (vel.Norm() != 0 && prom_topo.Norm() != 0) {

			ang_topo =  Math.Abs(1-pg)*(vel.Ang(prom_topo) + ht*(rnd.NextSimDouble(Math.PI)));
			// }

			vel.Rotate(ang_geom + ang_topo);

		}
// =============================================================================================

	}
}

