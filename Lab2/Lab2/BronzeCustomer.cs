namespace Lab2;

public class BronzeCustomer : Customer
{
    public BronzeCustomer(string name, string pass) : base(name, pass)
    {
    }

    public override double PriceTotal()
    {
        double total = 0;

        foreach (var product in Cart)
        {
            total += product.Price;
        }
        total -= total * 0.05;

        return Math.Round(total, 2);

    }
}