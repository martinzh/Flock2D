
using System.IO;
using System.Collections;
using Agent;
using Vector;
using Flock;

//using System;
//using System.Collections.Generic;

namespace DataLogger{

	public static class Logger{

	//public Logger (){}

// =============================================================================================

		public static void PrintPos(Flock2D flock, StreamWriter stData){

			string str = "";

			//foreach (Agent2D bird in flock.Elements){
			//	str = str + bird.Pos.Display() + "\t";
			//}

			int n = flock.Elements.Length;

			for (int i = 0; i < n; i++) {

				if (i < n - 1) {
					str = str + flock.Elements [i].Pos.Display () + "\t";
				} else {
					str = str + flock.Elements [i].Pos.Display ();
				}
			}

			stData.WriteLine(str);
		}

// =============================================================================================

		public static void PrintVel(Flock2D flock, StreamWriter stData){

			string str = "";

			//foreach (Agent2D bird in flock.Elements){
			//	str = str + bird.Vel.Display() + "\t";
			//}

			int n = flock.Elements.Length;

			for (int i = 0; i < n; i++) {

				if (i < n - 1) {
					str = str + flock.Elements [i].Vel.Display () + "\t";
				} else {
					str = str + flock.Elements [i].Vel.Display ();
				}
			}

			stData.WriteLine(str);
		}

// =============================================================================================

		public static void PrintCM(Flock2D flock, StreamWriter stData, StreamWriter stData1, int t){
			
			var cm = new Vector2D();
			double R = 0;

			string str = t + "\t";
			string str1 = t + "\t";

			foreach (Agent2D agent in flock.Elements){
				cm = cm + agent.Pos;
				R += agent.Pos.Norm();
			}

			cm = (1/(double)flock.Elements.Length) * cm;
			R /= (1/(double)flock.Elements.Length);

			str = str + cm.Display();
			str1 = str1 + R;

			stData.WriteLine(str);
			stData1.WriteLine(str1);
		}

// =============================================================================================

		public static double CalcPsi(Flock2D flock, double vo){   
			var vel_prom = new Vector2D();

			foreach (Agent2D agent in flock.Elements){
				vel_prom = vel_prom + agent.Vel;    
			}

			double psi = (1/(double)flock.Elements.Length*vo) * vel_prom.Norm();

			return psi;
		}

// =============================================================================================

		public static void CalcPsi(Flock2D flock, double vo, int t, StreamWriter stw){
			var vel_prom = new Vector2D();

			foreach (Agent2D agent in flock.Elements) {
				vel_prom = vel_prom + agent.Vel;    
			}

			double psi = (1/(double)flock.Elements.Length*vo) * vel_prom.Norm();

			string s = t + "\t" + psi ;

			stw.WriteLine(s);

		}

// =============================================================================================

		public static void CalcDistR(Flock2D flock, StreamWriter stw, int t, int tf){

			const double num_bin = 50;

			var hist = new int[(int)num_bin];

			var dist = new ArrayList();

			foreach (Agent2D ag1 in flock.Elements) {                
				foreach (Agent2D ag2 in flock.Elements) {
					double d = ag1.Pos.Dist(ag2.Pos); 
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

		public static void PrintMatrixDist(int t, double[,] matrix, int m, int n, string folder){

			string paso = folder + "/dist_mat/" + t + ".txt";

//			Console.WriteLine(paso);

			var fst = new FileStream(paso, FileMode.Create);

			var stw = new StreamWriter(fst);

			// stw.WriteLine("#{0}#",t);

			for(int i = 0; i < m; i ++){

				for (int j = 0; j < n; j++){

					if(j < n-1){
						stw.Write( "{0}\t" , matrix[i,j] );
					}else{
						stw.Write( "{0}" , matrix[i,j] );
					}

				}

				stw.WriteLine();
			}
			stw.Close();
		}

// =============================================================================================

		public static void PrintMatrixAdj(int t, int[,] matrix, int m, int n, string folder){

			string paso = folder + "/adj_mat/" + t + ".dat";

//			Console.WriteLine(paso);

			var fst = new FileStream(paso, FileMode.Create);

			var stw = new StreamWriter(fst);

			// stw.WriteLine("#{0}#",t);

			for(int i = 0; i < m; i ++){

				stw.Write("{0}\t",i);

				for (int j = 0; j < n; j++){

					if (matrix[i,j] != 0) stw.Write("{0}\t",j*matrix[i,j]);

				}

				stw.Write("\n");
			}
			stw.Close();
			}
	}
// =============================================================================================
}

