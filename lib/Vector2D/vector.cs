using System;

//using System.Collections.Generic;
//using System.Collections;

namespace Vector{

	public class MyRand {

		public Random Rnd;

		public MyRand(){
			Rnd = new Random (Guid.NewGuid ().GetHashCode ());
		}

		public double NextDouble(){ return Rnd.NextDouble(); }
		public double NextSimDouble(double l){ return 2*l * Rnd.NextDouble() - l; }
	}

	public class Vector2D
	{
		// ==================================== Elementos ==============================================

		public double X, Y;
		public int Length;

		// ==================================== Constructores ==========================================

		public Vector2D() { X = Y = 0; Length = 2; }
		public Vector2D(double x, double y) { X = x; Y = y; Length = 2; }
		public Vector2D(Vector2D vec) { X = vec.X; Y = vec.Y; Length = 2; }

		// ==================================== Operadores ==============================================

		public static Vector2D operator +(Vector2D v1, Vector2D v2){
		return new Vector2D(v1.X + v2.X, v1.Y + v2.Y) ;}

		public static Vector2D operator -(Vector2D v1, Vector2D v2){
		return new Vector2D(v1.X - v2.X,v1.Y - v2.Y) ;}

		public static Vector2D operator *(double l, Vector2D v){
		return new Vector2D( l*v.X , l*v.Y) ; }

		public static Vector2D operator *(Vector2D v, double l){
		return new Vector2D( l*v.X , l*v.Y) ; }

		// ==================================== Metodos ==============================================

		public string Display() { return X + "\t" + Y ; }

		public double Norm(){ return Math.Sqrt( X*X + Y*Y ); }

		public double Norm2(){ return X*X + Y*Y ; }

		public double Dot(Vector2D vec){return X*vec.X + Y*vec.Y ;}

		public double Dist(Vector2D vec){return (this-vec).Norm() ;}

		public double Dist2(Vector2D vec){return (this-vec).Norm2() ;}

		public double Ang(Vector2D vec){

			double a1 = Math.Atan2(X,Y);
			double a2 = Math.Atan2(vec.X,vec.Y);

			return a2-a1;
		}

		public void Rotate(double ang) {

			double nX = X*Math.Cos(ang)-Y*Math.Sin(ang);
			double nY = X*Math.Sin(ang)+Y*Math.Cos(ang);

			X = nX;
			Y = nY;
		}
	}
}