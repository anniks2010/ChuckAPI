using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nancy.Json;

namespace ChuckAPI2
{
    class Program
    {
        static void Main(string[] args)
        {
           
            ShowCategories();
            Console.WriteLine($"Please select joke category from the aboved list:");
            string userInput = Console.ReadLine();
            GetUserChoiseJoke(userInput);


            Console.ReadLine();
        }
        public static void ShowCategories()
        {
            string categoryUrl = "https://api.chucknorris.io/jokes/categories";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(categoryUrl);
            request.Method = "GET";

            var webResponse = request.GetResponse();
            var webStream = webResponse.GetResponseStream();

            using (var responseReader = new StreamReader(webStream))
            {
                var response = responseReader.ReadToEnd(); ////panen valmis koha, kuhu maha lugeda, mis on olemas vastuses
                ///Console.WriteLine(response); ////nüüd vaatame, mis me saame

                JavaScriptSerializer ser = new JavaScriptSerializer();  ///et vastust salvestatakse massiivi
                var categories = ser.Deserialize<List<string>>(response);

                foreach (string category in categories)
                {
                    Console.WriteLine(category);
                }
            }
        }
        public static void ShowRandomJoke()
        {
            string randomJokeUrl = "https://api.chucknorris.io/jokes/random";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(randomJokeUrl);
            request.Method = "GET";

            var webResponse = request.GetResponse();
            var webStream = webResponse.GetResponseStream();

            using (var responseReader = new StreamReader(webStream))
            {
                var response = responseReader.ReadToEnd(); ////panen valmis koha, kuhu maha lugeda, mis on olemas vastuses
                Joke randomJoke = JsonConvert.DeserializeObject<Joke>(response);
                Console.WriteLine(randomJoke.Value);
            }

        }
        public static void GetUserChoiseJoke(string userInput)
        {
            
            string userInputCategory = $"https://api.chucknorris.io/jokes/random?category={userInput}";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(userInputCategory);
            request.Method = "GET";

            var webResponse = request.GetResponse();
            var webStream = webResponse.GetResponseStream();

            using (var responseReader = new StreamReader(webStream))
            {
                var response = responseReader.ReadToEnd(); ////panen valmis koha, kuhu maha lugeda, mis on olemas vastuses
                Joke randomJoke = JsonConvert.DeserializeObject<Joke>(response);
                Console.WriteLine(randomJoke.Value);
            }
        }
    }
}
