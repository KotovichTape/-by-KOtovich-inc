using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Текстовый_редактор
{
    public class Figure
    {
        public string Name;
        public int Width;
        public int Height;

        public void Print()
        {
            Console.WriteLine($"{this.Name}\n" +
                $" {this.Width}\n" +
                $" {this.Height}\n");
        }
        public string View()
        {
            string result = "";

            result = $"{this.Name}\n" +
                $"{this.Width}\n" +
                $"{this.Height}\n";


            return result;      
        }
        public static List<Figure> DeserializeText(string text)
        {
            text = text.TrimEnd('\n','\r');
            List<Figure> list = new List<Figure>();

            string[] arrayOfLines = text.Split('\n');
            int numLines = arrayOfLines.Length;

            Figure oneFigure;

           

            for (int i = 0; i < numLines; i = i + 3)
            {
                

                oneFigure = new Figure();
                oneFigure.Name = arrayOfLines[i];
                oneFigure.Width = Int32.Parse(arrayOfLines[i + 1]);
                oneFigure.Height = Int32.Parse(arrayOfLines[i + 2]);

                list.Add(oneFigure);
            }
            // foreach(var item in list) {
            //   item.Print();
            // }

            return list;
        }
    }


    
}
