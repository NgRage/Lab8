using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class Book
{
    private int _id;
    private string _title;
    private string _author;
    private int _year;
    private double _price;
    private bool _isAvailable;

    public Book() 
    {
    
    }

    public Book(int id, string title, string author, int year, double price, bool isAvailable)
    {
        _id = id;
        _title = title;
        _author = author;
        _year = year;
        _price = price;
        _isAvailable = isAvailable;
    }

    public int Id
    {
        get 
        {
            return _id;
        }
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("ID должен быть больше 0.");
            }
            _id = value;
        }
    }
    public string Title
    {
        get 
        {
            return _title;
        }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Название не может быть" +
                    " пустым.");
            }
            _title = value;
        }
    }
    public string Author
    {
        get
        {
            return _author;
        }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Автор не может быть" +
                    " пустым.");
            }
            _author = value;
        }
    }
    public int Year
    {
        get 
        {
            return _year;
        }
        set
        {
            if (value > DateTime.Now.Year)
            {
                throw new ArgumentException($"Год должен быть в диапазоне" +
                    $" от 1000 до {DateTime.Now.Year}.");
            }
            _year = value;
        }
    }
    public double Price
    {
        get
        {
            return _price;
        }
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Цена не может " +
                    "быть отрицательной.");
            }
            _price = value;
        }
    }
    public bool IsAvailable
    {
        get 
        {
            return _isAvailable;
        }
        set
        {
            _isAvailable = value;
        }
    }

    public override string ToString()
    {
        string status;
        status = _isAvailable ? "Да" : "Нет";
        return $"[ID: {_id}] '{_title}' | Автор: {_author} |" +
            $" Год: {_year} | Цена: {_price:F2} | В наличии: {status}";
    }
}


