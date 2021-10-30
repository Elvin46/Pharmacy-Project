using PharmacyProject2.Helper;
using PharmacyProject2.Models;
using System;
using System.Collections.Generic;
using System.Threading;

namespace PharmacyProject2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Pharmacy> pharmacies = new List<Pharmacy>();
            while (true)
            {
                Help.Print("1-Admin\n2-Customer\n3-Exit");
                inputPanel:
                int check = Help.Parse();
                if (check >= 1 && check <= 3)
                {
                    Help.Blink();
                    if (check==3)
                    {
                        break;
                    }
                    switch (check)
                    {
                        case 1:
                            #region LoginAttempt

                            string username, password;
                            int ctr = 0;
                            do
                            {
                                Console.Write("Input a username: ");
                                username = Console.ReadLine();

                                Console.Write("Input a password: ");
                                password = Console.ReadLine();
                                if (username.Trim() != "Admin" || password.Trim() != "Admin123")
                                {
                                    Help.Print("Incorrect Username or Password!",ConsoleColor.DarkRed);
                                    ctr++;
                                }
                                else
                                    ctr = 1;

                            }
                            while ((username.Trim() != "Admin" || password.Trim() != "Admin123") && (ctr != 3));

                            if (ctr == 3)
                            {
                                Help.Print("\nLogin attempt three or more times. Try later!",ConsoleColor.DarkRed);
                                break;
                            }
                            #endregion
                            else
                            {
                                goto inputFunction;
                            }
                        case 2:

                            
                        default:
                            break;
                    }

                }
                Help.Print("Incorrect Number!!!", ConsoleColor.DarkRed);
                goto inputPanel;
                #region AdminPanel
            inputFunction:
                while (true)
                {
                    Help.Typing("1-Create Pharmacy \n2-Create Drug Type\n3-Add Drug\n4-exit");
                    check = Help.Parse();
                    if (check >= 1 && check <= 4)
                    {
                        if (check == 4)
                        {
                            break;
                        }
                        switch (check)
                        {
                            #region CreatePharmacy
                            case 1:
                                Help.Print("Enter The Name of Pharmacy", ConsoleColor.Yellow);
                                string pharmacyName = Console.ReadLine();
                                Help.Blink();
                                if (pharmacies.Exists(x => x.Name.ToLower() == pharmacyName.ToLower()))
                                {
                                    Help.Print("This Pharmacy is Exist", ConsoleColor.Red);
                                    goto case 1;
                                }
                                Pharmacy pharmacy = new Pharmacy(pharmacyName);
                                pharmacies.Add(pharmacy);
                                Help.Print("Creating is succesfull", ConsoleColor.Green);
                                Thread.Sleep(300);
                                break;
                            #endregion

                            #region CreateDrugType
                            case 2:
                                if (pharmacies.Count == 0)
                                {
                                    Help.Print("Pharmacy doesn't Exist", ConsoleColor.Red);
                                    Help.Print("Create Pharmacy!", ConsoleColor.Yellow);
                                    goto case 1;
                                }
                                Help.Print("Enter Drug Type", ConsoleColor.Yellow);
                                string name = Console.ReadLine();
                                Console.Clear();
                            inputPharmacyName:
                                Help.Typing("List of Pharmacies:\n", ConsoleColor.Yellow);
                                foreach (var item in pharmacies)
                                {
                                    Help.Print(item.ToString(), ConsoleColor.Green);
                                }

                                Help.Print("Enter The Name of Pharmacy", ConsoleColor.Yellow);
                                pharmacyName = Console.ReadLine();
                                Console.Clear();
                                Help.Blink();
                                Pharmacy existPharmacy = pharmacies.Find(x => x.Name.ToLower() == pharmacyName.ToLower());
                                if (existPharmacy == null)
                                {
                                    Help.Print("Choose Correct Pharmacy Name", ConsoleColor.Red);
                                    goto inputPharmacyName;
                                }

                                DrugType drugType = new DrugType(name);
                                if (!existPharmacy.CreateDrugType(drugType))
                                {
                                    Help.Print("This Type is exist", ConsoleColor.DarkRed);
                                    break;
                                }
                                Help.Print($"{drugType.Name} added to {existPharmacy.Name} Pharmacy", ConsoleColor.Green);
                                break;
                            #endregion

                            #region AddDrug
                            case 3:
                                if (pharmacies.Count == 0)
                                {
                                    Help.Print("Pharmacy doesn't Exist", ConsoleColor.Red);
                                    Help.Print("Create Pharmacy!", ConsoleColor.Yellow);
                                    goto case 1;
                                }
                            inputPharmacyName2:
                                Help.Typing("List of Pharmacies:\n", ConsoleColor.Yellow);
                                foreach (var item in pharmacies)
                                {
                                    Help.Print(item.ToString(), ConsoleColor.Green);
                                }

                                Help.Print("Enter The Name of Pharmacy", ConsoleColor.Yellow);
                                pharmacyName = Console.ReadLine();
                                Console.Clear();
                                Help.Blink();
                                existPharmacy = pharmacies.Find(x => x.Name.ToLower() == pharmacyName.ToLower());
                                if (existPharmacy == null)
                                {
                                    Help.Print("Choose Correct Pharmacy Name", ConsoleColor.Red);
                                    goto inputPharmacyName2;
                                }
                                Help.Print("Enter The Name of Drug", ConsoleColor.Yellow);
                                name = Console.ReadLine();
                                if (existPharmacy._drugTypes.Count == 0)
                                {
                                    Help.Print("Drug Type doesn't Exist", ConsoleColor.Red);
                                    Help.Print("Create Type!", ConsoleColor.Yellow);
                                    goto case 2;
                                }
                                Help.Typing("List of Types:\n", ConsoleColor.Yellow);
                                foreach (var item in existPharmacy._drugTypes)
                                {
                                    Help.Print(item.ToString(),ConsoleColor.Green);
                                }
                                Help.Print("Enter The Type of Drug", ConsoleColor.Yellow);
                                string drugTypeName = Console.ReadLine();
                                Console.Clear();
                                Help.Blink();
                                DrugType existType = existPharmacy._drugTypes.Find(x => x.Name.ToLower() == drugTypeName.ToLower());
                                if (existType == null)
                                {
                                    Help.Print("Choose Correct Type", ConsoleColor.Red);
                                    goto inputPharmacyName2;
                                }
                                Help.Print("Enter The Price of Drug", ConsoleColor.Yellow);
                                int price = Help.Parse();
                                Help.Print("Enter The Count of Drug", ConsoleColor.Yellow);
                                int count = Help.Parse();
                                Console.Clear();
                            

                                Drug drug = new Drug(name, existType, price, count);
                                if (!existType.AddDrug(drug))
                                {
                                    Help.Print("This Drug is exist", ConsoleColor.DarkRed);
                                    Help.Print($"The Count of {drug.Name} has increased {drug.Count} count", ConsoleColor.Green);
                                    break;
                                }
                                Help.Print($"{drug.Name} added to {existPharmacy.Name} Pharmacy", ConsoleColor.Green);
                                break;
                            #endregion
                            default:
                                break;
                        }

                    }
                    else
                    {
                        Help.Print("Incorrect Number!!!", ConsoleColor.DarkRed);
                        goto inputFunction;
                    }
                }
                #endregion

                #region CustomerPanel

                #endregion

            }
        }
    }
    enum Functions
    {
        CreatePharmacy = 1,
        AddDrug,
        InfoDrug,
        ShowDrugItems,
        SaleDrug,
        Exit
    }
}
