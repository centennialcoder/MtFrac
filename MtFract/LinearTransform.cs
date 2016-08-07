using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MtFract
{
	public class LinearTransform
	{
		//linear transform from one rectangle to another
		//includes offset and zoom (size multiplier)
		//doesn't include angular distortion

		//used to transform Plot (integer rectangle) to next plot in use.

		/*  |-------------|
		 *  |             |
		 *  |  (offset)   |
		 *  |   ..........|.....
		 *  |	  .			|    .
		 *  |	  .			|    .
		 *  |-------------|    .
		 *		  .              .
		 *		  ................
		 */



		//private
		DoublePt m_offset;		//dimensionless = units of original rectangle
		double m_zoom;				//dimensionless = new length/old length

		//properties
		public DoublePt offset
		{
			get { return m_offset; }
			set { m_offset = value; }
		}

		public double zoom
		{
			get { return m_zoom; }
			set { m_zoom = value; }
		}

		//constructors
		public LinearTransform()
			: this(new DoublePt(0,0), 1.0)
		{
		}

		public LinearTransform(DoublePt offset, double zoomval)
		{
			this.m_offset = offset;
			this.m_zoom = zoomval;
		}
	}
}
