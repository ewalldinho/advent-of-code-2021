using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Csharp.Solutions
{
    // Day 4: Giant Squid (https://adventOfCode.com/2021/day/4)
    public class Day04 : IPuzzle
    {
        public string CalculateSolution(Parts part, string inputData)
        {
            return part switch
            {
                Parts.Part1 => SolvePart1(inputData),
                Parts.Part2 => SolvePart2(inputData),
                _ => throw new ArgumentOutOfRangeException(nameof(part), part, "There are only 2 parts.")
            };
        }

        private static string SolvePart1(string inputData)
        {
            var bingo = ParseData(inputData);
            bool isWinner;
            List<BingoBoard> winners;
            do
            {
                var luckyNumber = bingo.DrawNumber();
                winners = bingo.MarkNumberOnBoards(luckyNumber);
                isWinner = winners.Count > 0;
            }
            while (!isWinner);

            var winningScore = 0;
            foreach (var winningBoard in winners)
            {
                var score = winningBoard.CalcWinningScore();
                if (score > winningScore)
                    winningScore = score;
            }
            return $"{winningScore}";
        }

        private static string SolvePart2(string inputData)
        {
            var bingo = ParseData(inputData);
            List<BingoBoard> winners;
            var boardsCount = bingo.BoardsCount;
            var winnersCount = 0;
            do
            {
                var luckyNumber = bingo.DrawNumber();
                winners = bingo.MarkNumberOnBoards(luckyNumber);
                winnersCount += winners.Count;
            }
            while (winnersCount < boardsCount);

            var winningBoard = winners.Last();
            var winningScore = winningBoard.CalcWinningScore();
            
            return $"{winningScore}";
        }


        private class BingoBoard
        {
            private readonly List<int> _numberMatrix;
            private readonly bool[,] _markedNumbersMatrix;
            private int _lastNumber;
            public bool HasWon { get; private set; }

            private BingoBoard(List<int> numberMatrix)
            {
                _markedNumbersMatrix = new bool[5, 5];
                _numberMatrix = numberMatrix;
            }

            public bool CheckNumber(int number)
            {
                var index = _numberMatrix.IndexOf(number);
                if (index <= -1)
                    return false;

                var row = index / 5;
                var col = index % 5;
                _markedNumbersMatrix[row, col] = true;
                HasWon = IsWinner(row, col);
                if (HasWon)
                    _lastNumber = number;

                return HasWon;
            }

            public int CalcWinningScore()
            {
                if (!HasWon)
                    throw new InvalidOperationException($"This board is not a winner.");

                var sum = 0;
                for (var row = 0; row < 5; row++)
                {
                    for (var col = 0; col < 5; col++)
                    {
                        if ( ! _markedNumbersMatrix[row, col])
                            sum += _numberMatrix[row*5 + col];
                    }
                }

                return sum * _lastNumber;
            }

            private bool IsWinner(int row, int col)
            {
                var count = 0;
                for (var c = 0; c < 5; c++)
                {
                    if (_markedNumbersMatrix[row, c])
                        count++;
                    if (count == 5)
                        return true;
                }
                count = 0;
                for (var r = 0; r < 5; r++)
                {
                    if (_markedNumbersMatrix[r, col])
                        count++;
                    if (count == 5)
                        return true;
                }
                return false;
            }

            public static BingoBoard Parse(string[] boardData)
            {
                var boardNumbers = string.Join(' ', boardData);
                var numbers = boardNumbers.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(n => n.Trim()).ToArray();
                if (numbers.Length != 25)
                    throw new ArgumentException("Invalid bingo board data");
                
                var bingoBoard = new BingoBoard(numbers.Select(int.Parse).ToList());
                return bingoBoard;
            }

        }

        private class BingoGame
        {
            private readonly Queue<int> _drawnNumbers;
            private readonly IEnumerable<BingoBoard> _boards;

            public BingoGame(IEnumerable<int> drawnNumbers, IEnumerable<BingoBoard> boards)
            {
                _drawnNumbers = new Queue<int>(drawnNumbers);
                _boards = boards;
            }

            public int BoardsCount => _boards.Count();

            public int DrawNumber()
            {
                return _drawnNumbers.Dequeue();
            }

            public List<BingoBoard> MarkNumberOnBoards(int number)
            {
                var winnerList = new List<BingoBoard>();
                foreach (var board in _boards.Where(b => !b.HasWon))
                {
                    var isWinner = board.CheckNumber(number);
                    if (isWinner)
                        winnerList.Add(board);
                }

                return winnerList;
            }

        }

        private static BingoGame ParseData(string inputData)
        {
            var lines = inputData.Split(Environment.NewLine);
            var numbers = lines[0].Split(',').Select(int.Parse);
            var boards = lines.Skip(2).Chunk(6).Select(BingoBoard.Parse).ToList();
            return new BingoGame(numbers, boards);
        }

    }

    

}