using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    static void Main()
    {
        // Define the matrix
        var matrix = new List<string>
        {
    "cold",
    "wind",
    "snow",
    "chill",
    "uhgf",
    "cold",
    "wind",
    "snow",
    "chill",
    "uhgf",
    "abcd",
    "heat",
    "rain",
    "hail",
    "mist",
    "dust",
    "wave",
    "fire",
    "lava",
    "rock",
    "sand"};


        // Define the word stream
        var wordStream = new List<string>
        {
          "cold","lrsf", "rock", "sand", "wind", "lava", "chill", "heat"
        };

        // Create an instance of WordFinder
        var wordFinder = new WordFinder(matrix);

        // Find words in the matrix
        var foundWords = wordFinder.Find(wordStream);

        // Print the found words
        Console.WriteLine("Found words:");
        foreach (var word in foundWords)
        {
            Console.WriteLine(word);
        }
    }
}


public class WordFinder
{
    // 2D array to store the matrix of characters
    private readonly char[,] _matrix;
    // Number of rows in the matrix
    private readonly int _rows;
    // Number of columns in the matrix
    private readonly int _cols;

    // Constructor to initialize the matrix from a list of strings
    public WordFinder(IEnumerable<string> matrix)
    {
        _rows = matrix.Count(); // Get the number of rows
        _cols = matrix.First().Length; // Get the number of columns (assuming all rows are of equal length)
        _matrix = new char[_rows, _cols]; // Initialize the 2D array

        int row = 0;
        // Fill the 2D array with characters from the list of strings
        foreach (var line in matrix)
        {
            for (int col = 0; col < _cols; col++)
            {
                _matrix[row, col] = line[col];
            }
            row++;
        }
    }

    public IEnumerable<string> Find(IEnumerable<string> wordstream)
    {
        // To store found words
        var foundWords = new HashSet<string>(); 

        // Check each word in the wordstream
        foreach (var word in wordstream)
        {
            if (SearchWord(word))
            {
                foundWords.Add(word); // Add the word to the set if found
            }
        }

        return foundWords.Take(10); // Return up to 10 found words
    }

    // Method to search for a word in the matrix
    private bool SearchWord(string word)
    {
        // Iterate through each cell in the matrix
        for (int row = 0; row < _rows; row++)
        {
            for (int col = 0; col < _cols; col++)
            {
                if (SearchHorizontallyAndVertically(word, row, col))
                    return true;
            }
        }
        return false;
    }

    // Method to search for a word horizontally and vertically from a given starting cell
    private bool SearchHorizontallyAndVertically(string word, int row, int col)
    {
        // Check horizontally
        if (col + word.Length <= _cols)
        {
            bool match = true;
            for (int i = 0; i < word.Length; i++)
            {
                if (_matrix[row, col + i] != word[i])
                {
                    match = false;
                    break;
                }
            }
            if (match) return true;
        }

        // Check vertically
        if (row + word.Length <= _rows)
        {
            bool match = true;
            for (int i = 0; i < word.Length; i++)
            {
                if (_matrix[row + i, col] != word[i])
                {
                    match = false;
                    break;
                }
            }
            if (match) return true;
        }

        return false;
    }
} 