using System.Security.Cryptography;

class Program
	{
	static void Main(string[] args)
		{
		Console.WriteLine("arg0 - <FilePath> (C:/...), arg1 - <SegmentSize> in bytes");

		// User Inputs
		string inputFilePath = Console.ReadLine();
		int inputSegmentSize = int.Parse(Console.ReadLine());

		SegmentHasher(inputFilePath, inputSegmentSize);

		// SegmentHasher Method
		static void SegmentHasher(string filePath, int segmentSize)
			{
			using(FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
				{
				using(SHA256 sha256 = SHA256.Create())
					{
					byte[] buffer = new byte[segmentSize];
					int bytesRead;
					long fileSize = fileStream.Length;

					Parallel.For(0, (int)(fileSize/segmentSize), i =>
						{
							// Monopoly 
							lock(fileStream)
								{
								fileStream.Seek(i * segmentSize, SeekOrigin.Begin);
								bytesRead=fileStream.Read(buffer, 0, buffer.Length);
								}
								byte[] segmentHash = sha256.ComputeHash(buffer, 0, bytesRead);
								string segmentHashHex = BitConverter.ToString(segmentHash).ToLower();
								Console.WriteLine($"Segment Hash: {segmentHashHex}");

						}
					);
					}
				}
			}
		}
	}
