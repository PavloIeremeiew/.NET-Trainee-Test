using Microsoft.AspNetCore.Http;
using NET_Trainee_Test_MVC.Models.Entities;
using System.Globalization;
using System.Text;

namespace NET_Trainee_Test_MVC.Utilities
{
    public static class CsvConvertor
    {
        public static List<Person> ReadCsv(IFormFile file)
        {
            var persons = new List<Person>();
            using var reader = new StreamReader(file.OpenReadStream());
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine()!;
                var values = line.Split(';');
                var product
                    = new Person
                    {
                        Name = values[0],
                        BirthDay = DateTime.Parse(values[1]),
                        IsMarried = bool.Parse(values[2]),
                        Phone = values[3],
                        Salary = decimal.Parse(values[4])
                    };
                persons.Add(product);
            }
            return persons;

        }

        public static byte[] ExportToCsv(IEnumerable<Person> persons)
        {
            var csv = new StringBuilder();

            foreach (var person in persons)
            {
                csv.AppendLine($"{person.Id};{person.Name};{person.BirthDay};{person.IsMarried};{person.Phone};{person.Salary}");
            }

            var memory = new MemoryStream();
            var writer = new StreamWriter(memory);
            writer.Write(csv);
            writer.Flush();
            memory.Position = 0;
            byte[] bytes = Encoding.UTF8.GetBytes(csv.ToString());

            return bytes;
        }
    }
}
