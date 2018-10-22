using Microsoft.Office.Excel.Interop;
using System.Runtime.InteropServices;
using System.IO;

public Stream ExcelToPdf(Stream excelFile)
{
	var excelTempLocation = Path.GetTempPath() + Guid.NewGuid().ToString() + ".xlsx"; 
	var pdfTempLocation = Path.GetTempPath() + Guid.NewGuid().ToString() + ".pdf";
	
	//Creating temporary excel file from stream parameter
	using (var fileStream = File.Create(excelTempLocation))
	{
		excelFile.Seek(0, SeekOrigin.Begin);
		excelFile.CopyTo(fileStream);
	}
	
	try
	{
		Application excel = new Application
		{
			Visible = false
		};

		//Exports excel file to pdf file
		Workbooks workbooks = excel.Workbooks;
		Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Open(excelTempLocation);
		workbook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, pdfTempLocation);

		workbook.Close(false);
		workbooks.Close();
		excel.Application.Quit();
		excel.Quit();

		ReleaseObject(workbook);
		ReleaseObject(workbooks);
		ReleaseObject(excel);

		var pdfMemoryStream = new MemoryStream();

		//Converting PDF file to memorystream
		using (FileStream pdfFileStream = File.Open(pdfTempLocation, FileMode.Open))
		{
			pdfFileStream.CopyTo(pdfMemoryStream);
		}

		return pdfMemoryStream;
	}
	catch (Exception ex)
	{
		Console.WriteLine(ex.StackTrace);
		throw ex;
	}
	finally
	{
		//Cleaning up temp files
		File.Delete(excelTempLocation);
		File.Delete(pdfTempLocation);

		GC.Collect();
	}
}

private void ReleaseObject(object obj)
{
	try
	{
		Marshal.FinalReleaseComObject(obj);
		obj = null;
	}
	catch (Exception ex)
	{
		obj = null;
		Console.WriteLine(ex.StackTrace);
		throw ex;
	}
}