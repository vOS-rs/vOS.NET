using System.Diagnostics;
using System.Threading.Tasks;
using vOS.API;

namespace vOS.Application.Echo
{
    public class Program
    {
        public string Tag;

        public static void vMain()
        {
            var vArgs = API.Application.Arguments;

            vArgs[0] = string.Empty;

            Console.WriteLine(string.Join(" ", vArgs));

            //BackgroundSpin();

            System.Console.Out.WriteLine();
            Process.GetCurrentProcess().
        }

        private async void BackgroundSpin()
        {
            while (true)
            {
                await Task.Delay(1000);
                System.Console.WriteLine(Tag);
            }
        }
    }
}
