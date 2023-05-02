using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BlogApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            // Read the categories from an external file
            string[] categories = File.ReadAllLines("categories.txt");

            // Display the available categories
            Console.WriteLine("Available Categories:");
            foreach (string category in categories)
            {
                Console.WriteLine(category);
            }
            Console.WriteLine();

            // Prompt the user to select a category
            Console.Write("Select a category: ");
            string selectedCategory = Console.ReadLine();

            // Validate the selected category
            if (!categories.Contains(selectedCategory))
            {
                Console.WriteLine("Invalid category selected.");
                Console.ReadKey();
                return;
            }

            // Prompt the user to enter the blog details
            Console.Write("Enter blog title: ");
            string title = Console.ReadLine();

            Console.Write("Enter blog content: ");
            string content = Console.ReadLine();

            // Create a new blog object
            Blog newBlog = new Blog
            {
                Category = selectedCategory,
                Title = title,
                Content = content,
                Date = DateTime.Now
            };

            // Save the new blog to a CSV file
            string fileName = "blogs.csv";

            if (!File.Exists(fileName))
            {
                // Create a new CSV file and write the header row
                using (StreamWriter sw = File.CreateText(fileName))
                {
                    sw.WriteLine("Category,Title,Content,Date");
                }
            }

            // Append the new blog to the CSV file
            using (StreamWriter sw = File.AppendText(fileName))
            {
                sw.WriteLine($"{newBlog.Category},{newBlog.Title},{newBlog.Content},{newBlog.Date}");
            }

            Console.WriteLine("Blog saved successfully.");
            Console.ReadKey();
        }
    }

    class Blog
    {
        public string Category { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
    }
}
