using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace MusicXMLreconstructionEngine
{
    public class MusicXMLreconstructionEngine
    {
        public static class Constants
        {
            public const string CLEF = "clef";
            public const string DIGIT = "digit";
        }

        public scorepartwise Scorepartwise { get; }

        public MusicXMLreconstructionEngine(string templatePath = @".\Templates\Partwise3.1.musicxml")
        {
            XmlSerializer ser = new XmlSerializer(typeof(scorepartwise));


            //https://stackoverflow.com/questions/13854068/dtd-prohibited-in-xml-document-exception
            //settings.ProhibitDtd is now obsolete,
            //using DtdProcessing instead: (new options of Ignore, Parse, or Prohibit)
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;

            using (XmlReader reader = XmlReader.Create(templatePath, settings))
            {
                Scorepartwise = (scorepartwise)ser.Deserialize(reader);
            }
        }

        public void ParseSymbol(string textSymbol)
        {
            var textSymbolSplit = textSymbol.Split('.');
            if (textSymbolSplit.Count() != 2)
            {
                throw new ArgumentException();
            }

            switch (textSymbolSplit.First())
            {
                case Constants.CLEF:
                    SetClef(textSymbolSplit.Last());
                    break;
                case Constants.DIGIT:
                    SetMeasure(textSymbolSplit.Last());
                    break;
                default:
                    throw new NotImplementedException();
            }


        }

        private void SetMeasure(string digit)
        {
            var digitSplit = digit.Split('-');
            if (digitSplit.Count() != 2)
            {
                throw new ArgumentException();
            }

            switch (digitSplit.Last())
            {
                case "L4":
                    ((attributes) Scorepartwise.part[0].measure[0].Items[1]).time[0].Items[0] 
                        = digitSplit.First();
                    break;
                case "L2":
                    ((attributes)Scorepartwise.part[0].measure[0].Items[1]).time[0].Items[1]
                        = digitSplit.First();
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private void SetClef(string clef)
        {
            var clefSplit = clef.Split('-');
            if (clefSplit.Count() != 2)
            {
                throw new ArgumentException();
            }

            ((attributes) Scorepartwise.part[0].measure[0].Items[1]).clef[0].line = clefSplit.Last();
            switch (clefSplit.Last())
            {
                case "G":
                    ((attributes) Scorepartwise.part[0].measure[0].Items[1]).clef[0].sign = clefsign.G;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

    }
}
