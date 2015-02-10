using System;
using System.Collections.Generic;
using System.Collections;

namespace Vector{

	public class MyRand {

		public Random rnd;

		public MyRand(){
			this.rnd = new Random(Guid.NewGuid().GetHashCode());
		}

		public double NextDouble(){ return this.rnd.NextDouble(); }
		public double NextSimDouble(double L){ return 2*L * this.rnd.NextDouble() - L; }
	}

	public class Vector2D
	{
		// ==================================== Elementos ==============================================

		public double x, y;
		public int Length;

		// ==================================== Constructores ==========================================

		public Vector2D() { x = y = 0; Length = 2; }
		public Vector2D(double x, double y) { this.x = x; this.y = y; Length = 2; }
		public Vector2D(Vector2D vec) { this.x = vec.x; this.y = vec.y; Length = 2; }

		// ==================================== Operadores ==============================================

		public static Vector2D operator +(Vector2D v1, Vector2D v2){
		return new Vector2D(v1.x + v2.x, v1.y + v2.y) ;}

		public static Vector2D operator -(Vector2D v1, Vector2D v2){
		return new Vector2D(v1.x - v2.x,v1.y - v2.y) ;}

		public static Vector2D operator *(double l, Vector2D v){
		return new Vector2D( l*v.x , l*v.y) ; }

		public static Vector2D operator *(Vector2D v, double l){
		return new Vector2D( l*v.x , l*v.y) ; }

		// ==================================== Metodos ==============================================

		public string Display() { return this.x + "\t" + this.y ; }

		public double Norm(){ return Math.Sqrt( x*x + y*y ); }

		public double Dot(Vector2D vec){return x*vec.x + y*vec.y ;}

		public double Dist(Vector2D vec){return (this-vec).Norm() ;}

		public double Ang(Vector2D vec){

			double a1 = Math.Atan2(x,y);
			double a2 = Math.Atan2(vec.x,vec.y);

			return a2-a1;
		}

		public void Rotate(double ang) {

			double X = x*Math.Cos(ang)-y*Math.Sin(ang);
			double Y = x*Math.Sin(ang)+y*Math.Cos(ang);

			x = X;
			y = Y;
		}
	}
}