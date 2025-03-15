public class F : IEquatable<F>
{
    public int i1 { get; set; }
    public int i2  { get; set; }
    public int i3  { get; set; }
    public int i4  { get; set; }
    public int i5  { get; set; }

    public static F Get()
    {
        return new F() { i1 = 1, i2 = 2, i3 = 3, i4 = 4, i5 = 5 };
    }

    public bool Equals(F other)
    {
        if(other == null) return false;

        if(i1 != other.i1) return false;
        if(i2 != other.i2) return false;
        if(i3 != other.i3) return false;
        if(i4 != other.i4) return false;
        if(i5 != other.i5) return false;

        return true;
    }
}