using CSUtilities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CSUtilities.Wpf
{
	/// <summary>
	/// Delegate for the work to execute with a notification method.
	/// </summary>
	/// <param name="progress"></param>
	public delegate void WorkDelegate(NotifyProgressDelegate progress);
	/// <summary>
	/// Delegate to notify the progress with a message.
	/// </summary>
	/// <param name="message"></param>
	public delegate void NotifyProgressDelegate(string message);
	/// <summary>
	/// Interaction logic for WorkProgress.xaml
	/// </summary>
	public partial class WorkProgress : UserControl
	{
		private Action m_work;
		private WorkDelegate m_notifiedWork;
		private Window m_parent;
		private WorkProgressViewModel m_context;
		private WorkProgress()
		{
			InitializeComponent();

			//Setup the datacontext
			m_context = new WorkProgressViewModel();
			this.DataContext = m_context;
		}
		private WorkProgress(Action action, Window parent) : this()
		{
			m_work = action;
			m_parent = parent;
		}
		private WorkProgress(WorkDelegate work, Window parent) : this()
		{
			m_parent = parent;
			m_notifiedWork = work;

			m_context.Message = "INTI";
		}
		/// <summary>
		/// Show the control in a window with the action progress.
		/// </summary>
		/// <param name="action"></param>
		public static void ShowProgress(Action action)
		{
			//Initialize the parent window where the progress bar is
			Window window = new Window();
			//Progress bar with the aciton
			WorkProgress dialog = new WorkProgress(action, window);

			//Setup the window
			window.Height = 150;
			window.Width = 450;
			window.Content = dialog;
			window.Activated += dialog.startWork;
			window.ShowDialog();
		}
		/// <summary>
		/// Show the control in a window with the action progress.
		/// </summary>
		/// <param name="work"></param>
		public static void ShowProgress(WorkDelegate work)
		{
			//Initialize the parent window where the progress bar is
			Window window = new Window();
			//Progress bar with the aciton
			WorkProgress dialog = new WorkProgress(work, window);

			//Setup the window
			window.Height = 150;
			window.Width = 450;
			window.Content = dialog;
			window.Activated += dialog.startNotifiedWork;
			window.ShowDialog();
		}
		//****************************************************************************************
		private void updateMessage(string message)
		{
			m_context.Message = message;
		}
		private void startNotifiedWork(object sender, EventArgs e)
		{
			Task t = Task.Run(() => m_notifiedWork.Invoke(updateMessage));
			t.GetAwaiter().OnCompleted(() => workCompleted(m_parent));
		}
		private void startWork(object sender, EventArgs e)
		{
			Task t = Task.Run(m_work);
			t.GetAwaiter().OnCompleted(() => workCompleted(m_parent));
		}
		/// <summary>
		/// Close the parent window when the work is done.
		/// </summary>
		/// <param name="parent"></param>
		private void workCompleted(Window parent)
		{
			if (parent.IsActive)
			{
				parent.Close();
			}
		}
	}
}
