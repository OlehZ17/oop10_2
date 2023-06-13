using System;
using System.IO;
using System.Text;

class Program
{
    static void Main()
    {
        // Завдання 1: Запис та читання з файлу з врахуванням зміщення
        FileStream fileStream = new FileStream("test.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);

        // Запис у файл
        string data1 = "Hello";
        byte[] dataBytes1 = Encoding.ASCII.GetBytes(data1);
        fileStream.Write(dataBytes1, 0, dataBytes1.Length);

        // Зчитування з файлу
        byte[] buffer = new byte[5];
        fileStream.Seek(0, SeekOrigin.Begin);
        fileStream.Read(buffer, 0, buffer.Length);
        string dataRead1 = Encoding.ASCII.GetString(buffer);
        Console.WriteLine("Data read from file: " + dataRead1);

        // Запис у файл з зміщенням
        string data2 = "World";
        byte[] dataBytes2 = Encoding.ASCII.GetBytes(data2);
        fileStream.Seek(5, SeekOrigin.Begin);
        fileStream.Write(dataBytes2, 0, dataBytes2.Length);

        // Зчитування з файлу зі зміщенням
        byte[] buffer2 = new byte[5];
        fileStream.Seek(5, SeekOrigin.Begin);
        fileStream.Read(buffer2, 0, buffer2.Length);
        string dataRead2 = Encoding.ASCII.GetString(buffer2);
        Console.WriteLine("Data read from file with offset: " + dataRead2);

        fileStream.Close();

        Console.WriteLine();

        // Завдання 2: Операції з бінарним файлом
        FileStream binaryFileStream = new FileStream("binaryFile.bin", FileMode.OpenOrCreate, FileAccess.ReadWrite);

        // Запис даних різних типів у файл
        int intValue = 42;
        binaryFileStream.Write(BitConverter.GetBytes(intValue), 0, sizeof(int));

        double doubleValue = 3.14;
        binaryFileStream.Write(BitConverter.GetBytes(doubleValue), 0, sizeof(double));

        string stringValue = "Hello World";
        byte[] stringBytes = Encoding.ASCII.GetBytes(stringValue);
        binaryFileStream.Write(stringBytes, 0, stringBytes.Length);

        // Перезапис раніше записаних даних
        binaryFileStream.Seek(0, SeekOrigin.Begin);
        int newIntValue = 123;
        binaryFileStream.Write(BitConverter.GetBytes(newIntValue), 0, sizeof(int));

        // Дописування даних в кінець файлу
        long endPosition = binaryFileStream.Length;
        binaryFileStream.Seek(endPosition, SeekOrigin.Begin);
        string additionalData = "Additional Data";
        byte[] additionalBytes = Encoding.ASCII.GetBytes(additionalData);
        binaryFileStream.Write(additionalBytes, 0, additionalBytes.Length);

        // Зчитування даних з файлу
        binaryFileStream.Seek(0, SeekOrigin.Begin);
        byte[] intBuffer = new byte[sizeof(int)];
        binaryFileStream.Read(intBuffer, 0, sizeof(int));
        int readIntValue = BitConverter.ToInt32(intBuffer, 0);
        Console.WriteLine("Read int value: " + readIntValue);

        byte[] doubleBuffer = new byte[sizeof(double)];
        binaryFileStream.Read(doubleBuffer, 0, sizeof(double));
        double readDoubleValue = BitConverter.ToDouble(doubleBuffer, 0);
        Console.WriteLine("Read double value: " + readDoubleValue);

        byte[] stringBuffer = new byte[binaryFileStream.Length - binaryFileStream.Position];
        binaryFileStream.Read(stringBuffer, 0, stringBuffer.Length);
        string readStringValue = Encoding.ASCII.GetString(stringBuffer);
        Console.WriteLine("Read string value: " + readStringValue);

        binaryFileStream.Close();

        Console.WriteLine();

        // Завдання 3: Операції з текстовим файлом
        string filePath = "textFile.txt";

        // Запис даних різних типів у файл
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("Line 1");
            writer.WriteLine("Line 2");
            writer.WriteLine("Line 3");
        }

        // Дописування даних в кінець файлу
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            writer.WriteLine("Line 4");
        }

        // Зчитування даних з файлу порціями по n символів з допомогою методу Read()
        int studentNumber = 2;
        using (StreamReader reader = new StreamReader(filePath))
        {
            char[] buffer3 = new char[studentNumber];
            reader.Read(buffer3, 0, studentNumber);
            string dataRead3 = new string(buffer3);
            Console.WriteLine("Data read from file with Read(): " + dataRead3);
        }

        // Зчитування вмісту файлу в буфер buffer за один раз
        using (StreamReader reader = new StreamReader(filePath))
        {
            char[] buffer4 = new char[reader.BaseStream.Length];
            reader.Read(buffer4, 0, buffer4.Length);
            string dataRead4 = new string(buffer4);
            Console.WriteLine("Data read from file in one go: " + dataRead4);
        }

        // Зчитування вмісту текстового файлу рядок за рядком і виведення на екран
        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                Console.WriteLine("Read line: " + line);
            }
        }

        // Посимвольне читання з файлу до кінця
        using (StreamReader reader = new StreamReader(filePath))
        {
            int character;
            while ((character = reader.Read()) != -1)
            {
                Console.Write((char)character);
            }
        }
    }
}

