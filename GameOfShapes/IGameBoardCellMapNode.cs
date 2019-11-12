namespace GameOfShapes
{
    public interface IGameBoardCellMapNode
    {
        IGameBoardCell GetBoardCell();

        CellNodeMapPositions GetMapPosition();
    }
}
