

namespace ConnectFourTDD;

public class Grid
{
    public List<Cell> _grid { get; private set; }

    public Grid(int rows = 6, int columns = 7)
    {
        this._grid = new List<Cell>();
        InitGridCells(rows, columns);
    }

    private void InitGridCells(int rows, int columns)
    {
        for (int i = 0; i < rows; i++)
        {
            for(int j = 0; j < columns; j++)
            {
                this._grid.Add(new Cell(i, j, ' '));
            }
        }
    }

    public List<Cell> GetGrid()
    {
        return _grid;
    }

    public int CountRows()
    {
        return _grid
            .GroupBy(c => c.row)
            .Count();
    }
    public int CountColumns()
    {
        return _grid
            .GroupBy(c => c.column)
            .Count();
    }

    public Cell GetCell(int row, int column)
    {
        return _grid
            .Where(c => c.row == row && c.column == column)
            .First();
    }

    public bool SetCell(int cellRow, int cellColumn, char cellValue)
    {
        try
        {
            GetCell(cellRow, cellColumn)
                .UpdateValue(cellValue);
            return true;
        }
        catch { 
            return false;
        }
    }

    public bool PutTokenInColumn(int columnIndex, char token)
    {
        List<Cell> selectedColumn = _grid
            .Where(c => c.column == columnIndex)
            .OrderBy(c => c.row)
            .ToList();
        if (selectedColumn.Count() != 0)
        {
            Cell? bottomValidCell = selectedColumn
                .Where(c => c.value == ' ')
                .LastOrDefault();
            if (bottomValidCell != null)
            {
                bottomValidCell
                    .UpdateValue(token);
                return true;
            }
        }
        return false;
    }
}