using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Goof(string[] args)
        {
            string userName = Environment.UserName;
            Console.WindowWidth = 90;
            Console.Title = userName + "   COCK ";
        }

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte virtualKey, byte scanCode, int flags, IntPtr extraInfo);

        static void Main(string[] args)
            
        {
            
            Console.WriteLine(@"██████  ███████  ██████ ██   ██ ██   ██  ██████  ██████  
██   ██ ██      ██      ██  ██  ██   ██ ██    ██ ██   ██ 
██   ██ █████   ██      █████   ███████ ██    ██ ██████  
██   ██ ██      ██      ██  ██  ██   ██ ██    ██ ██      
██████  ███████  ██████ ██   ██ ██   ██  ██████  ██      ");
            Console.ReadKey();

            bool toggle = false;
            byte toggleKey = 0x54; // T key by default
            int delay = 1000; // 1 second by default

            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Start/Stop toggling space bar");
                Console.WriteLine("2. Set toggle key");
                Console.WriteLine("3. Set delay in milliseconds");
                Console.WriteLine("4 Options");
                Console.WriteLine("5. Exit");
                Console.WriteLine();
                Console.Write("Enter your choice: ");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        toggle = !toggle;
                        Console.WriteLine("Toggling space bar: " + (toggle ? "ON" : "OFF"));
                        break;

                    case 2:
                        Console.Write("Enter the new toggle key (hexadecimal): ");
                        toggleKey = Convert.ToByte(Console.ReadLine(), 16);
                        Console.WriteLine("Toggle key set to: 0x" + toggleKey.ToString("X2"));
                        break;

                    case 3:
                        Console.Write("Enter the new delay in milliseconds: ");
                        delay = int.Parse(Console.ReadLine());
                        Console.WriteLine("Delay set to: " + delay + " ms");
                        break;

                    case 4:
                        Console.Clear() ;
                        Console.WriteLine(@"");
                        break;
                        
                    case 6:

                        break;


                        break;

                    case 5:
                        Console.WriteLine("Exiting program...");
                        Environment.Exit(0);
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine("Press " + (char)toggleKey + " to toggle space bar");

                if (toggle)
                {
                    Task.Run(() =>
                    {
                        while (toggle)
                        {
                            keybd_event(0x20, 0, 0, IntPtr.Zero); // Press space bar
                            keybd_event(0x20, 0, 0x0002, IntPtr.Zero); // Release space bar
                            Thread.Sleep(delay);
                        }
                    });
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
            }
        }
    }
}