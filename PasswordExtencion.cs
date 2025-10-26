namespace laba2_3sem_9var_zad1
{
    public static class PasswordExtencion
    {
        // Метод выделения среднего символа строки
        public static char MiddleChar(this string _string)
        {
            if (string.IsNullOrEmpty(_string)) throw new ArgumentNullException(nameof(_string));
            return ( _string.Length % 2 == 0 ) ? _string[(_string.Length / 2) - 1] : _string[(_string.Length / 2)];
        }
        // Проверка допустимой длины пароля(6-12)
        public static bool AcceptPassLength(this Password password)
        {
            if (password.Length > Password.MaxPassLength || password.Length < Password.MinPassLength) return false;
            return true;
        }
    }
}