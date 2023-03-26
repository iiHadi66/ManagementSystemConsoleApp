using ManagementSystemProject.Contexts;
using ManagementSystemProject.Migrations;
using ManagementSystemProject.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystemProject;

internal class Menu
{
    public void MainMenu()
    {
        Console.Clear();
        Console.WriteLine("\n ¤#¤#¤#¤#¤ HUVUD MENY ¤#¤#¤#¤#¤");
        Console.WriteLine("\n 1. Skapa ett ärende");
        Console.WriteLine(" 2. visa alla ärender");
        Console.WriteLine(" 3. Visa specifik ärende");
        Console.WriteLine(" 4. Ändra status på ärendet");
        Console.WriteLine("\n Ange ett av följande alternativ 1-4");
        var option = Console.ReadLine();

        switch (option)
        {
            case "1":
                CreatMatter();
                break;

            case "2":
                 ShowAllMatters();
                break;

            case "3":
                 ShowSpecificMatter();
                break;

                case "4":
                ChangeStatus();
                break;
        }
        Console.ReadKey();

    }

    public void CreatMatter()
    {
        using (var context = new DataContext())
        {
            Console.Clear();
            Console.WriteLine("\n ¤#¤#¤#¤#¤ SKAPA NY ÄRENDE ¤#¤#¤#¤#¤");
            // Be användaren om information om kunden och ärendet
            Console.Write("\nAnge förnamn: ");
            var firstName = Console.ReadLine();

            Console.Write("\nAnge efternamn: ");
            var lastName = Console.ReadLine();

            Console.Write("\nAnge e-post: ");
            var email = Console.ReadLine();

            Console.Write("\nAnge telefonnummer: ");
            var phoneNumber = Console.ReadLine();

            Console.Write("\nBeskriv ärendet: ");
            var description = Console.ReadLine();

            var time = DateTime.Now;
            var status = "Ej påbörjad";

            // Skapa kunden och ärendet och lägg till dem i databaskontexten
            var customer = new CustomerEntity
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber
            };

            var matter = new MatterEntity
            {
                Customer = customer,
                Description = description,
                Time = time,
                Status = status
            };

            context.Customers.Add(customer);
            context.Matters.Add(matter);

            // Spara ändringarna till databasen
            context.SaveChanges();
            Console.WriteLine("\n Felanmälan skapad!");
            Console.WriteLine("\n  ¤#¤#¤#¤#¤ ¤#¤#¤#¤#¤ ¤#¤#¤#¤#¤ ¤#¤#¤#¤#¤");
        }
    }

    public void ShowAllMatters()
    {
        using (var context = new DataContext())
        {
            var matter = context.Matters.Include(e => e.Customer).ToList();

            Console.Clear();
            Console.WriteLine("\n ¤#¤#¤#¤#¤ ALLA ÄRENDER ¤#¤#¤#¤#¤");

             foreach (var matterItem in matter)

            Console.WriteLine($"\n Ärende Nummer:{matterItem.MatterID} | Kund:{matterItem.Customer.FirstName} {matterItem.Customer.LastName} | Beskrivning:{matterItem.Description}. | Status:{matterItem.Status}");
            Console.WriteLine("\n ¤#¤#¤#¤#¤ ¤#¤#¤#¤#¤ ¤#¤#¤#¤#¤ ¤#¤#¤#¤#¤");
        }

            

    }

    public void ShowSpecificMatter()
    {
        using (var context = new DataContext())
        {

             ShowAllMatters();

            // Be användaren om ID:t för ärendet som ska visas
            Console.Write("\nAnge ID för ärendet: ");
            // Hämta ärendet med det angivna ID:t från databaskontexten
            var id = int.Parse(Console.ReadLine());
            var matterById = context.Matters.Include(e => e.Customer).FirstOrDefault(e => e.MatterID == id);


            // Skriv ut informationen om ärendet till konsolen
            if (matterById != null)
            {
                Console.Clear();
                Console.WriteLine("\n ¤#¤#¤#¤#¤  ÄRENDE INFORMATION ¤#¤#¤#¤#¤");
                Console.WriteLine($"  ID: {matterById.MatterID}");
                Console.WriteLine($"  Kund: {matterById.Customer.FirstName} {matterById.Customer.LastName}");
                Console.WriteLine($"  E-post: {matterById.Customer.Email}");
                Console.WriteLine($"  Telefonnummer: {matterById.Customer.PhoneNumber}");
                Console.WriteLine($"  Beskrivning: {matterById.Description}");
                Console.WriteLine($"  Skapat: {matterById.Time}");
                Console.WriteLine($"  Status: {matterById.Status}");
                Console.WriteLine("\n ¤#¤#¤#¤#¤ ¤#¤#¤#¤#¤ ¤#¤#¤#¤#¤ ¤#¤#¤#¤#¤");
            }
            else
            {
                Console.WriteLine($"Ett ärende med det angivna  ID:t {id} kunde inte hittas.");
            }
        }
    }


    public void ChangeStatus()
    {
        using (var context = new DataContext())
        {
            ShowAllMatters();

        // Be användaren om ID:t för ärendet som ska visas
        Console.Write("\nAnge ID för ärendet du vill ändra statuset: ");
        // Hämta ärendet med det angivna ID:t från databaskontexten
        var id = int.Parse(Console.ReadLine());
        var matterById = context.Matters.FirstOrDefault(e => e.MatterID == id);
            var notStarted = "Ej påbörjad";
            var started = "Påbörjad";
            var finished = "Avslutad";

            // Skriv ut informationen om ärendet till konsolen
            if (matterById != null)
            {
                // Be användaren om den nya statusen för ärendet
                Console.WriteLine("\n ¤#¤#¤#¤#¤    ÄNDRA STATUS    ¤#¤#¤#¤#¤");
                Console.Write($"Vilken status vill du sätta för ärendet? ({notStarted}, {started}, {finished}):");
                Console.WriteLine("\n ¤#¤#¤#¤#¤ ¤#¤#¤#¤#¤ ¤#¤#¤#¤#¤ ¤#¤#¤#¤#¤");
                var newStatus = Console.ReadLine();
                // Kontrollera om användaren matar in ett giltigt statusvärde
                if (newStatus == notStarted || newStatus == started || newStatus == finished)
                {
                // Uppdatera ärendet med den nya statusen
                matterById.Status = newStatus;
                // Spara ändringarna till databasen
                context.SaveChanges();

                Console.WriteLine($"Ärendet med ID {id} har uppdaterats till statusen {newStatus}.");

                }
                else
                {
                    Console.WriteLine("Felaktigt statusvärde angivet!");
                }

            }
            else
            {
                Console.WriteLine($"Ett ärende med det angivna  ID:t {id} kunde inte hittas.");
            }

        }
  }
}
