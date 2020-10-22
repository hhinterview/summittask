using SummitTechnicalTest.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;

namespace SummitTechnicalTest {

	class Program {

		static void Main(string[] args) {
			int[][] cells = GetCellsFromFile("Triangle.txt");
			int result = GetLargestRoute(ref cells);
			Console.WriteLine($"Result is {result}.");
		}

		//Ref does not serve a purpose here other than to tell the developer the array passed in will be modified.
		//Even without the ref word, the array passed in can be modified since the arrays passed into the functions are passed by ref.
		//If this behaviour is not acceptable, this func can be changed/overloaded to use Array.Copy() to create another copy of the array passed in.
		public static int GetLargestRoute(ref int[][] cells) {
			//starting from the row from one below, work your way up the triangle,
			//while summing up all the largest values together until the largest value is left at the top.
			for(int y = cells.Length - 2; y >= 0; y--) {
				int[] currentRow = cells[y];
				int[] rowBelow = cells[y + 1];
				for(int x = 0; x < currentRow.Length; x++) {
					/*				  a
					currentRow:		b   c
					rowBelow:	 d    e    f
					for currentRow[x] == b, valOnLeft(rowBelow[x]) is d and valOnRight(rowBelow[x+1]) is e  */
					int valOnLeft = rowBelow[x];
					int valOnRight = rowBelow[x + 1];
					//get the largest value from below and add it up to the current val.
					int maxVal = Math.Max(valOnLeft, valOnRight); 
					currentRow[x] += maxVal;
				}
			}
			return cells[0][0];
		}

		public static int[][] GetCellsFromFile(string filePath) {
			List<int[]> parsedLines = new List<int[]>();
			using(FileStream fileStream = File.OpenRead(filePath))
			using(StreamReader streamReader = new StreamReader(fileStream)) {
				//read each row as a string, then split them into individual string values for cells (delimeter: spaces)
				//then convert all the string values for cells into int values and finally add them to the parsedLines list to be later returned.
				int currentRowIndex = 0;
				while(!streamReader.EndOfStream) {
					string rowStr = streamReader.ReadLine();
					try {
						string[] cellsStr = rowStr.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
						int[] intCells = cellsStr.Select(cellVal => int.Parse(cellVal)).ToArray();
						parsedLines.Add(intCells);
					} catch(Exception e) {
						throw new RowParseException(currentRowIndex, e);
					}
					currentRowIndex++;
				}
			}
			return parsedLines.ToArray();
		}

	}

}
