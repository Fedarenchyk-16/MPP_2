using System;
using System.IO;
using System.Reflection;

namespace FakerLibrary
{
    public class Plugin
    {
        private readonly string _path = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName;

        public Tuple<Type, Generator.IGenerator> LoadPlugin(string type)
        {
            string path = _path + "/Plugins/" + type + "Generator/bin/Debug/" + type + "Generator.dll";

            Assembly plugin = Assembly.LoadFrom(path); //динамически подгружаем плагин
            Type[] types = plugin.GetTypes(); 
            foreach (Type current in types)
            {
                if (typeof(Generator.IGenerator).IsAssignableFrom(current)) 
                {
                    return new Tuple<Type, Generator.IGenerator>(current.BaseType.GetGenericArguments()[0],(Generator.IGenerator)Activator.CreateInstance(current));
                } //parent,instance
            }

            return null;
        }
    }
}
