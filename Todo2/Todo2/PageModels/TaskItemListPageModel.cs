using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreshMvvm;
using Todo2.Data;
using Todo2.Models;

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
            if (TaskList.Count < 1)
            {
                CreateSampleTasks();
            }
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

        private void CreateSampleTasks()
        {
            var firstTask = new TaskItem
            {
                Name = "wwwwww",
                Note = "qqqqqq",
                Priority = 1,
                Date = DateTime.Today
            };
            var secondTask = new TaskItem
            {
                Name = "tttttt",
                Note = "hhhhhh",
                Priority = 1,
                Date = DateTime.Today
            };

            _repository.UpsertTask(firstTask);
            _repository.UpsertTask(secondTask);

            LoadTasksFromRepository();
        }
    }
}
