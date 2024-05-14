using Spire.Pdf;
using Spire.Pdf.Fields;
using Spire.Pdf.Security;

class Program
{
    string inputFolderPath = @"ORIGINAL_FOLDER_YOU_WANT_TO_PROCESS";                                                                                              // Path of original input PDFs folder
    string outputFolderPath = @"OUTPUT_FOLDER_OF_PROCESSED_FILES";                                                                                                  // Destination path for output of processed PDFs files
    string errorFolderPath = @"ERROR_FOLDER";                                                                                                                       // Destination path for error files if any encounter errors. 

    string ownerPassword = "a3JBt93q^X3Z&S8*f#Q&HZUeEFYGA!";                                                                                                        // Encryption password. *** choose your own ***
    string openPassword = "72PDB&fi8QunYt9svHuRjXqZYqJv^#";                                                                                                         // *** if you want open selected view to open the document with already know password.
                                                                                                                                                                    // Replace "String.Empty" with openPassword Variable. %Line36% ***
    PdfDocument pdf;

    public Program()
    {
        pdf = new PdfDocument();
    }// end of Program constructor

    static void Main(string[] args)
    {
       Program program = new Program();
        program.Run();
    }// end of main

    /// <summary>
    /// A method that runs the document processing process
    /// </summary>
    public void Run()
    {
        try
        {
            if (!Directory.Exists(inputFolderPath))                                                                                                                     // Check if the input and output folders exist
            {
                Console.WriteLine("Input folder does not exist.");
                return;
            }// end of if-statement
            if (!Directory.Exists(outputFolderPath))                                                                                                                    // Check if the input and output folders exist
            {
                Console.WriteLine("Output folder does not exist.");
                return;
            }// end of if-statement

            string[] pdfFiles = Directory.GetFiles(inputFolderPath, "*.pdf");                                                                                           // Get all PDF files in the input folder
            foreach (string pdfFile in pdfFiles)                                                                                                                        // Iterate through each PDF file
            {
                ProcessDocument(pdfFile);
            }// end of foreach
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred in Run(): {ex.Message}");
        }
    }// end of Run

    /// <summary>
    /// A method load a pdf file, encrypt it and save it in a target folder (Keeping the original file in the input path while saving encrypted file on a new output folder). 
    /// </summary>
    /// <param name="filePath"> pdf folder path </param>
    private void ProcessDocument(string filePath)
    {
        Console.WriteLine($"Processing file: {Path.GetFileName(filePath)}");
        try
        {
            pdf.LoadFromFile(filePath);                                                                                                                             // Load the PDF document

            if (pdf != null)                                                                                                                                        // Check if the document is successfully loaded
            {
                pdf.Security.Encrypt(string.Empty, ownerPassword, PdfPermissionsFlags.Print, PdfEncryptionKeySize.Key128Bit);                                       // *** Only allow printing *** Encrypt the folder so its not editable. "Open and Permission password cannot be the same"

            }// end of if-statement

            string outputFile = Path.Combine(outputFolderPath, Path.GetFileNameWithoutExtension(filePath) + "_Locked.PDF");                                         // Get traget out folder and file path and combine them + edit file name
            pdf.SaveToFile(outputFile);                                                                                                                             // Save the modified PDF to the output folder
            Console.WriteLine($"\tEncrypting file completed for: {Path.GetFileNameWithoutExtension(filePath)}");
        }// end of try-catch
        catch (Exception ex)
        {
            MoveFileToTargetFolder(filePath);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error processing file {Path.GetFileNameWithoutExtension(filePath)}: {ex.Message}");
            Console.ForegroundColor = ConsoleColor.Gray;
        }// end of catch
    }// end of ProcessDocument

    /// <summary>
    /// A method that moves a file from an input folder to a target folder.
    /// </summary>
    /// <param name="filePath"> pdf folder path </param>
    private void MoveFileToTargetFolder(string filePath)
    {
        string fileName = Path.GetFileName(filePath);                                                                                                                      // Get input file path 
        string targetFilePath = Path.Combine(errorFolderPath, fileName);                                                                                                   // Grab the target folder and the input file path and combine them
        File.Move(filePath, targetFilePath);                                                                                                                               // Move the file to target foler.
    }
}// end of class
