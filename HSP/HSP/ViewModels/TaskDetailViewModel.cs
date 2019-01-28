using System;
using System.Diagnostics;
using System.Threading.Tasks;
using HSP.Abstractions;
using HSP.Models;
using HSP.Services;
using HSP.Pages;
using HSP.ViewModels;
using Xamarin.Forms;
using HSP;

namespace TaskList.ViewModels
{
    public class TaskDetailViewModel : BaseViewModel
    {
        ICloudTable<DataModel> table = App.CloudService.GetTable<DataModel>();

        public TaskDetailViewModel(DataModel item = null)
        {
            if (item != null)
            {
                Item = item;
                title = item.Text;
            }
            else
            {
                Item = new DataModel { Text = "New Item", Complete = false };
                title = "New Item";
            }
        }

        public DataModel Item { get; set; }

        Command cmdSave;
        public Command SaveCommand => cmdSave ?? (cmdSave = new Command(async () => await ExecuteSaveCommand()));

        async Task ExecuteSaveCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                if (Item.Id == null)
                {
                    await table.CreateItemAsync(Item);
                }
                else
                {
                    await table.UpdateItemAsync(Item);
                }
                MessagingCenter.Send<TaskDetailViewModel>(this, "ItemsChanged");
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[TaskDetail] Save error: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        Command cmdDelete;
        public Command DeleteCommand => cmdDelete ?? (cmdDelete = new Command(async () => await ExecuteDeleteCommand()));

        async Task ExecuteDeleteCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                if (Item.Id != null)
                {
                    await table.DeleteItemAsync(Item);
                }
                MessagingCenter.Send<TaskDetailViewModel>(this, "ItemsChanged");
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[TaskDetail] Save error: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}