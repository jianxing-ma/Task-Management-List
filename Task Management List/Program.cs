using System;
using System.Collections.Generic;
using System.IO;


namespace Task_Tracking_Application
{
 
    
    class TaskTrackingApp
    {
        
        public static void Main(string[] args)
        {
            List<string> Tasks = new List<string>();
            int i = 0;
            List<string> crossout = new List<string>();           
            
            do
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("\nTask Management App Toolbar");
                Console.ResetColor();
                Console.WriteLine("Write Task: W | Delete: Del | Crossout: C | Re-enter: R | Quit: ESC" +
                    "\nOpen file: O | Save file as: S | New file: N" +
                 "\nUp: UpArrow | Down: DownArrow | Previous Page: LeftArrow | Next Page: RightArrow");
              

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.O:                       
                        Tasks = OpenFile();
                        Console.Clear();
                        DisplayTasks(Tasks, crossout, i);
                        break;
                    case ConsoleKey.W:
                        Console.Clear();
                        CreateTasks(Tasks);
                        DisplayTasks(Tasks, crossout, i);
                        Console.WriteLine("\n");
                        break;
                    case ConsoleKey.S:
                        SaveFile(Tasks);
                        Console.Clear();
                        DisplayTasks(Tasks, crossout, i);
                        break;
                    case ConsoleKey.N:
                        Console.Clear();
                        Tasks = new List<string>();
                        crossout = new List<string>();
                        i = 0;
                        break;
                    case ConsoleKey.UpArrow:
                        Console.Clear();
                        if (i == 0)
                        {
                            DisplayTasks(Tasks, crossout, i = Tasks.Count - 1);
                        }
                        else
                        {
                            DisplayTasks(Tasks, crossout, --i);
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        Console.Clear();
                        if (i == Tasks.Count - 1)
                        {
                            DisplayTasks(Tasks, crossout, i = 0); ;
                        }
                        else
                        {
                            DisplayTasks(Tasks, crossout, ++i);
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        Console.Clear();
                        if (i / 20 < (Tasks.Count - 1) / 20)
                        {
                            i = i / 20 * 20 + 20;
                            DisplayTasks(Tasks, crossout, i);
                        }
                        else
                        {
                            i = 0;
                            DisplayTasks(Tasks, crossout, i);
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        Console.Clear();
                        if (i /20 == 0)
                        {
                            i = (Tasks.Count - 1) / 20 * 20;
                            DisplayTasks(Tasks, crossout, i);
                        }
                        else
                        {
                            i -= 20;
                            DisplayTasks(Tasks, crossout, i);
                        }
                        break;
                    case ConsoleKey.C:
                        Console.Clear();
                        if (Tasks.Count == 0)
                        {
                            DisplayTasks(Tasks, crossout, i);
                            Console.WriteLine("The list is empty, no item to cross out.");
                            break;
                        }
                        else
                        {
                            crossout.Add(Tasks[i]);
                            DisplayTasks(Tasks, crossout, i);
                            break;
                        }
                    case ConsoleKey.Delete:
                        if (Tasks.Count == 0)
                        {
                            Console.Clear();
                            Console.WriteLine("\nTask Management List\n\nList is empty, no item to delete.\n");
                            break;
                        }
                        else if (i != Tasks.Count - 1)
                        {
                            Tasks.RemoveAt(i);
                            while (crossout.Contains((Tasks[i])) && i < Tasks.Count - 1)
                            {
                                Tasks.RemoveAt(i);
                            }
                            if (i == Tasks.Count - 1 && crossout.Contains((Tasks[i])))
                            {
                                Tasks.RemoveAt(i); --i;
                            }
                        }
                        else
                        {
                            if (i == 0)
                            {
                                Tasks.RemoveAt(i);
                            }
                            else
                            {
                                Tasks.RemoveAt(i); --i;
                            }
                        }
                        Console.Clear();
                        DisplayTasks(Tasks, crossout, i);
                        break;
                    case ConsoleKey.R:
                        if (Tasks.Count != 0)
                        {
                            var re = Tasks[i];
                            Tasks.RemoveAt(i);
                            Tasks.Add(re);
                            Console.Clear();
                            DisplayTasks(Tasks, crossout, i);
                            break;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("\nThe tasks list is empty");
                            break;
                        }
                    case ConsoleKey.Escape:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.Clear();
                        DisplayTasks(Tasks, crossout, i);
                        Console.WriteLine("\nNot a valid command, please refer to the instruction below.");
                        break;


                }
            } while (true);


        }



        public static List<string> CreateTasks(List<string> Tasks)
        {           
            Console.WriteLine("\nEnter each task followed by enter (enter F when finished):\n");
            string input = Console.ReadLine();
            Console.Clear();

            if (input != "f")
            {              
                Tasks.Add(input);
                return CreateTasks(Tasks);
            }
            else
            {
                return Tasks;
            }           
        }


        public static void DisplayTasks(List<string> Tasks = default(List<string>), List<string> crossout = default(List<string>), int cursor = 0)
        {
            StreamWriter sw = File.CreateText(@"C:\Users\james\Desktop\MSSA\220\Projects\Project Week 4\Task Management List.md");            

            Console.WriteLine("\nTask Management List\n");

            for (int i = (cursor / 20)*20; i < Math.Min(Tasks.Count, (cursor / 20)*20 +20); i++)
            {
                if (crossout.Contains(Tasks[i]))
                {
                    if (i == cursor)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"{i + 1}. {Tasks[i]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine($"{i + 1}. {Tasks[i]}");
                        Console.ResetColor();
                    }
                }
                else
                {
                    if (i == cursor)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"{i + 1}. {Tasks[i]}");
                        sw.WriteLine($"{Tasks[i]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"{i+1}. {Tasks[i]}");
                        sw.WriteLine($"{Tasks[i]}");
                    }
                }
            
            }
            Console.ResetColor();
            sw.Close();
            Console.WriteLine("\n");
        }



        public static List<string> OpenFile()
        {
            Console.Write("\nEnter the file path or enter x to return to main menu: ");
            string filepath = Console.ReadLine();

            if(filepath == "x")
            {
                return new List<string>(); 
            }
            else if (File.Exists(filepath))
            {
                StreamReader sr = new StreamReader(filepath);
                List<string> Tasks = new List<string>();
                string line;

                do
                {
                    line = sr.ReadLine();
                    Tasks.Add(line);
                } while (line != null);
                sr.Close();

                Tasks.RemoveAt(Tasks.Count - 1);
                Console.Clear();
                return Tasks;
            }
            else
            {
                Console.WriteLine("\nFile path is invalid, please enter a valid path: ");
                return OpenFile();
            }
        }



        public static void SaveFile(List<string> Tasks = default)
        {
            // To be modified
            // C:\Users\james\Desktop\MSSA\220\Projects\Project Week 4\Project Week 4\bin\Debug\netcoreapp3.1 不报错
            // "/"不算做string的一部分？

            do
            {
                try
                {                    

                    Console.Write("\nEnter folder path or enter x to return to main menu): ");
                    //ConsoleKey d = Console.ReadKey(true).Key;
                    string folderpath = Console.ReadLine();                    

                    if (folderpath == "x")
                    {
                        break;
                    }
                    else if (!Directory.Exists(folderpath))
                    {
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        ClearCurrentConsoleLine();
                        Console.WriteLine("\nInvalid path, please input a valid path");
                    }
                    else
                    {
                        Console.Write("Enter file name: ");
                        string filename = Console.ReadLine();
                        //File.Create(@filepath).Close(); //or File.WriteAllText(@filepath, String.Empty);

                        using (StreamWriter sw = File.CreateText(Path.Combine(folderpath, filename)))
                        {                           
                            foreach (var item in Tasks)
                            {
                                sw.WriteLine(item);
                            }
                            sw.Close();
                            break;
                        }
                    }

                }
                catch (ArgumentException)
                {
                    //System.Diagnostics.Debug.WriteLine("No path entered, please input a valid path");
                    Console.SetCursorPosition(0, Console.CursorTop - 2);
                    ClearCurrentConsoleLine();
                    Console.WriteLine("\nNo path entered, please input a valid path");
                }
                catch (IOException)
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 2);
                    ClearCurrentConsoleLine();
                    Console.WriteLine("\nInvalid path, please input a valid path");
                }

            } while (true);


        }


        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }




    }



}
