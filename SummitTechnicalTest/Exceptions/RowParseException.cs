using System;
using System.Collections.Generic;
using System.Text;

namespace SummitTechnicalTest.Exceptions {

	//Gets thrown from the Main.GetCellsFromFile() to indicate that an error has occurred while parsing a row.
	public class RowParseException : Exception {

		public RowParseException(int rowIndex, Exception e) : base($"An error has occurred while parsing row with index of {rowIndex}.", e) { }

	}

}
