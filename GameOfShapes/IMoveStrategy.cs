namespace GameOfShapes
{
    public interface IMoveStrategy
    {
        IGameBoardCell CalculateOptimalCell(IShape shape, IGameBoardCell currentPosition);
    }
}
