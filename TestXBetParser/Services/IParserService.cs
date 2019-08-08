
using TestXBetParser.Model;

namespace TestXBetParser.Services
{
    public interface IParserService
    {
        void PrintData();
        void SeleniumParse();
        Match GetMatchById(int id);
    }
}
