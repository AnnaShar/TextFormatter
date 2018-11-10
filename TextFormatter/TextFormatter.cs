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

            Queue<string> currentLineQueue = new Queue<string>();
            int currentLineWordsLength = 0;
            string resultText = "";

            for (int i=0; i<allWords.Length-1; i++)
            {
                string currentWord = allWords[i];
                string nextWord = allWords[i + 1];

                currentLineQueue.Enqueue(currentWord);
                currentLineWordsLength += currentWord.Length;

                int currentLineMinNumberOfSpaces = currentLineQueue.Count;
                int currentLineLength = currentLineWordsLength + currentLineMinNumberOfSpaces;

                if (currentLineLength + nextWord.Length >= width)
                {
                    resultText += FormatLine(currentLineQueue, currentLineWordsLength, width);
                    currentLineQueue.Clear();
                    currentLineWordsLength = 0;
                }
            }

            string lastWord = allWords[allWords.Length - 1];
            currentLineQueue.Enqueue(lastWord);

            resultText += FormatLastLine(currentLineQueue);
            return resultText;
        }

        private string FormatLastLine(Queue<string> words)
        {
            return string.Join(" ", words.ToArray());
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

        private string FormatLine(Queue<string> words, int allWordsLength, int width)
        {
            string resultLine = "";
            int numberOfWords = words.Count;
            int numberOfGaps = numberOfWords - 1;

            if (numberOfGaps == 0)
                return words.Dequeue()+"\r\n";

            int averageGapLength = (width-allWordsLength)/ numberOfGaps;
            int allGapsLength = width - allWordsLength;
            int numberOfBiggerGaps = allGapsLength - averageGapLength*numberOfGaps;

            for (int i = 0; i < numberOfBiggerGaps; i++)
                resultLine += GetWordWithGap(words.Dequeue(), averageGapLength + 1);
            
            for (int i=numberOfBiggerGaps; i< numberOfWords-1; i++)
                resultLine += GetWordWithGap(words.Dequeue(), averageGapLength);

            resultLine += words.Dequeue() + "\r\n";
            return resultLine;
        }

        private string GetWordWithGap (string word, int gapLength)
        {
            return word + new string(' ', gapLength);
        }
    }
}
