using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMClasses.Interfaces
{
    public interface ITransponderParsing
    {
        List<string> TransponderParser(string transponderData);

    }
}
