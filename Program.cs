using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace Week1PD
{
    class Program
    {
        static string[] names = new string[50];
        static string[] ids = new string[50];
        static string[] bills = new string[50];
        static void Main(string[] args)
        {
            string name = "";
            string id = "";
            string bill = "";
            while (true)
            {
                int usersCount = 0;
                Console.Clear();
                header();
                readLine(ref usersCount);
                int option = firstMenu();
                if (option == 1)
                {
                    Console.Clear();
                    header();
                    int choice = adminFunction();
                    if (choice == 1)
                    {
                        Console.Clear();
                        header();
                        CityBillsFunction(names, ids, bills, usersCount);
                    }
                    if (choice == 2)
                    {
                        Console.Clear();
                        header();
                        int choice1 = checkAnyBillFunction();
                        if (choice1 == 1)
                        {
                            string nameBill = billByNameFunction();
                            showNameBill(usersCount, nameBill);
                        }
                        
                            if(choice1 == 2)
                        {
                            string idBill = billByIDFunction();
                            showIDBill(usersCount, idBill);
                        }
                    }
                    if (choice == 3)
                    {
                        int y = 0;
                        while (true)
                        {
                            Console.Clear();
                            header();
                            if (y != 0)
                            {
                                Console.WriteLine("\n  (Not Valid Name!)");
                            }
                            Console.Write("  Enter Name of Person: ");
                            name = Console.ReadLine();
                            bool flag = isValidName(name, usersCount);
                            if (flag == true)
                            {
                                break;
                            }
                            y++;
                        }
                        int x = 0;
                        while (true)
                        {
                            Console.Clear();
                            header();
                            Console.WriteLine("  Name of Person: " + name);
                            if (x != 0)
                            {
                                Console.WriteLine("\n  (Not Valid User ID!)");
                            }
                            Console.Write("  Enter Bill ID: ");
                            id = Console.ReadLine();
                            bool flag = isValidID(id, usersCount);
                            if (flag == true)
                            {
                                x = 0;
                                break;
                            }
                            x++;
                            Console.WriteLine("      (Checking ID... )");
                            Thread.Sleep(500);
                        }
                        while (true)
                        {
                            Console.Clear();
                            header();
                            Console.WriteLine("  Name of Person: " + name);
                            Console.WriteLine("  Bill ID of Person: " + id);
                            if (x != 0)
                            {
                                Console.WriteLine("\n  (Enter Correct Bill Amount!)");
                            }
                            Console.Write("  Enter Amount of Bill: ");
                            bill = Console.ReadLine();
                            bool flag = isValidBill(bill, usersCount);
                            if (flag == true)
                            {
                                break;
                            }
                            x++;
                        }
                        
                        addDataFunction(usersCount, name, id, bill);
                    }
                    if (choice == 4)
                    {
                        int choice1 = updateDataFunction();
                        if(choice1 == 1)
                        {
                            updateNameFunction(usersCount);
                        }
                        if(choice1 == 2)
                        {
                            updateBillFunction(usersCount);
                        }
                        else if(choice1 != 1 && choice1 != 2)
                        {
                            Console.WriteLine("Not Valid Input!");
                        }

                    }
                    if (choice == 5)
                    {
                        deleteDataFunction(names, ids, bills, usersCount);
                    }
                }
                if (option == 2 || option == 3)
                {
                    Console.WriteLine("Function Not Created!");
                }
                Console.ReadKey();
            }
        }
        static void header()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("*****************************************************************************************************");
            Console.WriteLine("*****     $                     -(ELECTRIC BILLS MANAGEMENT SYSTEM)-                      $     *****");
            Console.WriteLine("*****************************************************************************************************");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
        }

        static void readLine(ref int usersCount)
        {
            string path = "E:\\Week1PD\\Week1PD\\firstMenu.txt";
            string record = "";
            if (File.Exists(path))
            {
                StreamReader data = new StreamReader(path);
                while ((record = data.ReadLine()) != null)
                {
                    names[usersCount] = parseData(record, 1);
                    ids[usersCount] = parseData(record, 2);
                    bills[usersCount] = parseData(record, 3);
                    usersCount = usersCount + 1;
                    }
                data.Close();
            }
            else
            {
                Console.WriteLine("Not Exists!");
            }
        }
        static bool isValidID(string id, int usersCount)
        {
            bool flag = true;
            bool flag1 = true;
            for(int x = 0; x < id.Length; x++)
            {
                if((id[x] > 57 && id[x] < 64) || (id[x] > 90 && id[x] < 97) || (id[x] > 122) || (id[x] < 46) || (id[x] == 47))
                {
                    flag1 = false;
                    break;
                }
            }
            for (int x = 0; x < usersCount; x++)
            {
                if (id == ids[x])
                {
                    flag = false;
                    break;
                }
            }
            if (flag1 == true && flag == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static bool isValidName(string name, int usersCount)
        {
            bool flag = true;
            for (int x = 0; x < name.Length; x++)
            {
                if ((name[x] < 65) || (name[x] > 90 && name[x] < 97) || (name[x] > 122))
                {
                    flag = false;
                    break;
                }
            }
            return flag;
        }

        static bool isValidBill(string bill, int usersCount)
        {
            bool flag = true;
            bool flag1 = true;
            for (int x = 0; x < bill.Length; x++)
            {
                if (bill[x] < 48 || bill[x] > 57)
                {
                    flag = false;
                    break;
                }
            }
            int bil = int.Parse(bill);

                if (bil < 0)
                {
                    flag1 = false;
                }
            if (flag1 == true && flag == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static int firstMenu()
        {
            Console.WriteLine("  1: Sign In as Admin");
            Console.WriteLine("  2: Sign Up");
            Console.WriteLine("  3: Exit");
            Console.Write(" Enter Your Choice: ");
            int option = int.Parse(Console.ReadLine());
            return option;
        }
        static int adminFunction()
        {
            Console.WriteLine("  1: Check City Bills");
            Console.WriteLine("  2: Check any person's Bill");
            Console.WriteLine("  3: Add any Person's Data");
            Console.WriteLine("  4: Update someone's Data");
            Console.WriteLine("  5: Delete someone's Data");
            Console.Write(" Enter Your Choice: ");
            int option = int.Parse(Console.ReadLine());
            return option;
        }
        static void CityBillsFunction(string[] names, string[] ids, string[] bills, int usersCount)
        {
            Console.WriteLine("\tName\t\t|\t\tBill ID\t\t|\t\tBill Amount\n");
            for (int x = 0; x <= usersCount; x++)
            {
                Console.WriteLine("\t" + names[x] + "\t\t|\t\t" + ids[x] + "\t\t|\t\t" + bills[x]);
            }
        }
        static string parseData(string record, int field)
        {
            int comma = 1;
            string item = "";
            for (int x = 0; x < record.Length; x++)
            {
                if (record[x] == ',')
                {
                    comma++;
                }
                else if (comma == field)
                {
                    item = item + record[x];
                }
            }
            return item;
        }

        static void addDataFunction(int usersCount,string name, string id, string bill)
        {
            string path = "E:\\Week1PD\\Week1PD\\firstMenu.txt";
            StreamWriter f1 = new StreamWriter(path, true);
                f1.WriteLine(name + "," + id + "," + bill);
            f1.Flush();
            f1.Close();
        }
        static int checkAnyBillFunction()
        {
            Console.WriteLine("  1: Check Bill by Name");
            Console.WriteLine("  2: Check Bill by ID");
            Console.Write(" Enter Your Choice: ");
            int option = int.Parse(Console.ReadLine());
            return option;
        }
        static string billByNameFunction()
        {
            Console.Clear();
            header();
            Console.Write("  Enter Name Of Person: ");
            string nameBill = Console.ReadLine();
            return nameBill;
        }

        static void showNameBill(int usersCount, string nameBill)
        {
            Console.Clear();
            header();
            int resultCount = 0;
            Console.WriteLine("\tName\t\t|\t\tBill ID\t\t|\t\tBill Amount");
            for (int x = 0; x < usersCount; x++)
            {
                if(names[x] == nameBill)
                {
                    Console.WriteLine("\t" + names[x] + "\t\t|\t\t" + ids[x] + "\t\t|\t\t" + bills[x]);
                }
                else
                {
                    resultCount++;
                }
                if (resultCount == usersCount)
                {
                    Console.WriteLine(" \n      No Results Found!");
                }
            }
        }
        static string billByIDFunction()
        {
            Console.Clear();
            header();
            Console.Write("Enter Bill ID Of Person: ");
            string idBill = Console.ReadLine();
            return idBill;
        }

        static void showIDBill(int usersCount, string idBill)
        {
            int resultCount = 0;
            Console.WriteLine("Name\t\tBill ID\t\tBill Amount");
            for (int x = 0; x < usersCount; x++)
            {
                if (ids[x] == idBill)
                {
                    Console.WriteLine(names[x] + "\t\t" + ids[x] + "\t\t" + bills[x]);
                }
                else
                {
                    resultCount++;
                }
                if (resultCount == usersCount)
                {
                    Console.WriteLine("       No Results Found!");
                }
            }
        }
        static void deleteDataFunction(string[] names, string[] ids,string[] bills, int usersCount)
        {
            int idCheck = 0;
            int del = -1;
            Console.Clear();
            header();
            Console.Write("  Enter Name of Person: ");
            string name = Console.ReadLine();
            Console.Write("  Enter Bill ID of Person: ");
            string id = Console.ReadLine();
            for(int x = 0; x < usersCount; x++)
            {
                if(name == names[x] && id == ids[x])
                {
                    del = x;
                    break;
                }
                else
                {
                    idCheck++;
                }

            }
            if (idCheck == usersCount)
            {
                Console.WriteLine("       User Not Found!");
            }
            else
            {
                for (int x = del; x < usersCount; x++)
                {
                    names[x] = names[x + 1];
                    ids[x] = ids[x + 1];
                    bills[x] = bills[x + 1];
                    Console.WriteLine(names[x]);
                }
                usersCount--;
                string path = "E:\\Week1PD\\Week1PD\\firstMenu.txt";
                StreamWriter f1 = new StreamWriter(path, false);
                for (int x = 0; x <= usersCount; x++)
                {
                    f1.WriteLine(names[x] + "," + ids[x] + "," + bills[x]);
                }
                    f1.Flush();
                    f1.Close();
                
            }
        }
        static int updateDataFunction()
        {
            Console.Clear();
            header();
            Console.WriteLine("  1: Update Name of a Person");
            Console.WriteLine("  2: Update Amount of Bill of a Person");
            Console.Write("   Enter Your Choice: ");
            int choice = int.Parse(Console.ReadLine());
            return choice;
        }

        static void updateNameFunction(int usersCount)
        {
            Console.Clear();
            header();
            int resultCount = 0;
            Console.Write("  Enter Name of Person: ");
            string name = Console.ReadLine();
            Console.Write("  Enter Bill ID of Person: ");
            string id = Console.ReadLine();
            for(int x = 0; x < usersCount; x++)
            {
                if(ids[x] == id)
                {
                    string newName = changeNameFunction(names[x]);
                    names[x] = newName;
                    break;
                }
                else
                {
                    resultCount++;
                }
            }


            string path = "E:\\Week1PD\\Week1PD\\firstMenu.txt";
            StreamWriter f1 = new StreamWriter(path, false);
            for (int x = 0; x <= usersCount; x++)
            {
                f1.WriteLine(names[x] + "," + ids[x] + "," + bills[x]);
            }
            f1.Flush();
            f1.Close();


            if (resultCount == usersCount)
            {
                Console.WriteLine("      No Results Found!");
            }
        }
        static string changeNameFunction(string name)
        {
            Console.Clear();
            header();
            Console.Write("  Enter New Name: ");
            string newName = Console.ReadLine();
            return newName;
        }
        
        static void updateBillFunction(int usersCount)
        {
            Console.Clear();
            header();
            int resultCount = 0;
            Console.Write("  Enter Name of Person: ");
            string name = Console.ReadLine();
            Console.Write("  Enter Bill ID of Person: ");
            string id = Console.ReadLine();
            for(int x = 0; x < usersCount; x++)
            {
                if(ids[x] == id)
                {
                    string newBill = changeBillFunction(bills[x]);
                    bills[x] = newBill;
                    break;
                }
                else
                {
                    resultCount++;
                }
            }


            string path = "E:\\Week1PD\\Week1PD\\firstMenu.txt";
            StreamWriter f1 = new StreamWriter(path, false);
            for (int x = 0; x <= usersCount; x++)
            {
                f1.WriteLine(names[x] + "," + ids[x] + "," + bills[x]);
            }
            f1.Flush();
            f1.Close();


            if (resultCount == usersCount)
            {
                Console.WriteLine("      No Results Found!");
            }
        }
        static string changeBillFunction(string name)
        {
            Console.Clear();
            header();
            Console.Write("  Enter Updated Amount of Bill: ");
            string newBill = Console.ReadLine();
            return newBill;
        }
    }
}
