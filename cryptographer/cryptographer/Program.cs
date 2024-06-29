namespace cryptographer
{
    internal class Program
    {

        /**
         * Перед использованием программы подпишите сборку и скомпилируйте. 
         * 
         * Измените переменные CompanyName и SecretKey случайным образом.
         * 
         **/

        const string CompanyName = "Введите сюда секретное слово или название компании";

        const string SecretKey = "bываhÆjﺸ☻kИꞤA2SF%D†b†kﺸsd$5fkῺ!luРwe?ЬfudfАgdппd6:%munfлkdslыꬺва№4;ksfdsl&ЧемДлинееТемЛучше";

        static void Main(string[] args)
        {
            var pass = string.Empty;
            pass = GetPassword();
            Console.WriteLine(pass);

            // Если задано необходимое количество аргументов
            if(args.Length == 3) {

                string inputFullFileName = args[0];
                string outputFullFileName = args[1];

                // Если это шифрование
                if (args[3].Equals("e") || args[3].Equals("E"))
                {

                }
                // Иначе это дешифрация
                else
                {

                }
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