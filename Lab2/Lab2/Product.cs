namespace Lab2;

public class Product
{
    private string _name;
    private float _price;

    public string Name
    {
        get { return _name; }
        set { _name = value;  }
    }
    public float Price
    {
        get { return _price; }
        set { _price = value; }
    }

    public Product(string name, float price)
    {
        _name = name;
        _price = price;
    }

    // Metoder:

    public override string ToString()
    {
        return $"\nVara: {Name} \nStyckpris: {Price} SEK";
    }
}