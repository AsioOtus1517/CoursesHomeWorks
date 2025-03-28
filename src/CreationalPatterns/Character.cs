namespace CreationalPatterns;

public class Character : IMyCloneable<Character>, ICloneable
{
    public string Name { get; set; }

    public int Health { get; set; }

    public bool IsAlive => Health > 0;

    public virtual object Clone()
    {
        return MemberwiseClone();
    }

    public virtual Character MyClone()
    {
        return new Character() 
        { 
            Name = Name,
            Health = Health
        };
    }

    public override string ToString()
    {
        return $"{Name} has {Health}HP";
    }
}