namespace GameOfShapes
{
    public class GameBoardCellMapNode
    {
        private readonly IGameBoardCell _cell;

        IGameBoardCell Nord { get; set; }

        IGameBoardCell South { get; set; }

        IGameBoardCell East { get; set; }

        IGameBoardCell West { get; set; }

        IGameBoardCell NordWest { get; set; }

        IGameBoardCell EastNord { get; set; }

        IGameBoardCell SouthWest { get; set; }

        IGameBoardCell SouthEast { get; set; }

        public GameBoardCellMapNode(IGameBoardCell cell)
        {
            _cell = cell;
        }
    }
}
