# Sudoku Generator

## Main Goal

The primary goal of this project was to demonstrate my skills in handling **CPU-bound tasks** efficiently, making effective use of **parallelism** 
to fully leverage the power of the CPU, and applying **well-designed algorithms** to solve complex problems.

---

## Overview

**Sudoku Generator** is a robust C# application capable of generating Sudoku puzzles of varying difficulty levels, 
solving them, and exporting both the puzzles and their solutions as PDF files.

---

## 💡 Key Concepts Demonstrated

- A solid understanding and application of **Object-Oriented Programming (OOP)** principles, including the **SOLID** design principles
- **Design Patterns**:
  - **Factory Pattern**: For instantiating puzzle generators
  - **Observer Pattern**: For managing event-driven behavior
- Efficient **asynchronous programming** and **parallel execution** to optimize performance in CPU-bound operations
- **Robust error handling** and structured, centralized logging using **Serilog**
- Seamless integration with external libraries like **iText7** for PDF generation
- Implementation of a **backtracking algorithm** to solve Sudoku puzzles
- **Event-driven design** to decouple components and improve modularity
- Clean project structure with strong **separation of concerns**, organized by feature in dedicated directories
- Thorough unit testing using the NUnit testing framework, ensuring high code reliability and regression safety
- Use of Moq to isolate dependencies and simulate external behavior in unit tests
- Descriptive, atomic **Git commits** on isolated feature branches for clear version control
- Thorough **XML documentation** to support maintainability and IDE tooling

---

## External Libraries/Services Used

- [Serilog](https://serilog.net/) - For structured logging, enabling easier debugging and improving maintainability.
- [IText7](https://itextpdf.com/) - For robust PDF exporting
- NUnit and Moq - Used together to write isolated, repeatable unit tests by simulating external dependencies and asserting expected behavior

---

## How it works
1. Prompts user for number of Sudoku boards to generate: <br><br>
![Number of boards prompt message](https://github.com/user-attachments/assets/c603c75b-5f9d-4710-9fad-c8dcd8a77689)
2. Displays list of valid Difficulties and prompts user for valid difficulty (number 0-2):<br><br>
![Difficulty prompt message](https://github.com/user-attachments/assets/17704677-d71d-441a-bb16-ccf2736dd95c)
3. The application starts generating Sudoku puzzles in parallel using multiple threads.
4. While puzzles are being generated, a separate thread continuously displays the generation progress. Once all puzzles are generated, an event is triggered to stop the status thread:<br><br>
   ![Sudoku generation status message](https://github.com/user-attachments/assets/bf57376b-6830-4333-9e5d-549cb2ca32bd)
5. After puzzle generation, the application exports results to:
   - `sudoku.txt` – containing the puzzles
   - `solutions.txt` – containing the solutions
6. A second progress status thread reports the PDF generation process until completion::<br><br>
![Pdf generation status message](https://github.com/user-attachments/assets/3838bd71-a071-41ac-b9c6-9b4e079c4aa4)
7. Two PDF files were generated:<br><br>
![Pdf files in bin directory](https://github.com/user-attachments/assets/f36a5385-73a7-44ad-8d96-16dfd3b0c257)
- One with Sudoku boards to solve:<br><br>
![Pdf result with solvable Sudoku boards](https://github.com/user-attachments/assets/5d898424-5453-482a-a3e3-6cbec205e0e4)
- Second with solutions to them:<br><br>
![Pdf result with Sudoku solutions](https://github.com/user-attachments/assets/8ce29504-a748-43fb-88d9-d1048222066a)


## Installation

1. **Clone the Repository**
