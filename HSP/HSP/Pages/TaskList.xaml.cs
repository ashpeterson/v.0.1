using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HSP.ViewModels;

namespace HSP.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TaskList : ContentPage
	{
		public TaskList ()
		{
			InitializeComponent ();

            Title = "Task List ";

			BindingContext = new TaskListViewModel();
		}
	}
}