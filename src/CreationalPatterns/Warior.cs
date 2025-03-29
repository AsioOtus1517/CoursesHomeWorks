namespace CreationalPatterns;

public class Warior : Character
{
    public int Strength { get; set; }

    public void Attack(Character target) 
    {
        if(target.IsAlive) 
        {
            target.Health -= Strength;

            if(target.Health < 0) target.Health = 0;
        }
    }

    public override Warior MyClone() 
    {
        return new Warior() 
        {
            Name = Name,
            Health = Health,
            Strength = Strength
        };
    }

    public override string ToString()
    {
        return $"{Name} the Warior has {Health}HP and {Strength} Strength";
    }
}