namespace GameOfShapes
{
    public interface ISurvivalChecker
    {
        bool ShapeWillSurvive(IShape shape, IGameBoardCell cell);
    }
}
