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
                    Console.WriteLine("[Поток 2] " + output);
                }

                Console.WriteLine("[Поток 2] Завершено");
            });

            notepadThread.Start();

            cmdThread.Start();

            
        }
    }
}
//Гонка данных
//Гонка данных - это проблема многопоточного программирования, которая возникает когда несколько потоков пытаются обратиться к одному ресурсу,
//при этом итоговый результат этих операций всегда разный, тюк потоки получают этот ресурс в неизвестнои состоянии
/*
 * int counter = 0;
 * 
 * thread1
 * {
 *      for(1000)
 *      {
 *          counter++;
 *      }
 * }
 * thread2
 * {
 *      for(1000)
 *      {
 *          counter++;
 *      }
 * }
 * 
 * Атомарность - неделимоть. Атомарная операция - это операция, которую нельзя поделить, она выполняется с начала и до конца без прерываний.
 * 
 * Видимость изменений - это вторая проблема конкурентности. Она подразумевает неоднозначную видимость изменений между потоками, 
 * когда один поток изменяет данные а другой читает их.
 * 
 * Синхронизация потоков - это процесс который позволяет потокам синхронизировать свою работу через специализированные инструменты.
 * Благодаря этому потоки могут детерминированно работать с одним ресурсом.
 * 
 */