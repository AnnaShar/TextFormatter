using System;
using System.Collections.Generic;


namespace TextFormatter
{
    class Line
    {
        Queue<string> _wordsQueue;
        int _numberOfSpaces;

        public int Length { get; private set; }
        public int AllWordsLength { get; private set; }
        public int NumberOfWords
        {
            get
            {
                return _wordsQueue.Count;
            }
        }

        public Line()
        {
            _wordsQueue = new Queue<string>();
            _numberOfSpaces = 0;
            AllWordsLength = 0;
            Length = 0;
        }

        public void AddWord(string word)
        {
            _wordsQueue.Enqueue(word);
            _numberOfSpaces++;
            AllWordsLength += word.Length;
            Length = AllWordsLength + _numberOfSpaces;
        }
        public string GetWord()
        {
            if (_wordsQueue.Count == 0)
                 throw new InvalidOperationException("Line is empty.");

            string word = _wordsQueue.Dequeue();
            AllWordsLength -= word.Length;
            _numberOfSpaces--;
            Length = AllWordsLength + _numberOfSpaces;

            return word;
        }
        public void Clear()
        {
            _wordsQueue.Clear();
            _numberOfSpaces = 0;
            AllWordsLength = 0;
            Length = 0;
        }
        public string[] ToArray ()
        {
            return _wordsQueue.ToArray();
        }
    }
}
