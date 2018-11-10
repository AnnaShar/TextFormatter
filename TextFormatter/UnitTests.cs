using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextFormatter;

namespace TestTextFormatter
{
    [TestClass]
    public class UnitTest1
    {
        TextFormatter.TextFormatter formatter = new TextFormatter.TextFormatter();

        [TestMethod]
        public void Test_CorrectFormat()
        {
            string expected = "aa  bb  c d\nlastLine";
            string result = formatter.Justify("aa bb c d lastLine", 11);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_LinesLengthCorrectExceptLastOne()
        {
            int expectedWidth = 10;
            string inputText = "aaa bb cc aaa bb";
            string outputText = formatter.Justify(inputText, expectedWidth);
            string[] lines = GetLines(outputText);

            Assert.AreEqual(expectedWidth, lines[0].Length);
        }

        [TestMethod]
        public void Test_LastLineCorrect()
        {
            int Width = 10;
            string inputText = "aaa bb cc aaa bb";
            string outputText = formatter.Justify(inputText,Width);
            string[] lines = GetLines(outputText);
            int expectedLineLength = 6; //aaa bb
            Assert.AreEqual(expectedLineLength, lines[1].Length);
        }

        [TestMethod]
        public void Test_OneWordInLineCorrect()
        {
            int width = 7;
            string inputText = "aaaaa bbb cc aaa bb";
            string outputText = formatter.Justify(inputText, width);
            string[] lines = GetLines(outputText);
            int expectedLineLength = 5; //aaaaa
            Assert.AreEqual(expectedLineLength, lines[0].Length);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Width is less than some words in text.")]
        public void Test_IncorrectWidthException()
        {
            string inputText = "www wwww www";
            formatter.Justify(inputText, 3);
        }

        [TestMethod]
        public void Test_GapsLengthDifferenceCorrect()
        {
            int expectedMaxDifference = 1;
            string outputText = formatter.Justify(GenerateText(15), 20);

            string[] lines = GetLines(GenerateText(10));
            int maxDifference = 0;
            foreach (string line in lines)
            {
                string[] spaces = line.Split(new char[] { 'a' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 1; i < spaces.Length; i++)
                    if (spaces[i].Length == spaces[i - 1].Length)
                    {
                        int difference = Math.Abs(spaces[i].Length - spaces[i - 1].Length);
                        if (difference > maxDifference)
                            maxDifference = difference;
                    }
            }
            Assert.IsTrue(expectedMaxDifference >= maxDifference);
        }

        [TestMethod]
        public void Test_GapsLengthAreNotIncreasing()
        {
            bool expected = true;

            string outputText = formatter.Justify(GenerateText(15), 20);
            string[] lines = GetLines(outputText);

            foreach (string line in lines)
            {
                string[] spaces = GetSpacesGaps(line, new char[] { 'a' });
                for (int i = 1; i < spaces.Length; i++)
                    if (spaces[i].Length > spaces[i - 1].Length)
                        expected = false;
            }
            Assert.IsTrue(expected);
        }

        private string GenerateText(int numberOfWords)
        {
            Random random = new Random();
            string text = "";
            for (int i = 0; i < numberOfWords; i++)
                text += new string('a', random.Next(10)) + ' ';
            return text;
        }

        private string[] GetLines(string text)
        {
            return text.Split('\n');
        }

        private string[] GetSpacesGaps(string line, char[] letters)
        {
            return line.Split(letters, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
