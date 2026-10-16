using System.Diagnostics;

namespace Threads
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var notepadThread = new Thread(() =>
            {
                Console.WriteLine("[Поток 1] Запущен");
                Console.WriteLine("[Поток 1] Запуск Блокнота");

                Process notepad = Process.Start("notepad.exe");
                Console.WriteLine("[Поток 1] Блокнот запущен");

                Thread.Sleep(3000);

                Console.WriteLine("[Поток 1] Закрытие Блокнота");

                notepad.CloseMainWindow();

                Console.WriteLine("[Поток 1] Блокнот Закрыт");
                Console.WriteLine("[Поток 1] Завершено");
            });

            ProcessStartInfo cmdStartInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = "/c dir",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Thread cmdThread = new Thread(() =>
            {
                Console.WriteLine("[Поток 2] Запущен");
                Console.WriteLine("[Поток 2] Запуск Командной строки");

                using (Process cmd = Process.Start(cmdStartInfo))
                {
                    string output = cmd.StandardOutput.ReadToEnd();
                    cmd.WaitForExit();
                    Console.WriteLine("[Поток 2] Результат выполнения: ");
                    Console.WriteLine("[Поток 2] ", output);
                }

                Console.WriteLine("[Поток 2] Завершено");
            });

            notepadThread.Start();

            cmdThread.Start();

            
        }
    }
}
