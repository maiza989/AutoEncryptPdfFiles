# PDF Encryption Tool

This is a C# program for encrypting PDF files, making them non-editable. It processes all PDF files in a specified input folder, encrypts them, and saves the encrypted versions in an output folder. If there are any errors during processing, the program moves the problematic files to an error folder.

## Usage

1. Input Folder: Place the folder with PDF files you want to process in the ORIGINAL_FOLDER_YOU_WANT_TO_PROCESS folder.
2. Output Folder: Processed PDF files will be saved to the OUTPUT_FOLDER_OF_PROCESSED_FILES folder.
3. Error Handling: Any files that encounter errors during processing will be moved to the ERROR_FOLDER.
4. Encryption: The program encrypts the PDF files using a specified owner password.

## Requirements

- .NET Framework
- Spire.PDF library

## Installation

1. Clone or download this repository to your local machine.
2. Make sure you have the necessary dependencies installed (e.g., Spire.PDF).
3. Build the project using Visual Studio or your preferred C# compiler.

## Running the Program

1. Modify the inputFolderPath, outputFolderPath, and errorFolderPath variables in the Program.cs file to point to your desired input, output, and error folders.
2. Compile and run the program.
3. Monitor the console output for progress and any error messages.

