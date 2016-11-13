using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Todo2.Models;

namespace Todo2.Data
{
    public class SQLiteRepository : IRepository
    {
        private readonly SQLiteConnection _connection;
        private SQLiteCommand _command;
        public string StatusMessage { get; set; }

        public SQLiteRepository(string dbPath)
        {
            _connection = new SQLiteConnection(dbPath);
            _connection.CreateTable<TaskItem>();
        }

        public List<TaskItem> GetAllTasks()
        {
            return _connection.Table<TaskItem>().ToList();
        }

        public TaskItem GetTaskById(int id)
        {
            return _connection.Table<TaskItem>().ElementAt(id);
        }

        public List<TaskItem> GetTasksByPriority(bool done)
        {
            var doneToInt = done ? 1 : 0;

            _command = _connection.CreateCommand("SELECT * FROM [TaskItem] WHERE [Done] = ?", doneToInt);
            return _command.ExecuteQuery<TaskItem>();
        }

        public bool DeleteTasks(List<TaskItem> tasks)
        {

            foreach (var element in tasks)
            {
                try
                {
                    _command = _connection.CreateCommand("DELETE FROM TaskItem WHERE Id = ?", element.Id);
                    _command.ExecuteNonQuery();
                }
                catch (SQLiteException ex)
                {
                    StatusMessage = $"Failed to delete contact: {element.Name}. Error: {ex.Message}";
                    return false;
                }
            }
            return true;
        }

        public bool UpsertTask(TaskItem task)
        {
            int result;
            try
            {
                if (string.IsNullOrWhiteSpace(task.Name))
                    throw new Exception("Name is required");

                result = _connection.InsertOrReplace(task);
            }
            catch (SQLiteException ex)
            {
                StatusMessage = $"Failed to create task: {task.Name}. Error: {ex.Message}";
                return false;
            }
            if (result > 0)
            {
                return true;
            }
            return false;
        }

        public bool DeleteTaskById(int id)
        {
            try
            {
                _command = _connection.CreateCommand("DELETE FROM TaskItem WHERE Id = ?", id);
                _command.ExecuteNonQuery();
                return true;
            }
            catch (SQLiteException ex)
            {
                StatusMessage = $"Failed to delete contact with Id: {id}. Error: {ex.Message}";
                return false;
            }
        }
    }
}
