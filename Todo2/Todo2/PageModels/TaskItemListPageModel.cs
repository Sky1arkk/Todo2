using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FreshMvvm;
using Todo2.Data;
using Todo2.Models;
using Xamarin.Forms;

namespace Todo2.PageModels
{
    public class TaskItemListPageModel : FreshBasePageModel
    {
        private SQLiteRepository _repository = App.Repository;

        public ObservableCollection<TaskItem> TaskList { get; set; }

        public TaskItem SelectedTaskItem
        {
            get { return null; }
            set
            {
                CoreMethods.PushPageModel<TaskItemPageModel>(value);
                RaisePropertyChanged();
            }
        }

        public TaskItemListPageModel()
        {
            TaskList = new ObservableCollection<TaskItem>();
        }

        public override void Init(object data)
        {
            LoadTasksFromRepository();
        }

        public override void ReverseInit(object data)
        {
            LoadTasksFromRepository();
            base.ReverseInit(data);
        }

        public void LoadTasksFromRepository()
        {
            TaskList.Clear();
            List<TaskItem> getTaskItems = _repository.GetAllTasks();
            foreach (var task in getTaskItems)
            {
                TaskList.Add(task);
            }
        }
        
        public ICommand AddTaskCommand
        {
            get
            {
                return new Command(() =>
                {
                    CoreMethods.PushPageModel<TaskItemPageModel>(new TaskItem {Date = DateTime.Today});
                });
            }
        }

        public ICommand SortTasksByPriorityCommand
        {
            get
            {
                return new Command(() =>
                {
                    var taskItemsList = _repository.GetAllTasks();
                    taskItemsList = taskItemsList.OrderBy(item => item.Priority).ToList();
                    var tasksList = new ObservableCollection<TaskItem>();
                    foreach (var task in taskItemsList)
                    {
                        tasksList.Add(task);
                    }

                    TaskList = tasksList;
                });
            }
        }

        public ICommand SortTasksByDateCommand
        {
            get
            {
                return new Command(() =>
                {
                    var taskItemsList = _repository.GetAllTasks();
                    taskItemsList = taskItemsList.OrderBy(item => item.Date).ToList();
                    var tasksList = new ObservableCollection<TaskItem>();
                    foreach (var task in taskItemsList)
                    {
                        tasksList.Add(task);
                    }

                    TaskList = tasksList;
                });
            }
        }

        public ICommand DeleteCompletedTasksCommand
        {
            get
            {
                return new Command(() =>
                {
                    _repository.DeleteTasks(_repository.GetTasksByReadiness(true));
                    LoadTasksFromRepository();
                });
            }
        }
    }
}
