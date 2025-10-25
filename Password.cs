using System.ComponentModel;

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
        char[] _PassElements;
        

        // Длина пароля
        public int Length { get; set; }
        // Индексатор для элементов пароля
        public char this[int index]
        {
            get
            {
                if (index >= 0 && index < _PassElements.Length)
                    return _PassElements[index];
                else
                    throw new ArgumentOutOfRangeException(nameof(index),"Неверный индекс пароля");
            }
            set
            {
                if (index >= 0 && index < _PassElements.Length)
                {
                    _PassElements[index] = value;
                    Length = _PassElements.Length;
                }
            }
        }
        // Конструктор
        public Password(char[] password)
        {
            ArgumentNullException.ThrowIfNull(password);
            if (password.Length < MinPassLength || password.Length > MaxPassLength)
                throw new ArgumentException("Неправильная длина пароля(от 6 до 12)");
            if (password.Any(c => char.IsWhiteSpace(c) || char.IsSeparator(c) || char.IsControl(c)))
                throw new ArgumentException("Недопустимые символы в пароле.");
            _PassElements = password;
            Length = _PassElements.Length;
        }
        // Конструктор по умолчанию
        public Password()
        {
            _PassElements = "default".ToCharArray();
            Length = _PassElements.Length;
        }
        public void Print()
        {
            foreach (char c in _PassElements)
                Console.Write(c);
            Console.WriteLine();
        }
        // Перегруженный оператор - (password - item) замена последнего символа
        public static Password operator -(in Password password, char item)
        {
            ArgumentNullException.ThrowIfNull(password);

            password[^1] = item;
            return password;
        }
        // Перегруженный оператор > сравнение длин паролей 
        public static bool operator >(in Password password1, in Password password2)
        {
            ArgumentNullException.ThrowIfNull(password1);
            ArgumentNullException.ThrowIfNull(password2);

            return password1.Length > password2.Length;
        }
        // Парный перегруженный оператор < сравнение длин паролей  
        public static bool operator <(in Password password1, in Password password2)
        {
            ArgumentNullException.ThrowIfNull(password1);
            ArgumentNullException.ThrowIfNull(password2);

            return password1.Length < password2.Length;
        }
        // Перегруженный оператор != - проверка паролей на неравенство
        public static bool operator !=(in Password password1, in Password password2)
        {
            if (password1 is null || password2 is null) return true;

            if (password1.Length != password2.Length) return true;
            
            for (int index = 0; index < password1[index]; index++)
            {
                if (password1[index] != password2[index]) return true;
            }
            return false;
        }
        // Парный перегруженный оператор == - проверка паролей на неравенство
        public static bool operator ==(in Password password1, in Password password2)
        {
            return !(password1 != password2);
        }
        // Перегруженный оператор ++ сброс пароля на значение по умолчанию
        public static Password operator ++(in Password password)
        {
            return new Password();
        }
        // Перегруженнный оператор true - проверка пароля на стойкость
        public static bool operator true(in Password password)
        {
            if (password == null) return false;
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
            if (password == null) return string.Empty;
            string pass = new(password._PassElements);
            return pass;
        }
        private char EnterChar(int k)
        {
            if(k == 1) Console.WriteLine("Введите символ для замены: ");
            char symbol = 'a';
            return symbol;
        }
        

    }
}