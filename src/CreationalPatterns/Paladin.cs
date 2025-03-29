namespace CreationalPatterns;

public class Paladin : Warior
{
    public int HolyPower { get; set; } = 10;

    public void Heal(Character target) 
    {
        var spellCost = 1;
        if(HolyPower - spellCost >= 0) 
        {
            HolyPower -= spellCost;
            target.Health += 10;
        }
    }

    public override Paladin MyClone() 
    {
        return new Paladin() {
            Name = Name,
            Health = Health,
            HolyPower = HolyPower,
            Strength = Strength
        };
    }

    public override string ToString()
    {
        return $"{Name} the Paladin has {Health}HP, {HolyPower} of HolyPower and {Strength} strength";
    }
}