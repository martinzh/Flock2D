using System;
using System.IO;
using Flock;
using DataLogger;

//using System.Collections.Generic;
//using System.Collections;
//using Agent;
//using Vector;


namespace Swarm2D
{
	class MainClass
	{

		static void Main(string[] args)
		{
// ==================================== Parametros ==============================================

	// dt -> delta t
	// k -> Conectividad de la red de interaccion
	// Vo -> magnitud de la velocidad de las particulas
	// eta -> magnitud del ruido
	// w -> peso de vecindad geometrica
	// p  -> densidad
	// psi -> parametro de orden
	// regVel  -> regimen de velocidad (tamanio de paso relativo al radio de interaccion)
	// r0 -> radio de interaccion
	// N -> numero de particulas

		double dt, v0, eta, w, p, regVel, r0, l;

		int tf, step, k, n;

		string folder;

			if (args.Length != 0) {

				//Console.WriteLine (args.Length);

				k 	 = Convert.ToInt32(args [0]);
				eta  = Convert.ToDouble(args[1]);
				p    = Convert.ToDouble(args[2]);
				w    = Convert.ToDouble(args[3]);
				tf   = Convert.ToInt32(args[4]);
				step = Convert.ToInt32(args[5]);


				//folder = "./DATOS/data_eta" + args[1] + "_k" + args[0];
				folder = "/home/martin/DATOS_SIMS/DATOS/data_eta" + args[1] + "_k" + args[0] + "_w" + args[3];
				
			}
			else { // valores default

				k    = 0;
				eta  = 0.1;
				tf   = 1000;
				step = 50;
				p    = 500.0;
				w 	 = 0.5;
				//folder = "./DATOS/data_eta" + eta + "_k" + k ;
				folder = "/home/martin/DATOS_SIMS/DATOS/data_eta" + eta + "_k" + k + "_w" + w;
			}

			dt 	   = 1.0;
			v0     = 1.0;
			regVel = 0.08;

			r0 = v0*dt/regVel;
			//l = r0;
			l = 1.0;
			n = (int)(l*l*p);

			Console.WriteLine("Particulas = " + n);
			Console.WriteLine("densidad = " + p);
			Console.WriteLine("radio = " + r0);
			Console.WriteLine("conectividad = " + k);
			Console.WriteLine("intensidad de ruido = " + eta);
			Console.WriteLine("peso geometrica = " + w);
			Console.WriteLine("regimen de velocidad = " + regVel);
			Console.WriteLine("iteraciones = " + tf);
			Console.WriteLine("paso = " + step);

			Console.WriteLine();

			var flock = new Flock2D(n, l, v0, k);

//			Console.WriteLine("parts = " + flock.elements.Length);

// ==================================== Archivos salida ==============================================

			DirectoryInfo di = Directory.CreateDirectory(folder);
			
			di = Directory.CreateDirectory(folder + "/dist_mat");
			//di = Directory.CreateDirectory(folder + "/adj_mat");
			
// ==================================== Trayectorias ==============================================

			FileStream data;
			data = new FileStream( folder + "/trays.txt", FileMode.Create);
			var st_data = new StreamWriter(data);

// ==================================== Velocidades ==============================================

			FileStream vels;
			vels = new FileStream( folder + "/vels.txt", FileMode.Create);
			var st_vels = new StreamWriter(vels);

// ==================================== Desplazamiento promedio ==============================================
/*
			FileStream des_prom;
			des_prom = new FileStream( folder + "/des_prom.dat", FileMode.Create);
			var st_des_prom = new StreamWriter(des_prom);

// ==================================== Parametro de orden ==============================================

			FileStream psi_inst;
			psi_inst = new FileStream( folder + "/psi.dat", FileMode.Create);
			var st_psi_inst = new StreamWriter(psi_inst);

// ==================================== Distribucion R ==============================================

			FileStream dist_R;
			dist_R = new FileStream( folder + "/dist_R.dat", FileMode.Create);
			var st_dist_R = new StreamWriter(dist_R);
*/
// ==================================== Pars. Simulacion ==============================================

			 FileStream pars;
			 pars = new FileStream( folder + "/parametros.txt", FileMode.Create);
			 var st_pars = new StreamWriter(pars);

			st_pars.WriteLine("Particulas = " + n);
			st_pars.WriteLine("densidad = " + p);
			st_pars.WriteLine("radio = " + r0);
			st_pars.WriteLine("conectividad = " + k);
			st_pars.WriteLine("intensidad de ruido = " + eta);
			st_pars.WriteLine("peso geometrica = " + w);
			st_pars.WriteLine("regimen de velocidad = " + regVel);
			st_pars.WriteLine("iteraciones = " + tf);
			st_pars.WriteLine("paso = " + step);

// ==================================== Actualizacion ==============================================

			for (int i = 0; i < tf+1; i++){

				flock.Update(dt, w, eta, r0);

				// Console.WriteLine("t = {0}",i);

				if ( i%step == 0) {

					Console.WriteLine("t = {0}",i);

					Logger.PrintMatrixDist(i,flock.Dists, flock.Elements.Length, flock.Elements.Length, folder);
					// Logger.PrintMatrixAdj(i, flock.adj, flock.elements.Length, flock.elements.Length, folder);

					Logger.PrintPos(flock, st_data);
					Logger.PrintVel(flock, st_vels);
				}

			}

// ==================================== Cierra archivos ==============================================            

			st_data.Close();
			st_vels.Close();
			st_pars.Close();

			//st_des_prom.Close();
			//st_psi_inst.Close();
			//st_dist_R.Close();

			Console.WriteLine("done");
		}
	}
}
