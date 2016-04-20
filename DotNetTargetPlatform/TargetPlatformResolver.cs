using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DotNetTargetPlatform
{
    public class TargetPlatformResolver
    {
        public static IDictionary<string, TargetPlatform> List(string path)
        {
            var files = System.IO.Directory.GetFiles(path).Where(item =>
                item.EndsWith(".exe")
                || item.EndsWith(".dll"));

            var result = new Dictionary<string, TargetPlatform>();

            foreach (var item in files)
            {
                var fileName = System.IO.Path.GetFileName(item);
                result.Add(fileName, Resolve(item));
            }

            return result;
        }

        public static TargetPlatform Resolve(string assemblyPath)
        {
            var assemblyName = default(AssemblyName);

            try
            {
                assemblyName = System.Reflection.AssemblyName.GetAssemblyName(assemblyPath);
            }
            catch
            {
                return new TargetPlatform
                {
                    Descriptoin = string.Format("Unkown type {0}", System.IO.Path.GetFileName(assemblyPath)),
                };
            }

            switch (assemblyName.ProcessorArchitecture)
            {
                case ProcessorArchitecture.Amd64:
                    return new TargetPlatform { Platform = "X64", Descriptoin = "A 64-bit AMD processor only" };
                case ProcessorArchitecture.Arm:
                    return new TargetPlatform { Platform = "Arm", Descriptoin = "An ARM processor" };
                case ProcessorArchitecture.IA64:
                    return new TargetPlatform { Platform = "X64", Descriptoin = "A 64-bit Intel processor only" };
                case ProcessorArchitecture.MSIL:
                    return new TargetPlatform { Platform = "AnyCPU", Descriptoin = "Neutral with respect to processor and bits-per-word" };
                case ProcessorArchitecture.None:
                    return new TargetPlatform { Platform = "unknown", Descriptoin = "An unknown or unspecified combination of processor and bits-per-word" };
                case ProcessorArchitecture.X86:
                    return new TargetPlatform { Platform = "X86", Descriptoin = "A 32-bit Intel processor, either native or in the Windows on Windows environment on a 64-bit platform (WOW64)" };
            }

            return new TargetPlatform()
            {
                Platform = assemblyName.ProcessorArchitecture.ToString()
            };
        }
    }
}