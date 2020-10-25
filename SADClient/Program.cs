using RestSharp;
using System;

namespace SADClient
{
    class Program
    {
        static void Main(string[] args) {

            var bookAppender = new BookAppenderClient("http://localhost:3010/api");
            var bookRemover = new BookRemoverClient("http://localhost:3010/api");

            var response = bookAppender.Execute(new RestRequest("/claims"));
            Console.WriteLine($"bookAppender claims: {response.StatusCode} / {response.Content}");

            response = bookAppender.Execute(new RestRequest("/add"));
            Console.WriteLine($"bookAppender emty add: {response.StatusCode} / {response.Content}");

            response = bookAppender.Execute(new RestRequest("/deleteByAuthor"));
            Console.WriteLine($"bookAppender emty delete: {response.StatusCode} / {response.Content}");

            Console.WriteLine();

            response = bookRemover.Execute(new RestRequest("/claims"));
            Console.WriteLine($"bookRemover claims: {response.StatusCode} / {response.Content}");

            response = bookRemover.Execute(new RestRequest("/add"));
            Console.WriteLine($"bookRemover check add: {response.StatusCode} / {response.Content}");

            response = bookRemover.Execute(new RestRequest("/deleteByAuthor"));
            Console.WriteLine($"bookRemover check delete: {response.StatusCode} / {response.Content}");

            Console.WriteLine();

            Console.WriteLine("Let start to work");
            do
            {
                Console.Write("Author: ");
                string author = Console.ReadLine();
                Console.Write("Title: ");
                string title = Console.ReadLine();

                // End of work
                if (string.IsNullOrWhiteSpace(author) && string.IsNullOrWhiteSpace(title)) break;

                IRestClient executor;
                IRestRequest request;

                // Fill both values? add the book
                if (!string.IsNullOrEmpty(title))
                {
                    executor = bookAppender;
                    request = new RestRequest($"/add?author={author}&title={title}", Method.GET);
                }
                // otherwise remove all books of this author
                else 
                {
                    executor = bookRemover;
                    request = new RestRequest($"/deleteByAuthor?author={author}", Method.GET);
                }

                response = executor.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    Console.WriteLine(response.Content);
                else 
                    Console.WriteLine($"ERROR: {response.StatusCode}");

            } while (true);
        }
    }
}
