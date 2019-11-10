using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GameOfShapes;

namespace GUI.WinForms
{
    public partial class MainForm : Form , IMainForm
    {
        private const int defaultCountCellWidth = 5;
        private const int defaultCountCellHeight = 5;

        Dictionary<Point, ShapeTypes> _shapes = new Dictionary<Point, ShapeTypes>(new PointEqualityComparer());
        ISession _gameSession;
        int _boardWidth;
        int _boardHeight;
        Board board;
        private ShapeTypes currentShapeForAdd;
       
        public MainForm()
        {
            InitializeComponent();
            this.currentShapeForAdd = ShapeTypes.Circle;
            this.CreateNewBoard(defaultCountCellWidth, defaultCountCellHeight);
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void NewBoard_Click(object sender, EventArgs e)
        {
            NewBoardForm dialogForm = new NewBoardForm(this);
            dialogForm.ShowDialog();
        }

        public void CreateNewBoard(int width, int height)
        {
            this.boardPanel.CreateGraphics().Clear(this.boardPanel.BackColor);
            this.board = new Board(this, width, height);
            _shapes.Clear();
            _gameSession = null;
            _boardHeight = height;
            _boardWidth = width;
            this.EndListeningMouse();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            if (this.board != null) this.board.DrawBoard();
        }

        private void AddCircle_Click(object sender, EventArgs e)
        {
            this.StartListeningMouse(ShapeTypes.Circle);
        }

        private void AddTriangle_Click(object sender, EventArgs e)
        {
            this.StartListeningMouse(ShapeTypes.Triangle);
        }

        private void AddSquare_Click(object sender, EventArgs e)
        {
            this.StartListeningMouse(ShapeTypes.Square);
        }

        private void StartListeningMouse(ShapeTypes typeShapeForAdd)
        {
            this.currentShapeForAdd = typeShapeForAdd;
            this.boardPanel.MouseClick += new MouseEventHandler(BoardPanel_MouseClick);
        }

        private void EndListeningMouse()
        {
            this.currentShapeForAdd = ShapeTypes.Circle;
            this.boardPanel.MouseClick -= new MouseEventHandler(BoardPanel_MouseClick);
        }

        private void BoardPanel_MouseClick(object sender, MouseEventArgs e)
        {
            var cellPoint = this.board.GetCellPointerByCoordinates(e.X, e.Y);
            if (_shapes.ContainsKey(cellPoint))
            {
                return;
            }

            RenderShape(cellPoint, currentShapeForAdd);
            _shapes.Add(cellPoint, currentShapeForAdd);
        }

        private void OnMove(Dictionary<Point, ShapeTypes> data)
        {
            this.boardPanel.CreateGraphics().Clear(this.boardPanel.BackColor);
            this.board = new Board(this, _boardWidth, _boardHeight);
            foreach (var cell in data)
            {
                RenderShape(cell.Key, cell.Value);
            }
        }

        private void OnWon(IShape shape)
        {
            MessageBox.Show("The round has fnished! We have the winner");
        }

        private void RenderShape(Point cellPoint, ShapeTypes shapeType)
        {
            if (this.board != null)
            {
                switch (shapeType)
                {
                    case ShapeTypes.Circle:
                        {
                            this.board.AddCircle(cellPoint.X, cellPoint.Y);
                            break;
                        }
                    case ShapeTypes.Triangle:
                        {
                            this.board.AddTriangle(cellPoint.X, cellPoint.Y);
                            break;
                        }
                    case ShapeTypes.Square:
                        {
                            this.board.AddSquare(cellPoint.X, cellPoint.Y);
                            break;
                        }
                }
            }
        }

        private void NexStep_Click(object sender, EventArgs e)
        {
            if(_gameSession == null)
            {
                BuildSession();
            }
            _gameSession.PlayRound();
            this.EndListeningMouse();
        }

        private void BuildSession()
        {
            var builder = new GameOfShapesFactory(_shapes.ToDictionary(i => i.Key, i => i.Value), _boardWidth, _boardHeight);
            _gameSession = builder.BuildGameAndGetSession();
            _gameSession.ShapeMovedEvent += OnMove;
            _gameSession.SomeShapeWonEvent += OnWon;
        }
    }


}
