using RestSharp;
using System;
using System.Collections.Generic;

namespace SADClient
{
    class Program
    {
        static void Main(string[] args)
        {

            // Register our RestClients
            Dictionary<Type, Auth0RestClient> restClients = new Dictionary<Type, Auth0RestClient> {
                { typeof(BookAppenderClient),  new BookAppenderClient("http://localhost:3010/api")},
                { typeof(BookRemoverClient),  new BookRemoverClient("http://localhost:3010/api") }
            };

            // Check available RestClients
            foreach (var type in restClients.Keys)
            {
                var response = restClients[type].Execute(new RestRequest("/claims"));
                Console.WriteLine($"{type} claims: {response.StatusCode} / {response.Content}");

                response = restClients[type].Execute(new RestRequest("/add"));
                Console.WriteLine($"{type} add (w/o parameters): {response.StatusCode} / {response.Content}");

                response = restClients[type].Execute(new RestRequest("/deleteByAuthor"));
                Console.WriteLine($"{type} delete (w/o parameters): {response.StatusCode} / {response.Content}");

                Console.WriteLine();

            }


            Console.WriteLine("Let's start to work");
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
                    executor = restClients[typeof(BookAppenderClient)];
                    request = new RestRequest($"/add?author={author}&title={title}", Method.GET);
                }
                // otherwise remove all books of this author
                else
                {
                    executor = restClients[typeof(BookRemoverClient)]; ;
                    request = new RestRequest($"/deleteByAuthor?author={author}", Method.GET);
                }

                var response = executor.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    Console.WriteLine($"{executor.GetType()}: {response.Content}");
                else
                    Console.WriteLine($"{executor.GetType()} ERROR: {response.StatusCode}");

            } while (true);
        }
    }
}
