using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFormatter
{
    class LineFormatter
    {
        int _width;
        public LineFormatter(int width)
        {
            _width = width;
        }

        public string FormatLine(Line line)
        {
            string resultLine = "";
            int numberOfWords = line.NumberOfWords;
            int numberOfGaps = numberOfWords - 1;

            if (numberOfGaps == 0)
                return line.GetWord() + "\r\n";

            int averageGapLength = (_width - line.AllWordsLength) / numberOfGaps;
            int allGapsLength = _width - line.AllWordsLength;
            int numberOfBiggerGaps = allGapsLength - averageGapLength * numberOfGaps;

            for (int i = 0; i < numberOfBiggerGaps; i++)
                resultLine += GetWordWithGap(line.GetWord(), averageGapLength + 1);

            for (int i = numberOfBiggerGaps; i < numberOfWords - 1; i++)
                resultLine += GetWordWithGap(line.GetWord(), averageGapLength);

            resultLine += line.GetWord() + "\r\n";
            return resultLine;
        }

        private string GetWordWithGap(string word, int gapLength)
        {
            return word + new string(' ', gapLength);
        }

        public string FormatLastLine(Line line)
        {
            return string.Join(" ", line.ToArray());
        }
    }
}
