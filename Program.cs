using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Recursive_Delete
{
	class Program
	{
		static Regex DeleteRegex;
		static readonly List<string> FilesToDelete = new List<string>();
		static bool DeleteMatch;
		static void Main()
		{
			var path = Directory.GetCurrentDirectory();
			Console.WriteLine(path);

			Console.WriteLine("Enter your regex:");
			string line = Console.ReadLine();
			DeleteRegex = new Regex(line);
			Console.WriteLine(DeleteRegex);

			Console.WriteLine("Delete if match [y/n]?");
			string response = Console.ReadLine().ToLower();
			while(response[0] != 'y' && response[0] != 'n')
			{
				Console.WriteLine("Delete if match [y/n]?");
				response = Console.ReadLine().ToLower();
			}

			DeleteMatch = response[0] == 'y';
			
			foreach (var folder in Directory.EnumerateDirectories(path))
			{
				IterateFolder(folder);
			}
			foreach (var file in FilesToDelete)
			{
				Console.WriteLine(file);
			}

			File.WriteAllLines(Path.Combine(path, "output.log"), FilesToDelete);
			foreach (var file in FilesToDelete)
			{
				File.Delete(file);
			}

			Console.ReadLine();
		}

		static void IterateFolder(string path)
		{
			foreach (var dir in Directory.EnumerateDirectories(path))
			{
				IterateFolder(dir);
			}
			foreach(var file in Directory.EnumerateFiles(path))
			{
				var match = DeleteRegex.Match(file);
				if(match.Success == DeleteMatch)
				{
					FilesToDelete.Add(file);
				}
			}
		}
	}
}
