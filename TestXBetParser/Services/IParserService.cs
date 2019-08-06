using System.Collections.Generic;
using System.Threading.Tasks;
using TestXBetParser.Model;

namespace TestXBetParser.Services
{
    public interface IParserService
    {
        Task ParseMatchesData();
        void PrintData();
    }
}
