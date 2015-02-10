using System;
using System.IO;
using System.Collections.Generic;

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

		public static void GetDists(string path, int N, int t){

			string distsFile = path + "/dist_mat/" + t.ToString() + ".dat";

			Console.WriteLine(distsFile);

			try{
	        	using (StreamReader sr = new StreamReader(distsFile)){
		                while((line = sr.ReadLine()) != null){
							// Console.WriteLine (line);
							string[] chunk = line.Split('\t');
							parsList.Add(Convert.ToDouble(chunk[1]));
		   				}
          	}
        	}
    	catch (Exception e){
          Console.WriteLine("The file could not be read:");
          Console.WriteLine(e.Message);
    	}
		
			double[] parsArray = parsList.ToArray ();

		}

		// =============================================================================================


	}
}
