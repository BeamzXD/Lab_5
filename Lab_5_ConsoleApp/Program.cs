using System.Collections;

public class MyMatrix
{
    private int [,] matrix;
    private Random random = new Random();

    public int Rows { get; private set; }
    public int Cols { get; private set; }

    public MyMatrix(int rows, int cols, int minValue, int maxValue)
    {
        Rows = rows;
        Cols = cols;
        matrix = new int[Rows, Cols];
        Fill(minValue, maxValue);
    }

    public void Fill(int minValue, int maxValue){
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Cols; j++)
            {
                matrix[i, j] = random.Next(minValue, maxValue);
            }
        }
    }

    public void ChangeSize(int NewRows, int NewCols, int minValue, int maxValue)
    {
        int [,] mat = new int[NewRows, NewCols];
        int minRows = Math.Min(Rows, NewRows);
        int minCols = Math.Min(Rows, NewCols);

        for(int i = 0; i < minRows; i++)
        {
            for(int j = 0; j < minCols; j++)
            {  
                    mat[i,j] = matrix[i,j];
            }
        }

        for(int i = 0; i < NewRows; i++)
        {
            for(int j = minCols; j < NewCols; j++)
            {  
                if (i == NewRows - 1 && NewRows != minRows){
                    for (int k = 0; k < NewCols - 1; k++){
                        mat[i,k] = random.Next(minValue,maxValue);
                    }
                }
                mat[i,j] = random.Next(minValue,maxValue);
            }
        } 
        matrix = mat;
        this.Rows = NewRows;
        this.Cols = NewCols;
    }

    public void ShowPartialy(int startRow, int endRow, int startColumn, int endColumn){
        for (int i = startRow - 1; i < endRow; i++)
        {
            for (int j = startColumn - 1; j < endColumn; j++)
            {
                Console.Write($"{matrix[i, j]:F2}\t");
            }
            Console.WriteLine();
        }
    }

    public void Show()
    {
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Cols; j++)
            {
                Console.Write($"{matrix[i, j]:F2}\t");
            }
            Console.WriteLine();
        }
    }

    public double this[int row, int col]
    {
        get
        {
            if (row >= 0 && row < matrix.GetLength(0) && col >= 0 && col < matrix.GetLength(1))
            {
                return matrix[row, col];
            }
            else
            {
                throw new IndexOutOfRangeException("Неверные индексы для доступа к элементам матрицы");
            }
        }
        set
        {
            if (row >= 0 && row < matrix.GetLength(0) && col >= 0 && col < matrix.GetLength(1))
            {
                matrix[row, col] = (int)value;
            }
            else
            {
                throw new IndexOutOfRangeException("Неверные индексы для установки элементов матрицы");
            }
        }
    }
}


public class MyList<T> : IEnumerable<T>
{
    private T[] items; // Массив для хранения элементов
    private int count; // Текущее количество элементов

    // Конструктор по умолчанию
    public MyList(int size)
    {
        items = new T[size];
        count = 0;
    }

    public MyList(int size, IEnumerable<T> collection)
    {
        items = new T[size];
        count = 0;
        foreach (var item in collection)
        {
            Add(item);
        }
    }
    // Свойство для получения количества элементов
    public int Count => count;

    // Индексатор для получения или установки элемента по индексу
    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= count)
                throw new ArgumentOutOfRangeException();
            return items[index];
        }
        set
        {
            if (index < 0 || index >= count)
                throw new ArgumentOutOfRangeException();
            items[index] = value;
        }
    }

    // Метод для добавления элемента
    public void Add(T item)
    {
        if (count == items.Length)
        {
            Resize();
        }
        items[count++] = item;
    }

    // Метод для увеличения размера массива
    private void Resize()
    {
        T[] newArray = new T[items.Length + 1];
        for (int i = 0; i < items.Length; i++)
        {
            newArray[i] = items[i];
        }
        items = newArray;
    }

    // Реализация интерфейса IEnumerable<T> для возможности использования foreach
    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < count; i++)
        {
            yield return items[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class MyDictionary<TKey, TValue> : IEnumerable
{
    private TKey[] keys;
    private TValue[] values;
    private int count;

    public MyDictionary(int size)
    {
        keys = new TKey[size];
        values = new TValue[size];
        count = 0;
    }

    // Метод для добавления элемента
    public void Add(TKey key, TValue value)
    {
        if (count == keys.Length)
        {
            Array.Resize(ref keys, keys.Length + 1);
            Array.Resize(ref values, values.Length + 1);
        }

        keys[count] = key;
        values[count] = value;
        count++;
    }

    // Индексатор для получения значения по ключу
    public TValue this[TKey key]
    {
        get
        {
            for (int i = 0; i < count; i++)
            {
                if (keys[i].Equals(key))
                {
                    return values[i];
                }
            }
            throw new KeyNotFoundException("Ключ не найден.");
        }
    }

    // Свойство для получения количества элементов
    public int Count
    {
        get { return count; }
    }

    // Реализация интерфейса IEnumerable для поддержки foreach
    public IEnumerator GetEnumerator()
    {
        for (int i = 0; i < count; i++)
        {
            yield return new KeyValuePair<TKey, TValue>(keys[i], values[i]);
        }
    }
}



class Program{
    static void Main(){
        Console.WriteLine("Задаие №1");
        MyMatrix matrix = new MyMatrix(3, 3, 1, 10);

        matrix.Show();
        Console.WriteLine();
        matrix.ChangeSize(4, 4, 1, 10);
        matrix.Show();

        Console.WriteLine();
        matrix.ChangeSize(2, 2, 1, 10);
        matrix.Show();

        Console.WriteLine();
        matrix.ShowPartialy(1,1,2,2);
        Console.WriteLine();

        Console.WriteLine("Задаие №2");
        MyList<int> list = new MyList<int>(5) { 1, 2, 3, 4, 5 };
        list.Add(6);
        Console.WriteLine(list.Count);

        for (int i = 0; i < list.Count; i++)
        {
            Console.Write(list[i]);
        }
        Console.WriteLine();

        foreach (var item in list)
        {
            Console.Write(item);
        }
        Console.WriteLine();
        Console.WriteLine("Задаие №3");
        MyDictionary<string, int> dictionary = new MyDictionary<string, int>(2) {{"orange", 1}};
        dictionary.Add("apple", 1);
        dictionary.Add("banana", 2);
        Console.WriteLine(dictionary["apple"]);
        Console.WriteLine(dictionary.Count);

        foreach (var pair in dictionary)
        {
            Console.WriteLine(pair);
        }   
    }
}