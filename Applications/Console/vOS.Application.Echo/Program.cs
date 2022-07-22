using System;
using System.Threading.Tasks;
using vOS.API;

namespace vOS.Application.Echo
{
    public class Program
    {
        public string Tag;

        public int Main(string[] args)
        {
            //var args = API.Application.Arguments;

            System.Console.WriteLine(Tag);
            Tag = Guid.NewGuid().ToString();
            System.Console.WriteLine(Tag);

            args[0] = string.Empty;

            System.Console.WriteLine(string.Join(" ", args));

            BackgroundSpin();

            return 0;
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
