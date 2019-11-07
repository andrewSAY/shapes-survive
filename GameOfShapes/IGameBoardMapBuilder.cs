using System.Collections.Generic;

namespace GameOfShapes
{
    public interface IGameBoardMapBuilder
    {
        IEnumerable<IGameBoardCell> Build(); 
    }
}
