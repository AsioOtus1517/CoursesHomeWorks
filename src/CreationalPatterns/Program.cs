using CreationalPatterns;

using Microsoft.Extensions.DependencyInjection;

var serviceContainer = new ServiceCollection();

serviceContainer.AddKeyedTransient<IMyCloneable<Character>, Character>("Character", (_, _) => new Character() { Name = "Merchant", Health = 50 });
serviceContainer.AddKeyedTransient<IMyCloneable<Character>, Warior>("Warior", (_, _) => new Warior() { Name = "Guard", Health = 100, Strength = 20 });
serviceContainer.AddKeyedTransient<IMyCloneable<Character>, Paladin>("Paladin", (_, _) => new Paladin() { Name = "Paladin", Health = 200, Strength = 10, HolyPower = 20 });

serviceContainer.AddKeyedTransient<ICloneable, Character>("CloneableCharacter", (_, _) => new Character() { Name = "Merchant", Health = 50 });
serviceContainer.AddKeyedTransient<ICloneable, Warior>("CloneableWarior", (_, _) => new Warior() { Name = "Guard", Health = 100, Strength = 20 });
serviceContainer.AddKeyedTransient<ICloneable, Paladin>("CloneablePaladin", (_, _) => new Paladin() { Name = "Paladin", Health = 200, Strength = 10, HolyPower = 20 });

using var servicesProvider = serviceContainer.BuildServiceProvider();

#region IMyClonable Usage
    
var character = servicesProvider.GetKeyedService<IMyCloneable<Character>>("Character");
var warior = servicesProvider.GetKeyedService<IMyCloneable<Character>>("Warior");
var paladin = servicesProvider.GetKeyedService<IMyCloneable<Character>>("Paladin");

CloneWithMyCloneable(character).ToList().ForEach(Console.WriteLine);
CloneWithMyCloneable(warior).ToList().ForEach(Console.WriteLine);
CloneWithMyCloneable(paladin).ToList().ForEach(Console.WriteLine);

#endregion

Console.WriteLine("--------------------------------------------------");

#region IClonable Usage

var cCharacter = (Character)servicesProvider.GetRequiredKeyedService<ICloneable>("CloneableCharacter");
var cWarior = (Warior)servicesProvider.GetRequiredKeyedService<ICloneable>("CloneableWarior");
var cPaladin = (Paladin)servicesProvider.GetRequiredKeyedService<ICloneable>("CloneablePaladin");

CloneWithCloneable<Character>(cCharacter).ToList().ForEach(Console.WriteLine);
CloneWithCloneable<Warior>(cWarior).ToList().ForEach(Console.WriteLine);
CloneWithCloneable<Paladin>(cPaladin).ToList().ForEach(Console.WriteLine);

#endregion

Console.WriteLine(servicesProvider);

serviceContainer.Clear();

Console.WriteLine(servicesProvider);

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