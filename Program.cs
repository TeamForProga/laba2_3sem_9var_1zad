using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;

namespace laba2_3sem_9var_zad1
{
    
    public class Password
    {
        public const int MinPassLength = 6;
        public const int MaxPassLength = 12;
        // Популярные пароли для проверки пароля на стойкость
        private enum Standart_Passwords
        {
            [Description("123456")]Password1, [Description("qwerty1")] Password2, [Description("qwerty12")] Password3,
            [Description("qwerty123")] Password4, [Description("12345678")] Password5, [Description("QWERTY123")] Password6,
            [Description("qwertyuiop")] Password7, [Description("QwErTy123")] Password8, [Description("Qwerty123")] Password9,
            [Description("1234567")] Password10, [Description("QWERTYUIOP")] Password11, [Description("password")] Password12
        }
        // Элементы пароля
        char[] PassElements;
        

        // Длина пароля
        public int Length { get; set; }
        // Индексатор для элементов пароля
        public char this[int index]
        {
            get
            {
                if (index >= 0 && index < PassElements.Length)
                    return PassElements[index];
                else
                    throw new ArgumentOutOfRangeException();
            }
            set
            {
                if (index >= 0 && index < PassElements.Length)
                {
                    PassElements[index] = value;
                    Length = PassElements.Length;
                }
            }
        }
        // Конструктор
        public Password(char[] password)
        {
            if (password.Length < MinPassLength || password.Length > MaxPassLength)
                throw new Exception("Неправильная длина пароля(от 6 до 12)");
            foreach (char c in password)
            {
                if (char.IsWhiteSpace(c) || char.IsSeparator(c) || char.IsControl(c))
                    throw new Exception("Недопустимые символы в пароле.");
            }
            PassElements = password;
            Length = PassElements.Length;
        }
        // Конструктор по умолчанию
        public Password()
        {
            PassElements = "default".ToCharArray();
            Length = PassElements.Length;
        }
        public void Print()
        {
            foreach (char c in PassElements)
                Console.Write(c);
            Console.WriteLine();
        }
        // Перегруженный оператор - (password - item) замена последнего символа
        public static Password operator -(in Password password1, char item)
        {
            if (password1 is null) { throw new Exception(""); }
            password1[^1] = item;
            return password1;
        }
        // Перегруженный оператор > сравнение длин паролей 
        public static bool operator >(in Password password1, in Password password2)
        {
            return password1.Length > password2.Length;
        }
        // Парный перегруженный оператор < сравнение длин паролей  
        public static bool operator <(in Password password1, in Password password2)
        {
            return password1.Length < password2.Length;
        }
        // Перегруженный оператор != - проверка паролей на неравенство
        public static bool operator !=(in Password password1, in Password password2)
        {
            bool key = false;
            int index = 0;
            while (key != true && index < password1[index] && index < password2[index])
            {
                if (password1[index] != password2[index]) key = true;
                index++;
            }
            return key;
        }
        // Парный перегруженный оператор == - проверка паролей на неравенство
        public static bool operator ==(in Password password1, in Password password2)
        {
            bool key = true;
            int index = 0;
            while (key != false && index < password1[index] && index < password2[index])
            {
                if (password1[index] != password2[index]) key = false;
                index++;
            }
            return key;
        }
        // Перегруженный оператор ++ сброс пароля на значение по умолчанию
        public static Password operator ++(in Password password)
        {
            return new Password();
        }
        // Перегруженнный оператор true - проверка пароля на стойкость
        public static bool operator true(in Password password)
        {
            string pass = (string)password;
            bool digit = false, upper = false, punctuation = false, symbol = false ; 

            foreach (var standart_pass in Enum.GetValues<Standart_Passwords>())
            {
                if (pass == standart_pass.ToString()) return false;
            }
            foreach(char c  in pass.ToCharArray())
            {
                if (char.IsDigit(c)) digit = true;
                if (char.IsUpper(c)) upper = true;
                if (char.IsPunctuation(c)) { punctuation = true; symbol = true; } 
                if (char.IsSymbol(c)) { symbol = true; punctuation = true; }
            }
            return digit & upper & punctuation & symbol;
        }
        
        // Парный перегруженнный оператор false - проверка пароля на стойкость
        public static bool operator false(in Password password)
        {
            if (password) return false;
            else return true; 
         }
        // Перегруженный оператор преобразования типа Password -> string
        public static implicit operator string(Password password)
        {
            string pass = new(password.PassElements);
            return pass;
        }
        private char EnterChar(int k)
        {
            if(k == 1) Console.WriteLine("Введите символ для замены: ");
            char symbol = 'a';
            return symbol;
        }
        

    }
    public static class PasswordExtencion
    {
        public static char MiddleChar(this string _string)
        {
            if (string.IsNullOrEmpty(_string)) throw new ArgumentNullException(nameof(_string));
            return ( _string.Length % 2 == 0 ) ? _string[_string.Length / 2] : _string[_string.Length / 2 + 1];
        }
        public static bool AcceptPassLength(this Password password)
        {
            if (password.Length > Password.MaxPassLength || password.Length < Password.MinPassLength) return false;
            return true;
        }
    }

    public class Program
    {
        static void Main()
        {
            Password password1 = new Password();
            Password password2 = new Password("Abc@123".ToCharArray());

            Console.WriteLine(" === Проверка оператора - - замена последнего символа (password - item) === \n");
            Console.Write($"Пароль до: ");
            password1.Print();
            char item = 'Z';
            password1 = password1 - item;
            Console.Write($"Пароль после замены на {item}: ");
            password1.Print();
            Console.WriteLine("\n === Проверка оператора  > - сравнение длин паролей === \n");
            Console.Write($"Пароль {(string)password1} {(password1 > password2 ? "длиннее" : "не длиннее")} чем {(string)password2}");

            Console.WriteLine("\n\n === Проверка оператора  != - проверка паролей на неравенство === \n");
            Console.Write($"Пароль {(string)password1} {(password1 != password2 ? "не равен" : "равен")} {(string)password2}");
            

            Console.WriteLine("\n\n === Проверка оператора  ++ сброс пароля на значение по умолчанию === \n");
            password1++;
            Console.Write("Сброшенный пароль: ");
            password1.Print();

            Console.WriteLine("\n === Проверка оператора  true - проверка пароля на стойкость === \n");
            Console.WriteLine($"Пароль {(string)password1} {(password1 ? "надежен" : "не надежен")}");
            Console.Write($"Пароль {(string)password2} {(password2 ? "надежен" : "не надежен")}\n");

        }
    }
}