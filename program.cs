using System;
using System.Collections.Generic;
					
public class Program
{
	public static byte[] memory = new byte[256];
	public static int ptr = 0;
	public static List<string> func = new List<string>();
	public static string uInput;
	
	public static void Main()
	{
		Console.WriteLine(@"/| byline v1.0.0 | leave no param blank | run to run |\");
		Console.WriteLine(@"\| ~256b of mem~ | -------------------- | bridget gg |/");
		//turingTest();
		
		while (true) // Take user input (uInput)
		{
			uInput = Console.ReadLine();
			if (uInput != "run") { func.Add(uInput); }
			else { break; }
		}
		
		foreach (string f in func) // Iterate through lines and run code
		{
			string[] parts = f.Split(); // Split into parts
			parts[0] = parts[0].ToLower(); // Undercase the func name :)
			
			switch (parts[0])
			{
				case "add": // Add val to current location in memory, takes one arguement
					memory[ptr] += Convert.ToByte(Int32.Parse(parts[1]));
					break;
				case "sub": // Subtract val from current location in memory, takes one arguement
					memory[ptr] -= Convert.ToByte(Int32.Parse(parts[1]));
					break;
				
				case "lft": // Move pointer one left, takes no arguments
					if (ptr == 0) {ptr = 255;}
					else { ptr -= 1; }
					break;
				case "rgt": // Move pointer one right, takes no arguments
					if (ptr == 255) {ptr = 0;}
					else { ptr += 1; }
					break;
				case "jmp": // Move to point in memory, takes one arguement
					try { ptr = Int32.Parse(parts[1]); }
					catch { Console.WriteLine("Jump target out of range"); }
					break;
				case "swt": // Switch two values in memory, takes two arguements
					try
					{
						byte sVal = memory[Int32.Parse(parts[1])];
						memory[Int32.Parse(parts[1])] = memory[Int32.Parse(parts[2])];
						memory[Int32.Parse(parts[2])] = sVal;
					}
					catch {Console.WriteLine("Switch failed, memory address out of range [0-255]?");}
					break;
					
				case "out": // Output from current, 0 - ASCII | 1 - Literal , takes two arguemnts
					if (Int32.Parse(parts[1]) == 0) { Console.WriteLine(buildStringFromMemory(Int32.Parse(parts[2]), "")); }
					else { Console.WriteLine(outputBytesFromMemory(Int32.Parse(parts[2]), "")); }
					break;
				case "inp": // Input to current, takes no arguments
					try { memory[ptr] = Convert.ToByte(Int32.Parse(Console.ReadLine())); }
					catch { Console.WriteLine("Invalid Input."); }
					break;
				
				case null:
					Console.WriteLine("Something broke! This is the null case!");
					break;
			}
		}
	}
	
	public static string buildStringFromMemory(int reach, string finalString)
	{
		for (int i = 0; i < reach; i++) { finalString += Convert.ToChar(memory[ptr + i]); }
		return finalString;
	}
	public static string outputBytesFromMemory(int reach, string finalString)
	{
		for (int i = 0; i < reach; i++) { finalString += Convert.ToString(memory[ptr + i]) + " "; }
		return finalString;
	}
	
	public static void turingTest()
	{
		func.Add("add 72");
		func.Add("rgt"); // H
		func.Add("add 101");
		func.Add("rgt"); // e
		func.Add("add 108");
		func.Add("rgt"); // l
		func.Add("add 108");
		func.Add("rgt"); // l
		func.Add("add 111");
		func.Add("rgt"); // o
		func.Add("add 44");
		func.Add("rgt"); // ,
		func.Add("add 32");
		func.Add("rgt"); // [SPACE]
		func.Add("add 119");
		func.Add("rgt"); // w
		func.Add("add 111");
		func.Add("rgt"); // o
		func.Add("add 114");
		func.Add("rgt"); // r
		func.Add("add 108");
		func.Add("rgt"); // l
		func.Add("add 100");
		func.Add("rgt"); // d
		func.Add("add 33");
		func.Add("rgt"); // !
		
		func.Add("jmp 0");
		func.Add("out 0 13");
	}
}
