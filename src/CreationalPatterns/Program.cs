using CreationalPatterns;

using Microsoft.Extensions.DependencyInjection;

var serviceContainer = new ServiceCollection();

serviceContainer
    .AddTransient<IMyCloneable<Character>, Character>(_ => new Character() { Name = "Merchant", Health = 50 })
    .AddTransient<IMyCloneable<Warior>, Warior>(_ => new Warior() { Name = "Guard", Health = 100, Strength = 20 })
    .AddTransient<IMyCloneable<Paladin>, Paladin>(_ => new Paladin() { Name = "Paladin", Health = 200, Strength = 10, HolyPower = 20 });

serviceContainer
    .AddKeyedTransient<ICloneable, Character>("ClonableCharacter", (_, _) => new Character() { Name = "Merchant", Health = 50 })
    .AddKeyedTransient<ICloneable, Warior>("ClonableWarior", (_, _) => new Warior() { Name = "Guard", Health = 100, Strength = 20 })
    .AddKeyedTransient<ICloneable, Paladin>("ClonablePaladin", (_, _) => new Paladin() { Name = "Paladin", Health = 200, Strength = 10, HolyPower = 20 });

using var servicesProvider = serviceContainer.BuildServiceProvider();

#region IMyClonable Usage
    
var character = servicesProvider.GetRequiredService<IMyCloneable<Character>>();
var warior = servicesProvider.GetRequiredService<IMyCloneable<Warior>>();
var paladin = servicesProvider.GetRequiredService<IMyCloneable<Paladin>>();

CloneWithMyCloneable(character).ToList().ForEach(Console.WriteLine);
CloneWithMyCloneable(warior).ToList().ForEach(Console.WriteLine);
CloneWithMyCloneable(paladin).ToList().ForEach(Console.WriteLine);

#endregion

Console.WriteLine("--------------------------------------------------");

#region IClonable Usage

var cCharacter = (Character)servicesProvider.GetRequiredKeyedService<ICloneable>("ClonableCharacter");
var cWarior = (Warior)servicesProvider.GetRequiredKeyedService<ICloneable>("ClonableWarior");
var cPaladin = (Paladin)servicesProvider.GetRequiredKeyedService<ICloneable>("ClonablePaladin");

CloneWithCloneable<Character>(cCharacter).ToList().ForEach(Console.WriteLine);
CloneWithCloneable<Warior>(cWarior).ToList().ForEach(Console.WriteLine);
CloneWithCloneable<Paladin>(cPaladin).ToList().ForEach(Console.WriteLine);

#endregion

serviceContainer.Clear();

return;

IList<T> CloneWithMyCloneable<T>(IMyCloneable<T> clonable) 
{
    var random = new Random((int)DateTime.Now.Ticks);
    var count = random.Next(2, 10);
    var clones = new List<T>();

    for (int i = 0; i < count; i++) clones.Add(clonable.MyClone());

    return clones;
}

IList<T> CloneWithCloneable<T>(ICloneable clonable) 
{
    var random = new Random((int)DateTime.Now.Ticks);
    var count = random.Next(2, 10);
    var clones = new List<T>();

    for (int i = 0; i < count; i++) clones.Add((T)clonable.Clone());

    return clones;
}