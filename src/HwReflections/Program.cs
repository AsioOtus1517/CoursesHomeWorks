using System.Diagnostics;
using System.Text.RegularExpressions;
using HwReflections;
using Newtonsoft.Json;

var fS = F.Get();
var w = new Stopwatch();

var jsonSerialization = string.Empty;
var mySerialization = string.Empty;

w.Start();

for (var i = 0; i < 100000; i++) 
{
    mySerialization = JsonConvertWithReflection.Serialize(fS);
    // Console.WriteLine(mySerialization);
}

w.Stop();

var mySerializationTime = w.ElapsedMilliseconds;

mySerialization = Regex.Replace(mySerialization, @"\s+", "");
mySerialization = Regex.Replace(mySerialization, "\"", "");

w.Restart();

for (var i = 0; i < 100000; i++) 
{
    var fD = JsonConvertWithReflection.Deserialize<F>(mySerialization);
    // Console.WriteLine(fD.Equals(fS));
}

w.Stop();

var myDeserializationTime = w.ElapsedMilliseconds;

w.Restart();

// Newtonsoft.Json serialization/deserialization
for (var i = 0; i < 100000; i++) 
{
    jsonSerialization = JsonConvert.SerializeObject(fS, Formatting.Indented);
    // Console.WriteLine(jsonSerialization);
}

w.Stop();

var jsonSerializationTime = w.ElapsedMilliseconds;

w.Restart();

for (var i = 0; i < 100000; i++) 
{
    var fD = JsonConvert.DeserializeObject<F>(jsonSerialization);
    // Console.WriteLine(fD.Equals(fS));
}

w.Stop();

var jsonDeserializarionTime = w.ElapsedMilliseconds;

Console.WriteLine($"My Serialization: {mySerializationTime}");
Console.WriteLine($"My Deserialization: {myDeserializationTime}");
Console.WriteLine($"Newtonsoft.Json Serialization: {jsonSerializationTime}");
Console.WriteLine($"Newtonsoft.Json Deserialization: {jsonDeserializarionTime}");