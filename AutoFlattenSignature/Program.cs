using Spire.Pdf;
using Spire.Pdf.Security;
class Program
{
    static void Main(string[] args)
    {
        string inputFolderPath = @"ORIGINAL_FOLDER_YOU_WANT_TO_PROCESS";                                                                                                // Path of original input PDFs folder
        string outputFolderPath = @"OUTPUT_FOLDER_OF_PROCESSED_FILES";                                                                                                  // Destination path for output of processed PDFs files
        string errorFolderPath = @"ERROR_FOLDER";                                                                                                                       // Destination path for error files if any encounter errors. 

        if (!Directory.Exists(inputFolderPath))                                                                                                                         // Check if the input and output folders exist
        {
            Console.WriteLine("Input folder does not exist.");
            return;
        }// end of if-statement
        if (!Directory.Exists(outputFolderPath))                                                                                                                        // Check if the input and output folders exist
        {
            Console.WriteLine("Output folder does not exist.");
            return;
        }// end of if-statement

        string[] pdfFiles = Directory.GetFiles(inputFolderPath, "*.pdf");                                                                                               // Get all PDF files in the input folder
        foreach (string pdfFile in pdfFiles)                                                                                                                            // Iterate through each PDF file
        {
            Console.WriteLine($"Processing file: {Path.GetFileName(pdfFile)}");
            try
            {
                PdfDocument pdf = new PdfDocument();                                                                                                                    // Load the PDF document
                pdf.LoadFromFile(pdfFile);

                if (pdf != null)                                                                                                                                        // Check if the document is successfully loaded
                {
                    pdf.Security.Encrypt(string.Empty,"1",PdfPermissionsFlags.FillFields,PdfEncryptionKeySize.Key128Bit);                                               // Encrypt the folder so its not editable. "Open and Permission password cannot be the same"
                    string outputFile = Path.Combine(outputFolderPath, Path.GetFileName(pdfFile));                                                                      // Save the modified PDF to the output folder
                    pdf.SaveToFile(outputFile);
                    Console.WriteLine($"\tEncrypting file completed for: {Path.GetFileName(pdfFile)}");
                }// end of if-statement
                else
                {
                    Console.WriteLine($"Error processing file {Path.GetFileName(pdfFile)}: PDF document or its form is null.");
                    File.Move(inputFolderPath, errorFolderPath);                                                                                                        // Moving error file.                                                                                         
                }// end of else-statement
            }// end of try-catch
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing file {Path.GetFileName(pdfFile)}: {ex.Message}");
                File.Move(inputFolderPath, errorFolderPath);                                                                                                                                                                                               
            }// end of catch
        }// end of foreach
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("All PDF files encrpted successfully.");
        Console.ForegroundColor = ConsoleColor.Gray;
    }// end of main
}// end of class
