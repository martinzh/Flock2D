using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using Agent;
using Vector;
using Flock;

namespace DataLogger{

	public class Logger{

	public Logger (){}

// =============================================================================================

		public static void Print_pos(Flock2D flock, StreamWriter st_data, int t){

			// string str = "";
			string str = t + "\t";

//			foreach (Agent2D agent in flock.elements){
//				str = str + agent.pos.Display() + "\t";
//			}

			foreach (Vector2D vec in flock.pos){
				str = str + vec.Display() + "\t";
			}

			st_data.WriteLine(str);
		}

// =============================================================================================

		public static void Print_vel(Flock2D flock, StreamWriter st_data, int t){

			string str = t + "\t";

//			string str = "";

//			foreach (Agent2D agent in flock.elements){
//				str = str + agent.vel.Display() + "\t";
//			}

			foreach (Vector2D vec in flock.vels){
				str = str + vec.Display() + "\t";
			}

			st_data.WriteLine(str);
		}

// =============================================================================================

		public static void Print_CM(Flock2D flock, StreamWriter st_data, StreamWriter st_data1, int t){
			
			Vector2D cm = new Vector2D();
			double R = 0;

			string str = t + "\t";
			string str1 = t + "\t";

			foreach (Agent2D agent in flock.elements){
				cm = cm + agent.pos;
				R += agent.pos.Norm();
			}

			cm = (1/(double)flock.elements.Length) * cm;
			R /= (1/(double)flock.elements.Length);

			str = str + cm.Display();
			str1 = str1 + R;

			st_data.WriteLine(str);
			st_data1.WriteLine(str1);
		}

// =============================================================================================

		public static double CalcPsi(Flock2D flock, double Vo){   
			Vector2D vel_prom = new Vector2D();

			foreach (Agent2D agent in flock.elements){
				vel_prom = vel_prom + agent.vel;    
			}

			double psi = (1/(double)flock.elements.Length*Vo) * vel_prom.Norm();

			return psi;
		}

// =============================================================================================

		public static void CalcPsi(Flock2D flock, double Vo, int t, StreamWriter stw){
			Vector2D vel_prom = new Vector2D();

			foreach (Agent2D agent in flock.elements) {
				vel_prom = vel_prom + agent.vel;    
			}

			double psi = (1/(double)flock.elements.Length*Vo) * vel_prom.Norm();

			string s = t + "\t" + psi ;

			stw.WriteLine(s);

		}

// =============================================================================================

		public static void CalcDistR(Flock2D flock, StreamWriter stw, int t, int tf){

			double num_bin = 50;

			int[] hist = new int[(int)num_bin];

			ArrayList dist = new ArrayList();

			foreach (Agent2D ag1 in flock.elements) {                
				foreach (Agent2D ag2 in flock.elements) {
					double d = ag1.pos.Dist(ag2.pos); 
					if(d>0) dist.Add(d);
				}
			}

			dist.Sort();

			dist.ToArray();

			double epsilon = (double)dist[dist.Count-1]/num_bin;

			// Console.WriteLine(epsilon);

			foreach (double d in dist) {
				// Console.WriteLine(d/epsilon);
				int b = (int)(d/epsilon);

				// Console.WriteLine(b);

				if(b > 0 ) hist[b-1] ++;
				// hist[(int)(d/epsilon)-1] ++;
				// 
			}

			string s = "# t = " + t;

			// stw.WriteLine("# t = {0}",t);
			stw.WriteLine(s);

			for (int i = 0; i < hist.Length; i++) {
				// Console.WriteLine("bin {0} = {1}",i,hist[i]);

				// s = t%tf + "\t" + i*epsilon + "\t" + hist[i];
				s = t%tf + "\t" + i + "\t" + hist[i];
				// s = i*epsilon + "\t" + hist[i];

				// stw.WriteLine("{0}\t{1}",i,hist[i]);
				stw.WriteLine(s);
			}

			// Console.WriteLine("primero : " + dist[1] + "\t ultimo : " + dist[dist.Count-1]);
			// s = "\n\n" ;
			s = "\n" ;
			// stw.Write("\n\n");
			stw.Write(s);
		}

// =============================================================================================

//		public static void PrintMatrix(/*int t,*/ int[,] matrix, int M, int N){
//
////			Console.WriteLine("#{0}#",t);
//
//			for(int i = 0; i < M; i ++){
//
//				for (int j = 0; j < N; j++){
//
//					Console.Write("{0}\t",matrix[i,j]);
//
//				}
//
//				Console.WriteLine();
//
//			}
//			Console.WriteLine();
//			Console.WriteLine();
//		}

// =============================================================================================

		public static void PrintMatrixDist(int t, double[,] matrix, int M, int N, string folder){

			string paso = folder + "/dist_mat/" + t + ".dat";

//			Console.WriteLine(paso);

			FileStream fst = new FileStream(paso, FileMode.Create);

			StreamWriter stw = new StreamWriter(fst);

			// stw.WriteLine("#{0}#",t);

			for(int i = 0; i < M; i ++){

				for (int j = 0; j < N; j++){

					stw.Write( "{0}\t" , matrix[i,j] );

				}

				stw.WriteLine();
			}
			stw.Close();
		}

// =============================================================================================

		public static void PrintMatrixAdj(int t, int[,] matrix, int M, int N, string folder){

			string paso = folder + "/adj_mat/" + t + ".dat";

//			Console.WriteLine(paso);

			FileStream fst = new FileStream(paso, FileMode.Create);

			StreamWriter stw = new StreamWriter(fst);

			// stw.WriteLine("#{0}#",t);

			for(int i = 0; i < M; i ++){

				stw.Write("{0}\t",i);

				for (int j = 0; j < N; j++){

					if (matrix[i,j] != 0) stw.Write("{0}\t",j*matrix[i,j]);

				}

				stw.Write("\n");
			}
			stw.Close();
			}
	}
// =============================================================================================
}

