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
                inputPanel:
                Help.Print("1-Admin\n2-Customer\n3-Exit");
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
                                Help.Print("Login is successfull", ConsoleColor.Green);
                                Thread.Sleep(1000);
                                Console.Clear();
                                goto inputFunction;
                            }
                        case 2:
                            #region Customer
                            if (pharmacies.Count == 0)
                            {
                                Help.Print("Pharmacy doesn't Exist", ConsoleColor.Red);
                                goto inputPanel;
                            }
                            Help.Print("List of Pharmacies", ConsoleColor.Yellow);
                            int counter = 0;
                            foreach (var item in pharmacies)
                            {
                                Help.Print(item.ToString(), ConsoleColor.Green);
                                if (item._drugTypes.Count != 0)
                                {
                                    counter++;
                                }
                            }
                            if (counter == 0)
                            {
                                Help.Print("There isn't drug in any pharmacy", ConsoleColor.DarkRed);
                                break;
                            }
                            Help.Print("Enter The Name of Pharmacy", ConsoleColor.Yellow);
                            goto inputFunction1;
                            #endregion
                        default:
                            break;
                    }
                    break;
                }
                Help.Print("Incorrect Number!!!", ConsoleColor.DarkRed);
                goto inputPanel;
                #region AdminPanel
            inputFunction:
                while (true)
                {
                    Help.Typing("1-Create Pharmacy \n2-Create Drug Type\n3-Add Drug\n4-Remove\n5-exit");
                    check = Help.Parse();
                    if (check >= 1 && check <= 5)
                    {
                        if (check == 5)
                        {
                            goto inputPanel;
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
                                inputTypeName:
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
                                    goto inputTypeName;
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

                            #region RemoveDrug
                            case 4:
                                if (pharmacies.Count == 0)
                                {
                                    Help.Print("Pharmacy doesn't Exist", ConsoleColor.Red);
                                    Help.Print("Create Pharmacy!", ConsoleColor.Yellow);
                                    goto case 1;
                                }
                            inputPharmacyName3:
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
                                    goto inputPharmacyName3;
                                }
                                if (existPharmacy._drugTypes.Count == 0)
                                {
                                    Help.Print("Drug Type doesn't Exist", ConsoleColor.Red);
                                    Help.Print("Create Type!", ConsoleColor.Yellow);
                                    goto case 2;
                                }
                                foreach (var item in existPharmacy._drugTypes)
                                {
                                    Help.Print(item.ToString(),ConsoleColor.Yellow);
                                    if (item.ShowDrugItems() == null)
                                    {
                                        Help.Print("There isn't Drug", ConsoleColor.DarkRed);
                                        continue;
                                    }
                                    foreach (var item1 in item.ShowDrugItems())
                                    {
                                        Help.Print($"{item1.Id}.{item1.Name}", ConsoleColor.Cyan);
                                    }
                                }
                                Help.Print("Enter The Type of drug you want to remove",ConsoleColor.Yellow);
                                inputTypeName1:
                                drugTypeName = Console.ReadLine();
                                Console.Clear();
                                Help.Blink();
                                existType = existPharmacy._drugTypes.Find(x => x.Name.ToLower() == drugTypeName.ToLower());
                                if (existType == null)
                                {
                                    Help.Print("Choose Correct Type", ConsoleColor.Red);
                                    goto inputTypeName1;
                                }
                                Help.Print("Enter The ID you want to remove",ConsoleColor.Yellow);
                                int id = Help.Parse();
                                if (!existType.RemoveStudent(id))
                                {
                                    Help.Print("There isn't Drug with this ID");
                                    break;
                                }
                                Help.Print("Removing is Successfull",ConsoleColor.Green);
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
            inputFunction1:
                Help.Print("Enter The Name of Pharmacy", ConsoleColor.Yellow);
                while (true)
                {
                #region TryPharmacy

                inputPharmacyName2:
                    string pharmacyName = Console.ReadLine();
                    Pharmacy existPharmacy = pharmacies.Find(x => x.Name.ToLower() == pharmacyName.ToLower());
                    if (existPharmacy == null)
                    {
                        Help.Print("Choose Correct Pharmacy Name", ConsoleColor.Red);
                        goto inputPharmacyName2;
                    }
                    if (existPharmacy._drugTypes.Count == 0)
                    {
                        Help.Print("There isn't Drug in Pharmacy", ConsoleColor.DarkRed);
                        Help.Print("Choose another Pharmacy", ConsoleColor.DarkRed);
                        foreach (var item in pharmacies)
                        {
                            Help.Print(item.ToString(), ConsoleColor.Green);
                        }
                        goto inputPharmacyName2;
                    }
                    #endregion
                    inputFunction21:
                    Help.Typing("1-Information about drugs \n2-Show All Drugs\n3-Buy Drug\n4-exit");
                    check = Help.Parse();
                    if (check >= 1 && check <= 4)
                    {
                        if (check == 4)
                        {
                            goto inputPanel;
                        }
                        switch (check)
                        {
                                #region InfoDrug
                            case 1:

                                foreach (var item in existPharmacy._drugTypes)
                                {
                                    Help.Print(item.ToString(), ConsoleColor.Yellow);
                                    if (item.ShowDrugItems() == null)
                                    {
                                        Help.Print("There isn't Drug", ConsoleColor.DarkRed);
                                        continue;
                                    }
                                    foreach (var item1 in item.ShowDrugItems())
                                    {
                                        Help.Print($"{item1.Id}.{item1.Name}", ConsoleColor.Cyan);
                                    }
                                }
                                Help.Print("Enter The Type of drug you want to remove", ConsoleColor.Yellow);
                            inputTypeName1:
                                string drugTypeName = Console.ReadLine();
                                Console.Clear();
                                Help.Blink();
                                DrugType existType = existPharmacy._drugTypes.Find(x => x.Name.ToLower() == drugTypeName.ToLower());
                                if (existType == null)
                                {
                                    Help.Print("Choose Correct Type", ConsoleColor.Red);
                                    goto inputTypeName1;
                                }
                                Help.Print("Enter Drug Name",ConsoleColor.Yellow);
                                string name = Console.ReadLine();
                                if (existType.InfoDrug(name)==null)
                                {
                                    Help.Print("There isn't this drug",ConsoleColor.DarkRed);
                                }
                                Drug drug = existType.InfoDrug(name);
                                Help.Print(drug.ToString(),ConsoleColor.Green);
                                goto inputFunction21;
                            #endregion

                                #region ShowDrugs
                            case 2:
                                Console.Clear();
                                foreach (var item in existPharmacy._drugTypes)
                                {
                                    Help.Typing(item.ToString(), ConsoleColor.Yellow);
                                    if (item.ShowDrugItems() == null)
                                    {
                                        Help.Print("There isn't Drug in Pharmacy", ConsoleColor.DarkRed);
                                        break;
                                    }
                                    foreach (var item1 in item.ShowDrugItems())
                                    {
                                        Help.Typing($"{item1.Id}.{item1.Name}", ConsoleColor.Cyan);
                                    }
                                }
                                goto inputFunction21;
                            #endregion

                                #region SaleDrug
                            case 3:
                                foreach (var item in existPharmacy._drugTypes)
                                {
                                    Help.Print(item.ToString(), ConsoleColor.Yellow);
                                    if (item.ShowDrugItems() == null)
                                    {
                                        Help.Print("There isn't Drug", ConsoleColor.DarkRed);
                                        continue;
                                    }
                                    foreach (var item1 in item.ShowDrugItems())
                                    {
                                        Help.Print($"{item1.Id}.{item1.Name}", ConsoleColor.Cyan);
                                    }
                                }
                                Help.Print("Enter The Type of drug you want to remove", ConsoleColor.Yellow);
                            inputTypeName2:
                                drugTypeName = Console.ReadLine();
                                Console.Clear();
                                Help.Blink();
                                existType = existPharmacy._drugTypes.Find(x => x.Name.ToLower() == drugTypeName.ToLower());
                                if (existType == null)
                                {
                                    Help.Print("Choose Correct Type", ConsoleColor.Red);
                                    goto inputTypeName2;
                                }
                                Help.Print("Enter Your Cash", ConsoleColor.Yellow);
                                int cash = Help.Parse();
                            inputDrugName2:
                                Help.Print("Enter the Name of Drug", ConsoleColor.Yellow);
                                name = Console.ReadLine();
                            inputDrugAmount:
                                Help.Print("Enter the Amount of Drug", ConsoleColor.Yellow);
                                int count = Help.Parse();
                                Help.Blink();
                                Drug findDrug = existType.SaleDrug(name, count, cash);
                                if (findDrug == null || findDrug.Count == 0)
                                {
                                    Help.Print("This drug doesn't exist", ConsoleColor.DarkRed);
                                    Help.Print("Do you want to buy another drug?yes/no", ConsoleColor.Yellow);
                                    string ans = Console.ReadLine();
                                    if (ans.ToLower() == "yes")
                                    {
                                        existType.ShowDrugItems();
                                        goto inputDrugName2;
                                    }
                                    if (ans.ToLower() == "no")
                                    {
                                        break;
                                    }
                                    Help.Print("Enter The correct answer!!!", ConsoleColor.DarkRed);
                                }
                                if (findDrug.Count < count)
                                {
                                    Help.Print($"We have just {findDrug.Count} {findDrug.Name}", ConsoleColor.DarkRed);
                                    goto inputDrugAmount;
                                }
                                if (cash < findDrug.Price * count)
                                {
                                    Help.Print($"Total Amount: {findDrug.Price * count}\nYour Cash isn't Enough", ConsoleColor.DarkRed);
                                    if (cash < findDrug.Price)
                                    {
                                        Help.Print("You can't buy this drug:(", ConsoleColor.DarkRed);
                                        break;
                                    }
                                    goto inputDrugAmount;
                                }
                                findDrug.Count -= count;
                                Help.Print("Buying is Succesfull!", ConsoleColor.Green);
                                goto inputFunction21;
                            #endregion
                            default:
                                break;
                        }
                    }
                    else
                    {
                        Help.Print("Incorrect Number!!!", ConsoleColor.DarkRed);
                        goto inputFunction1;
                    }
                }
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
