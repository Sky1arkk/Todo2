using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo2.Models;

namespace Todo2.Data
{
    public interface IRepository
    {
        List<TaskItem> GetAllTasks();

        TaskItem GetTaskById(int id);

        List<TaskItem> GetTasksByReadiness(bool done);
        
        bool DeleteTasks(List<TaskItem> tasks);

        bool UpsertTask(TaskItem task);

        bool DeleteTaskById(int id);
    }
}
