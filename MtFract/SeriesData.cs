//The MIT License(MIT)
//Copyright(c) 2016 Mark Scherer

//Permission is hereby granted, free of charge, to any person obtaining a copy of this
//software and associated documentation files (the "Software"), to deal in the Software
//without restriction, including without limitation the rights to use, copy, modify, merge, 
//publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons
//to whom the Software is furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all copies or
//substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
//INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
//PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
//FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
//OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
//OTHER DEALINGS IN THE SOFTWARE. 


using System;

namespace MtFract
{
	#region Public Structs

		public enum eZoomType
		{
			Manual = 3,
			SeriesIn = 4,
			SeriesOut = 5,
			SeriesPan = 6,
		}

	#endregion

	public class SeriesData
	{
		//Data needed by frmMain to calculate Mandelbrot plots

		#region Fields

			//Render Series
			private eZoomType m_zoomType;
			private int m_PanPix;
			private double m_zoomVal = 1d;
			private MultiPt m_zoomCenter;
			private bool m_bWaitingForCenter;

			//File Info
			private string m_SavePath;
			private string m_SavePrefix;
			private int m_NextSaveNum;
			private string m_Extension;
			private System.Drawing.Imaging.ImageFormat m_format;

		#endregion //Fields

		#region Properties

			public eZoomType ZoomType
			{
				set { m_zoomType = value; }
				get { return m_zoomType; }
			}

			public int PanPix
			{
				get { return m_PanPix; }
				set { m_PanPix = value; }
			}

			public double ZoomVal
			{
				get { return m_zoomVal; }
				set { m_zoomVal = value; }
			}

			public MultiPt ZoomCenter
			{
				get { return m_zoomCenter; }
				set { m_zoomCenter = value; }
			}

			public bool bWaitingForCenter
			{
				get { return m_bWaitingForCenter; }
				set { m_bWaitingForCenter = value; }
			}

			//Save File Data
			public string SavePath
			{
				get { return m_SavePath; }
				set { m_SavePath = value; }
			}

			public string SavePrefix
			{
				get { return m_SavePrefix; }
				set { m_SavePrefix = value; }
			}

			public int NextSaveNum
			{
				get { return m_NextSaveNum; }
				set { m_NextSaveNum = value; }
			}

			public string NextSaveFile
			{
				get
				{
					string sOut = "default";
					try
					{
						//get next save file
						sOut = m_SavePath + "\\" + m_SavePrefix +
							m_NextSaveNum.ToString("0000") + m_Extension;
					}
					catch (Exception ex) { cErrlog.Log(ex); }
					return sOut;
				}
			}

			public string NextSaveFileShort
			{
				get
				{
					string sOut = "default";
					try
					{
						//get next save file
						sOut =m_SavePrefix +
							m_NextSaveNum.ToString("0000") + m_Extension;
					}
					catch (Exception ex) { cErrlog.Log(ex); }
					return sOut;
				}
			}

			public string Extension
			{
				get { return m_Extension; }
				set {
					//enforce enumerated values
					string sValue = value.ToLower();
					switch (sValue)
					{
						case ".png":
							m_Extension = ".png";
							m_format = System.Drawing.Imaging.ImageFormat.Png;
							break;
						default:
							m_Extension = ".bmp";
							m_format = System.Drawing.Imaging.ImageFormat.Bmp;
							break;
					}
				}
			}

			public System.Drawing.Imaging.ImageFormat Format
			{
				get { return m_format; }
			}

		#endregion //Properties

		#region Constructors

			public SeriesData()
			{
				//default constructor
				m_zoomType = eZoomType.Manual;
				m_PanPix = 0;
				m_zoomVal = 1.01F;
				m_zoomCenter = new MultiPt(0d, 0d);
				m_bWaitingForCenter = false;

				m_SavePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
				m_SavePrefix = "MandelPic";
				m_NextSaveNum = 1;
				m_Extension = ".bmp";
				m_format = System.Drawing.Imaging.ImageFormat.Bmp;
			}

		#endregion //Constructors

		#region Public Methods

			public void IncrementSaveNum()
			{
				m_NextSaveNum++;
			}

		#endregion //Publc Methods
	}
}
