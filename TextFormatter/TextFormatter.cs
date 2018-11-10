using System;
using System.Collections.Generic;


namespace TextFormatter
{
    public class TextFormatter
    {
        public string Justify(string text, int width)
        {
            string[] allWords = SplitTextToWords(text);

            if (!CheckWidthCorrectness(allWords, width))
                throw new Exception("Width is less than some words in text.");

            Line currentLine = new Line();
            LineFormatter lineFormatter = new LineFormatter(width);
            string resultText = "";

            for (int i=0; i<allWords.Length-1; i++)
            {
                string currentWord = allWords[i];
                string nextWord = allWords[i + 1];

                currentLine.AddWord(currentWord);

                if (currentLine.Length + nextWord.Length >= width)
                {
                    resultText += lineFormatter.FormatLine(currentLine);
                    currentLine.Clear();
                }
            }

            string lastWord = allWords[allWords.Length - 1];
            currentLine.AddWord(lastWord);

            resultText += lineFormatter.FormatLastLine(currentLine);
            return resultText;
        }

        private string[] SplitTextToWords(string text)
        {
            return text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }

        private bool CheckWidthCorrectness (string[] words, int width)
        {
            foreach (string word in words)
            {
                if (word.Length > width)
                    return false;
            }
            return true;
        }
    }
}
