# classToMapping
EnumGenerator
---------
Для работы генератора в проекте ITS.Core.<Название> должна быть папка Domain, в ней две папки: Enums и EnumStrings, первая - для перечислений, 
вторая - для конвертеров перечислений в строку и обратно, кроме того, в папке EnumString должен быть интерфейс IEnumString<T>.

Формат входных файлов
---------
В папке со входными файлами должен быть один файл c названием namespaces.cfg, в котором указаны: первой строкой namespace перечислений,
второй - namespace конвертеров(пример: ITS.Core.Spectrum.Domain.Enums ITS.Core.Spectrum.Domain.EnumStrings) и один или несколько текстовых файлов (.txt) с данными для перечислений:
первой строкой могут идти необязательные опции: [Flags] и/или [IgnoreCase] (пишутся через пробел, описание опций - ниже), следующей строкой идёт комментарий к перечислению, 
следующей - название перечисления, дальше строка за строкой идёт сначала описание элемента перечисления, затем название элемента. 
Нужно понимать, что описание элемента будет сгенерировано в строковый литерал, следовательно, нужно экранировать специальные символы 
(например, символ обратного слеша \ нужно писать как \\). Необходимо задать два аргумента при вызове: первый аргумент - путь к папке с входными файлами (.txt и .cfg), 
второй аргумент - путь к директории проекта (если делать через внешние инструменты visual studio - "$(ProjectDir)").

Интеграция с Visual Studio
---------
Удобно добавить файл .exe генератора как внешний инструмент в visual studio: Средства->Внешние инструменты->Добавить, ввести любое название,
в строке Команда указать путь к релизной версии .exe генератора, в строке Аргументы указать описанные выше аргументы, нажать галочку Использовать окно вывода.
В результате генератор считывает данные из namespaces.cfg и текстовых файлов, создаёт на каждый текстовый файл перечисление в папке Enums и конвертер в папке EnumStrings, 
дальше он редактирует .csproj файл в директории проекта, добавляя сгенерированные файлы в проект.
Обращаться к методам полученных конвертеров можно через статическое свойство Instance или создавая новый объект конвертера, и вызывая на нём методы.

Описание опций
---------
[Flags] - нужен, если перечисление требует возможности выбора нескольких элементов одновременно через битовую операцию "или" ( | ), 
означает что перечисление будет помечено атрибутом [Flags] и элементам в соответствие будет ставиться степени двойки. В этом случае в качестве элемента под номером 0 будет сгенерирован элемент NoData(преобразовывается в "Нет данных"). 

[IgnoreCase] используется, если сравнение строк для преобразования из строки в элемент перечисления нужно производить без учёта регистра.

Файл IEnumString<T>
---------

	using System;
	namespace ITS.Core.Bridges.Domain.EnumStrings
	{
      public interface IEnumStrings<T>
          where T : struct, IConvertible
      {
          string GetName(T value);
          T GetElement(string name);
      }
	}

Пример файла namespaces.cfg
---------
ITS.Core.Spectrum.Domain.Enums  

ITS.Core.Spectrum.Domain.EnumStrings


