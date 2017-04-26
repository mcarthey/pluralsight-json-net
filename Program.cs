using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Json.Demo.Models;
using Newtonsoft.Json;

namespace Json.Demo
{
    class Program
    {
        // https://jsfiddle.net/mcarthey/hg9d1qLm/2/
        static void Main(string[] args)
        {
            string json = @"{
                firstName: 'Mark',
                lastName: 'McArthey',
                courseCount: 3
            }";

            Console.WriteLine("Step 1: Output JSON");
            Console.WriteLine(json);
            Console.WriteLine(Environment.NewLine);

            Console.WriteLine("Step 2: Output Author FirstName");
            Author author = JsonConvert.DeserializeObject<Author>(json);
            Console.WriteLine(author.FirstName);
            Console.WriteLine(Environment.NewLine);

            Console.WriteLine("Step 3: Output serialized Author class");
            string jsonSerialized = JsonConvert.SerializeObject(author); //, Formatting.Indented);
            Console.WriteLine(jsonSerialized);
            Console.WriteLine(Environment.NewLine);
        }
    }
}
