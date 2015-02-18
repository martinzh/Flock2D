using System;
using System.IO;
using System.Collections.Generic;
using AForge.Math;
using System.Linq;
using Vector;

namespace StatsLib{

	public class StatsTool{

		public StatsTool (){}

// =============================================================================================

		public static double[] GetParams(string path){

			string parsFile = path + "/parametros.dat";

			Console.WriteLine (parsFile);

			string line;

			List<double> parsList = new List<double>();

			try{
	        	using (StreamReader sr = new StreamReader(parsFile)){
		                while((line = sr.ReadLine()) != null){
							// Console.WriteLine (line);
							string[] chunk = line.Split('=');
							parsList.Add(Convert.ToDouble(chunk[1]));
		   				}
          	}
        	}
    	catch (Exception e){
          Console.WriteLine("The file could not be read:");
          Console.WriteLine(e.Message);
    	}
		
			double[] parsArray = parsList.ToArray ();

			return parsArray;
		}

// =============================================================================================

		public static void GetDists(string path, int N, int t, double[,] result){

			string distsFile = path + "/dist_mat/" + t.ToString() + ".dat";

			Console.WriteLine(distsFile);

			String input = File.ReadAllText( distsFile );

			int i = 0, j = 0;

			// Console.WriteLine ("start read");

			foreach (var row in input.Split('\n'))
			{
				j = 0;

				foreach (var col in row.Trim().Split('\t'))
				{	
					double dis;
					Double.TryParse (col.Trim(), out dis);
					//Console.WriteLine ( "{0}\t{1}\t{2}" ,i , j, dis);
					if(i < N) result[i, j] = dis;
					j++;
				}
				i++;
			}

			// Console.WriteLine ("end read");
		}

// =============================================================================================

		public static double[] DistMatToVec(double [,] distsMat, int N){

			int M = (N*(N-1))/2;
			// Console.WriteLine(M);
			double[] vecDist = new double[M];

			// Console.WriteLine(N);

			int i = 0;

			for( int j = 0; j < N; j ++){
				for(int k = j+1; k < N; k ++){
					// Console.WriteLine("{0}\t{1}\t{2}", j,k,i);
					vecDist[i] = distsMat[j,k];
					i++;
				}
			}
			return vecDist;
		}

// =============================================================================================

		public static void GetVecs(string path, string vecs, int n, double[,] result){

			string vecsFile = path + "/" + vecs + ".dat";

			Console.WriteLine(vecsFile);

			String input = File.ReadAllText( vecsFile );

			int i = 0, j = 0;

			// Console.WriteLine ("start read");

			foreach (var row in input.Split('\n'))
			{
				j = 0;
				if (i < n) 
				{
					foreach (var col in row.Trim().Split('\t'))
					{	
						double comp;
						Double.TryParse (col.Trim(), out comp);
						// Console.WriteLine ( "{0}\t{1}\t{2}" ,i , j, comp);
						result[i, j] = comp;
						j++;
					}
					i++;					
				}
			}

			// Console.WriteLine ("end read");
		}

// =============================================================================================

		public static void CalcHist(double[] dists, int iter, int step){
			IEnumerable<int> numbers = Enumerable.Range(0, iter+1).Select(x => x * step);

			foreach (int t in numbers) {
				Console.WriteLine (t);
			}

			//Histogram hist = new Histogram(dists);
		}


// =============================================================================================

	}
}
