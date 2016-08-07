using System;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace MtFract
{
	public class Coloring
	{
		//supports coloring algorithms for Mandelbrot set

		#region enums

			public enum eCycleType
			{
				Linear = 0,
				Geometric = 1,
				Exponential = 2,
			}

		#endregion //enums

		#region Private Data

			private int _numColors;
			private int _cycleStart;
			private double _cycleFactor;
			private eCycleType _cycleType;
			private string _colorFile;
			private string _name;
			private Color[] _colorMap;

		#endregion //Private Data

		#region Properties

			/// <summary>
			/// size of ColorMap array
			/// </summary>
			public int NumColors
			{	
				get { return _numColors; }
			}								//size of colorMap array

			/// <summary>
			/// where non-linear color functions start
			/// </summary>
			public int CycleStart
			{
				get { return _cycleStart; }
				set {
					if (value >= 1)
						_cycleStart = value;
					else
						_cycleStart = 1;
				}
			}							//where non-linear color functions start

			/// <summary>
			/// scaling coefficient
			/// </summary>
			public double CycleFactor
			{
				get { return _cycleFactor; }
				set {
					if (_cycleFactor > 0d)
						_cycleFactor = value;
					else
						_cycleFactor = 1d;
				}
			}						

			/// <summary>
			/// color Scaling method
			/// </summary>
			public eCycleType CycleType
			{
				get { return _cycleType; }
				set { _cycleType = value; }
			}					

			/// <summary>
			/// Full path of color file
			/// </summary>
			public string ColorFile
			{
				get { return _colorFile; }
				set { _colorFile = value; }
			}

			public string Name
			{
				get
				{
					return _name;
				}
			}
			/// <summary>
			/// array of rgb color values
			/// </summary>
			public Color[] ColorMap
			{
				get { return _colorMap; }
				set 
				{ 
					_colorMap = value;
					_numColors = _colorMap.Length;
				}
			}							

		#endregion //Properties

		#region Methods

			//Constructor
			public Coloring(eCycleType type, 
									int cyclestart,
									double cyclefactor,
									string sColorFile)
			{
				_cycleStart = cyclestart;
				_cycleFactor = cyclefactor;
				_cycleType = type;
				_colorFile = sColorFile;
				LoadColorMap(sColorFile);
			}

			public Coloring() : this(eCycleType.Linear, 1, 1.0, "MarkS.map")
			{
				//default constructor
			}


			//public methods

			/// <summary>
			/// load a map file as defined by fractint into m_ColorMap
			/// Each line is a color tuplet of r g b (decimal byte) seperated by spaces
			/// can have header [CycleType]
			//		next line is cycleType|CycleStart|CycleFactor
			/// </summary>
			/// <param name="sColorFile"></param>
			public void LoadColorMap(string sColorFile)
			{
				//
				Color[] tempColor;
				string[] sLines;
				int r,g,b;
				bool bNextIsHeader = false;

				try
				{
					//Check File
					if (!File.Exists(sColorFile))
					{
						sColorFile = FindAnyColorMap();
						if (sColorFile == "")
							return;
					}

					//find name
					_name = Path.GetFileName(sColorFile);

					//Load all lines of Color File
					sLines = File.ReadAllLines(sColorFile);
					_numColors = sLines.Length;
					tempColor = new Color[_numColors];

					//convert line of r,g,b into a Color
					//Map file define by fractint or with header
					int ColorNum = 0;
					for (int i = 0; i < _numColors; i++)
					{
						//check for header
						if (sLines[i] == "[CycleType]")
						{
							bNextIsHeader = true;
							continue;
						}

						if (bNextIsHeader)
						{
							ParseHeader(sLines[i]);
							bNextIsHeader = false;
							continue;
						}

						//skip blank rows
						if (sLines[i] =="")
						{
							Console.WriteLine("Skipping " + i.ToString());
							continue;	//skip blank rows
						}

						//load color
						r = 0; g = 0; b = 0;	//defaults to black
						int.TryParse(fnParseWord(ref sLines[i]), out r);
						int.TryParse(fnParseWord(ref sLines[i]), out g);
						int.TryParse(fnParseWord(ref sLines[i]), out b);
						tempColor[ColorNum] = Color.FromArgb(r, g, b);
						ColorNum++;
					}

					//set ColorMap
					_colorMap = tempColor;
					_numColors = _colorMap.Length;

					//remove empty colors
					if (ColorNum < _numColors)
					{
						ChangeNumColors(ColorNum);
					}
				}
				catch (Exception ex) { cErrlog.Log(ex); }
			}

			public System.Drawing.Color ColorLookup(int NumIter, int MaxIter)
			{	//returns a color based on chosen color function

				Color colorOut = Color.Black;
				int colorIdx = 0;				//index to ColorMap for returned color

				try
				{
					//always black if >= MaxIterations
					if ((NumIter >= MaxIter) || (NumIter < 1) || (NumColors == 0))
						return Color.Black;

					//linear below cycleStart
					if ((NumIter < _cycleStart) || (_cycleFactor == 0))
					{
						//cannot process functions with cycleFacter of 0 (1/cf)
						colorIdx = NumIter % NumColors;
					}
					else
					{
						//remove iterations before cycleStart
						NumIter = (NumIter - _cycleStart);

						switch (CycleType)
						{
							case eCycleType.Linear:
								colorIdx = _cycleStart + (int)(NumIter / _cycleFactor);
								break;

							case eCycleType.Geometric:
								colorIdx = _cycleStart + (int)(System.Math.Pow(NumIter, (1d / _cycleFactor)));
								break;

							case eCycleType.Exponential:
								//ln(x)/Ln(factor) = Log factor(x)
								//exponential coloring uses Log base (cyclefactor) of numIter
								colorIdx = CycleStart + (int)(System.Math.Log(NumIter) / System.Math.Log(_cycleFactor));
								break;
						}
						colorIdx = colorIdx % NumColors;
					}

					//look up color from ColorMap
					colorOut = ColorMap[colorIdx];
				}
				catch (Exception ex) { cErrlog.Log(ex); }
				return colorOut;
			}

			public void ChangeNumColors(int newNumColors)
			{
				Color[] newArray;
				int NumToCopy;

				//changes NumColors, length of ColorMap
				try
				{
					newArray = new Color[newNumColors];

					//How many elements can we copy
					NumToCopy = newNumColors;
					if (NumToCopy > _colorMap.Length)
						NumToCopy = _colorMap.Length;

					//Copy elements from colorMap to new array
					Array.Copy(_colorMap, newArray, NumToCopy);

					//set colorMap to new array
					_colorMap = newArray;

					//initialize any extra colors to black
					if (newNumColors > _numColors)
					{
						for (int i = _numColors; i < newNumColors; i++)
						{
							_colorMap[i] = Color.Black;
						}
					}

					//set NumColors
					_numColors = newNumColors;

				}
				catch (InvalidCastException ex) { cErrlog.Log(ex); }
			}

		#endregion //public Methods

		#region Private Methods
			
			private string FindAnyColorMap()
			{	//tries to find a color map in ColorMaps subdirectory
				string sOut = "";
				string sPath;
				string[] sPatterns;
				string[] sFiles;
				System.Windows.Forms.OpenFileDialog OpenDialog1 = new System.Windows.Forms.OpenFileDialog();

				try
				{
					//get path
					sPath = Directory.GetCurrentDirectory().ToString();
					sPath = Path.GetFullPath(sPath) + "\\ColorMaps";

					//find first file
					sPatterns = new string[1];
					sPatterns[0]= "*.map";
					sFiles = Directory.GetFiles(sPath,"*.map");
					if (sFiles.Length > 0)
						sOut = sFiles[0];
				}
				catch(Exception ex){cErrlog.Log(ex);}
				return sOut;
			}

			private string fnParseWord(ref string sLine)
			{
				//Destructively parses a word (whitespace or comma seperated) from sLine
				//Returns the trimmed word
				string sOut = "";
				char sChar = (char)0;
				bool bWhiteLast = true;

				try
				{
					sLine = sLine.Trim();
					if (sLine.Length < 1)
						return sOut;
					else
				
					do
					{
						sChar = sLine[0];

						//Check if we are at a seperator (or whitespace only if last wasn't whitespace)
						if ((sChar == ',') || (sChar == '\n') || (sChar == '\r') ||
							((!bWhiteLast) && (sChar == ' ')) ||
							((!bWhiteLast) && (sChar == '\t')))
							break;	//leave loop
						else
							sOut += sChar;

						//Check if this is whitespace
						if ((sChar == ' ') || (sChar == '\t'))
							bWhiteLast = true;
						else
							bWhiteLast = false;

						if (sLine.Length < 2)
							break;		//this is last char

						//remove first char
						sLine = sLine.Substring(1);

					} while (true);
					
				}
				catch (Exception ex) { cErrlog.Log(ex); }
				return sOut;
			}

			public static string fnGetDelimitedWord(string sInput, int WordNum, string Delimiter)
			{
				string sOut = "";
				int LastPos, CurPos;
				int Cnt;

				try
				{
					//Default Delimiter
					if (Delimiter == "")
						Delimiter = "|";

					if (WordNum >= 1)
					{
						CurPos = -1;
						LastPos = -1;

						for (Cnt = 1; Cnt <= WordNum; Cnt++)
						{
							LastPos = CurPos;
							CurPos = sInput.IndexOf(Delimiter, LastPos + 1);
							if (CurPos == -1)
								break;	//no more delimiters
						}

						if (CurPos == -1)
						{
							if (Cnt < WordNum)
								return sOut;  //Not enough words
							else
							{	//Go from LastPos to end of string
								sOut = sInput.Substring(LastPos + 1);
							}
						}
						else
						{
							//Go from LastPos to CurPos
							sOut = sInput.Substring(LastPos + 1, CurPos - LastPos - 1);
						}

					}
				}
				catch (Exception ex) { cErrlog.Log(ex); }
				return sOut;
			}

			private void ParseHeader(string sLine)
			{
				//parses this header line into CycleType, CycleStart, CycleFactor
				string sType, sStart, sFactor;
				int type, start;
				float factor;

				try
				{
					sType = fnGetDelimitedWord(sLine, 1, ",");
					sStart = fnGetDelimitedWord(sLine, 2, ",");
					sFactor = fnGetDelimitedWord(sLine, 3, ",");

					int.TryParse(sType, out type);
					if ((type >= 0) && (type <= 2))
					{
						_cycleType = (eCycleType)type;
					}

					int.TryParse(sStart, out start);
					if (start > 0)
					{
						_cycleStart = start;
					}

					float.TryParse(sFactor, out factor);
					if (factor >= 1.0)
					{
						_cycleFactor = factor;
					}
				}
				catch(Exception ex){cErrlog.Log(ex);}
			}

		#endregion //Private Methods
	}
}