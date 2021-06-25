using System;
using System.Collections.Generic;

namespace ParkingGarage
{
    class Program
    {
        static void Main(string[] args)
        {
            ParkingGarage parkingGarage = new ParkingGarage(50, 10);
            while (true)
            {
                Console.WriteLine("What would you like to do? You can say 'take ticket', 'pay for parking', 'check my ticket', 'leave garage' or 'quit': ");
                string ask = Console.ReadLine().ToLower();
                if (ask == "quit")
                {
                    break;
                } else if (ask == "pay for parking")
                {
                    parkingGarage.payForParking();
                } else if (ask == "check my ticket")
                {
                    parkingGarage.checkTicket();
                } else if (ask == "take ticket")
                {
                    parkingGarage.takeTicket();
                } else if (ask == "leave garage")
                {
                    parkingGarage.leaveGarage();
                }
            }
        }
    }

    public class ParkingGarage
    {
        int parkingSpaces;
        double costPerHour;
        int availableTickets;
        int availableSpaces;
        IDictionary<int, string> allTickets = new Dictionary<int, string>();
        List<int> activeTickets = new List<int>();

        public ParkingGarage(int parkingSpaces, double costPerHour)
        {
            this.parkingSpaces = parkingSpaces;
            this.costPerHour = costPerHour;
            this.availableTickets = parkingSpaces;
            this.availableSpaces = parkingSpaces;
        }

        public void takeTicket()
        {
            int newTicketNum;
            if (this.availableTickets >= 1)
            {
                newTicketNum = this.allTickets.Count + 1;
                this.allTickets.Add(newTicketNum, "Unpaid");
                this.activeTickets.Add(newTicketNum);
                this.availableTickets -= 1;
                this.availableSpaces -= 1;
                Console.WriteLine("Ticket printed");
                Console.WriteLine("Your ticket numer is: " + newTicketNum);
            } else
            {
                Console.WriteLine("There aren't any spot available");
            }
        }

        public int checkTicket()
        {
            while (true)
            {
                Console.WriteLine("Enter your ticket number: ");
                int currentTicket = int.Parse(Console.ReadLine());
                if (activeTickets.Contains(currentTicket)) {
                    if (this.allTickets[currentTicket] == "Paid")
                    {
                        Console.WriteLine("Your ticket is paid");
                        return currentTicket;
                    } else
                    {
                        Console.WriteLine("How much time did you spend in the garage?");
                        double time = double.Parse(Console.ReadLine());
                        double total = (time / 60) * this.costPerHour;
                        Console.WriteLine("Your total for " + time + " minutes is $" + String.Format("{0:0.00}", total));
                        return currentTicket;
                    }
                } else if (this.allTickets.Keys.Contains(currentTicket))
                {
                    Console.WriteLine("Your ticket is not active and is " + this.allTickets[currentTicket]);
                    return currentTicket;
                } else
                {
                    Console.WriteLine("Please enter a valid ticket number");
                    return 0;
                }
            }
        }

        public void payForParking()
        {
            int currentTicket = checkTicket();
            if (!this.activeTickets.Contains(currentTicket))
            {
                return;
            } else
            {
                Console.WriteLine("Would you like to pay? y/n");
                string pay = Console.ReadLine().ToLower();
                if (pay == "y" || pay == "yes")
                {
                    Console.WriteLine("Processing your payment...");
                    Console.WriteLine("Success!");
                    Console.WriteLine("Thank you for your payment!");
                    this.allTickets[currentTicket] = "Paid";
                }
            }
        }

        public void leaveGarage()
        {
            if (this.activeTickets.Count == 0)
            {
                Console.WriteLine("You have not taken a ticket yet");
                return;
            } else
            {
                Console.WriteLine("Please enter your ticket number:");
                int currentTicket = int.Parse(Console.ReadLine());
                if (!this.activeTickets.Contains(currentTicket))
                {
                    Console.WriteLine("That is not a valid ticket number");
                    return;
                } else if (this.allTickets[currentTicket] == "Paid")
                {
                    Console.WriteLine("Thank you, have a nice day");
                    this.availableSpaces += 1;
                    this.availableTickets += 1;
                    this.activeTickets.Remove(currentTicket);
                    return;
                } else
                {
                    Console.WriteLine("You have an unpaid balance. Please select the 'Pay for Parking' option and try again");
                    return;
                }
            }
        }

    }
}
