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
        private SQLiteCommand _command;
        private readonly string _dbPath;

        public SQLiteRepository(string dbPath)
        {
            _dbPath = dbPath;

            using (var connection = new SQLiteConnection(dbPath))
            {
                try
                {
                    connection.BeginTransaction();
                    connection.CreateTable<TaskItem>();
                    connection.Commit();
                }
                catch (SQLiteException)
                {
                    connection.Rollback();
                }
            }
        }

        public List<TaskItem> GetAllTasks()
        {
            using (var connection = new SQLiteConnection(_dbPath))
            {
                return connection.Table<TaskItem>().ToList();
            }
        }

        public TaskItem GetTaskById(int id)
        {
            using (var connection = new SQLiteConnection(_dbPath))
            {
                return connection.Table<TaskItem>().ElementAt(id);
            }
        }

        public List<TaskItem> GetTasksByPriority(bool done)
        {
            using (var connection = new SQLiteConnection(_dbPath))
            {
                var doneToInt = done ? 1 : 0;

                _command = connection.CreateCommand("SELECT * FROM [TaskItem] WHERE [Done] = ?", doneToInt);
                return _command.ExecuteQuery<TaskItem>();
            }
        }

        public bool DeleteTasks(List<TaskItem> tasks)
        {
            using (var connection = new SQLiteConnection(_dbPath))
            {
                foreach (var element in tasks)
                {
                    try
                    {
                        _command = connection.CreateCommand("DELETE FROM TaskItem WHERE Id = ?", element.Id);
                        connection.BeginTransaction();
                        _command.ExecuteNonQuery();
                        connection.Commit();
                    }
                    catch (SQLiteException)
                    {
                        connection.Rollback();
                        return false;
                    }
                }
                return true;
            }
        }

        public bool UpsertTask(TaskItem task)
        {
            using (var connection = new SQLiteConnection(_dbPath))
            {
                int result;
                try
                {
                    if (string.IsNullOrWhiteSpace(task.Name))
                        return false;

                    connection.BeginTransaction();
                    result = connection.InsertOrReplace(task);
                    connection.Commit();
                }
                catch (SQLiteException)
                {
                    connection.Rollback();
                    return false;
                }
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public bool DeleteTaskById(int id)
        {
            using (var connection = new SQLiteConnection(_dbPath))
            {
                try
                {
                    _command = connection.CreateCommand("DELETE FROM TaskItem WHERE Id = ?", id);
                    connection.BeginTransaction();
                    _command.ExecuteNonQuery();
                    connection.Commit();
                    return true;
                }
                catch (SQLiteException)
                {
                    connection.Rollback();
                    return false;
                }
            }
        }
    }
}
