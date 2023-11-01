using System.Diagnostics;
using System.Security.Cryptography;

namespace ConsoleAppNPP2
	{
	public static class ConsoleAppMethods
		{
		// SegmentHasher Method
		public static void SegmentHasher(string filePath, int segmentSize)
			{
			Stopwatch stopWatch = new Stopwatch();
			stopWatch.Start();

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
							fileStream.Seek(i*segmentSize, SeekOrigin.Begin);
							bytesRead=fileStream.Read(buffer, 0, buffer.Length);
							}

						byte[] segmentHash = sha256.ComputeHash(buffer, 0, bytesRead);
						string segmentHashHex = BitConverter.ToString(segmentHash).ToLower();
						Console.WriteLine($"Segment Hash: {segmentHashHex}");
					}
					);
					}
				}

			stopWatch.Stop();
			Console.WriteLine($"Elapsed time is {stopWatch.ElapsedMilliseconds} ms");
			}
		}
	}
