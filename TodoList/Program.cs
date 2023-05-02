using System;
using System.Data.SQLite;

namespace TodoList
{

    class Program
    {
        static void Main(string[] args)
        {
            // create a new database connection
            SQLiteConnection connection = new SQLiteConnection("Data Source=todolist.db");
            connection.Open();

            // create a new table to store tasks
            string createTableSQL = "CREATE TABLE IF NOT EXISTS tasks (id INTEGER PRIMARY KEY, description TEXT, completed INTEGER)";
            SQLiteCommand createTableCommand = new SQLiteCommand(createTableSQL, connection);
            createTableCommand.ExecuteNonQuery();

            // display all tasks
            string selectTasksSQL = "SELECT * FROM tasks";
            SQLiteCommand selectTasksCommand = new SQLiteCommand(selectTasksSQL, connection);
            SQLiteDataReader reader = selectTasksCommand.ExecuteReader();
            List<Todo> todos = new List<Todo>();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string description = reader.GetString(1);
                bool completed = reader.GetBoolean(2);
                Todo newTodo = new Todo(id, description, completed);
                todos.Add(newTodo);
                //Console.WriteLine($"{id}. {description} - {(completed ? "Completed" : "Incomplete")}");
            }

            // close the database connection
            connection.Close();

            Menu(todos);
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        static void Menu(List<Todo> todos)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. View all Todos");
                Console.WriteLine("2. Add Todo");
                Console.WriteLine("3. Update Todo");
                Console.WriteLine("4. Delete Todo");
                int choice;
                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    continue;
                }
                if (int.TryParse(input, out choice))
                {
                    int id = 0;
                    switch (choice)
                    {
                        case 1:
                            ViewTodos(todos);
                            break;
                        case 2:
                            AddTodo(todos);
                            break;
                        case 3:
                            Console.Clear();
                            Console.WriteLine("Enter an ID");
                            id = int.Parse(Console.ReadLine());
                            UpdateTodo(todos, id);
                            break;
                        case 4:
                            Console.Clear();
                            Console.WriteLine("Enter an ID");
                            id = int.Parse(Console.ReadLine());
                            DeleteTodo(todos, id);
                            break;
                        case 5:
                            //    MarkComplete(todos);
                            break;
                    }
                }
            }
        }
        static void UpdateTodo(List<Todo> todos, int ID)
        {
            Console.Clear();
            Console.WriteLine("Enter your new text");
            string response = Console.ReadLine();
                // create a new database connection
                SQLiteConnection connection = new SQLiteConnection("Data Source=todolist.db");
                connection.Open();

                // delete the task from the database
                string deleteTaskSQL = "UPDATE tasks SET description='" + response + "' WHERE id=@id";
                SQLiteCommand deleteTaskCommand = new SQLiteCommand(deleteTaskSQL, connection);
                deleteTaskCommand.Parameters.AddWithValue("@id", ID);
                deleteTaskCommand.ExecuteNonQuery();

                // close the database connection
                connection.Close();

                // update the object
                for (int i = 0; i < todos.Count; i++)
                {
                    if (todos[i].ID == ID)
                    {
                        todos[i].Text = response;
                    }
                }
        }
        static void DeleteTodo(List<Todo> todos, int ID)
        {
            Console.Clear();
            Console.WriteLine($"Are you sure you want to delete task {ID}? (y/n)");
            string response = Console.ReadLine();
            if (response.ToLower() == "y")
            {
                // create a new database connection
                SQLiteConnection connection = new SQLiteConnection("Data Source=todolist.db");
                connection.Open();

                // delete the task from the database
                string deleteTaskSQL = "DELETE FROM tasks WHERE id=@id";
                SQLiteCommand deleteTaskCommand = new SQLiteCommand(deleteTaskSQL, connection);
                deleteTaskCommand.Parameters.AddWithValue("@id", ID);
                deleteTaskCommand.ExecuteNonQuery();

                // close the database connection
                connection.Close();

                // update the object
                for (int i = 0; i < todos.Count; i++)
                {
                    if (todos[i].ID == ID)
                    {
                        todos.RemoveAt(i);
                    }
                }
            }
        }

        static void ViewTodos(List<Todo> todos)
        {
            foreach (Todo item in todos)
            {
                Console.WriteLine(item.ID + ". " + item.Text);

            }
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        static void AddTodo(List<Todo> todos)
        {
            // create a new database connection
            SQLiteConnection connection = new SQLiteConnection("Data Source=todolist.db");
            connection.Open();

            Console.Clear();
            Console.WriteLine("Text:");
            string tood = Console.ReadLine();
            int highest = 0;
            foreach (Todo item in todos)
            { // get the highest id
                if (item.ID > highest)
                {
                    highest = item.ID;
                }
            }
            Todo todo = new Todo(highest + 1, tood, false);
            todos.Add(todo);
            string insertTaskSQL = "INSERT INTO tasks (description, completed) VALUES (@description, @completed)";
            SQLiteCommand insertTaskCommand = new SQLiteCommand(insertTaskSQL, connection);
            insertTaskCommand.Parameters.AddWithValue("@description", todo.Text);
            insertTaskCommand.Parameters.AddWithValue("@completed", todo.Complete ? 1 : 0);
            insertTaskCommand.ExecuteNonQuery();
            connection.Close();
        }
    }

    class Todo
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public bool Complete { get; set; }

        public Todo(int id, string text, bool complete)
        {
            ID = id;
            Text = text;
            Complete = complete;
        }
    }
}
