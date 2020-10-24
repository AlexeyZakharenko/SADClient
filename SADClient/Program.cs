using RestSharp;
using System;

namespace SADClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new Auth0RestClient("http://localhost:3010/api");

            do
            {
                Console.Write("Author: ");
                string author = Console.ReadLine();
                Console.Write("Title: ");
                string title = Console.ReadLine();


                if (string.IsNullOrWhiteSpace(author) || string.IsNullOrWhiteSpace(title)) break;

                var request = new RestRequest($"/add?author={author}&title={title}", Method.GET);
                var response = client.Execute(request);
                Console.WriteLine(response.Content);

            } while (true);



        }
    }
}
