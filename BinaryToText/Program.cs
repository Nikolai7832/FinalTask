using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Counters;
namespace FinalTask
{
    [Serializable]
    internal class Student
    {
        internal string Name { get; set; }
       internal  string Group { get; set; }
       internal DateTime DateOfBirth { get; set; }
        private Student(string Name, string Group, DateTime DateOfBirth)
        {
            this.Name = Name;
            this.Group = Group;
            this.DateOfBirth = DateOfBirth;
        }
    }
    
    class Programm
    {


        static void Main(string[] args)
        {
           string FilePath = Directory.GetCurrentDirectory();
            for (int i = 0; i < 4; i++)
            {
                DirectoryInfo dir = Directory.GetParent(FilePath);
                FilePath = dir.FullName;
            }
             FilePath += "\\Students.dat";
            Console.WriteLine(FilePath);
            try
            {
                using (var fs = new FileStream(FilePath, FileMode.Open)) ;
            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка доступа к файлу");
            }

            if (File.Exists(@FilePath))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                Student[] students;
               
                    using (var fs = new FileStream(FilePath, FileMode.Open))
                    {

                        students = (Student[])formatter.Deserialize(fs);
                    }

                    Counter[] counter = new Counter[students.Length];

                    for (int i = 0; i < students.Length; i++)
                    {
                        Counter counter1 = new Counter(students[i].Group);
                        counter[i] = counter1;
                    }
                    var group = Counter.UniqWordCounter(counter);
                    string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\Students";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                        for (int i = 0; i < group.Length; i++)
                        {
                            FileInfo file = new FileInfo(path + $"\\{group[i]}.txt");
                            if (!file.Exists)
                            {
                                using (StreamWriter sw = file.CreateText())
                                {
                                    for (int j = 0; j < students.Length; j++)
                                    {
                                        if (students[j].Group == group[i])
                                        {
                                            sw.WriteLine($"{students[j].Name}, {students[j].DateOfBirth.ToShortDateString()}");
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
            
            else
            {
                Console.WriteLine("Неверный ввод");
                Main(args);
            }
        }
    }
}