using System;
using System.IO;

/// <summary>
/// Напишите программу, которая чистит нужную нам папку от файлов  и папок, которые не использовались более 30 минут 
/// На вход программа принимает путь до папки. 
/// При разработке постарайтесь предусмотреть возможные ошибки (нет прав доступа, папка по заданному адресу не существует, передан некорректный путь)
/// и уведомить об этом пользователя.
/// </summary>
namespace CleanerFolder
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Введите путь до папки");
                string path = Console.ReadLine();
                Cleaner.CleanerFolder(path);
            }
            catch(Exception ex)
            {
                //Выводим текст исключения в консоль
                Console.WriteLine("Ошибка очистки" + ex.Message);
            }
        }
    }
    static class Cleaner
    {
        /// <summary>
        /// Функция очистки неиспользуемых файлов
        /// </summary>
        /// <param name="path">путь до паки</param>
        static public void CleanerFolder(string path)
        {
            //Создали экземпляр класса для работы с директорией
            DirectoryInfo directoryInfo = new DirectoryInfo(path);

            //ПРоверили, что директория существует
            if (!directoryInfo.Exists)
                throw new ArgumentException("Папка не существует");

            //Выгрузили информацию по всем файлам в директории
            var files = directoryInfo.GetFiles();
            //Проверили время последнего использования по каждому файлу и удалили старые
            foreach (var file in files)
            {
                if (file.LastAccessTime + TimeSpan.FromMinutes(30) < DateTime.Now)
                    file.Delete();
            }

            //Выгрузили информацию по всем директориям в директории
            var directories = directoryInfo.GetDirectories();
      
            foreach (var directory in directories)
            {
                //для каждой директории запустили очиститель (чтобы удалить файлы и папки внутри)
                CleanerFolder(directory.FullName);

                //Проверили время последнего использования по каждой директории и удалили старые
                if (directory.LastAccessTime + TimeSpan.FromMinutes(30) < DateTime.Now)
                {
                    directory.Delete();
                }
                    
            }

        }
    }
}
