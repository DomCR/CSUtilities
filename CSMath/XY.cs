using System;
using System.Numerics;

namespace CSMath
{
	public struct XY : IVector<XY>
	{
		public readonly static XY Zero = new XY(0, 0);
		public readonly static XY AxisX = new XY(1, 0);
		public readonly static XY AxisY = new XY(0, 1);

		public double X { get; set; }
		public double Y { get; set; }

		public XY(double x, double y)
		{
			X = x;
			Y = y;
		}

		public XY(double[] components) : this(components[0], components[1]) { }

		/// <summary>
		/// Get the angle
		/// </summary>
		/// <returns>angle in radians</returns>
		public double GetAngle()
		{
			return Math.Atan2(Y, X);
		}

		public double[] GetComponents()
		{
			return new double[] { X, Y };
		}

		public XY SetComponents(double[] components)
		{
			return new XY(components);
		}

		public override string ToString()
		{
			return $"{X},{Y}";
		}

		/// <summary>
		/// Get the angle from 2 vectors
		/// </summary>
		/// <param name="u"></param>
		/// <param name="v"></param>
		/// <returns>angle in radians</returns>
		public static double GetAngle(XY u, XY v)
		{
			XY dir = v.Substract(u);
			return dir.GetAngle();
		}


		public static explicit operator XY(XYZ xyz)
		{
			return new XY(xyz.X, xyz.Y);
		}
	}
}
