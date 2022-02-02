using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Csharp.Solutions
{
    public class Day16 : IPuzzle
    {
        private const int LiteralTypeId = 4;

        public string CalculateSolution(Parts part, string inputData)
        {
            return part switch
            {
                Parts.Part1 => $"{SolvePart1(inputData)}",
                Parts.Part2 => $"{SolvePart2(inputData)}",
                _ => throw new ArgumentOutOfRangeException(nameof(part), part, "There are only 2 parts.")
            };
        }
        
        private static int SolvePart1(string hexBitsTransmission)
        {
            var bitSystem = BitSystem.ParseBitSystem(hexBitsTransmission);
            var sum = bitSystem.CalculateSumOfPacketVersions();
            return sum;
        }

        private static long SolvePart2(string hexBitsTransmission)
        {
            var bitSystem = BitSystem.ParseBitSystem(hexBitsTransmission);
            bitSystem.ParseBitsPackets();
            return bitSystem.EvaluateExpression();
        }

        private enum PacketType
        {
            OperatorSum = 0,
            OperatorProduct = 1,
            OperatorMinimum = 2,
            OperatorMaximum = 3,
            Literal = 4,
            OperatorGreaterThan = 5,
            OperatorLessThan = 6,
            OperatorEqual = 7,
        }

        internal class BitsPacket
        {
            public int Version { get; set; }
            public int TypeId { get; set; }
            public long LiteralValue { get; set; }
            public int Length { get; set; }
            public List<BitsPacket> Packets { get; set; } = new();

            public long Evaluate()
            {
                var packetType = (PacketType)TypeId;
                return packetType switch
                {
                    PacketType.OperatorSum => Packets.Sum(p => p.Evaluate()),
                    PacketType.OperatorProduct => Packets.Aggregate(1L, (acc, p) => acc * p.Evaluate()),
                    PacketType.OperatorMinimum => Packets.Min(p => p.Evaluate()),
                    PacketType.OperatorMaximum => Packets.Max(p => p.Evaluate()),
                    PacketType.Literal => LiteralValue,
                    PacketType.OperatorGreaterThan => Packets[0].Evaluate() > Packets[1].Evaluate() ? 1 : 0,
                    PacketType.OperatorLessThan => Packets[0].Evaluate() < Packets[1].Evaluate() ? 1 : 0,
                    PacketType.OperatorEqual => Packets[0].Evaluate() == Packets[1].Evaluate() ? 1 : 0,
                    _ => throw new ArgumentOutOfRangeException(nameof(packetType))
                };
            }
        }

        private class BitSystem
        {
            private readonly string _binaryTransmissionBits;
            private int _index;
            private BitsPacket Root;

            private BitSystem(string binaryTransmissionBits)
            {
                Root = new BitsPacket();
                _index = 0;
                _binaryTransmissionBits = binaryTransmissionBits;
            }

            public static BitSystem ParseBitSystem(string hexBitsTransmission)
            {
                var binaryTransmission = ConvertTransmissionToBin(hexBitsTransmission);


                return new BitSystem(binaryTransmission);
            }

            public int CalculateSumOfPacketVersions(int? indexLimit = null, int? packetsLimit = null)
            {
                var sum = 0;
                var packetsCount = 0;
                var binaryTransmission = _binaryTransmissionBits;
                //var index = startIndex;
                var canReadPackages = true;
                while (canReadPackages)
                {
                    var version = Convert.ToInt32(binaryTransmission.Substring(_index, 3), 2);
                    var typeId = Convert.ToInt32(binaryTransmission.Substring(_index + 3, 3), 2);
                    _index += 6;
                    sum += version;
                    if (typeId == LiteralTypeId)
                    {
                        var isReadingLiteral = true;
                        while (isReadingLiteral)
                        {
                            var bitsGroup = binaryTransmission.AsSpan(_index, 5);
                            //literalValue += bitsGroup[1..];
                            isReadingLiteral = bitsGroup[0] == '1';
                            _index += 5;
                        }
                    }
                    else
                    {
                        if (binaryTransmission[_index++] == '0')
                        {
                            
                            var subPacketsTotalLength = Convert.ToInt32(binaryTransmission.Substring(_index, 15), 2);
                            _index += 15;
                            //var subPackets = binaryTransmission.Substring(index, subPacketsTotalLength);
                            //index += subPacketsTotalLength;

                            sum += CalculateSumOfPacketVersions(indexLimit: _index + subPacketsTotalLength);
                            //index += subPacketsTotalLength;
                        }
                        else
                        {
                            var numberOfSubPackets = Convert.ToInt32(binaryTransmission.Substring(_index, 11), 2);
                            _index += 11;

                            sum += CalculateSumOfPacketVersions(packetsLimit: numberOfSubPackets);
                            //index += 
                        }
                    }

                    packetsCount++;

                    canReadPackages = _index < (indexLimit ?? binaryTransmission.Length)
                                    && !(packetsLimit <= packetsCount)
                                    && _index + 10 < binaryTransmission.Length;     // catch leading 0s
                }

                return sum;
            }

            private static string ConvertTransmissionToBin(string hexBitsTransmission)
            {
                var binaryTransmissionBits = hexBitsTransmission.Select(c => Convert.ToInt16(c.ToString(), 16))
                    .Select(b => Convert.ToString(b, 2).PadLeft(4, '0'));
                return string.Join(string.Empty, binaryTransmissionBits);
            }

            public void ParseBitsPackets()
            {
                Root = ParsePackets().Single();
            }

            private IEnumerable<BitsPacket> ParsePackets(int? indexLimit = null, int? packetsLimit = null)
            {
                var packets = new List<BitsPacket>();
                var binaryTransmission = _binaryTransmissionBits;
                //var index = startIndex;
                var canReadPackages = true;
                while (canReadPackages)
                {
                    var packet = new BitsPacket
                    {
                        Version = Convert.ToInt32(binaryTransmission.Substring(_index, 3), 2),
                        TypeId = Convert.ToInt32(binaryTransmission.Substring(_index + 3, 3), 2)
                    };
                    _index += 6;
                    //sum += version;
                    if (packet.TypeId == LiteralTypeId)
                    {
                        var isReadingLiteral = true;
                        var literalValue = string.Empty;
                        while (isReadingLiteral)
                        {
                            var bitsGroup = binaryTransmission.AsSpan(_index, 5);
                            literalValue += new string(bitsGroup[1..]);
                            isReadingLiteral = bitsGroup[0] == '1';
                            _index += 5;
                        }
                        packet.LiteralValue = Convert.ToInt64(literalValue, 2);
                    }
                    else
                    {
                        if (binaryTransmission[_index++] == '0')
                        {

                            var subPacketsTotalLength = Convert.ToInt32(binaryTransmission.Substring(_index, 15), 2);
                            _index += 15;
                            //var subPackets = binaryTransmission.Substring(index, subPacketsTotalLength);
                            //index += subPacketsTotalLength;

                            var subPackets = ParsePackets(indexLimit: _index + subPacketsTotalLength);
                            packet.Packets.AddRange(subPackets);
                            //index += subPacketsTotalLength;
                        }
                        else
                        {
                            var numberOfSubPackets = Convert.ToInt32(binaryTransmission.Substring(_index, 11), 2);
                            _index += 11;

                            var subPackets = ParsePackets(packetsLimit: numberOfSubPackets);
                            packet.Packets.AddRange(subPackets);
                            //index += 
                        }
                    }
                    
                    packets.Add(packet);

                    canReadPackages = _index < (indexLimit ?? binaryTransmission.Length)
                                    && !(packetsLimit <= packets.Count)
                                    && _index + 10 < binaryTransmission.Length;     // catch leading 0s
                }

                return packets;
            }

            public long EvaluateExpression()
            {
                return Root.Evaluate();
            }

        }

        //private int CalculateSumOfPacketVersions(string hexBitsTransmission)
        //{
        //    var binaryTransmission = ConvertTransmissionToBin(hexBitsTransmission);
        //    var index = 0;
        //    while (index < binaryTransmission.Length)
        //    {
        //        var version = Convert.ToInt32(binaryTransmission.Substring(index, 3), 2);
        //        var typeId = Convert.ToInt32(binaryTransmission.Substring(index + 3, 3), 2);
        //        index += 6;
        //        if (typeId == LiteralTypeId)
        //        {
        //            var isReadingLiteral = true;
        //            var literalValue = string.Empty;
        //            while (isReadingLiteral)
        //            {
        //                var bitsGroup = binaryTransmission.Substring(index, 5);
        //                //literalValue += bitsGroup[1..];
        //                isReadingLiteral = bitsGroup[0] == '1';
        //                index += 5;
        //            }
        //        }
        //        else
        //        {
        //            if (binaryTransmission[index++] == '0')
        //            {
        //                var subPacketsTotalLength = Convert.ToInt32(binaryTransmission.Substring(index, 15), 2);
        //                index += 15;
        //                var subPackets = binaryTransmission.Substring(index, subPacketsTotalLength);
        //            }
        //            else
        //            {
        //                var numberOfSubPackets = Convert.ToInt32(binaryTransmission.Substring(index, 11), 2);

        //            }
        //        }
        //    }

        //    return 0;
        //}

    }
}
