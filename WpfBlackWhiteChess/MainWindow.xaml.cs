using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.ComponentModel;
using System.Timers;

namespace WpfBlackWhiteChess
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ChessBoard chessBoard;
        Boolean nextWhite = false;
        System.Timers.Timer myTimer = new System.Timers.Timer(100);
        List<Ellipse> blinkingEllipses = new List<Ellipse>();
        public MainWindow()
        {
            InitializeComponent();
            chessBoard = new ChessBoard();
            chessBoard.initialize();
            refreshChessBoard();
            myTimer.Elapsed +=  HandleTimer;
        }

        private void HandleTimer(Object source, ElapsedEventArgs e)
        {

        }
        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Rectangle_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {

        }

        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(canvas1);
            int indexI = -1;
            int indexJ = -1;
            pointToIndex(point, out indexI, out indexJ);
            if (indexI != -1 && indexJ != -1 && chessBoard.BoardProperty[indexI, indexJ] == ColorEnum.UNDEFINED && chessBoard.isValidToPutChess(indexI, indexJ, nextWhite? ColorEnum.WHITE:ColorEnum.BLACK))
            {
                chessBoard.putChess(indexI, indexJ, nextWhite ? ColorEnum.WHITE : ColorEnum.BLACK);
                refreshChessBoard();
                if (nextWhite)
                {
                    //drawEllipse(point, Colors.White);
                    nextWhite = false;
                }
                else
                {
                    //drawEllipse(point, Colors.Black);
                    nextWhite = true;
                }

                ellipseAnimate();

                Thread computerThread = new Thread(new ThreadStart(computerPutChess));
                computerThread.Start();
            }
        }

        private void computerPutChess()
        {
            Thread.Sleep(2000);
            Dispatcher.BeginInvoke(new Action(() =>
            {
                chessBoard.decideToPutChessAI(nextWhite ? ColorEnum.WHITE : ColorEnum.BLACK);
                nextWhite = !nextWhite;
                refreshChessBoard();
                ellipseAnimate();
                if (chessBoard.checkIfPossibleToPutChess(nextWhite ? ColorEnum.WHITE : ColorEnum.BLACK) == false)
                {
                    nextWhite = !nextWhite;
                    computerPutChess();
                }
                
            }));
            
        }

        private void ellipseAnimate()
        {
            foreach (Tuple<int, int> tupleElement in chessBoard.BlinkingPositions)
            {
                int top = tupleElement.Item1 * 70 + 3;
                int left = tupleElement.Item2 * 70 + 3;
                foreach (var element in canvas1.Children)
                {
                    if (element is Ellipse)
                    {
                        Ellipse ellipse = element as Ellipse;
                        if (Canvas.GetTop(ellipse) == top && Canvas.GetLeft(ellipse) == left)
                        {
                            blinkingEllipses.Add(ellipse);
                        }
                    }
                }
            }
            chessBoard.BlinkingPositions.Clear();
            foreach (Ellipse ellipse in blinkingEllipses)
            {
                DoubleAnimation dblAnim = new DoubleAnimation();
                dblAnim.From = 1.0;
                dblAnim.To = 0.0;

                dblAnim.Duration = new Duration(TimeSpan.FromSeconds(1));
                TranslateTransform tt = new TranslateTransform();
                ellipse.RenderTransform = tt;
                // Reverse when done.
                dblAnim.AutoReverse = true;

                // Loop forever.
                //dblAnim.RepeatBehavior = RepeatBehavior.Forever;
                ellipse.BeginAnimation(Ellipse.OpacityProperty, dblAnim);
            }
        }

        private void computationTimeByAI()
        { 
        }

        private void pointToIndex(Point point, out int indexI, out int indexJ)
        {
            if (point.X < 560 && point.Y < 560)
            {
                indexI = ((int)point.Y / 70);
                indexJ = ((int)point.X / 70);
            }
            else
            {
                indexI = -1;
                indexJ = -1;
            }            
        }

        private void refreshChessBoard()
        {
            UIElement el = null;
            for (int inx = canvas1.Children.Count - 1; inx >= 0; inx--)
                if ((el = canvas1.Children[inx]) is Ellipse)
                    canvas1.Children.Remove(el);

            int whiteNumber = 0;
            int blackNumber = 0;
            for (int i = 0; i < 8; i++ )
                for (int j = 0; j < 8; j++)
                {
                    if (chessBoard.BoardProperty[i, j] == ColorEnum.WHITE)
                    {
                        whiteNumber++;
                        whiteChessNumber.Text = whiteNumber.ToString();
                        drawEllipse(i, j, Colors.White);
                    }
                    else if (chessBoard.BoardProperty[i, j] == ColorEnum.BLACK)
                    {
                        blackNumber++;
                        blackChessNumber.Text = blackNumber.ToString();
                        drawEllipse(i, j, Colors.Black);
                    }
                }

        }


        private void drawEllipse(int indexI, int indexJ, Color color)
        {
            if (indexI >= 0 && indexI < 8 && indexJ >= 0 && indexJ < 8)
            {
                Point point = new Point(indexJ * 70 + 3, indexI * 70 + 3);
                drawEllipse(point, color);
            }
            else
            {
                throw new Exception("Index out of scope!");
            }
        }

        private void drawEllipse(Point point, Color color)
        {
            int indexI = -1;
            int indexJ = -1;
            Point ellipsePosition = calculateEllipsePosition(point, out indexI, out indexJ);
            chessBoard.updateBoardArray(indexI, indexJ, color == Colors.White ? ColorEnum.WHITE : ColorEnum.BLACK);
            Ellipse myEllipse = new Ellipse();
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();

            mySolidColorBrush.Color = color;

            myEllipse.Fill = mySolidColorBrush;
            myEllipse.StrokeThickness = 2;
            myEllipse.Stroke = Brushes.Black;

            // Set the width and height of the Ellipse.
            myEllipse.Width = 64;
            myEllipse.Height = 64;

            if (ellipsePosition.X != -1 && ellipsePosition.Y != -1)
            {
                Canvas.SetTop(myEllipse, ellipsePosition.Y);
                Canvas.SetLeft(myEllipse, ellipsePosition.X);
            }
            canvas1.Children.Add(myEllipse);

        }

        private Point calculateEllipsePosition(Point mouseClick, out int indexI, out int indexJ)
        {
            Point returnValue = new Point();
            if(mouseClick.X < 560 && mouseClick.Y < 560)
            {
                indexI = ((int)mouseClick.Y / 70);
                indexJ = ((int)mouseClick.X / 70);
                returnValue.X = ((int)mouseClick.X / 70)*70 + 3;
                returnValue.Y = ((int)mouseClick.Y / 70)*70 + 3;
                return returnValue;
            }
            indexI = -1;
            indexJ = -1;
            return new Point(-1, -1);            
        }
    }
}
