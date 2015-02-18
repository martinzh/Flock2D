using System;
using StatsLib;

namespace StatTest
{
	class MainClass
	{
		public static void Main (string[] args)
		{

			string f = "0.0";

			string path = "/home/martin/DATOS_SIMS/DATOS/data_f" + f;

			// Console.WriteLine (path);

			double[] parsArray = StatsTool.GetParams(path);

			// foreach (double pars in parsArray) 
			// {
			// 	Console.WriteLine(pars);
			// }


			int N           = (int)parsArray[0]; //Numero de particulas
			//double ro       = parsArray[1]; //Densidad
			double r_0      = parsArray[2]; //Radio interaccion (vel_reg)
			//int links       = (int)parsArray[3]; // numero de links
			//double frac     = parsArray[4]; //Fraccion de N en largo alcance
			//double noise_sh = parsArray[5]; //Ruido corto
			//double noise_lg = parsArray[6]; //Ruido largo
			//double rel_weig = parsArray[7]; //Peso Relativo vecindades
			//double reg_vel  = parsArray[8]; //Regimen de velocidad
			int t_f         = (int)parsArray[9]; //iteraciones totales
			int step        = (int)parsArray[10]; //frecuencia de muestreo

			int itTot = t_f/step;

			// Console.WriteLine("itTot = {0}", itTot);
			Console.WriteLine("tf = {0}\tstep = {1}\titTot = {2}",t_f,step,itTot);

			// Console.WriteLine("ok");

			double[,] dists = new double[N, N];

			double[,] trays = new double[itTot+1, (2*N)+1];

			double[,] vels = new double[itTot+1, (2*N)+1];

			// dists = StatsTool.GetDists(path, N, 0);
			StatsTool.GetDists(path, N, 0, dists);
			// for (int i = 0; i < N; i++) {
			// 	Console.WriteLine (dists [0, i]);
			// }
			Console.WriteLine("ok Dist");

			double[] vecDist = StatsTool.DistMatToVec(dists,N);
			Console.WriteLine("ok vecDist");

			StatsTool.GetVecs(path,"trays", itTot, trays);
			StatsTool.GetVecs(path,"vels", itTot, vels);
			
			// for (int i = 0; i < N+1; i++) {
			// 	Console.WriteLine (trays [0, i]);
			// }

			// for (int i = 0; i < N+1; i++) {
			// 	Console.WriteLine (vels [1, i]);
			// }

			Console.WriteLine("ok GetVecs");

			StatsTool.CalcHist(vecDist, itTot, step);

		}
	}
}
