namespace Lab2;

public class GoldCustomer : Customer
{
    public GoldCustomer(string name, string pass) : base(name, pass)
    {
    }

    public override double PriceTotal()
    {
        double total = 0;

        foreach (var product in Cart)
        {
            total += product.Price;
        }
        total -= total * 0.15;

        return Math.Round(total, 2);
    }
}