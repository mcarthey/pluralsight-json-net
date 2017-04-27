using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Json.Demo.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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

            Console.WriteLine("**********************************************");
            Console.WriteLine("** Basic JSON serialization/deserialization **");
            Console.WriteLine("**********************************************");
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

            Console.WriteLine("*******************************");
            Console.WriteLine("** Dynamic and ExpandoObject **");
            Console.WriteLine("*******************************");
            Console.WriteLine("Step 1: Create dynamic object and serialize");
            dynamic authorDynamic = new ExpandoObject();
            authorDynamic.Name = "Mark McArthey";
            authorDynamic.Courses = new List<string> { "DIY", "Nutrition", "Programming" };
            string jsonDynamic = JsonConvert.SerializeObject(authorDynamic);
            Console.WriteLine(jsonDynamic);
            Console.WriteLine(Environment.NewLine);

            Console.WriteLine("Step 2: Deserialize dynamic object");
            dynamic jsonDeserialized = JsonConvert.DeserializeObject(jsonDynamic);
            Console.WriteLine(jsonDeserialized);
            Console.WriteLine(Environment.NewLine);

            Console.WriteLine("***********");
            Console.WriteLine("** Dates **");
            Console.WriteLine("***********");
            author.courseDate = new DateTime(2017, 04, 27, 11, 0, 0).ToUniversalTime();
            Console.WriteLine("Step 1: Do not specify any date format");
            var jsonDateDefault = JsonConvert.SerializeObject(author, Formatting.Indented);
            Console.WriteLine(jsonDateDefault);
            Console.WriteLine(Environment.NewLine);

            Console.WriteLine("Step 2: Microsoft date - pre .Net 4.5");
            JsonSerializerSettings settingsMicrosoftDate = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
            };
            string jsonMicrosoftDate = JsonConvert.SerializeObject(author, Formatting.Indented, settingsMicrosoftDate);
            Console.WriteLine(jsonMicrosoftDate);
            Console.WriteLine(Environment.NewLine);

            Console.WriteLine("Step 3: ISO date format");
            JsonSerializerSettings settingsIsoDate = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat
            };
            string jsonIsoDate = JsonConvert.SerializeObject(author, Formatting.Indented, settingsIsoDate);
            Console.WriteLine(jsonIsoDate);
            Console.WriteLine(Environment.NewLine);

            Console.WriteLine("Step 4: Custom date format");
            JsonSerializerSettings settingsCustomDate = new JsonSerializerSettings
            {
                DateFormatString = "yyyy/MM/dd"
            };
            string jsonCustomDate = JsonConvert.SerializeObject(author, Formatting.Indented, settingsCustomDate);
            Console.WriteLine(jsonCustomDate);
            Console.WriteLine(Environment.NewLine);

            Console.WriteLine("********************");
            Console.WriteLine("** Error Handling **");
            Console.WriteLine("********************");
            List<string> errors = new List<string>();

            JsonSerializerSettings jSS = new JsonSerializerSettings
            {
                Error = (sender, errorArgs) =>
                {
                    errors.Add(errorArgs.ErrorContext.Error.Message);
                    errorArgs.ErrorContext.Handled = true;
                },
                Converters = { new IsoDateTimeConverter() }
            };

            Console.WriteLine("Step 1: Handle the error");
            List<DateTime> deserializedDates = JsonConvert.DeserializeObject<List<DateTime>>(@"[
                                                                    '2017-04-27T16:00:00Z',
                                                                    '2017/04/27',
                                                                    '2017/04/41'
                                                                    ]", jSS);
            Console.WriteLine("Dates:");
            foreach (DateTime dateTime in deserializedDates)
            {
                Console.WriteLine(dateTime.ToShortDateString());
            }
            Console.WriteLine(Environment.NewLine);

            Console.WriteLine("Errors:");
            foreach (var err in errors)
            {
                Console.WriteLine(err);
            }
            Console.WriteLine(Environment.NewLine);

            Console.WriteLine("Step 2: Don't handle the error");
            List<DateTime> deserializedDatesError = JsonConvert.DeserializeObject<List<DateTime>>(@"[
                                                                    '2017-04-27T16:00:00Z',
                                                                    '2017/04/27',
                                                                    '2017/04/41'
                                                                    ]");

        }
    }
}
