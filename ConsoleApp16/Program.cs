using System;
using System.Collections.Generic;
using System.Linq;

public class PhoneBookEntry
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
}

public class PhoneBook
{
    private List<PhoneBookEntry> entries = new List<PhoneBookEntry>();

    public void AddEntry(string firstName, string lastName, string phoneNumber)
    {
        // Validate input parameters
        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(phoneNumber))
        {
            throw new ArgumentException("First name, last name, and phone number cannot be empty or null.");
        }

        // Validate phone number format (a simple check for illustration purposes)
        if (!phoneNumber.All(char.IsDigit))
        {
            throw new ArgumentException("Phone number can only contain digits.");
        }

        // Check if the phone number already exists
        if (entries.Any(e => e.PhoneNumber == phoneNumber))
        {
            throw new InvalidOperationException("Phone number already exists in the phone book.");
        }

        // Add the entry
        entries.Add(new PhoneBookEntry
        {
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = phoneNumber
        });
    }

    public List<PhoneBookEntry> Search(string query)
    {
        return entries.Where(entry =>
            entry.FirstName.ToLower().Contains(query.ToLower()) ||
            entry.LastName.ToLower().Contains(query.ToLower()) ||
            entry.PhoneNumber.Contains(query)).ToList();
    }
}

class Program
{
    static void Main(string[] args)
    {
        PhoneBook phoneBook = new PhoneBook();

        try
        {
            // Add entries with possible exceptions
            phoneBook.AddEntry("John", "Doe", "123-456-7890");
            phoneBook.AddEntry("Jane", "Smith", "234-567-8901");
            phoneBook.AddEntry("Alice", "Johnson", "345-678-9012");
            // Trying to add an entry with an existing phone number will throw an exception
            // phoneBook.AddEntry("Bob", "Brown", "123-456-7890");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

        // Search by first name, last name, or phone number
        Console.Write("Enter first/last name or phone number to search: ");
        string query = Console.ReadLine();

        List<PhoneBookEntry> results = phoneBook.Search(query);

        if (results.Count == 0)
        {
            Console.WriteLine("No matching entries found.");
        }
        else
        {
            Console.WriteLine("Matching entries:");
            foreach (var entry in results)
            {
                Console.WriteLine($"First Name: {entry.FirstName}, Last Name: {entry.LastName}, Phone Number: {entry.PhoneNumber}");
            }
        }
    }
}

