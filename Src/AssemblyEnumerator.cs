using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ExceptionReflector
{
    [Serializable]
    class AssemblyEnumerator
    {
        private static string _filePath;
        private Assembly _assembly;

        //TODO: [Backlog] Tratar todos os pontos em que o erro de carga de assembly ocorre. 
        static Assembly TryResolveAssembly(object sender, ResolveEventArgs args)
        {
            //TODO: [Backlog] Criar um tratamento mais sofisticado cirando uma ordem de reposiórios para busca e integrando com GAC, NuGet, etc. Em caso de falha ao encontrar o arquivo, solicitar que o uuário informe o local...

            string folderPath = Path.GetDirectoryName(_filePath);
            string assemblyPath = Path.Combine(folderPath, new AssemblyName(args.Name).Name + ".dll");

            if (!File.Exists(assemblyPath)) return null;

            Assembly assembly = Assembly.LoadFrom(assemblyPath);
            return assembly;
        }

        public AssemblyEnumerator(string filePath)
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.AssemblyResolve += new ResolveEventHandler(TryResolveAssembly);

            _filePath = filePath;
            _assembly = Assembly.LoadFrom(filePath);                                  
        }

        public string FilePath
        {
            get
            {
                return _filePath;
            }
        }

        public IEnumerable<string> NameSpaces
        {
            get
            {
                try
                {
                    Type[] types = _assembly.GetTypes();
                    return types.Select(t => t.Namespace).Distinct();
                }
                catch (ReflectionTypeLoadException e)
                {
                    foreach (var loaderException in e.LoaderExceptions)
                    {
                        Debug.Print(loaderException.ToString());
                    }

                    return Enumerable.Empty<string>();
                }
            }
        }

        public IEnumerable<Type> GetClasses(string nameSpace)
        {
            return _assembly.GetTypes().Where(t => !t.IsInterface && String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal)).ToArray();
        }

        public IEnumerable<MethodInfo> GetMethodds(Type classType)
        {
            return classType.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
            //return classType.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        }
    }
}
