using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static banki.Program;

namespace banki
{
    internal class Program
    {
        public struct Customer
        {
            public string Name;
            public int[] Balances;
            public double[] Kamatok;
        }

        static void Main(string[] args)
        {
             string[] firstnames = {
                "László", "Anna", "István", "Zoltán", "Gábor",
                "János", "Mária", "Péter", "Katalin",
                "Tamás", "Béla", "Júlia", "Attila", "Csaba",
                "Ferenc", "Sándor", "Ágnes", "Edit", "Tünde",
                "András", "Balázs", "Lilla", "Levi",
                "Árpád", "Imre", "Miklós", "Réka",
                "Iván", "Gergő", "Róbert", "Ilona",
                "György", "Zsolt", "Orsolya", "Dóra", "Mihály",
                "Vera", "Gyula", "Emese", "Adri",
                "Bence", "Eszter", "Noémi", "Judit", "Ádám", "Nóra"
            };
            string[] months = new string[] { "Január", "Február", "Március", "Április", "Május", "Június", "Július", "Augusztus", "Szeptember", "Október", "November", "December" };

            double interest = 0.01;
            double loan_interest = 28;


            Random random = new Random();

            void customerInit(int CUSTOMER_COUNT, int BALANCE_COUNT, Customer[] customers)
            {
                for (int i = 0; i < CUSTOMER_COUNT; i++)
                {
                    Customer customer = new Customer();
                    customer.Name = firstnames[random.Next(0, firstnames.Length)];
                    customer.Balances = new int[BALANCE_COUNT];
                    customer.Kamatok = new double[BALANCE_COUNT];
                    for (int j = 0; j < BALANCE_COUNT; j++)
                    {
                        customer.Balances[j] = random.Next(-500_000, 1_000_000);
                    }
                    customers[i] = customer;
                }
            }

            void printCustomerTable(int BALANCE_COUNT, Customer[] customers)
            {
                string monthLine = "\t";

                for (int i = 0; i< BALANCE_COUNT;i++)
                {
                    string monthShort = months[i][0] + "" + months[i][1] + ""+ months[i][2];
                    monthLine += monthShort.ToUpper() + "\t";
                } 
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(monthLine);
                
                foreach (Customer customer in customers)
                {
                    string name = customer.Name;
 
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(name);
                    foreach (int balance in customer.Balances)
                    {
                        if(balance > 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("\t" + balance);
                        } else
                        {
                            Console.ForegroundColor= ConsoleColor.Red;
                            Console.Write("\t" + balance);
                        }
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();
                }
            }

            void printCustomerAvgs(Customer[] customers)
            {
                foreach (Customer customer in customers) {
                    double balanceSum = 0;

                    foreach (int balance in customer.Balances)
                    {
                        balanceSum += balance;
                    }

                    double avg = balanceSum / customer.Balances.Length;

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($"{customer.Name}");
                    if (avg >= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("\t" + avg);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("\t" +avg);
                    }
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            void printMonthlyBankBalance(int BALANCE_COUNT, Customer[] customers){
                for (int i = 0; i < BALANCE_COUNT; i++)
                {
                    string month = months[i];
                    int sum = 0;

                    foreach (Customer customer in customers)
                    {
                        sum += customer.Balances[i];
                    }

                    Console.ForegroundColor= ConsoleColor.Yellow;
                    Console.Write(month);
                    
                    if(sum >= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("\t" + sum);
                    } else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("\t" +  sum);
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();

                }
            }            
            
            void printBestMonthForBank(int BALANCE_COUNT, Customer[] customers){
                int bestMonth = 0;
                int bestBalance = int.MaxValue;
                
                for (int i = 0; i < BALANCE_COUNT; i++)
                {
                    int sum = 0;

                    foreach (Customer customer in customers)
                    {
                        sum += customer.Balances[i];
                    }

                   if(sum < bestBalance)
                    {
                        bestMonth = i;
                        bestBalance = sum;
                    }

                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("A legjobb hónap: ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(months[bestMonth]);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\t" + bestBalance);
                Console.WriteLine();
            }

            void yearlyRichestCustomer(Customer[] customers)
            {
                int sum = 0;
                Customer richestCustomer = new Customer();

                foreach (Customer customer in customers)
                {
                    int customerSum = 0;
                    foreach (int balance in customer.Balances)
                    {
                        customerSum += balance;
                    }
                    if(customerSum > sum)
                    {
                        richestCustomer.Name = customer.Name;
                        sum = customerSum;
                    }
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("A leggazdagabb: ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(richestCustomer.Name);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\t" + sum);
                Console.WriteLine();
            }

            void setInterstRates()
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Látraszóló éves kamat:");
                    Console.ForegroundColor = ConsoleColor.Green;
                    interest = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Hitel éves kamat:");
                    Console.ForegroundColor = ConsoleColor.Green;
                    loan_interest = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

                    Console.WriteLine("Értékek sikeresen frissítve!");
                } catch
                {
                    Console.WriteLine("Hiba történt az értékel felvétele közben.");
                }
            }

            void calculateCustomerInterests(Customer[] customers)
            {
                foreach (Customer customer in customers)
                {
                    for (int i = 0; i < customer.Balances.Length; i++)
                    {
                        double pay = 0;
                        double balance = customer.Balances[i];
                        if (balance > 0)
                        {
                            pay = Math.Round(balance * (interest / 12));
                        }
                        else
                        {
                            pay = Math.Round(balance * (loan_interest / 12 / 100));
                        }
                        customer.Kamatok[i] = pay;
                    }
                }
            }

            void printInterestCalculation(int BALANCE_COUNT, Customer[] customers)
            {
                string monthLine = "\t";

                for (int i = 0; i < BALANCE_COUNT; i++)
                {
                    string monthShort = months[i][0] + "" + months[i][1] + "" + months[i][2];
                    monthLine += monthShort.ToUpper() + "\t";
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(monthLine);

                foreach (Customer customer in customers)
                {
                    string name = customer.Name;

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(name);
                    Console.ForegroundColor = ConsoleColor.Green;

                    for(int i = 0; i < customer.Balances.Length; i++)
                    {
                        double pay = 0;
                        double balance = customer.Balances[i];
                        if (balance > 0)
                        {
                            pay = Math.Round(balance * (interest / 12));
                        }
                        else
                        {
                            pay = Math.Round(balance * (loan_interest / 12 / 100));
                        }
                        if (pay >= 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("\t" + pay);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("\t" + pay);
                        }
                        customer.Kamatok[i] = pay;
                    }


                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();
                }
            }

            void printYearlyBankInterestBalance(Customer[] customers)
            {
                calculateCustomerInterests(customers);
                double bank_balance = 0;

                foreach (Customer customer in customers)
                {
                    double customer_balance = 0;
                    foreach (double customer_interest in customer.Kamatok)
                    {
                        customer_balance += customer_interest * (-1);
                    }
                    bank_balance += customer_balance;
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("A bank egyenlege az év végére: ");    
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(bank_balance);

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
            }

            void printCustomerBalance(Customer[] customers)
            {
                foreach (Customer customer in customers)
                {
                    int sum = 0;
                    foreach(int balance in customer.Balances)
                    {
                        sum += balance;
                    }
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(customer.Name);
                    if(sum > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("\t" + sum);
                    } else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("\t" + sum);
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();
                }
            }

            void addNewCustomer(int CUSTOMER_COUNT, int BALANCE_COUNT, Customer[] customers)
            {
                Customer customer = new Customer();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Adjon meg egy nevet");
                Console.ForegroundColor = ConsoleColor.Green;
                customer.Name = Console.ReadLine();
                int[] balances = new int[BALANCE_COUNT];
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Adja meg a havi egyenlegeket:");
                for (int i = 0; i < BALANCE_COUNT; i++)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(months[i] + "\t");
                    Console.ForegroundColor = ConsoleColor.Green;
                    balances[i] = int.Parse(Console.ReadLine());
                }
                customer.Balances = balances;
                customer.Kamatok = new double[BALANCE_COUNT];

                Array.Resize<Customer>(ref customers, customers.Length +1);
                Console.WriteLine(customers.Length);
                customers[customers.Length] = customer;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
            }


            void home()
            {
                Console.Clear();
                menu();
            }

            void menu()
            {
                Console.ForegroundColor= ConsoleColor.Green;
                Console.WriteLine("\r\n\r\n ________  ________  ________   ___  __             _______  ________  ________  ________     \r\n|\\   __  \\|\\   __  \\|\\   ___  \\|\\  \\|\\  \\          /  ___  \\|\\   __  \\|\\   __  \\|\\   __  \\    \r\n\\ \\  \\|\\ /\\ \\  \\|\\  \\ \\  \\\\ \\  \\ \\  \\/  /|_       /__/|_/  /\\ \\  \\|\\  \\ \\  \\|\\  \\ \\  \\|\\  \\   \r\n \\ \\   __  \\ \\   __  \\ \\  \\\\ \\  \\ \\   ___  \\      |__|//  / /\\ \\  \\\\\\  \\ \\  \\\\\\  \\ \\  \\\\\\  \\  \r\n  \\ \\  \\|\\  \\ \\  \\ \\  \\ \\  \\\\ \\  \\ \\  \\\\ \\  \\         /  /_/__\\ \\  \\\\\\  \\ \\  \\\\\\  \\ \\  \\\\\\  \\ \r\n   \\ \\_______\\ \\__\\ \\__\\ \\__\\\\ \\__\\ \\__\\\\ \\__\\       |\\________\\ \\_______\\ \\_______\\ \\_______\\\r\n    \\|_______|\\|__|\\|__|\\|__| \\|__|\\|__| \\|__|        \\|_______|\\|_______|\\|_______|\\|_______|\r\n                                                                                              \r\n                                                                                              \r\n                                                                                              \r\n\r\n");
                Console.ForegroundColor= ConsoleColor.Yellow;
                Console.WriteLine("Hány ügyfél van?");
                Console.ForegroundColor = ConsoleColor.Green;
                int CUSTOMER_COUNT = int.Parse(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Hány havi adat?");
                Console.ForegroundColor = ConsoleColor.Green;
                int input = int.Parse(Console.ReadLine());
                int BALANCE_COUNT = input > 20 ? 12 : input;

                Customer[] customers = new Customer[CUSTOMER_COUNT];

                customerInit(CUSTOMER_COUNT, BALANCE_COUNT, customers);

                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("MENUI");
                    Console.WriteLine("1-Táblázat | 2-Átlag | 3-Havi bontás | 4-Évi leggazdagabb | 5-Legjobb a banknak");
                    Console.WriteLine("| 6 Kamat beállítás | 7 - Ügyfélszámla Kamat kiszámolás | 8 - Kamatból adódó éves banki egyenleg |");
                    Console.WriteLine("9 - Ügyfél kamatterhelés / jóváírás | 10 - Ügyfél hozzáadása");
                    Console.WriteLine("HOME - Vissza | EXIT - Kilépés");
                    Console.ForegroundColor = ConsoleColor.Green;
                    string option = Console.ReadLine();

                    switch (option.ToLower())
                    {
                        case "1":
                            printCustomerTable(BALANCE_COUNT, customers);
                            break;
                        case "2":
                            printCustomerAvgs(customers);
                            break;
                        case "3":
                            printMonthlyBankBalance(BALANCE_COUNT, customers);
                            break;
                        case "4":
                            yearlyRichestCustomer(customers);
                            break;
                        case "5":
                            printBestMonthForBank(BALANCE_COUNT, customers);
                            break;
                        case "6":
                            setInterstRates();
                            break;
                        case "7":
                            printInterestCalculation(BALANCE_COUNT, customers);
                            break;
                        case "8":
                            printYearlyBankInterestBalance(customers);
                            break; 
                        case "9":
                            printCustomerBalance(customers);
                            break;
                        case "10":
                            addNewCustomer(CUSTOMER_COUNT, BALANCE_COUNT, customers);
                            break;
                        case "home":
                            home();
                            break;
                        case "exit":
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Hibás adat!");
                            break;
                    }

                }
            }

            menu();
        
        }
    }
}
