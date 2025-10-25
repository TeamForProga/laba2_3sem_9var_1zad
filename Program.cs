using System.Linq.Expressions;
using System.Text;

namespace laba2_3sem_9var_zad1
{
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