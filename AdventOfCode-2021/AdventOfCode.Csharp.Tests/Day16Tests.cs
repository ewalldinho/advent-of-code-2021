using AdventOfCode.Csharp.Solutions;
using Xunit;

namespace AdventOfCode.Csharp.Tests
{
    public class Day16Tests
    {
        private readonly Day16 _day16Solution = new();

        [Theory]
        [InlineData("38006F45291200", 9)]
        [InlineData("8A004A801A8002F478", 16)]
        [InlineData("620080001611562C8802118E34", 12)]
        [InlineData("C0015000016115A2E0802F182340", 23)]
        [InlineData("A0016C880162017C3686B18A3D4780", 31)]
        public void CorrectlyCalculatesSolutionPart1_After10Steps(string transmission, int expectedPacketVersionSum)
        {
            var packetVersionSum = _day16Solution.CalculateSolution(Parts.Part1, transmission);

            Assert.Equal(expectedPacketVersionSum.ToString(), packetVersionSum);
        }

        [Theory]
        [InlineData("38006F45291200", 1)]   // 10 < 20 -> 1
        [InlineData("C200B40A82", 3)]       // 1 + 2 -> 3
        [InlineData("04005AC33890", 54)]    // 6 * 9 -> 54
        [InlineData("880086C3E88112", 7)]   // min(7, 8, 9) -> 7
        [InlineData("CE00C43D881120", 9)]   // max(7, 8, 9) -> 9
        [InlineData("D8005AC2A8F0", 1)]     // 5 < 15 -> 1
        [InlineData("F600BC2D8F", 0)]       // 5 > 15 -> 0  
        [InlineData("9C005AC2F8F0", 0)]     // 5 == 15 -> 0  
        [InlineData("9C0141080250320F1802104A08", 1)]  // 1 + 3 == 2 * 2 -> 1
        public void CorrectlyCalculatesSolutionPart2_After40Steps(string transmission, int expectedValue)
        {
            var expressionValue = _day16Solution.CalculateSolution(Parts.Part2, transmission);

            Assert.Equal(expectedValue.ToString(), expressionValue);
        }

    }
}
