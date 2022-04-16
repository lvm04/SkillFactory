using System;
using System.Xml.Serialization;
using System.IO;

namespace SF.Module7
{
    public static class Extension
    {
        /// <summary>
        /// Сохранение объекта C# в XML-файл 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="fileName"></param>
        public static void SaveXMLFile<T>(this T obj, string fileName)
        {
            XmlSerializer writer = new XmlSerializer(typeof(T));
            FileStream file = File.Create(fileName);

            writer.Serialize(file, obj);
            file.Close();
        }

    }
}