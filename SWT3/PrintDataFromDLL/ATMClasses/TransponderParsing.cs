using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMClasses.Interfaces;

namespace ATMClasses
{
    public class TransponderParsing : ITransponderParsing
    {
        public List<string> TransponderParser(string transponderData)
        {
            List<string> transponderParts = transponderData.Split(';').ToList();
            return transponderParts;
        }
    }
}
