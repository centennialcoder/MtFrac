using System;
using System.IO;
using System.Reflection;
using System.Text;

public class cErrlog
{
	//Global Error handler class to be called from every catch block
	//	Doesn't use WinForms classes	so can be called by DLL's
	public static void Log(Exception Ex)
	{
		string sPath;
		string sFilename;
		DateTime dt = DateTime.Now;
		StringBuilder sMsg = new StringBuilder("");
		StreamWriter MyWriter;
		try
		{
			Assembly assem = Assembly.GetExecutingAssembly();
			AssemblyName an = assem.GetName();
			Module Mod = assem.ManifestModule;
			sPath = assem.Location;
			//remove .exe, add Errors.txt
			sFilename = sPath.Substring(0, sPath.Length - 4) + " Errors.txt";
			//Open error log to append
			MyWriter = new StreamWriter(sFilename,true);

			//Create Error message
			sMsg.Append(dt.ToString("yyyy-MM-dd HH:mm:ss.fff"));
			sMsg.Append(" " + Mod.Name + " Version " + an.Version.ToString() + "\r\n");
			sMsg.Append(Ex.ToString());
			if (Ex.InnerException != null)
				sMsg.Append(Ex.InnerException.ToString());
			sMsg.Append("\r\n------------------------------------------\r\n");

			//Write to Log
			MyWriter.WriteLine(sMsg);
			MyWriter.Close();

		}
		catch(Exception e)
		{
		//Can't write to log - this statement prevents compiler warning
			Console.WriteLine(e.Message);
		}
	}

	public static void Log(Exception Ex, string sComment)
	{
		string sPath;
		string sFilename;
		DateTime dt = DateTime.Now;
		StringBuilder sMsg = new StringBuilder("");
		StreamWriter MyWriter;
		try
		{
			Assembly assem = Assembly.GetExecutingAssembly();
			AssemblyName an = assem.GetName();
			Module Mod = assem.ManifestModule;
			sPath = assem.Location;
			//remove .exe, add Errors.txt
			sFilename = sPath.Substring(0, sPath.Length - 4) + " Errors.txt";
			//Open error log to append
			MyWriter = new StreamWriter(sFilename, true);

			//Create Error message
			sMsg.Append(dt.ToString("yyyy-MM-dd HH:mm:ss.fff"));
			sMsg.Append(" " + Mod.Name + " Version " + an.Version.ToString() + "\r\n");
			sMsg.Append(sComment + "\r\n");
			sMsg.Append(Ex.ToString());
			if (Ex.InnerException != null)
				sMsg.Append(Ex.InnerException.ToString());
			sMsg.Append("\r\n------------------------------------------\r\n");

			//Write to Log
			MyWriter.WriteLine(sMsg);
			MyWriter.Close();

		}
		catch (Exception e)
		{
			//Can't write to log - this statement prevents compiler warning
			Console.WriteLine(e.Message);
		}
	}

	//Catches thread exceptions
	public static void UIThreadException(object sender, System.Threading.ThreadExceptionEventArgs t)
	{
		Log(t.Exception);	
	}

	//logs unhandled exceptions
	public static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
	{
		Exception ex = (Exception)e.ExceptionObject;
		Log(ex);
		//program will now terminate
	}
}
