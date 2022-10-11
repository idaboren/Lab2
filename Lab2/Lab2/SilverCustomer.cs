namespace Lab2;

public class SilverCustomer : Customer
{
    public SilverCustomer(string name, string pass) : base(name, pass)
    {
    }
    public override double PriceTotal()
    {
        double total = 0;

        foreach (var product in Cart)
        {
            total += product.Price;
        }
        total -= total * 0.10;

        return Math.Round(total, 2);
    }
}