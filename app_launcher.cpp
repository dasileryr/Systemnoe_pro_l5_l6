#include <iostream>
#include <string>
#include <cstdlib>
#include <limits>
#include <fstream>
#include <locale>

#ifdef __APPLE__
#include <unistd.h>
#endif

class AppLauncher {
public:
    void showMenu() {
        std::cout << "\n========================================\n";
        std::cout << "   Запуск приложений (Версия 1 - C++)\n";
        std::cout << "========================================\n\n";
        std::cout << "1. Блокнот (TextEdit)\n";
        std::cout << "2. Калькулятор\n";
        std::cout << "3. Paint (Preview)\n";
        std::cout << "4. Своё собственное приложение\n";
        std::cout << "0. Выход\n";
        std::cout << "\nВыберите опцию: ";
    }

    bool launchApplication(const std::string& appName, const std::string& displayName = "") {
        std::string command;
        
#ifdef __APPLE__
        // Для macOS используем команду open
        command = "open -a \"" + appName + "\"";
#elif _WIN32
        // Для Windows
        command = "start " + appName;
#else
        // Для Linux
        command = appName + " &";
#endif

        int result = system(command.c_str());
        
        if (result == 0) {
            std::string name = displayName.empty() ? appName : displayName;
            std::cout << "\n✓ " << name << " успешно запущен!\n";
            return true;
        } else {
            std::cout << "\n✗ Ошибка при запуске приложения: " << appName << "\n";
            return false;
        }
    }

    void launchNotepad() {
        std::cout << "\nЗапуск Блокнота (TextEdit)...\n";
        launchApplication("TextEdit", "Блокнот (TextEdit)");
    }

    void launchCalculator() {
        std::cout << "\nЗапуск Калькулятора...\n";
        launchApplication("Calculator", "Калькулятор");
    }

    void launchPaint() {
        std::cout << "\nЗапуск Paint (Preview)...\n";
        launchApplication("Preview", "Paint (Preview)");
    }

    void launchCustomApp() {
        std::cout << "\nВведите путь к приложению: ";
        std::string appPath;
        std::cin.ignore();
        std::getline(std::cin, appPath);

        if (appPath.empty()) {
            std::cout << "Путь не может быть пустым!\n";
            return;
        }

        // Проверка существования файла
        std::ifstream file(appPath);
        bool isApp = appPath.length() >= 4 && 
                     appPath.substr(appPath.length() - 4) == ".app";
        if (!file.good() && !isApp) {
            std::cout << "Предупреждение: Файл может не существовать.\n";
        }

        std::string command;
        
#ifdef __APPLE__
        bool isApp = appPath.length() >= 4 && 
                     appPath.substr(appPath.length() - 4) == ".app";
        command = "open \"" + appPath + "\"";
#elif _WIN32
        command = "\"" + appPath + "\"";
#else
        command = "\"" + appPath + "\" &";
#endif

        std::cout << "\nЗапуск приложения: " << appPath << "\n";
        int result = system(command.c_str());

        if (result == 0) {
            std::cout << "✓ Приложение успешно запущено!\n";
        } else {
            std::cout << "✗ Ошибка при запуске приложения.\n";
        }
    }

    void run() {
        int choice;
        
        while (true) {
            showMenu();
            std::cin >> choice;

            if (std::cin.fail()) {
                std::cin.clear();
                std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');
                std::cout << "\nОшибка: введите число!\n";
                continue;
            }

            switch (choice) {
                case 1:
                    launchNotepad();
                    break;
                case 2:
                    launchCalculator();
                    break;
                case 3:
                    launchPaint();
                    break;
                case 4:
                    launchCustomApp();
                    break;
                case 0:
                    std::cout << "\nВыход из программы...\n";
                    return;
                default:
                    std::cout << "\nНеверный выбор! Попробуйте снова.\n";
                    break;
            }

            std::cout << "\nНажмите Enter для продолжения...";
            std::cin.ignore();
            std::cin.get();
        }
    }
};

int main() {
    // Установка локали для поддержки кириллицы
    setlocale(LC_ALL, "ru_RU.UTF-8");
    
    AppLauncher launcher;
    launcher.run();
    
    return 0;
}

