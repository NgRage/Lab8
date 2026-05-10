using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        BookHelper helper = new BookHelper("library.dat");
        string choice = "";
        bool isRunning = true;

        bool isFieldValid = false;
        Book newBook = null;
        string inputStr = "";

        int tempId = 0;
        int tempYear = 0;
        double tempPrice = 0;
        double p1 = 0;
        double p2 = 0;
        List<Book> results = null;

        try
        {
            helper.LoadData();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка загрузки БД: " + ex.Message);
        }

        while (isRunning)
        {
            Console.WriteLine("\n1. Список всех книг");
            Console.WriteLine("2. Добавить новую книгу");
            Console.WriteLine("3. Удалить по ID");
            Console.WriteLine("4. Запрос 1: Поиск по автору");
            Console.WriteLine("5. Запрос 2: Диапазон цен");
            Console.WriteLine("6. Запрос 3: Средняя стоимость");
            Console.WriteLine("7. Запрос 4: Самый старый год издания");
            Console.WriteLine("0. Выход");
            Console.Write("Выбор: ");

            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    results = helper.GetList();
                    if (results.Count == 0)
                    {
                        Console.WriteLine("База пуста.");
                    }
                    else
                    {
                        foreach (var b in results)
                        {
                            Console.WriteLine(b);
                        }
                    }
                    break;

                case "2":
                    newBook = new Book();

                    isFieldValid = false;
                    while (!isFieldValid)
                    {
                        Console.Write("Введите ID: ");
                        try
                        {
                            tempId = int.Parse(Console.ReadLine());
                            if (helper.IsIdExists(tempId))
                            {
                                Console.WriteLine("Ошибка: Книга с таким " +
                                    "ID уже есть в базе!");
                            }
                            else
                            {
                                newBook.Id = tempId;
                                isFieldValid = true;
                            }
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Ошибка: Введено не" +
                                " целое число!");
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }

                    isFieldValid = false;
                    while (!isFieldValid)
                    {
                        Console.Write("Название: ");
                        try
                        {
                            newBook.Title = Console.ReadLine();
                            isFieldValid = true;
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    isFieldValid = false;
                    while (!isFieldValid)
                    {
                        Console.Write("Автор: ");
                        try
                        {
                            newBook.Author = Console.ReadLine();
                            isFieldValid = true;
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }

                    isFieldValid = false;
                    while (!isFieldValid)
                    {
                        Console.Write("Год: ");
                        try
                        {
                            tempYear = int.Parse(Console.ReadLine());
                            newBook.Year = tempYear;
                            isFieldValid = true;
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Ошибка: Введено не" +
                                " целое число!");
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }

                    isFieldValid = false;
                    while (!isFieldValid)
                    {
                        Console.Write("Цена: ");
                        try
                        {
                            tempPrice = double.Parse(Console.ReadLine());
                            newBook.Price = tempPrice;
                            isFieldValid = true;
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Ошибка: Введено не число!");
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }

                    isFieldValid = false;
                    while (!isFieldValid)
                    {
                        Console.Write("В наличии (1-да, 0-нет): ");
                        inputStr = Console.ReadLine();
                        if (inputStr == "1")
                        {
                            newBook.IsAvailable = true;
                            isFieldValid = true;
                        }
                        else if (inputStr == "0")
                        {
                            newBook.IsAvailable = false;
                            isFieldValid = true;
                        }
                        else
                        {
                            Console.WriteLine("Ошибка: Введите строго" +
                                " 1 или 0.");
                        }
                    }

                    try
                    {
                        helper.Add(newBook);
                        Console.WriteLine("Книга успешно добавлена!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;

                case "3":
                    Console.Write("ID для удаления: ");
                    try
                    {
                        tempId = int.Parse(Console.ReadLine());
                        if (helper.Remove(tempId))
                        {
                            Console.WriteLine("Запись удалена.");
                        }
                        else
                        {
                            Console.WriteLine("Запись с таким ID не найдена.");
                        }
                    }
                    catch (FormatException) { Console.WriteLine("Ошибка: Введено не целое число!"); }
                    break;

                case "4":
                    Console.Write("Введите автора: ");
                    inputStr = Console.ReadLine();
                    results = helper.GetByAuthor(inputStr);
                    if (results.Count == 0)
                    {
                        Console.WriteLine("Книги не найдены.");
                    }
                    else
                    {
                        foreach (var b in results)
                        {
                            Console.WriteLine(b);
                        }
                    }
                    break;

                case "5":
                    try
                    {
                        Console.Write("Цена ОТ: ");
                        p1 = double.Parse(Console.ReadLine());
                        Console.Write("Цена ДО: ");
                        p2 = double.Parse(Console.ReadLine());
                        results = helper.GetByPriceRange(p1, p2);
                        if (results.Count == 0)
                        {
                            Console.WriteLine("Книги не найдены.");
                        }
                        else
                        {
                            foreach (var b in results)
                            {
                                Console.WriteLine(b);
                            }
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Ошибка: Введено не число!");
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;

                case "6":
                    Console.WriteLine($"Средняя цена книг:" +
                        $" {helper.GetAveragePrice():F2}");
                    break;

                case "7":
                    Console.WriteLine($"Год самой старой книги:" +
                        $" {helper.GetOldestYear()}");
                    break;

                case "0":
                    isRunning = false;
                    break;

                default:
                    Console.WriteLine("Неверный ввод меню.");
                    break;
            }
        }
    }
}