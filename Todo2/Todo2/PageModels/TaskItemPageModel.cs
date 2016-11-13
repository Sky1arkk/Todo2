using System;
using System.Collections.Generic;
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
    public class TaskItemPageModel : FreshBasePageModel
    {
        private SQLiteRepository _repository = App.Repository;

        public TaskItem TaskItem { get; set; }

        public override void Init(object initData)
        {
            base.Init(initData);

            var tempItem = initData as TaskItem;

            TaskItem = new TaskItem
            {
                Id = tempItem.Id,
                Name = tempItem.Name,
                Note = tempItem.Note,
                Priority = tempItem.Priority,
                Date = tempItem.Date
            };
        }

        public ICommand SaveCommand
        {
            get
            {
                return new Command((() =>
                {
                    if (TaskItem.IsValid())
                    {
                        _repository.UpsertTask(TaskItem);
                        CoreMethods.PopPageModel(TaskItem);
                    }
                }));
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                return new Command((() =>
                {
                    if (TaskItem.IsValid())
                    {
                        if (TaskItem.Id != null) _repository.DeleteTaskById((int)TaskItem.Id);
                        CoreMethods.PopPageModel(TaskItem);
                    }
                }));
            }
        }
    }
}
