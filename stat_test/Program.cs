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

			int N           = (int)parsArray[0]; //Numero de particulas
			double ro       = parsArray[1]; //Densidad
			double r_0      = parsArray[2]; //Radio interaccion (vel_reg)
			double frac     = parsArray[3]; //Fraccion de N en largo alcance
			double noise_sh = parsArray[4]; //Ruido corto
			double noise_lg = parsArray[5]; //Ruido largo
			double rel_weig = parsArray[6]; //Peso Relativo vecindades
			double reg_vel  = parsArray[7]; //Regimen de velocidad
			int t_f         = (int)parsArray[8]; //iteraciones totales
			int step        = (int)parsArray[9]; //frecuencia de muestreo

			int itTot = t_f/step;

			Console.WriteLine("ok");

			double[,] dists = new double[N, N];

			dists = StatsTool.GetDists(path, N, 0);

			Console.WriteLine("ok Dist");

			//for (int i = 0; i < N; i++) {
			//	Console.WriteLine (dists [0, i]);
			//}

			double[] vecDist = StatsTool.DistMatToVec(dists,N);

		}
	}
}
