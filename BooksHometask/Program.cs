using System;
using System.ComponentModel;

/*1. Створити базовий абстрактний клас. Мінімум 2 властивості
 2. Створити два дочірніх класи. Мінімум 3 властивості і всі різних типів
 3. Створити масив (кількість єлементів вказує користувач)
 4. Дати користувачеві заповнити масив. Користувач має сам обрати який саме клас (з двох дочірніх) він зараз хоче створити. 
 5. Користувач у будь-який момент заповнення масива може переглянути уже заповнені елементи
*/

namespace hometask2_3_
{
    class Program
    {
        const string ChoosedFilmedBooks = "filmed";
        const string ChoosedNotFilmedBooks = "not-filmed";
        const string ChoosedDisplay = "display";
        const string Yes = "yes";

        static void Main(string[] args)
        {
            var shouldRepeat = false;
            var сhooseNewClass = false;
            Writings[] arr;
            int arraySize;

            do
            {
                shouldRepeat = false;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Enter the number of books");
                Console.ForegroundColor = ConsoleColor.White;

                if (!int.TryParse(Console.ReadLine(), out arraySize))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Enter the numeric value!");
                    shouldRepeat = true;
                }
            }
            while (shouldRepeat);

            arr = new Writings[arraySize];

            for (int i = 0; i < arr.Length; i++)
            {
                string choosedCommand;
                do
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Please check book.");
                    Console.WriteLine("If you`d to choose filmed book - enter \"filmed\"");
                    Console.WriteLine("If you`d to choose not-filmed book - enter \"not-filmed\"");
                    Console.WriteLine("Enter \"display\" for show your entered data");
                    Console.WriteLine("Enter your choose:");
                    Console.ForegroundColor = ConsoleColor.White;
                    choosedCommand = Console.ReadLine().ToLower();

                    switch (choosedCommand)
                    {
                        case ChoosedFilmedBooks:
                            var filmed = new Filmed();
                            filmed.FillSelf();
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            arr[i] = filmed;
                            if (IsContinue()) { сhooseNewClass = true; }
                            else if (!IsContinue()) {сhooseNewClass = false; }
                            break;
                        case ChoosedNotFilmedBooks:
                            var not_filmed = new Not_filmed();
                            not_filmed.FillSelf();
                            arr[i] = not_filmed;
                            if (IsContinue()) сhooseNewClass = true;
                            else if (!IsContinue()) сhooseNewClass = false;
                            break;
                        case ChoosedDisplay:
                            PrintArray(arr);
                            сhooseNewClass = false;
                            break;
                    }
                } while (!сhooseNewClass);
            }
            
        }

        static void PrintArray(object[] array)
        {
            for (int i = 0; i < array.Length; i ++) 
            {
                if (array[i] != null)
                {
                    Console.WriteLine(array[i].ToString());
                }
            }
        }

        static bool IsContinue()
        {
            Console.WriteLine("Do you want to continue work with this class? Enter \"yes/no\" ");
            Console.ForegroundColor = ConsoleColor.White;
            string isContinue = Console.ReadLine().ToLower();
            if (isContinue.Equals(Yes)) return true;
            else return false;
        }
    }

    abstract class Writings
    {
        public virtual string AuthorName { get; set; }
        public virtual int Year { get; set; }
        public virtual void FillSelf()
        {
            Console.WriteLine("Enter the author`s name::");
            this.AuthorName = Console.ReadLine();

            Console.WriteLine("Enter year of publishing:");
            this.Year = SafeSet<int>();
        }

        public T SafeSet<T>()
        {
            bool isOk = false;
            string input;
            var errorMessage = string.Format("Please enter correct type ({0})", typeof(T));
            TypeConverter converter = null;
            T returnValue = default(T);

            do
            {
                input = Console.ReadLine();
                try
                {
                    converter = TypeDescriptor.GetConverter(typeof(T));
                    returnValue = (T)converter.ConvertFromString(input);
                    isOk = true;
                }
                catch (Exception)
                {
                    isOk = false;
                    Console.WriteLine(errorMessage);
                }
            }
            while (!isOk);
            return returnValue;
        }
    }

    class Filmed : Writings
    {
        private string MainActorName { get; set; }
        private string Director { get; set; }
        private int Budget { get; set; }

        public override void FillSelf()
        {
            Console.WriteLine("Enter budget of film:");
            this.Budget = SafeSet<int>();

            Console.WriteLine("Enter the director`s name:");
            this.Director = Console.ReadLine();

            Console.WriteLine("Enter main actor`s name:");
            this.MainActorName = Console.ReadLine();
        }

        public override string ToString()
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            return string.Format("Author name:{0}, Year:{1}, Director: {2}, Budget: {3}, MainActorName: {4}",
                                  AuthorName, Year, Director, Budget, MainActorName);
        }
    }

    class Not_filmed : Writings
    {
        private int Page { get; set; }
        private int PurchasePrice { get; set; }
        public override void FillSelf()
        {
            base.FillSelf();

            Console.WriteLine("Enter the author`s name:");
            this.AuthorName = Console.ReadLine();

            Console.WriteLine("Enter the number of page:");
            this.Page = SafeSet<int>();

            Console.WriteLine("Enter price: ");
            this.PurchasePrice = SafeSet<int>();
        }

        public override string ToString()
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            return string.Format("Author name:{0}, Year:{1}, Page: {2}, PurchasePrice: {3}",
                                  AuthorName, Year, Page, PurchasePrice);
        }
    }
}

