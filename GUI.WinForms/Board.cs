using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GUI.WinForms
{
    class Board
    {
        private MainForm mainForm;
        private Graphics graphics;
        private Pen penBlack;
        private int cellSize;
        private int shapeSize;
        private int halfShapeSize;
        private int countCellWidth;
        private int countCellHeight;
        private int boardSizeWidth;
        private int boardSizeHeight;
        

        public Board(MainForm _mainForm, int _countCellWidth, int _countCellHeight)
        {
            this.mainForm = _mainForm;
            this.countCellWidth = _countCellWidth;
            this.countCellHeight = _countCellHeight;
            
            this.CalculateSize();
            
            this.InitializeGraphics();
            this.DrawBoard();
        }

        private void InitializeGraphics()
        {
            this.graphics = this.mainForm.BoardPanel.CreateGraphics();
            this.penBlack = new Pen(Color.Black);
            this.penBlack.Width = 1;
        }

        public void DrawBoard()
        {
            int i;
            int currentY = 0;

            for (i = 0; i <= countCellHeight; i++)
            {
                this.graphics.DrawLine(penBlack, 0, currentY, this.boardSizeWidth, currentY);
                currentY += this.cellSize;
            }

            int currentX = 0;

            for (i = 0; i <= countCellWidth; i++)
            {
                this.graphics.DrawLine(penBlack, currentX, 0, currentX, this.boardSizeHeight);
                currentX += this.cellSize;
            }
        }

        public void AddCircle(int x, int y)
        {            
            var cellCenterX = GetCellCenterCoordinate(x);
            var cellCenterY = GetCellCenterCoordinate(y);

            this.graphics.FillEllipse(Brushes.Chocolate, cellCenterX - this.halfShapeSize, 
                cellCenterY - this.halfShapeSize, this.shapeSize, this.shapeSize);
        }

        public void AddTriangle(int x, int y)
        {           
            var cellCenterPointX = GetCellCenterCoordinate(x);
            var cellCenterPointY = GetCellCenterCoordinate(y);

            Point[] pointsOfTriangle = new Point[]
            {
                new Point(cellCenterPointX - this.halfShapeSize, cellCenterPointY + this.halfShapeSize),
                new Point(cellCenterPointX, cellCenterPointY - this.halfShapeSize),
                new Point(cellCenterPointX + this.halfShapeSize, cellCenterPointY + this.halfShapeSize),
            };
            
            this.graphics.FillPolygon(Brushes.Blue, pointsOfTriangle);
        }

        public void AddSquare(int x, int y)
        {
            var cellCenterX = GetCellCenterCoordinate(x);
            var cellCenterY = GetCellCenterCoordinate(y);

            this.graphics.FillRectangle(Brushes.Green, cellCenterX - this.halfShapeSize,
                cellCenterY - this.halfShapeSize, this.shapeSize, this.shapeSize);
        }

        public void AddRelationLine(Point one, Point two)
        {
            var centerOnePointX = GetCellCenterCoordinate(one.X);
            var centerOnePointY = GetCellCenterCoordinate(one.Y);

            var centerTwoPointX = GetCellCenterCoordinate(two.X);
            var centerTwoPointY = GetCellCenterCoordinate(two.Y);

            var pen = new Pen(Color.Red);
            pen.Width = 3;

            this.graphics.DrawLine(pen, centerOnePointX, centerOnePointY, centerTwoPointX, centerTwoPointY);
        }


        private int GetCellCenterCoordinate(int position)
        {
            return position * cellSize + cellSize / 2;
        }

        /// <summary>
        /// Returns a pointer (number by horizontal and vertical) to a cell by coordinates of a some point (e.g. a cursor position)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Point GetCellPointerByCoordinates(int x, int y)
        {
            var cellYNumber = (int)Math.Truncate((decimal)y / cellSize);
            var cellXNumber = (int)Math.Truncate((decimal)x / cellSize);

            return new Point(cellXNumber, cellYNumber);
        }

        private void CalculateSize()
        {
            int cellSizeWidth = this.mainForm.BoardPanel.Width / this.countCellWidth;
            int cellSizeHeight = this.mainForm.BoardPanel.Height / countCellHeight;
            this.cellSize = cellSizeWidth < cellSizeHeight ? cellSizeWidth : cellSizeHeight;
            this.shapeSize = this.cellSize / 2;
            this.halfShapeSize = this.shapeSize / 2;

            this.boardSizeWidth = this.countCellWidth * this.cellSize;
            this.boardSizeHeight = this.countCellHeight * this.cellSize;
        }
    }
}
