using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMClasses
{
    public class TransponderParsing
    {
        public static List<string> TransponderParser(string transponderData)
        {
            List<string> transponderParts = transponderData.Split(';').ToList();
            return transponderParts;
        }
    }
}
