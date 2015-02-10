using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using Flock;
using Agent;
using Vector;
using DataLogger;

namespace Swarm2D
{
	class MainClass
	{

		static void Main(string[] args)
		{
// ==================================== Parametros ==============================================

	// dt -> delta t
	// f -> fraccion de conexiones aleatorias relativo al total de parts
	// Vo -> magnitud de la velocidad de las particulas
	// ht ->ruido topologico
	// hg -> ruido geometrico
	// pg -> peso de vecindad geometrica
	// p  -> densidad
	// psi -> parametro de orden
	// l  -> regimen de velocidad (tamanio de paso relativo al radio de interaccion)
	// r -> radio de interaccion
	// N -> numero de particulas

		double dt = 1, f, Vo = 1, ht, hg, pg, p, l, r, N;

		int L = 30, T, step, k;

		string folder;

			if (args.Length != 0) {

				Console.WriteLine (args.Length);

				f      = Convert.ToDouble(args[0]);
				pg     = Convert.ToDouble(args[1]);
				p      = Convert.ToDouble(args[2]);
				T      = Convert.ToInt32(args[3]);
				l      = Convert.ToDouble(args[4]);
				ht     = Convert.ToDouble(args[5]);
				hg     = Convert.ToDouble(args[6]);
				step   = Convert.ToInt32(args[7]);
				// folder = "./DATOS/data_f" + args[0] + "_pg" + args[1];
				folder = "./DATOS/data_f" + args[0] ;
				
			}
			else { // valores default

				f      = 0.005;

				// ht     = 0.1;
				// hg     = 0.1;

				ht     = 0.0;
				hg     = 0.0;

				pg     = 0;
				p      = 1.2;
				l      = 0.25;
				//T      = 25000;
				T      = 1000;
				step   = 50;
				// folder = "./DATOS/data_f" + f + "_pg" + pg;
				folder = "./DATOS/data_f" + f ;
				
			}

			N = (double)L*(double)L*p;
			r = Vo*dt/l;
			k = (int)(f * N);

			Console.WriteLine("Particulas = " + (int)N);
			Console.WriteLine("densidad = " + p);
			Console.WriteLine("radio = " + r);
			Console.WriteLine("links = " + (int)k);
			// Console.WriteLine("f = " + args[0]);
			Console.WriteLine("f = " + f);
			Console.WriteLine("ruido geometrico = " + hg);
			Console.WriteLine("ruido topologico = " + ht);
			Console.WriteLine("peso geometrico = " + pg);
			Console.WriteLine("regimen de velocidad = " + l);
			Console.WriteLine("iteraciones = " + T);
			Console.WriteLine("paso = " + step);

			Console.WriteLine();

			Flock2D flock = new Flock2D((int)N, L, Vo, (int)f, r);

//			Console.WriteLine("parts = " + flock.elements.Length);

// ==================================== Archivos salida ==============================================

			DirectoryInfo di = Directory.CreateDirectory(folder);
			
			di = Directory.CreateDirectory(folder + "/dist_mat");
			di = Directory.CreateDirectory(folder + "/adj_mat");
			
// ==================================== Trayectorias ==============================================

			FileStream data;
			data = new FileStream( folder + "/trays.dat", FileMode.Create);
			StreamWriter st_data = new StreamWriter(data);

// ==================================== Velocidades ==============================================

			FileStream vels;
			vels = new FileStream( folder + "/vels.dat", FileMode.Create);
			StreamWriter st_vels = new StreamWriter(vels);

// ==================================== Desplazamiento promedio ==============================================

			FileStream des_prom;
			des_prom = new FileStream( folder + "/des_prom.dat", FileMode.Create);
			StreamWriter st_des_prom = new StreamWriter(des_prom);

// ==================================== Parametro de orden ==============================================

			FileStream psi_inst;
			psi_inst = new FileStream( folder + "/psi.dat", FileMode.Create);
			StreamWriter st_psi_inst = new StreamWriter(psi_inst);

// ==================================== Distribucion R ==============================================

			FileStream dist_R;
			dist_R = new FileStream( folder + "/dist_R.dat", FileMode.Create);
			StreamWriter st_dist_R = new StreamWriter(dist_R);

// ==================================== Pars. Simulacion ==============================================

			 FileStream pars;
			 pars = new FileStream( folder + "/parametros.dat", FileMode.Create);
			 StreamWriter st_pars = new StreamWriter(pars);

			st_pars.WriteLine("Particulas = " + (int)N);
			st_pars.WriteLine("densidad = " + p);
			st_pars.WriteLine("radio = " + r);
			st_pars.WriteLine("links = " + (int)k);
			//st_pars.WriteLine("f = " + args[0]);
			st_pars.WriteLine("f = " + f);
			st_pars.WriteLine("ruido geometrico = " + hg);
			st_pars.WriteLine("ruido topologico = " + ht);
			st_pars.WriteLine("peso geometrico = " + pg);
			st_pars.WriteLine("regimen de velocidad = " + l);
			st_pars.WriteLine("iteraciones = " + T);

// ==================================== Actualizacion ==============================================

			for (int i = 0; i < T+1; i++){

				flock.Update(dt, pg, hg ,ht);

				// Console.WriteLine("t = {0}",i);

				if ( i%step == 0) {

					Console.WriteLine("t = {0}",i);

					Logger.PrintMatrixDist(i,flock.dists, flock.elements.Length, flock.elements.Length, folder);
					// Logger.PrintMatrixAdj(i, flock.adj, flock.elements.Length, flock.elements.Length, folder);

					Logger.Print_pos(flock, st_data, i);
					Logger.Print_vel(flock, st_vels, i);
				}

			}

// ==================================== Cierra archivos ==============================================            

			st_data.Close();
			st_vels.Close();
			st_des_prom.Close();
			st_psi_inst.Close();
			st_dist_R.Close();
			st_pars.Close();

			Console.WriteLine("done");
		}
	}
}
