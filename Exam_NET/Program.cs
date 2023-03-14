using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Linq;
using System.Threading;

namespace Exam_NET
{
    class Program
    {
        public class Word
        {
            public string say { get; set; }
            public string translation { get; set; }
            public string synonyms { get; set; } = "";
            public Word() { }
            public Word(string s, string t)
            {
                say = s;
                translation = t;
            }
            public void Add_Syn(string temp)
            {
                synonyms += (temp + " ");
            }
        }
        public class Words
        {
            private string file;
            public void Start()
            {
                Console.WriteLine("1. Создать словарь\t2. Открыть словарь");
                int choose = Convert.ToInt32(Console.ReadLine());

                if (choose == 1)
                {
                    Console.WriteLine("1. Русско - английский\n2. Английско - русский");
                    int choose2 = Convert.ToInt32(Console.ReadLine());
                    if (choose2 == 1)
                    {
                        file = "../../Rus_Words.xml";
                        FileStream stream = new FileStream(file, FileMode.Create);
                        stream.Close();
                        Console.Write("Введите слово: ");
                        string temp_1 = Console.ReadLine();
                        Console.Write("Введите перевод: ");
                        string temp_2 = Console.ReadLine();
                        Word word = new Word(temp_1, temp_2);
                        Console.WriteLine("1. Добавить синоним\n2. Пропустить");
                        int choese = Convert.ToInt32(Console.ReadLine());
                        while (choese == 1)
                        {
                            Console.Write("Введите перевод: ");
                            temp_2 = Console.ReadLine();
                            word.Add_Syn(temp_2);
                            Console.WriteLine("1. Добавить синоним\n2. Пропустить");
                            choese = Convert.ToInt32(Console.ReadLine());
                        }
                        XDocument doc = new XDocument(new XElement("Words", new XElement("Word", new XAttribute("say", word.say), new XAttribute("translation", word.translation), new XElement("synonyms", word.synonyms))));
                        doc.Save(file);
                        Console.Clear();
                        Choose();
                    }
                    else if (choose2 == 2)
                    {
                        file = "../../Eng_Words.xml";
                        FileStream stream = new FileStream(file, FileMode.Create);
                        stream.Close();
                        Console.Write("Введите слово: ");
                        string temp_1 = Console.ReadLine();
                        Console.Write("Введите перевод: ");
                        string temp_2 = Console.ReadLine();
                        Word word = new Word(temp_1, temp_2);
                        Console.WriteLine("1. Добавить синоним\n2. Пропустить");
                        int choese = Convert.ToInt32(Console.ReadLine());
                        while (choese == 1)
                        {
                            Console.Write("Введите перевод: ");
                            temp_2 = Console.ReadLine();
                            word.Add_Syn(temp_2);
                            Console.WriteLine("1. Добавить синоним\n2. Пропустить");
                            choese = Convert.ToInt32(Console.ReadLine());
                        }
                        XDocument doc = new XDocument(new XElement("Words", new XElement("Word", new XAttribute("say", word.say), new XAttribute("translation", word.translation), new XElement("synonyms", word.synonyms))));
                        doc.Save(file);
                        Choose();
                    }
                    else
                    {
                        Console.WriteLine("Error");
                    }
                }
                else if (choose == 2)
                {
                    Console.WriteLine("1. Русско - английский\n2. Английско - русский");
                    choose = Convert.ToInt32(Console.ReadLine());
                    if (choose == 1)
                    {
                        file = "../../Rus_Words.xml"; 
                        Choose();
                    }
                    else if (choose == 2)
                    {
                        file = "../../Eng_Words.xml"; 
                        Choose();
                    }
                    else
                    {
                        Console.WriteLine("Error");
                    }
                }
                else
                {
                    Console.WriteLine("Error");
                }
            }
            public void Choose()
            {
                Console.WriteLine("---Словарь---\n1. Добавить\t2. Удалить\n3. Поиск\t4. Экспорт\n5. Изменить\n0. Выход");
                int choose = Convert.ToInt32(Console.ReadLine());
                if (choose == 1)
                {
                    Add();
                }
                if (choose == 2)
                {
                    Delete();
                }
                if (choose == 3)
                {
                    Search_Transl();
                }
                if (choose == 4)
                {
                    Export();
                }
                if (choose == 5)
                {
                    Change();
                }
                if (choose == 0)
                {
                    Environment.Exit(0);
                }

            }
            public void Add()
            {
                Console.Write("Введите слово: ");
                string temp_1 = Console.ReadLine();
                Console.Write("Введите перевод: ");
                string temp_2 = Console.ReadLine();
                Word word = new Word(temp_1, temp_2);
                Console.WriteLine("1. Добавить ещё перевод\n2. Пропустить");
                int choose = Convert.ToInt32(Console.ReadLine());
                while (choose == 1)
                {
                    Console.Write("Введите перевод: ");
                    temp_2 = Console.ReadLine();
                    word.Add_Syn(temp_2);
                    Console.WriteLine("1. Добавить ещё перевод\n2. Пропустить");
                    choose = Convert.ToInt32(Console.ReadLine());
                }
                XDocument d = XDocument.Load(file);
                d.Root.Add(new XElement("Word", new XAttribute("say", word.say), new XAttribute("translate", word.translation), new XElement("synonyms", word.synonyms)));
                d.Save(file);
                Choose();
            }
            public void Change()
            {
                Console.WriteLine("1. Заменить слово\t2. Заменить перевод");
                int choose = Convert.ToInt32(Console.ReadLine());
                if (choose == 1)
                {
                    Console.Write("Введите слово: ");
                    string search = Console.ReadLine();
                    Console.Write("Введите новое слово: ");
                    string change = Console.ReadLine();
                    XDocument d = XDocument.Load(file);
                    XElement e = d.Root;
                    var tom = d.Element("Words")?
                        .Elements("Word")
                        .FirstOrDefault(p => p.Attribute("say")?.Value == search);
                    if (tom != null)
                    {
                        var word = tom.Attribute("say");
                        if (word != null)
                        {
                            word.Value = change;
                        }
                        d.Save(file);
                        Console.WriteLine("Слово заменено");
                        Thread.Sleep(1250);
                    }
                }
                else
                {
                    Console.Write("Введите перевод: ");
                    string search = Console.ReadLine();
                    Console.Write("Введите новый перевод: ");
                    string change = Console.ReadLine();
                    XDocument d = XDocument.Load(file);
                    XElement e = d.Root;
                    var tom = d.Element("Words")?
                        .Elements("Word")
                        .FirstOrDefault(p => p.Attribute("translation")?.Value == search);
                    if (tom != null)
                    {
                        var translate = tom.Attribute("translation");
                        if (translate != null)
                        {
                            translate.Value = change;
                        }
                        d.Save(file);
                        Console.WriteLine("Слово было заменено");
                        Thread.Sleep(1250);
                    }
                }
                Choose();
            }
            public void Delete()
            {
                Console.WriteLine("1. Удалить слово\t2. Удалить перевод");
                int choose = Convert.ToInt32(Console.ReadLine());
                if (choose == 1)
                {
                    Console.Write("Введите слово: ");
                    string search = Console.ReadLine();
                    XDocument d = XDocument.Load(file);
                    XElement e = d.Root;
                    if (e != null)
                    {
                        var tom = d.Element("Words")?
                        .Elements("Word")
                        .FirstOrDefault(p => p.Attribute("say")?.Value == search);
                        if (tom != null)
                        {
                            tom.Remove();
                            d.Save(file);
                            Console.WriteLine("Слово было удалено");
                            Thread.Sleep(1250);
                        }
                    }
                }
                else
                {
                    Console.Write("Введите перевод: ");
                    string search2 = Console.ReadLine();
                    XDocument d = XDocument.Load(file);
                    XElement e = d.Root;
                    if (e != null)
                    {
                        var tom = d.Element("Words")?
                        .Elements("Word")
                        .FirstOrDefault(p => p.Attribute("translate")?.Value == search2);
                        if (tom != null)
                        {
                            var transl = tom.Element("synonyms").Value;
                            if (transl == "" || transl == null)
                            {
                                Console.WriteLine("Нельзя удалить, т.к нет других переводов");
                            }
                            else
                            {
                                string[] temp = tom.Element("synonyms").Value.Split(' ');
                                List<string> list = new List<string>(temp);
                                var translate = tom.Attribute("translation");
                                if (translate != null) translate.Value = list[0];
                                list.RemoveAt(0);
                                string temp_2 = "";
                                if (list.Count != 0)
                                {
                                    for (int i = 0; i < list.Count; i++)
                                        temp_2 += list[i] + " ";
                                    temp_2 = temp_2.Remove(temp_2.Length - 1);
                                    tom.Element("synonyms").Value = temp_2;
                                }
                                Console.WriteLine("Перевод заменен синонемом");
                                Thread.Sleep(1250);
                            }
                        }
                    }
                    d.Save(file);
                }
                Choose();
            }
            public void Search_Transl()
            {
                Console.Write("Введите слово: ");
                string search = Console.ReadLine();
                XDocument d = XDocument.Load(file);
                XElement e = d.Root;
                var tom = d.Element("Words")?
                    .Elements("Word")
                    .FirstOrDefault(p => p.Attribute("say")?.Value == search);
                if (tom != null)
                {
                    var translate = tom.Attribute("translation");
                    var translates = tom.Element("synonyms");
                    Console.WriteLine("Перевод: " + translate.Value + " " + translates.Value);
                    Console.ReadLine();
                }
                Choose();
            }
            public void Export()
            {
                Console.Write("Введите слово: ");
                string temp = Console.ReadLine();
                XDocument d = XDocument.Load(file);
                XElement e = d.Root;
                var search = d.Element("Words")?
                    .Elements("Word")?
                    .FirstOrDefault(p => p.Attribute("say")?.Value == temp);
                string word = search?.Attribute("say")?.Value;
                string translate = search?.Attribute("translation")?.Value;
                string translates = search?.Element("synonyms")?.Value;
                FileStream stream = new FileStream("../../result.xml", FileMode.Create); stream.Close();
                XDocument doc = new XDocument(new XElement("Words", new XElement("Word", new XAttribute("say", word), new XAttribute("translation", translate), new XElement("synonyms", translates))));
                doc.Save("../../Exported.xml");
                Console.WriteLine("Слово экспортировано");
                Thread.Sleep(1250);
                Choose();
            }
        }
        static void Main(string[] args)
        {
            Words vocabluar = new Words();
            vocabluar.Start();
        }
    }
}