using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;
using Текстовый_редактор;

Console.Clear();
Console.WriteLine("Введите путь до файла (вместе с названием), который вы хотите открыть:");
Console.WriteLine("-------------------------------------------------------------");

string userInput;

userInput = Console.ReadLine();

if(!File.Exists(userInput))
{
    Console.WriteLine("Файл не найден");
    Environment.Exit(-1);
}





string userInputFormat = userInput.Substring(userInput.Length - 4);

//".txt" ".xml" "json"

string filename = userInput;



Console.Clear();
Console.WriteLine("Сохраните файл в одном из трёх форматов (txt, json, xml) - F1. Закрыть программу - Escape.");
Console.WriteLine("---------------------------------------------------");

string readed_text = File.ReadAllText(userInput);
List<Figure> list = new List<Figure>();



switch (userInputFormat)
{
    case ".txt":
        list = Figure.DeserializeText(readed_text);
        break;
    case ".xml":
        XmlSerializer xml = new XmlSerializer(typeof(List<Figure>));

        

        using (FileStream fs = new FileStream(userInput, FileMode.Open))
        {
            list = (List<Figure>)xml.Deserialize(fs);
        }
        break;
    case "json":
        list = JsonConvert.DeserializeObject<List<Figure>>(readed_text); 
        break;
    default:
        Console.WriteLine("Неправильный формат файла");
        Environment.Exit(1);
        break;
}
        foreach(var item in list)
        {
            
            item.Print();
        }
//Console.WriteLine(text);

ConsoleKeyInfo inputKey = Console.ReadKey();

bool FileIsDone = false;

while(!FileIsDone)
{
    switch(inputKey.Key)
    {
        case ConsoleKey.F1:
            Console.Clear();



            Console.WriteLine("Введите путь до файла (вместе с названием), куда вы хотите сохранить текст:");
            Console.WriteLine("---------------------------------------------------");
            string userInput2;

            userInput2 = Console.ReadLine();



            string userInputFormat2 = userInput2.Substring(userInput2.Length - 4);

            switch (userInputFormat2)
            {
                case ".txt":
                   
                    string text = "";

                    foreach (var item in list)
                    {
                        text += item.View();
                        
                    }
                    text = text.TrimEnd('\n', '\r');
                    File.WriteAllText(userInput2 , text);
                    FileIsDone = true;
                    break;
                case ".xml":
                    XmlSerializer xml = new XmlSerializer(typeof(List<Figure>));
                    
                    using (FileStream fs = new FileStream(userInput2, FileMode.OpenOrCreate))
                    {
                        xml.Serialize(fs, list);
                    }
                    FileIsDone = true;
                    break;
                case "json":
                    string json = JsonConvert.SerializeObject(list);

                    File.WriteAllText($"{userInput2}", json);
                    
                    
                        FileIsDone = true;
                    break;
                default :
                    Console.WriteLine("Введён недопустимый формат файла или некорректный путь");
                    Environment.Exit(1);
                    break;
            }


           
            break;
        case ConsoleKey.Escape:
            Environment.Exit(1);
            break;
        default:
            Console.WriteLine("Нажмите F1 или Escape");
            inputKey = Console.ReadKey();
            break;
    }
    Console.Clear();
}
