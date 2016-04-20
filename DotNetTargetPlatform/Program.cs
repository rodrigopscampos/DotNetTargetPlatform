using System;

namespace DotNetTargetPlatform
{
    class Program
    {
        static void Main(string[] args)
        {
            var arguments = new ArgumentReader(args);

            var result = TargetPlatformResolver.List(arguments.Path);

            foreach (var item in result)
                Console.WriteLine(string.Format("{0, -70}: {1, -10} {2}", item.Key, item.Value.Platform, item.Value.Descriptoin));
        }
    }

    internal class ArgumentReader
    {
        public string Path { get; private set; }

        public ArgumentReader(string[] args)
        {
            if (args == null || args.Length < 1)
                this.Path = ".";
            else
                this.Path = args[0];
        }
    }

    public class TargetPlatform
    {
        public string Platform { get; set; }
        public string Descriptoin { get; set; }

        public bool IsValid { get { return !string.IsNullOrEmpty(Platform); } }
    }
}
