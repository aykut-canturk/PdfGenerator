# PDF Generator

A simple C# application that demonstrates how to create PDF documents using PdfSharpCore library.

## Features

- Generate PDF documents with text content
- Add images/logos to PDF documents
- Format text with custom fonts and styles
- Implement proper text wrapping for paragraphs
- Position elements precisely on the page

## Requirements

- .NET Core 3.1 or higher
- PdfSharpCore package

## Installation

1. Clone this repository or download the source code
2. Install required packages:
   ```
   dotnet add package PdfSharpCore
   ```
3. Place your logo image file named "logo.png" in the project directory

## Usage

Run the application using:

```
dotnet run
```

This will generate a PDF file named "helloworld.pdf" in the project directory.

## Code Structure

The application is organized as follows:

- `Program.cs`: Contains the entry point and main classes
  - `Program`: Main class with entry point
  - `PdfGenerator`: Class that handles PDF generation logic

### Main Components

- **PDF Document Creation**: Sets up the PDF document, pages, and graphics context
- **Logo Handling**: Loads and positions a logo image on the page
- **Text Processing**: Formats and positions text content with proper wrapping
- **Text Wrapping Algorithm**: Custom implementation that wraps text properly within page margins

## Customization

You can customize the generated PDF by:

- Changing the logo image (replace logo.png)
- Modifying text content in the `GetLoremIpsum()` method
- Adjusting margins, fonts, colors, and other layout parameters in the `GeneratePdf()` method
- Adding more content types or additional pages

## License

This project is open source and available under the MIT License.
