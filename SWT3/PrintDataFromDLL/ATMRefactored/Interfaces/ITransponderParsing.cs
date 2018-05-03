using System.Collections.Generic;

namespace ATMRefactored.Interfaces
{
    public interface ITransponderParsing
    {
        List<string> TransponderParser(string transponderData);

    }
}
