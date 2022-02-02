using AdventOfCode.Csharp.Solutions;
using Xunit;

namespace AdventOfCode.Csharp.Tests
{
    public class Day10Tests
    {
        private readonly Day10 _day10Solution = new();

        [Fact]
        public void FindsCorrectIllegalCharacterIndex()
        {
            const string line = "<()>[}{<";

            var errorIndex = Day10.FindIllegalCharacterIndex(line);

            Assert.Equal(5, errorIndex);
        }


        [Theory]
        [InlineData("<()>([{<", ">}])")]
        [InlineData("[{<<<{{[(<{{{{[]()}{[]()}}<[[]<>]<[]()>>}([[[][]]]<[{}<>]((){})>)}>)[<([<<(){}>{(){}}>[[<>{}](<>[", "])]])>]]}}>>>}]")]
        [InlineData("<<{(<({[<{{<[<()()><{}{}>][[()[]][(){}]]>((<[]<>>(<>{}))((<>{}){<>()}))}<{({{}}[<>{}])({{}{}}{{}", "})}>}>]})>)}>>")]
        public void FindsCorrectCompletionString_version0(string line, string expectedCompletion)
        {
            var actualCompletion = Day10.FindCompletionString_v0(line);

            Assert.Equal(expectedCompletion, actualCompletion);
        }

        [Theory]
        [InlineData("<()>([{<", ">}])")]
        [InlineData("[{<<<{{[(<{(<[{}<>]((){})>)}>)[<([<<(){}>{(){}}>[[{}](<>[", "])]])>]]}}>>>}]")]
        [InlineData("<<{(<({[<{{<[<()()><{}{}>][[()[]][(){}]]>((<[]<>>(<>{}))((<>{}){<>()}))}<{({{}}[<>{}])({{}{}}{{}", "})}>}>]})>)}>>")]
        public void FindsCorrectCompletionString(string line, string expectedCompletion)
        {
            var actualCompletion = Day10.FindCompletionString(line);

            Assert.Equal(expectedCompletion, actualCompletion);
        }


        [Theory]
        [InlineData("}}]])})]", 288957)]
        [InlineData(")}>]})", 5566)]
        [InlineData("}}>}>))))", 1480781)]
        [InlineData("]]}}]}]}>", 995444)]
        [InlineData("])}>", 294)]
        [InlineData("])]])>]]}}>>>}]", 14032246867)]  // overflown 32bit int: 1147344979 
        [InlineData("})}>}>]})>)}>>", 4099427724)]  // overflown 32bit int: -195539572 
        public void CorrectlyCalculatesLineAutocompleteScore(string completionString, long expectedScore)
        {
            var actualScore = Day10.CalcAutocompleteScore(completionString);

            Assert.Equal(expectedScore, actualScore);
        }

        [Fact]
        public void CorrectlyCalculatesSolutionPart1()
        {
            var inputData = GetTestData();

            var result = _day10Solution.CalculateSolution(Parts.Part1, inputData);

            Assert.Equal("26397", result);
        }
        
        [Fact]
        public void CorrectlyCalculatesSolutionPart2()
        {
            var inputData = GetTestData();

            var result = _day10Solution.CalculateSolution(Parts.Part2, inputData);

            Assert.Equal("288957", result);
        }


        private static string GetTestData() =>
            @"[({(<(())[]>[[{[]{<()<>>
[(()[<>])]({[<{<<[]>>(
{([(<{}[<>[]}>{[]{[(<()>
(((({<>}<{<{<>}{[]{[]{}
[[<[([]))<([[{}[[()]]]
[{[{({}]{}}([{[{{{}}([]
{<[[]]>}<{[{[{[]{()[[[]
[<(<(<(<{}))><([]([]()
<{([([[(<>()){}]>(<<{{
<{([{{}}[<[[[<>{}]]]>[]]";

    }
}
