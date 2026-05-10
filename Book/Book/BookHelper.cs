using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class BookHelper 
{
    private List<Book> _books;
    private string _path;

    public BookHelper(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException("Путь к файлу не задан.");
        }
        _path = filePath;
        _books = new List<Book>();
    }

    public string Path
    {
        get
        {
            return _path;
        }
        set
        {
            _path = value;
        }
    }

    public void LoadData()
    {
        _books.Clear();
        if (!File.Exists(_path))
        {
            return;
        }

        using (BinaryReader br = new BinaryReader(File.Open(_path, FileMode.Open)))
        {
            while (br.BaseStream.Position < br.BaseStream.Length)
            {
                Book b = new Book();
                b.Id = br.ReadInt32();
                b.Title = br.ReadString();
                b.Author = br.ReadString();
                b.Year = br.ReadInt32();
                b.Price = br.ReadDouble();
                b.IsAvailable = br.ReadBoolean();
                _books.Add(b);
            }
        }
    }

    public void SaveData()
    {
        using (BinaryWriter bw = new BinaryWriter(File.Open(_path, FileMode.Create)))
        {
            foreach (var b in _books)
            {
                bw.Write(b.Id);
                bw.Write(b.Title);
                bw.Write(b.Author);
                bw.Write(b.Year);
                bw.Write(b.Price);
                bw.Write(b.IsAvailable);
            }
        }
    }

    public List<Book> GetList() => _books;

    public bool IsIdExists(int id)
    {
        return _books.Any(b => b.Id == id);
    }

    public void Add(Book b)
    {
        if (_books.Any(x => x.Id == b.Id))
        {
            throw new Exception("Книга с таким ID уже существует" +
                " в базе.");
        }
        _books.Add(b);
        SaveData();
    }

    public bool Remove(int id)
    {
        int removedCount;
        removedCount = _books.RemoveAll(b => b.Id == id);
        if (removedCount > 0)
        {
            SaveData();
            return true;
        }
        return false;
    }

    public List<Book> GetByAuthor(string author)
    {
        if (string.IsNullOrWhiteSpace(author))
        {
            return new List<Book>();
        }
        return _books.Where(b => b.Author.IndexOf
        (author, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
    }

    public List<Book> GetByPriceRange(double min, double max)
    {
        if (min > max)
        {
            throw new ArgumentException("Минимальная цена не может" +
                " быть больше максимальной.");
        }
        return _books.Where(b => b.Price >= min && b.Price <= max).ToList();
    }

    public double GetAveragePrice()
    {
        return _books.Any() ? _books.Average(b => b.Price) : 0;
    }

    public int GetOldestYear()
    {
        return _books.Any() ? _books.Min(b => b.Year) : 0;
    }
}

