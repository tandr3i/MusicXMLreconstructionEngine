using MusicXMLreconstructionEngine;
using System;
using Xunit;

namespace MusicXMLreconstructionEngineTests
{
    public class UnitTest1
    {
        [Fact]
        public void TestGClef()
        {
            var engine = new MusicXMLreconstructionEngine.MusicXMLreconstructionEngine();
            engine.ParseSymbol("clef.G-L2");

        }
    }
}
