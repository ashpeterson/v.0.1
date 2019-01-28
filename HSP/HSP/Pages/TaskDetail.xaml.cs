using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HSP.ViewModels;
using HSP.Models;
using HSP.Pages;
//using TaskList.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskList.ViewModels;

namespace HSP.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TaskDetail : ContentPage
	{
		public TaskDetail (DataModel item = null)
		{
			InitializeComponent ();

            Title = "Task Details";

			BindingContext = new TaskDetailViewModel(item);
		}
	}
}