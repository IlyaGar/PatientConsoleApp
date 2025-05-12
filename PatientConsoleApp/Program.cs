using Newtonsoft.Json;
using System.Text;

var client = new HttpClient();
client.BaseAddress = new Uri("https://localhost:44395");

for (int i = 0; i < 100; i++)
{
    var patient = new
    {
        Name = new { Given = new List<string> { "John" + i, "Smith" + i }, Family = "Smith" },
        BirthDate = DateTime.Now.AddYears(-20).ToString("yyyy-MM-dd")
    };

    var content = new StringContent(
       JsonConvert.SerializeObject(patient),
       Encoding.UTF8,
       "application/json"
   );

    var response = await client.PostAsync("/patient", content);

    if (response.IsSuccessStatusCode)
    {
        string jsonResponse = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Patient {i + 1} created successfully");
        Console.WriteLine("Response:");
        Console.WriteLine(jsonResponse);
    }
    else
    {
        Console.WriteLine($"Error creating patient {i + 1}. Status code: {response.StatusCode}");
    }

    await Task.Delay(1000);
}


Console.WriteLine("All patients processed.");