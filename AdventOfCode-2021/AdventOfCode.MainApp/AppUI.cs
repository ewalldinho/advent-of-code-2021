using System;
using System.Text;

namespace AdventOfCode.MainApp
{
    internal enum GuiStatus
    {
        ChoosingPuzzle = 1,
        Calculating = 2,
        ShowingResult = 3
    }

    internal class AppUI
    {
        const string Title = "Advent of Code 2021";
        const char Space = ' ';
        const char TopLeft = '┌';
        const char TopRight = '┐';
        const char BottomLeft = '└';
        const char BottomRight = '┘';
        const char Horizontal = '─';
        const char Vertical = '│';
        const char VerticalRight = '├';
        const char VerticalLeft = '┤';

        //private const int MinDay = 1;
        //private const int MaxDay = 25;
        private const int Part1Col = 3;
        private const int Part2Col = 43;
        private const int ResultsLine = 17;

        private GuiStatus status = GuiStatus.ChoosingPuzzle;
        private bool isLanguageCsharp = false;
        private (string part1, string part2) results;

        private readonly string[] _matrix = new string[]
        {
            "▄████████▄       ▐█      ▄██████▄   ██████████     ██       ▐████████▌   ████████   ██████████   ████████    ████████  ",
            "██      ██      ▄██     ██      ██  ▀▀     ██     ██    ██  ▐█          ██      ▀▀  ▀▀      ██  ██      ██  ██      ██ ",
            "██      ██    ▄████     ██      ██        ██     ██    ▐█▌  ██          ██                 ██   ██      ██  ██      ██ ",
            "██      ██   ██▀ ██             ██    ▄▄▀▀      ██     ▐█▌  ██          ██                ██    ██      ██  ██      ██ ",
            "██      ██       ██            ██     ▀▀▀█▄     ██     ██   ████████▄   ▐████████        ██      ████████    ████████▌ ",
            "██      ██       ██         ███           ▀▀█▄  █████████           ██  ██      ██      ██      ██      ██          ██ ",
            "██      ██       ██       ██                ██        ▐█▌           ██  ██      ██     ██       ██      ██          ██ ",
            "██      ██       ██      ██     ▄▄  ▄▄     ▄██        ██    ▄▄      ██  ██      ██    ██        ██      ██  ▄▄      ██ ",
            "▀████████▀  ▄████████▀  ██████████   ▀██████▀         ██     ███████     ████████     ██         ████████    ████████  ",
        };

        public AppUI()
        {
            Console.CursorVisible = false;
            results = (string.Empty, string.Empty);
        }

        public void DisplayChoice(int selectedDay)
        {
            status = GuiStatus.ChoosingPuzzle;
        }

        public void DisplayProgress()
        {
            status = GuiStatus.Calculating;
        }

        public void DisplayResults(string part1, string part2)
        {
            status = GuiStatus.ShowingResult;
            results = (part1, part2);
        }

        public bool Display(int selectedDay)
        {
            Console.SetCursorPosition(0, 0);
            Console.Clear();
            
            DisplayMenu(selectedDay);

            switch (status)
            {
                case GuiStatus.ChoosingPuzzle:
                    ShowChoice();
                    break;
                case GuiStatus.Calculating:
                    ShowProgress();
                    break;
                case GuiStatus.ShowingResult:
                    ShowResults(results.part1, results.part2);
                    break;
            }

            // handle user input
            var isRunning = true;
            // change status:
            // * pressed 0..9, <--, --> show selectedDay
            // * pressed enter --> show calculating... accept no input
            //   - calculation ended --> show result
            // * Esc - change isRunning to false, Quit
            // 

            return isRunning;
        }

        private void DisplayMenu(int selectedDay)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            StringBuilder sb = new StringBuilder(30);
            sb.Append(TopLeft);
            sb.Append(new string(Horizontal, 78));
            sb.Append(TopRight);
            sb.AppendLine();

            //Console.Write(TopLeft);
            //Console.Write(new string(Horizontal, 78));
            //Console.WriteLine(TopRight);

            sb.Append(Vertical);
            sb.Append(new string(Space, 78));
            sb.Append(Vertical);
            sb.AppendLine();
            //Console.Write(Vertical);
            //Console.Write(new string(Space, 78));
            //Console.WriteLine(Vertical);

            sb.Append(VerticalRight);
            sb.Append(new string(Horizontal, 78));
            sb.Append(VerticalLeft);
            sb.AppendLine();
            //Console.Write(VerticalRight);
            //Console.Write(new string(Horizontal, 78));
            //Console.WriteLine(VerticalLeft);

            for (var i = 0; i < 13; i++)
            {
                sb.AppendLine($"{Vertical}{new string(Space, 78)}{Vertical}");
                //Console.Write(Vertical);
                //Console.Write(new string(Space, 78));
                //Console.WriteLine(Vertical);
            }

            sb.AppendLine($"{VerticalRight}{new string(Horizontal, 78)}{VerticalLeft}");
            //Console.Write(VerticalRight);
            //Console.Write(new string(Horizontal, 78));
            //Console.WriteLine(VerticalLeft);

            sb.AppendLine($"{Vertical}{new string(Space, 78)}{Vertical}");
            //Console.Write(Vertical);
            //Console.Write(new string(Space, 78));
            //Console.WriteLine(Vertical);

            sb.AppendLine($"{BottomLeft}{new string(Horizontal, 78)}{BottomRight}");
            //Console.Write(BottomLeft);
            //Console.Write(new string(Horizontal, 78));
            //Console.WriteLine(BottomRight);

            Console.Write(sb);

            Console.CursorTop = 1;
            Console.CursorLeft = (80 - Title.Length) / 2;
            Console.Write(Title);

            DisplayDigits(selectedDay, 5, (80-22)/2);
        }

        private void DisplayDigits(int number, int row, int col)
        {
            if (number is <0 or >99)
                number = 0;

            Console.ForegroundColor = isLanguageCsharp ? ConsoleColor.DarkGreen : ConsoleColor.DarkCyan;

            var digits = new int[] { number / 10, number % 10 };
            foreach (var line in _matrix)
            {
                var displayLine = string.Concat(line.Substring(digits[0] * 12, 10), new string(Space, 5), line.Substring(digits[1] * 12, 10));
                Console.CursorTop = row;
                Console.CursorLeft = col;
                Console.WriteLine(displayLine);
                row++;
            }

        }

        private void DrawWindow(int width = 80, int height = 25)
        {
            // title : 3 lines
            // digits : 9 lines
            // digits margin : 2 lines
            // parts : 2-3 lines
            // controls legend : 3 lines

        }

        private void ShowChoice()
        {

        }

        private void ShowProgress()
        {
            Console.SetCursorPosition(Part1Col, ResultsLine);
            Console.Write("Part1: calculating...");
            Console.SetCursorPosition(Part2Col, ResultsLine);
            Console.Write("Part2: calculating...");
        }

        private void ShowResults(string answerPart1, string answerPart2)
        {
            Console.SetCursorPosition(Part1Col, ResultsLine);
            Console.Write("Part1: ");
            if (!answerPart1.Contains(Environment.NewLine))
            {
                Console.Write(answerPart1);
            }
            else
            {
                var lines = answerPart1.Split(Environment.NewLine);
                Console.Write($"multiline ({lines.Length} lines)");
                var row = ResultsLine + 2;
                foreach (var line in lines)
                {
                    Console.SetCursorPosition(Part1Col, row);
                    Console.Write(line);
                    row++;
                }
            }

            Console.SetCursorPosition(Part2Col, ResultsLine);
            Console.Write("Part2: ");
            if (!answerPart2.Contains(Environment.NewLine))
            {
                Console.Write(answerPart2);
            }
            else
            {
                var lines = answerPart2.Split(Environment.NewLine);
                Console.Write($"multiline ({lines.Length} lines)");
                var row = ResultsLine + 2;
                foreach (var line in lines)
                {
                    Console.SetCursorPosition(Part2Col, row);
                    Console.Write(line);
                    row++;
                }
            }
        }
    }
}
