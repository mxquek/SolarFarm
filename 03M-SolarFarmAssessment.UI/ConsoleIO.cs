using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03M_SolarFarmAssessment.UI
{
    class ConsoleIO
    {
		private int? _GetInt(string prompt, bool nullable)
		{
			int? result = null;
			bool valid = false;
			while (!valid)
			{
				Console.Write($"{prompt}: ");
				string input = Console.ReadLine();
				if (nullable == true && input == "")
				{
					valid = true;
					//Display("TESTING\t\tNULL ENTERED");			//testing
					continue;
				}

				if (!int.TryParse(input, out int temp))
				{
					Error("Please input a proper integer");
				}
				else
				{
					result = temp;
					valid = true;
				}
			}
			return result;
		}
		private decimal? _GetDecimal(string prompt, bool nullable)
		{
			decimal? result = null;
			bool valid = false;
			while (!valid)
			{
				Console.Write($"{prompt}: ");
				string input = Console.ReadLine();
				if (nullable == true && input == "")
				{
					valid = true;
					//Display("TESTING\t\tNULL ENTERED");			//testing
					continue;
				}

				if (!decimal.TryParse(input, out decimal temp))
				{
					Error("Please input a proper decimal");
				}
				else
				{
					result = temp;
					valid = true;
				}
			}
			return result;
		}
		private string _GetString(string prompt, bool nullable)
        {
			bool valid = false;
			string input = "";
			while (!valid)
			{
				Console.Write($"{prompt}: ");
				input = Console.ReadLine();
				if (!nullable)
				{
					if(input == "")
                    {
						Error("Input cannot be empty.");
						continue;
					}
				}
				valid = true;
			}
			return input;
		}
		public int GetIntRecquired(string prompt)
		{
			return _GetInt(prompt, false).Value;
		}
		public int? GetIntOptional(string prompt)
		{
			return _GetInt(prompt, true);
		}
		public decimal GetDecRequired(string prompt)
		{
			return _GetDecimal(prompt, false).Value;
		}
		public decimal? GetDecOptional(string prompt)
		{
			return _GetDecimal(prompt, true);
		}
		public DateTime GetDateTime(string prompt)
		{
			DateTime result = new DateTime();
			bool valid = false;
			while (!valid)
			{
				Console.Write($"{prompt}: ");
				if (!DateTime.TryParse(Console.ReadLine(), out result))
				{
					Error("Please input a proper date\n");
				}
				else
				{
					valid = true;
				}
			}
			return result;
		}
		public String GetStringRecquired(string prompt)
		{
			return _GetString(prompt, false);
		}
		public String GetStringOptional(string prompt)
		{
			return _GetString(prompt, true);
		}
		public void Display(string message)
		{
			Console.WriteLine(message);
		}
		public void Error(string message)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Display(message);
			Console.ForegroundColor = ConsoleColor.White;
		}
		public void Warn(string message)
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			Display(message);
			Console.ForegroundColor = ConsoleColor.White;
		}
		public void Success(string message)
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Display(message);
			Console.ForegroundColor = ConsoleColor.White;
		}
	}
}
