using ConsoleAppNPP2;

class Program
	{
	static void Main(string[] args)
		{
		Console.WriteLine("arg0 - <FilePath> (C:/...), arg1 - <SegmentSize> in bytes");

		while(true)
			{
			// User Inputs
			string inputFilePath = Console.ReadLine();
			int inputSegmentSize = int.Parse(Console.ReadLine());

			ConsoleAppMethods.SegmentHasher(inputFilePath, inputSegmentSize);

			Console.WriteLine("Continue? y/n");
			string keepGoing = Console.ReadLine();

			if(keepGoing=="y")
				{
				continue;
				}
			else
				{
				break;
				}
			}
		}
	}
