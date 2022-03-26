using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03M_SolarFarmAssessment.UI
{
    class ConsoleIO
    {
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
					Error("Please input a proper decimal\n");
				}
				else
				{
					result = temp;
					valid = true;
				}
			}
			return result;
		}
		public int GetInt(string prompt)
		{
			int result = -1;
			bool valid = false;
			while (!valid)
			{
				Console.Write($"{prompt}: ");
				if (!int.TryParse(Console.ReadLine(), out result))
				{
					Error("Please input a proper integer\n");
				}
				else
				{
					valid = true;
				}
			}
			return result;
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
		public String GetString(string prompt)
		{
			Console.Write($"{prompt}: ");
			return Console.ReadLine();
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
