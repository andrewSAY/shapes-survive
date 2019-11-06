using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GUI.WinForms
{
    public partial class MainForm : Form , IMainForm
    {
        enum TypeShape
        {
            Empty,
            Circle,
            Triangle,
            Square
        }

        Board board;
        private TypeShape currentShapeForAdd;
       
        public MainForm()
        {
            InitializeComponent();
            this.currentShapeForAdd = TypeShape.Empty;
            this.CreateNewBoard(0, 0);
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
            this.EndListeningMouse();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            if (this.board != null) this.board.DrawBoard();
        }

        private void AddCircle_Click(object sender, EventArgs e)
        {
            this.StartListeningMouse(TypeShape.Circle);
        }

        private void AddTriangle_Click(object sender, EventArgs e)
        {
            this.StartListeningMouse(TypeShape.Triangle);
        }

        private void AddSquare_Click(object sender, EventArgs e)
        {
            this.StartListeningMouse(TypeShape.Square);
        }

        private void StartListeningMouse(TypeShape typeShapeForAdd)
        {
            this.currentShapeForAdd = typeShapeForAdd;
            this.boardPanel.MouseClick += new MouseEventHandler(BoardPanel_MouseClick);
        }

        private void EndListeningMouse()
        {
            this.currentShapeForAdd = TypeShape.Empty;
            this.boardPanel.MouseClick -= new MouseEventHandler(BoardPanel_MouseClick);
        }

        private void BoardPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.board != null)
            {
                var cellPointer = this.board.GetCellPointerByCoordinates(e.X, e.Y);
                switch (this.currentShapeForAdd)
                {
                    case TypeShape.Circle:
                    {
                        this.board.AddCircle(cellPointer.X, cellPointer.Y);
                        break;
                    }
                    case TypeShape.Triangle:
                    {
                        this.board.AddTriangle(cellPointer.X, cellPointer.Y);
                        break;
                    }
                    case TypeShape.Square:
                    {
                        this.board.AddSquare(cellPointer.X, cellPointer.Y);
                        break;
                    }
                }
            }
        }

        private void NexStep_Click(object sender, EventArgs e)
        {
            this.EndListeningMouse();
        }


    }


}
