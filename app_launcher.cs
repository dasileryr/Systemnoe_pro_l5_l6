using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace AppLauncher
{
    class Program
    {
        static void ShowMenu()
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("   Запуск приложений (Версия 2 - C#)");
            Console.WriteLine("========================================\n");
            Console.WriteLine("1. Блокнот (TextEdit)");
            Console.WriteLine("2. Калькулятор");
            Console.WriteLine("3. Paint (Preview)");
            Console.WriteLine("4. Своё собственное приложение");
            Console.WriteLine("0. Выход");
            Console.Write("\nВыберите опцию: ");
        }

        static bool LaunchApplication(string appName, string displayName = "")
        {
            try
            {
                ProcessStartInfo startInfo;

                if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    // Для macOS используем команду open
                    startInfo = new ProcessStartInfo
                    {
                        FileName = "/usr/bin/open",
                        Arguments = $"-a \"{appName}\"",
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    // Для Windows
                    startInfo = new ProcessStartInfo
                    {
                        FileName = appName,
                        UseShellExecute = true
                    };
                }
                else
                {
                    // Для Linux
                    startInfo = new ProcessStartInfo
                    {
                        FileName = appName,
                        UseShellExecute = true
                    };
                }

                Process.Start(startInfo);
                string name = string.IsNullOrEmpty(displayName) ? appName : displayName;
                Console.WriteLine($"\n✓ {name} успешно запущен!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ Ошибка при запуске приложения {appName}: {ex.Message}");
                return false;
            }
        }

        static void LaunchNotepad()
        {
            Console.WriteLine("\nЗапуск Блокнота (TextEdit)...");
            LaunchApplication("TextEdit", "Блокнот (TextEdit)");
        }

        static void LaunchCalculator()
        {
            Console.WriteLine("\nЗапуск Калькулятора...");
            LaunchApplication("Calculator", "Калькулятор");
        }

        static void LaunchPaint()
        {
            Console.WriteLine("\nЗапуск Paint (Preview)...");
            LaunchApplication("Preview", "Paint (Preview)");
        }

        static void LaunchCustomApp()
        {
            Console.Write("\nВведите путь к приложению: ");
            string appPath = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(appPath))
            {
                Console.WriteLine("Путь не может быть пустым!");
                return;
            }

            // Проверка существования файла
            if (!File.Exists(appPath) && !Directory.Exists(appPath))
            {
                Console.WriteLine("Предупреждение: Файл или директория могут не существовать.");
            }

            try
            {
                ProcessStartInfo startInfo;

                if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    if (appPath.EndsWith(".app"))
                    {
                        startInfo = new ProcessStartInfo
                        {
                            FileName = "/usr/bin/open",
                            Arguments = $"\"{appPath}\"",
                            UseShellExecute = false,
                            CreateNoWindow = true
                        };
                    }
                    else
                    {
                        startInfo = new ProcessStartInfo
                        {
                            FileName = "/usr/bin/open",
                            Arguments = $"\"{appPath}\"",
                            UseShellExecute = false,
                            CreateNoWindow = true
                        };
                    }
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    startInfo = new ProcessStartInfo
                    {
                        FileName = appPath,
                        UseShellExecute = true
                    };
                }
                else
                {
                    startInfo = new ProcessStartInfo
                    {
                        FileName = appPath,
                        UseShellExecute = true
                    };
                }

                Console.WriteLine($"\nЗапуск приложения: {appPath}");
                Process.Start(startInfo);
                Console.WriteLine("✓ Приложение успешно запущено!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Ошибка при запуске приложения: {ex.Message}");
            }
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            int choice;

            while (true)
            {
                ShowMenu();
                string input = Console.ReadLine();

                if (!int.TryParse(input, out choice))
                {
                    Console.WriteLine("\nОшибка: введите число!");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        LaunchNotepad();
                        break;
                    case 2:
                        LaunchCalculator();
                        break;
                    case 3:
                        LaunchPaint();
                        break;
                    case 4:
                        LaunchCustomApp();
                        break;
                    case 0:
                        Console.WriteLine("\nВыход из программы...");
                        return;
                    default:
                        Console.WriteLine("\nНеверный выбор! Попробуйте снова.");
                        break;
                }

                Console.WriteLine("\nНажмите Enter для продолжения...");
                Console.ReadLine();
            }
        }
    }
}

