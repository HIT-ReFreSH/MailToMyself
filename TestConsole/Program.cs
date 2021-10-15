using System.Threading.Tasks;
using HitRefresh.MailToMyself;
using PlasticMetal.MobileSuit;

namespace TestConsole
{
    internal class Program
    {
        [SuitAlias("smt")]
        public async void SendMailTest(string options, string password)
        {
            var server = new LoopbackMail(LoopbackMailOptions.Parse(options), password);
            await server.SendAsync("Test", "TEST");
        }
        static async Task Main(string[] args)
        {
            Suit.GetBuilder().UsePowerLine().Build<Program>().Run();
        }
    }
}
