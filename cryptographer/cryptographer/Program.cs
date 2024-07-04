namespace Cryptographer
{
    internal class Program
    {

        /**
         * Перед использованием программы подпишите сборку и скомпилируйте.
         *
         * Аргумент 0 - входной файл. При шифровании - это не зашифрованный файл. При расшифровании - это зашифрованный файл. 
         * Аргумент 1 - выходной файл. При шифровании - это зашифрованный файл. При расшифровании - это не зашифрованный файл.
         * Аргумент 2 - Проводится шифрация? Если проводится шифрация, то значение аргумента должно быть "e" или "E".
         *   Если профодится дешифрация, то должно быть указано значение "d" или "D".
         * Аргумент 3 - Перезаписывать ли выходной зашифрованный файл, если указанный соответствующий файл существует на диске.
         *   true - перезаписывать, false - выдать ошибку и приостановить работу.
         * Аргумент 4 - Пароль Unicode. Оно же ключ шифрования. Не обязательный параметр, если его не ввести, то запроситься
         * пароль, так чтобы его никто не подсмотрел на мониторе.
         *
         *********************************************************************************************************************** 
         * Примеры вызова программы:
         *  1:
         *  2:
         *  3:
         *  4:
         ***********************************************************************************************************************
         * Коды ошибок программы:
         * 0 - Всё прошло отлично!
         * Отличное от 0 является ошибкой:
         * 1 - Аргумент 2 задан не верно.
         * 2 - Аргумент 3 задан не верно.
         * 3 - Количество аргументов задано не верно. Их должно быть 5 или 4.
         * 4 - Входной файл не существует.
         * 5 - Выходной файл не может быть перезаписан.
         **/
        static int Main(string[] args)
        {
            // Запросить пароль, если он не задан в аргументах.
            var pass = string.Empty;
            if (args.Length < 5) {
                Console.WriteLine("Enter the key of crypting: ");
                pass = GetPassword();
                Console.WriteLine();           
            }

            // Если задано необходимое количество аргументов
            if(args.Length == 4) {

                // Имя входного файла.
                string inputFullFileName = args[0];
                // Имя выходного файла.
                string outputFullFileName = args[1];
                // Возможно перезаписать выходной файл?
                bool isOutputOverridable;
                bool isEncryption;

                // Если это шифрование
                if (args[2].Equals("e") || args[2].Equals("E"))
                {
                    isEncryption = true;
                }
                // иначе это дешифрация
                if (args[2].Equals("d") || args[2].Equals("D"))
                {
                    isEncryption = false;
                }
                // иначе ошибка
                else {
                    Console.WriteLine("Error: Argument 3 is wrong! It must have value in (e, E, d, D). e or E - It is encription, d or D - decription.");
                    return 1;
                }

                if(!bool.TryParse(args[3], out isOutputOverridable))
                {
                    /*
                        Если файл "inputFullFileName" не существует (не существует файл для шифрования). То это ошибка, приложение прерывается и выводит сообщение.
                        Если файл "outputFullFileName" не существует на диске то это правильно. Если он существует, то
                        перезапись возможна только с аргументом 3.
                    */
                    if(File.Exists(inputFullFileName)) {
                        if(File.Exists(outputFullFileName)) {
                            if(!isOutputOverridable) {
                                Console.WriteLine("Error: Output file can't be overwritten!");
                                return 5;
                            }
                        }
                    }
                    else {
                        Console.WriteLine("Error: Input file doesn't exist!");
                        return 4;
                    }

                    if(isOutputOverridable)
                        Console.WriteLine("The output file wouldn be rewritten.");  
                    else
                        Console.WriteLine("The output file wouldn't be rewritten.");
                }
                else {
                    Console.WriteLine("Error: Argument 4 is wrong! It must have value in (e, E, d, D). e or E - It is encription, d or D - decription.");
                    return 2;
                }

                // Берём из аргументов пароль.
                if(args.Length == 5) {
                    pass = args[4];
                }

                Cryptographer.Crypt(pass, inputFullFileName, outputFullFileName, isEncryption);
                return 0;
            }
            else {
                return 3;
            }
        }

        private static string GetPassword()
        {
            var pass = string.Empty;
            ConsoleKey key;
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    Console.Write("\b \b");
                    pass = pass[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    pass += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);
            return pass;
        }
    }
}