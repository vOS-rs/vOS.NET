using System;
using System.Threading.Tasks;

namespace vOS.Application.Echo
{
    public class Program
    {
        public string Tag;

        public int Main(string[] args)
        {
            vOS.API.Instance.Load();
            var vArgs = vOS.API.Application.Arguments;

            vArgs[0] = string.Empty;

            System.Console.WriteLine(string.Join(" ", vArgs));

            //BackgroundSpin();

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
