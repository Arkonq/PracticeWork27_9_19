using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using ClassLib;
using Newtonsoft.Json;

namespace PracticeWork27_9_19
{
	class Program
	{
		static void CSV_Read_Demo()
		{
			/*
				●	Из csv файла (имя; фамилия; телефон; год рождения) прочитать записи в коллекцию. 
					Каждая запись коллекции должна иметь тип Person. 
					Сериализовать коллекцию с помощью класса SoapFormatter и сохранить в файл.
			*/

			List<Person> people = new List<Person>();

			string[] line;
			StreamReader streamReader = new StreamReader("data.csv");
			string data = streamReader.ReadLine();
			while ((data = streamReader.ReadLine()) != null)
			{
				line = data.Split(',');
				people.Add(new Person
				{
					FirstName = line[0],
					LastName = line[1],
					PhoneNum = long.Parse(line[2]),
					Year = Int32.Parse(line[3])
				});
			}

			SoapFormatter serializer = new SoapFormatter();
			using (FileStream fs = new FileStream("people.soap", FileMode.OpenOrCreate))
			{
				serializer.Serialize(fs, people);
			}

			//	●	Самостоятельно рассмотреть библиотеку Newtonsoft.Json и сериализовать коллекцию в json файл.

			string json = JsonConvert.SerializeObject(people);
			var result = JsonConvert.DeserializeObject<List<Person>>(json);
			;
		}


		static void Main()
		{
			CSV_Read_Demo();
			Practicework();
			Console.WriteLine("Done!");
		}

		private static void Practicework()
		{
			/*	
				●	Создать библиотеку классов с именем «ClassLib». 
				○	В библиотеке «ClassLib» создать класс с именем «РС», описывающий компьютер. Данный класс должен включать:  
				■	3–4 поля (марка, серийный номер и т.д.),  
				■	свойства (к каждому полю),  
				■	конструкторы (по умолчанию, с параметрами),  
				■	методы, моделирующие функционирование ПЭВМ (включение/выключение, перегрузку). 
				○	Создать новый проект (тип — консольное приложение) с именем «SerializConsolApp». Добавить ссылку на библиотеку «ClassLib».
				○	В функции Main() данного проекта создать коллекцию (на базе обобщенного класса List<T> ) и добавить в коллекцию 4–5 объектов класса «РС». 
				○	Произвести сериализацию коллекции в бинарный файл с именем «listSerial.txt» в каталоге на диске D. 
					В случае наличия аналогичного файла в каталоге старый файл перезаписать новым файлом и вывести об этом уведомление. 
				○	Создать новый проект (тип — консольное приложение) с именем «DeserializConsolApp». Добавить ссылку на библиотеку «ClassLib».
				○	В функции Main() произвести десериализацию коллекции, созданной в проекте с именем «SerializConsolApp» и вывести на экран.
			*/
			List<PC> computers = new List<PC>();

			PC pc1 = new PC("Apple", "12345", "Intel", 256);
			PC pc2 = new PC("Apple", "12346", "Intel", 512);
			PC pc3 = new PC("Shmapple", "12347", "Intel", 256);
			PC pc4 = new PC("Pear", "12348", "Intel", 512);

			computers.AddRange(new List<PC> { pc1, pc2, pc3, pc4 });

			var serializer = new BinaryFormatter();

			using (var stream = File.OpenWrite($"D:/listSerial.txt"))
			{
				serializer.Serialize(stream, computers);
			}

			using (var stream = File.OpenRead($"D:/listSerial.txt"))
			{
				var result = serializer.Deserialize(stream) as List<PC>;
				foreach (var computer in result)
				{
					Console.WriteLine($"{computer.MadeBy}, {computer.SerialNum}, {computer.Processor} , {computer.SSD_GB}");
				}
			}
		}
	}
}
