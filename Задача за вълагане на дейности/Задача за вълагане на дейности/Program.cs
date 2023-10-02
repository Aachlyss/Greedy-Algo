using System;

class HungarianAlgorithm
{
    private static int[,] ReduceMatrix(int[,] costMatrix)
    {
        int rowCount = costMatrix.GetLength(0);
        int colCount = costMatrix.GetLength(1);

        for (int i = 0; i < rowCount; i++)
        {
            int min = int.MaxValue;
            for (int j = 0; j < colCount; j++)
            {
                if (costMatrix[i, j] < min)
                {
                    min = costMatrix[i, j];
                }
            }
            for (int j = 0; j < colCount; j++)
            {
                costMatrix[i, j] -= min;
            }
        }

        for (int j = 0; j < colCount; j++)
        {
            int min = int.MaxValue;
            for (int i = 0; i < rowCount; i++)
            {
                if (costMatrix[i, j] < min)
                {
                    min = costMatrix[i, j];
                }
            }
            for (int i = 0; i < rowCount; i++)
            {
                costMatrix[i, j] -= min;
            }
        }

        return costMatrix;
    }

    private static int[] FindZeroInRow(int[,] costMatrix)
    {
        int rowCount = costMatrix.GetLength(0);
        int colCount = costMatrix.GetLength(1);

        int[] rowZeros = new int[rowCount];

        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < colCount; j++)
            {
                if (costMatrix[i, j] == 0)
                {
                    rowZeros[i]++;
                    break;
                }
            }
        }

        return rowZeros;
    }

    private static int[] FindZeroInCol(int[,] costMatrix)
    {
        int rowCount = costMatrix.GetLength(0);
        int colCount = costMatrix.GetLength(1);

        int[] colZeros = new int[colCount];

        for (int j = 0; j < colCount; j++)
        {
            for (int i = 0; i < rowCount; i++)
            {
                if (costMatrix[i, j] == 0)
                {
                    colZeros[j]++;
                    break;
                }
            }
        }

        return colZeros;
    }

    private static bool TryFindAssignment(int row, int[,] costMatrix, bool[] assignedRows, bool[] assignedCols, int[] assignment)
    {
        int colCount = costMatrix.GetLength(1);

        for (int col = 0; col < colCount; col++)
        {
            if (costMatrix[row, col] == 0 && !assignedCols[col])
            {
                assignedCols[col] = true;
                if (assignment[col] == -1 || TryFindAssignment(assignment[col], costMatrix, assignedRows, assignedCols, assignment))
                {
                    assignment[col] = row;
                    return true;
                }
            }
        }

        return false;
    }

    public static int[] Solve(int[,] costMatrix)
    {
        int rowCount = costMatrix.GetLength(0);
        int colCount = costMatrix.GetLength(1);

        int[] assignment = new int[colCount];
        for (int i = 0; i < colCount; i++)
        {
            assignment[i] = -1;
        }

        // Step 1: Reduce the cost matrix
        costMatrix = ReduceMatrix(costMatrix);

        // Step 2: Find the minimum number of lines to cover all zeros
        int[] rowZeros = FindZeroInRow(costMatrix);
        int[] colZeros = FindZeroInCol(costMatrix);

        int linesToCover = 0;
        for (int i = 0; i < rowCount; i++)
        {
            if (rowZeros[i] == 0)
            {
                linesToCover++;
            }
        }

        // If the number of lines to cover zeros is less than N, we need to add more lines
        while (linesToCover < rowCount)
        {
            int minUncoveredValue = int.MaxValue;

            // Find the minimum uncovered value
            for (int i = 0; i < rowCount; i++)
            {
                if (rowZeros[i] == 0)
                {
                    for (int j = 0; j < colCount; j++)
                    {
                        if (colZeros[j] == 0 && costMatrix[i, j] < minUncoveredValue)
                        {
                            minUncoveredValue = costMatrix[i, j];
                        }
                    }
                }
            }

            // Subtract the minimum uncovered value from all uncovered values and add it to the doubly covered values
            for (int i = 0; i < rowCount; i++)
            {
                if (rowZeros[i] == 0)
                {
                    for (int j = 0; j < colCount; j++)
                    {
                        if (colZeros[j] == 0)
                        {
                            costMatrix[i, j] -= minUncoveredValue;
                        }
                        else if (colZeros[j] == 1)
                        {
                            costMatrix[i, j] += minUncoveredValue;
                        }
                    }
                }
            }

            // Recalculate the zeros in rows and columns
            rowZeros = FindZeroInRow(costMatrix);
            colZeros = FindZeroInCol(costMatrix);

            // Update the lines to cover
            linesToCover = 0;
            for (int i = 0; i < rowCount; i++)
            {
                if (rowZeros[i] == 0)
                {
                    linesToCover++;
                }
            }
        }

        // Step 3: Find an initial feasible solution
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < colCount; j++)
            {
                if (costMatrix[i, j] == 0 && assignment[j] == -1)
                {
                    assignment[j] = i;
                    break;
                }
            }
        }

        // Step 4: Try to improve the solution
        for (int i = 0; i < rowCount; i++)
        {
            bool[] assignedRows = new bool[rowCount];
            bool[] assignedCols = new bool[colCount];

            // Try to find an assignment starting from each row
            if (TryFindAssignment(i, costMatrix, assignedRows, assignedCols, assignment))
            {
                break;
            }
        }

        return assignment;
    }

    static void Main(string[] args)
    {
        // Пример на входни данни: матрица с разходи
        int[,] costMatrix = {
            {5, 9, 1},
            {10, 3, 2},
            {8, 7, 4}
        };

        int[] assignment = Solve(costMatrix);

        // Отпечатване на оптималното разпределение
        for (int i = 0; i < assignment.Length; i++)
        {
            Console.WriteLine($"Служител {assignment[i] + 1} изпълнява Дейност {i + 1}");
        }
    }
}
