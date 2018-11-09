using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace TextFormatter
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = "";
            using (StreamReader str = new StreamReader("inputText.txt"))
            {
                text = str.ReadToEnd();
            }

            TextFormatter textFormatter = new TextFormatter();
            string resultText = textFormatter.Justify(text, 20);

            using (StreamWriter stw = new StreamWriter("outputText.txt"))
            {
                stw.Write(resultText);
            }
        }
    }
}
