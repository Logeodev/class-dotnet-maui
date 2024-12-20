
using FluentAssertions;
using ConnectFourTDD;

namespace TestConnectFour
{
    public class TestGrid
    {
        [Fact]
        public void Grid_Init_IsGridInitialized6x7()
        {
            // arrange
            Grid grid;
            // act
            grid = new Grid(6, 7);
            // assert
            grid.CountRows().Should().Be(6);
            grid.CountColumns().Should().Be(7);
        }

        [Fact]
        public void Grid_Init_CellsAreInitializedEmpty()
        {
            // arrange
            Grid grid = new Grid(6, 7);
            // act

            // assert
            for (int i = 0; i < grid.CountRows(); i++)
            {
                for (int j = 0; j < grid.CountColumns(); j++)
                {
                    grid.GetCell(i, j).value.Should().Be(' ');
                }
            }
        }

        [Theory]
        [InlineData(0, 0, 'Y')]
        [InlineData(4, 6, 'D')]
        [InlineData(2, 3, 'B')]
        public void Grid_Modify_SetAndCheckCellValue(
            int cellRow,
            int cellColumn,
            char cellValue
        )
        {
            // arrange
            Grid grid = new Grid(6, 7);
            // act
            grid.SetCell(cellRow, cellColumn, cellValue);
            // assert
            grid.GetCell(cellRow, cellColumn).value.Should().Be(cellValue);
        }

        [Theory]
        [InlineData(0, 0, 'Y', true)]
        [InlineData(4, 8, 'D', false)]
        [InlineData(10, -6, 'B', false)]
        public void Grid_SetCell_ChecksForValidIndexBeforeSet(
            int cellRow,
            int cellColumn,
            char cellValue,
            bool expectedValidSet
        )
        {
            // arrange
            Grid grid = new Grid(6, 7);
            // act
            bool cellIsSet = grid.SetCell(cellRow, cellColumn, cellValue);
            // assert
            cellIsSet.Should().Be(expectedValidSet);

            if (cellIsSet) grid.GetCell(cellRow, cellColumn).value.Should().Be(cellValue);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(6, true)]
        [InlineData(12, false)]
        [InlineData(-5, false)]
        public void Grid_InsertNewToken_PutsTokenIfColumnIndexValid(
            int columnIndex,
            bool expectedAchieved
            )
        {
            // arrange
            Grid grid = new Grid();
            // act
            bool achieved = grid.PutTokenInColumn(columnIndex, 'Y');
            // assert
            achieved.Should().Be(expectedAchieved);
        }

        //[Theory]
        //[InlineData(0, 0, 'Y', true)]
        //[InlineData(4, 8, 'D', false)]
        //[InlineData(10, -6, 'B', false)]
        //public void Grid_InsertNewToken_CheckForValidPosition(
            
        //    )
        //{

        //}
    }
}
