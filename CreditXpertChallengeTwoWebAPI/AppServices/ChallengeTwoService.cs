using Microsoft.AspNetCore.Mvc;

namespace CreditXpertChallengeWebAPI.AppServices
{
    public class ChallengeTwoService
    {
        public ChallengeTwoService(int totalMoves)
        {
            BoardStates = new List<UIBoard>();
            StartingBoard = new Board(totalMoves);
        }
        public Board StartingBoard { get; set; }
        public List<UIBoard> BoardStates { get; set; }

        public void ConstructBoards()
        {
            for (int i = 0; i < StartingBoard.TotalMoveCount; i++)
            {
                StartingBoard.Move();
                BoardStates.Add(Map(StartingBoard, i + 1));
            }
        }

        public UIBoard Map(Board board, int move)
        {
            var toReturn = new UIBoard
            {
                XMax = board.XCount * 2 + 1,
                YMax = board.YCount * 2 + 1,
                TotalMoves = move,
                Squares = new List<UISquare>(),
            };

            foreach (var item in board.Squares)
            {
                toReturn.Squares.Add(new UISquare
                {
                    XIndex = Math.Abs(board.XCount + item.Key.Item1),
                    YIndex = Math.Abs(item.Key.Item2 - board.YCount),
                    Color = item.Value
                });
            }
            toReturn.Ant = new UIAnt
            {
                XIndex = Math.Abs(board.XCount + board.Ant.CurrentSquare.X),
                YIndex = Math.Abs(board.Ant.CurrentSquare.Y - board.YCount),
            };

            switch (board.Ant.FacingDirection)
            {
                case Direction.Left:
                    toReturn.Ant.Direction = "&#8592;";
                    break;
                case Direction.Right:
                    toReturn.Ant.Direction = "&#8594;";
                    break;
                case Direction.Up:
                    toReturn.Ant.Direction = "&#8593;";
                    break;
                case Direction.Down:
                    toReturn.Ant.Direction = "&#8595;";
                    break;

            }

            return toReturn;
        }

    }

    public class UIBoard
    {
        public int XMax { get; set; }
        public int YMax { get; set; }

        public int TotalMoves { get; set; }

        public List<UISquare> Squares { get; set; }

        public UIAnt Ant { get; set; }
    }

    public class UISquare
    {
        public int XIndex { get; set; }
        public int YIndex { get; set; }

        public Color Color { get; set; }

    }

    public class UIAnt
    {
        public int XIndex { get; set; }
        public int YIndex { get; set; }
        public string Direction { get; set; }
    }



    public class Board
    {
        public Board() { }
        public Board(int totalMovecount)
        {
            TotalMoveCount = totalMovecount;
            Ant = new Ant(new Square(0, 0, Color.White), Direction.Right);
            Squares = new Dictionary<(int, int), Color>() { { (0, 0), Color.White } };
            XCount = 1;
            YCount = 1;
        }
        public int XCount { get; set; }
        public int YCount { get; set; }
        public int TotalMoveCount { get; set; }
        public Dictionary<(int, int), Color> Squares { get; set; }
        public List<Square> SuareList { get; set; }
        public Ant Ant { get; set; }
        public void Move()
        {

            if (Ant.CurrentSquare.Color == Color.White)
            {
                //change the current square
                Squares[(Ant.CurrentSquare.X, Ant.CurrentSquare.Y)] = Color.Black;

                //Move the Ant
                Ant.MoveClockWiseAndTakeAStep();

                //Set the current Square for the Ant
                if (Squares.ContainsKey((Ant.CurrentSquare.X, Ant.CurrentSquare.Y)))
                {
                    //If the squre the Ant landed on already has color
                    Ant.CurrentSquare.Color = Squares[(Ant.CurrentSquare.X, Ant.CurrentSquare.Y)];
                }
                else
                {
                    //If the color has not been set then default it to white and add to to the list of squares
                    Ant.CurrentSquare.Color = Color.White;
                    Squares.Add((Ant.CurrentSquare.X, Ant.CurrentSquare.Y), Color.White);
                }

            }
            else if (Ant.CurrentSquare.Color == Color.Black)
            {

                Squares[(Ant.CurrentSquare.X, Ant.CurrentSquare.Y)] = Color.White;

                Ant.MoveCounterClockWiseAndTakeAStep();

                if (Squares.ContainsKey((Ant.CurrentSquare.X, Ant.CurrentSquare.Y)))
                {
                    Ant.CurrentSquare.Color = Squares[(Ant.CurrentSquare.X, Ant.CurrentSquare.Y)];
                }
                else
                {
                    Ant.CurrentSquare.Color = Color.White;
                    Squares.Add((Ant.CurrentSquare.X, Ant.CurrentSquare.Y), Color.White);
                }

            }

            if (Math.Abs(Ant.CurrentSquare.X) > XCount) XCount = Math.Abs(Ant.CurrentSquare.X);
            if (Math.Abs(Ant.CurrentSquare.Y) > YCount) YCount = Math.Abs(Ant.CurrentSquare.Y);

        }
        public Board Clone()
        {
            var toReturn = new Board
            {
                XCount = XCount,
                YCount = YCount,
                TotalMoveCount = TotalMoveCount,
                Squares = new Dictionary<(int, int), Color>(),
                Ant = new Ant()
                {
                    CurrentSquare = new Square
                    {
                        X = Ant.CurrentSquare.X,
                        Y = Ant.CurrentSquare.Y,
                        Color = Ant.CurrentSquare.Color
                    },
                    FacingDirection = Ant.FacingDirection,
                }
            };

            foreach (var item in Squares)
            {
                toReturn.Squares.Add(item.Key, item.Value);
            }
            return toReturn;
        }
    }

    public class Square
    {
        public Square()
        {

        }
        public Square(int x, int y, Color color)
        {
            X = x;
            Y = y;
            Color = color;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public Color Color { get; set; }

    }

    public class Ant
    {
        public Ant(Square square, Direction facingDirection)
        {
            CurrentSquare = square;
            FacingDirection = facingDirection;
        }
        public Ant()
        {

        }
        public Square CurrentSquare { get; set; }
        public Direction FacingDirection { get; set; }
        public void MoveClockWiseAndTakeAStep()
        {
            switch (FacingDirection)
            {
                case Direction.Right:
                    FacingDirection = Direction.Down;
                    CurrentSquare.Y--;
                    break;
                case Direction.Down:
                    FacingDirection = Direction.Left;
                    CurrentSquare.X--;
                    break;
                case Direction.Left:
                    FacingDirection = Direction.Up;
                    CurrentSquare.Y++;
                    break;
                case Direction.Up:
                    FacingDirection = Direction.Right;
                    CurrentSquare.X++;
                    break;

            }
        }
        public void MoveCounterClockWiseAndTakeAStep()
        {
            switch (FacingDirection)
            {
                case Direction.Right:
                    FacingDirection = Direction.Up;
                    CurrentSquare.Y++;
                    break;
                case Direction.Down:
                    FacingDirection = Direction.Right;
                    CurrentSquare.X++;
                    break;
                case Direction.Left:
                    FacingDirection = Direction.Down;
                    CurrentSquare.Y--;
                    break;
                case Direction.Up:
                    FacingDirection = Direction.Left;
                    CurrentSquare.X--;
                    break;

            }
        }

    }

    public enum Color
    {
        White, Black
    }

    public enum Direction
    {
        Left, Right, Up, Down
    }

}