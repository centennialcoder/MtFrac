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
using System.Windows.Forms;

namespace MTFrac
{
	public class ToolStripBtn: ToolStripControlHost
	{
		// call base constructor passing in Button
		public ToolStripBtn() : base (new System.Windows.Forms.Button()){}


		public Button ButtonControl
		{
			get { return Control as Button; }
		}

		//Expose Text property
		public override string Text
		{
			get { return base.Text;}
			set { base.Text = value; }
		}

			
		//// Subscribe and unsubscribe the control events you wish to expose.
		protected override void OnSubscribeControlEvents(Control c)
		{
			//connect base events
			base.OnSubscribeControlEvents(c);

			//Cast control to Button
			Button aButton = (Button)c;

			//Add event
			aButton.Click += new EventHandler(OnButtonClicked);
		}

		//protected override void OnSubscribeControlEvents(Control c)
		//{
		//   // Call the base so the base events are connected.
		//   base.OnSubscribeControlEvents(c);

		//   // Cast the control to a MonthCalendar control.
		//   MonthCalendar monthCalendarControl = (MonthCalendar) c;

		//   // Add the event.
		//   monthCalendarControl.DateChanged +=
		//      new DateRangeEventHandler(OnDateChanged);
		//}

		//protected override void OnUnsubscribeControlEvents(Control c)
		//{
		//   // Call the base method so the basic events are unsubscribed.
		//   base.OnUnsubscribeControlEvents(c);

		//   // Cast the control to a MonthCalendar control.
		//   MonthCalendar monthCalendarControl = (MonthCalendar) c;

		//   // Remove the event.
		//   monthCalendarControl.DateChanged -=
		//      new DateRangeEventHandler(OnDateChanged);
		//}

		//Declare Click event
		public event EventHandler ButtonClicked;

		//Rase ButtonClicked event
		private void OnButtonClicked(object sender, EventArgs e)
		{
			if (ButtonClicked != null)
			{
				ButtonClicked(this, e);
			}
		}

		//// Declare the DateChanged event.
		//public event DateRangeEventHandler DateChanged;

		//// Raise the DateChanged event.
		//private void OnDateChanged(object sender, DateRangeEventArgs e)
		//{
		//   if (DateChanged != null)
		//   {
		//      DateChanged(this, e);
		//   }
		//}
	}


}
