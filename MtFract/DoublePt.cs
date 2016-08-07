using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MtFract
{
	public class DoublePt
	{
		//2d point using double floating point values

		//private
		private double m_x;
		private double m_y;


		//properties
		public double x
		{
			get { return m_x; }
			set { 
				m_x = value; 
			}
		}

		public double y
		{
			get { return m_y; }
			set { m_y = value; }
		}

		//constructors
		public DoublePt(double x, double y)
		{
			m_x = x;
			m_y = y;
		}

		public DoublePt() : this(0,0)
		{
			//zero for both params
		}


		//methods
		public override string ToString()
		{
			string sOut = "";
			sOut = string.Format("({0:#,0.000000000000000;-#,0.000000000000000},{1:#,0.000000000000000;-#,0.000000000000000})", x, y);
			return sOut;
		}
			
	}
}
