using Lab2;

// Exempeldata:

List<Product> stock = new List<Product>();
stock.AddRange(new[]
{
    new Product ("Svärd", 59.90),
    new Product ("Sköld", 45.50),
    new Product ("Pilbåge", 59.90)
});

List<Customer> customers = new List<Customer>();
customers.AddRange(new Customer[]
{
    new GoldCustomer ("Knatte", "123") {Cart = {stock[1], stock[2]}},
    new SilverCustomer ("Fnatte", "321") {Cart = {stock[1], stock[1]}},
    new BronzeCustomer ("Tjatte", "213") {Cart = {stock[0], stock[2]}}
});

// Slut på exempeldata 

string input = null;
string inputName;
string inputPass;
Customer loggedInCustomer = null;

while (true)
{
    if (loggedInCustomer != null)
    {
        LoggedInMenu();
    }
    else
    {
        StartMenu();
    }
}

// Metoder:

void StartMenu()
{
    Console.WriteLine("Välkommen till butiken!");
    Console.WriteLine("Vad vill du göra?");
    Console.WriteLine("L - Logga in");
    Console.WriteLine("R - Registrera ny kund");
    input = Console.ReadLine().ToLower();

    switch (input)
    {
        // Logga in
        case "l":
            LogIn();
            break;

        // Registrera ny kund
        case "r":
            AddCustomer();
            break;

        default:
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Det är inte ett alternativ. Försök igen.");
            Console.ForegroundColor = ConsoleColor.White;
            break;
    }
}

void LoggedInMenu()
{
    while (input != "u")
    {
        Console.Clear();
        Console.WriteLine($"Du är inloggad som: {loggedInCustomer.Name}");
        Console.WriteLine("Vad vill du göra?");
        Console.WriteLine("H - Handla");
        Console.WriteLine("S - Se kundvagn");
        Console.WriteLine("G - Gå till kassan");
        Console.WriteLine("U - Logga ut");
        input = Console.ReadLine().ToLower();

        switch (input)
        {
            // Handla
            case "h": 
                Console.Clear();
                foreach (Product product in stock)
                {
                    Console.WriteLine(product);
                }

                Console.WriteLine("\nSkriv varans namn för att lägga till i kundvagn");
                Console.WriteLine("T - tillbaka");
                input = Console.ReadLine().ToLower();

                while (input != "t")
                {
                    var result = loggedInCustomer.AddToCart(input, stock);
                    if (result)
                    {
                        Console.WriteLine($"{input} är tillagd. Vill du lägga till något mer?");
                        input = Console.ReadLine().ToLower();
                    }
                    else
                    {
                        Console.WriteLine($"{input} finns inte i vårt sortiment. Försök igen. Kanske du stavade fel?");
                        input = Console.ReadLine().ToLower();
                    }
                }
                break;

            // Se kundvagn
            case "s":
                while (input != "t")
                {
                    Console.Clear();
                    Console.WriteLine(loggedInCustomer.ShowCart());
                    Console.WriteLine("T - Tillbaka");
                    input = Console.ReadLine().ToLower();
                }
                break;

            // Gå till kassa
            case "g": 
                Console.Clear();
                var total = loggedInCustomer.PriceTotal();
                Console.WriteLine($"Det blir totalt {total} SEK");
                Console.WriteLine("B - Betala");
                Console.WriteLine("T - Tillbaka");
                input = Console.ReadLine().ToLower();

                while (input != "t")
                {
                    if (input == "b")
                    {
                        loggedInCustomer.Cart.Clear();
                        Console.WriteLine("Tack för ditt köp!");
                        Console.WriteLine("T - Tillbaka");
                        input = Console.ReadLine().ToLower();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Det är inte ett alternativ. Försök igen.");
                        Console.ForegroundColor = ConsoleColor.White;
                        input = Console.ReadLine();
                    }
                }
                break;

            // Logga ut
            case "u": 
                Console.Clear();
                inputName = null;
                inputPass = null;
                loggedInCustomer = null;
                break;

            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Det är inte ett alternativ. Försök igen.");
                Console.ForegroundColor = ConsoleColor.White;
                input = Console.ReadLine();
                break;
        }
    }
}

void LogIn()
{
    Console.Clear();
    Console.WriteLine("Logga in");

    while (loggedInCustomer == null)
    {
        Console.WriteLine("Namn:");
        inputName = Console.ReadLine();

       var result = customers.Any(cust => cust.Name == inputName);
       if (result)
       {
           var customer = customers.SingleOrDefault(cust => cust.Name == inputName);
           Console.WriteLine("Lösenord:");
           inputPass = Console.ReadLine();

           if (customer.VerifyPassword(inputPass))
           {
               loggedInCustomer = customer;
           }
           else
           {
               Console.WriteLine("Fel lösenord. Försök igen.");
           }
       }
       else
       {
           Console.WriteLine("Namnet finns inte. Vill du registrera en ny användare?");
           Console.WriteLine("J - Ja");
           Console.WriteLine("N - Nej");
           input = Console.ReadLine().ToLower();
           if (input == "j")
           {
               AddCustomer();
           }
           else if (input == "n")
           {
               Console.WriteLine("Namnet finns inte. Försök igen.");
           }
           else
           {
               Console.ForegroundColor = ConsoleColor.Red;
               Console.WriteLine("Det är inte ett alternativ. Försök igen.");
               Console.ForegroundColor = ConsoleColor.White;
           }
       }
    }
}

void AddCustomer()
{
    Console.Clear();
    Console.WriteLine("Registrera ny kund");
    while (loggedInCustomer == null)
    {
        Console.WriteLine("Namn:");
        inputName = Console.ReadLine();

        var result = customers.Any(cust => cust.Name == inputName);
        if (result)
        {
            Console.WriteLine("Namnet är upptaget. Försök igen.");
        }
        else
        {
            Console.WriteLine("Lösenord:");
            inputPass = Console.ReadLine();
            customers.Add(new Customer(inputName, inputPass));
            loggedInCustomer = customers.Last();
        }
    }
}
