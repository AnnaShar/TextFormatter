using System;
using System.Collections.Generic;


namespace TextFormatter
{
    public class TextFormatter
    {
        public string Justify(string text, int width)
        {
            string resultText = "";
            string[] words = SplitTextToWords(text);
            CheckWidthCorrectness(words, width);

            Queue<string> currentLine = new Queue<string>();
            int currentLineLength = 0;
            for (int i=0; i<words.Length-1; i++)
            {
                string currentWord = words[i];
                string nextWord = words[i + 1];
                currentLine.Enqueue(currentWord);
                currentLineLength += currentWord.Length;

                if (currentLineLength + currentLine.Count + nextWord.Length >= width)
                {
                    resultText += FormatLine(currentLine, currentLineLength, width);
                    currentLine.Clear();
                    currentLineLength = 0;
                }
            }
            string lastWord = words[words.Length - 1];
            currentLine.Enqueue(lastWord);
            for (int j=0; j<currentLine.Count-1; j++)
                resultText += currentLine.Dequeue() + " ";
            resultText += currentLine.Dequeue();
            return resultText;
        }

        private string[] SplitTextToWords(string text)
        {
            return text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }

        private void CheckWidthCorrectness (string[] words, int width)
        {
            foreach (string word in words)
            {
                if (word.Length > width)
                    throw new Exception("Width is less than some words in text.");
            }
        }

        private string FormatLine(Queue<string> words, int lineLength, int width)
        {
            string line = "";
            int numberOfSpaces = words.Count - 1;
            if (numberOfSpaces == 0)
                return words.Dequeue()+" \r\n";
            int averageSpaceLength = (width-lineLength)/ numberOfSpaces;
            int numberOfBiggerSpaceLength = (width - lineLength) - averageSpaceLength*numberOfSpaces;
            for (int i=0; i<numberOfBiggerSpaceLength; i++)
            {
                line += words.Dequeue() + new string(' ', averageSpaceLength + 1);
            }
            for (int i=numberOfBiggerSpaceLength; i<words.Count; i++)
            {
                line += words.Dequeue() + new string(' ', averageSpaceLength);
            }
            line += words.Dequeue() + "\r\n";
            return line;
        }
    }
}
