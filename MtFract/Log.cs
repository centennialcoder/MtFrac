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
using System.Collections.Concurrent;
using System.IO;

namespace MtFract
{
	public static class Log
	{
		private const string LogDateFmt = "yyyy-MM-dd HH:mm:ss.fff";
		private static ConcurrentQueue<string> _logQ;
		private static bool _isShutdown;

		//static constructor
		static Log()
		{
			_logQ = new ConcurrentQueue<string>();
			System.Threading.Thread logThread = new System.Threading.Thread(WriteLogFile);
			logThread.Name = "Log Thread";
			logThread.Start();
		}

		public static bool IsShutdown
		{
			get { return _isShutdown; }
			set { _isShutdown = value; }
		}

		/// <summary>
		/// Log message to Log Queue
		/// </summary>
		/// <param name="sMsg"></param>
		/// <param name="list"></param>
		public static void Info(String sMsg, params object[] list)
		{
			string formattedMsg = string.Format(sMsg, list);

			try
			{
				//write to log queue
				_logQ.Enqueue(string.Format("{0:yyyy-MM-dd HH:mm:ss.fff} {1}", DateTime.Now, formattedMsg));
			}
			catch (Exception ex) { cErrlog.Log(ex); }
		}

		/// <summary>
		/// periodically dequeues log entries and writes to the log file
		/// </summary>
		private static void WriteLogFile()
		{
			string fileName, filePath;
			string item;

			try
			{
				//get Default filename & path
				System.Reflection.Assembly assem = System.Reflection.Assembly.GetExecutingAssembly();
				filePath = assem.Location;
				//remove .exe, add log.txt
				fileName = filePath.Substring(0, filePath.Length - 4) + " log.txt";

				//Create path if it doesn't exist
				filePath = Directory.GetParent(fileName).ToString();
				if (Directory.Exists(filePath) == false)
				{
					Directory.CreateDirectory(filePath);
				}

				do
				{
					if (_logQ.TryPeek(out item))
					{
						//Open Filestream
						using (FileStream fs = File.Open(fileName, FileMode.OpenOrCreate))
						{
							fs.Seek(0, SeekOrigin.End);
							StreamWriter sw = new StreamWriter(fs);
							//add everything in Q to filestream
							while (_logQ.TryDequeue(out item))
							{
								try
								{
									//write to log
									sw.WriteLine(item);
								}
								catch (IOException ex) { cErrlog.Log(ex); }					
							}
							sw.Flush();
							//close streamwriter
							if (sw != null) { sw.Close(); }
						}
					}

					//wait a while 
					System.Threading.Thread.Sleep(1000);
				} while (!_isShutdown);
				Console.WriteLine("Log Thread is shutting down");				
			}
			catch (Exception ex) { cErrlog.Log(ex); }
		}


	}
}
