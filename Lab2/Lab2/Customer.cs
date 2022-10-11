namespace Lab2;

public class Customer
{
    private string _name;
    public string Name
    {
        get { return _name;}
    }

    private string _pass;
    public string Pass
    {
        get { return _pass; }
        set { _pass = value; }
    }

    private List<Product> _cart;
    public List<Product> Cart 
    { 
        get { return _cart; }
        set { _cart = value; }
    }

    public Customer(string name, string pass)
    {
        _name = name;
        _pass = pass;
        _cart = new List<Product>();
    }

    // Metoder:

    public override string ToString()
    {
        return $"Namn: {Name} \nLösenord: {Pass} \nI Kundvagnen:{ShowCart()}";
    }

    public bool AddToCart(string inputName, List<Product> stock)
    {
        foreach (var product in stock)
        {
            if (product.Name.ToLower() == inputName)
            {
                Cart.Add(product);
                return true;
            }
        }
        return false;
    }

    public string ShowCart()
    {
        var products = Cart.Distinct();
        var result = "";

        foreach (var product in products)
        {
            var amount = Cart.Count(prod => prod == product);
            var amountCost = amount * product.Price;

            result += $"{product}\nAntal: {amount} \nTotalt: {amountCost} SEK\n";
        }
        result += $"\nTotal kostnad: {PriceTotal()} SEK\n";

        return result;
    }

    public virtual double PriceTotal()
    {
        double total = 0;

        foreach (var product in Cart)
        {
            total += product.Price;
        }
        return Math.Round(total, 2);
    }

    public bool VerifyPassword(string inputPass)
    {
        if (inputPass == Pass)
        {
            return true;
        }
        return false;
    }
}