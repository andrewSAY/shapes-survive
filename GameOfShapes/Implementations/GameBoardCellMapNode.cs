namespace GameOfShapes.Implementations
{
    public readonly struct GameBoardCellMapNode : IGameBoardCellMapNode
    {
        private readonly IGameBoardCell _gameBoardCell;

        private readonly CellNodeMapPositions _mapPosition;

        public GameBoardCellMapNode(IGameBoardCell gameBoardCell, CellNodeMapPositions mapPosition)
        {
            _gameBoardCell = gameBoardCell;
            _mapPosition = mapPosition;
        }

        public IGameBoardCell GetBoardCell()
        {
            return _gameBoardCell;
        }

        public CellNodeMapPositions GetMapPosition()
        {
            return _mapPosition;
        }
    }
}
