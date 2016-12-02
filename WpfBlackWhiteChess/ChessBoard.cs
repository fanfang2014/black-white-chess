using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfBlackWhiteChess
{
    enum ColorEnum
    {
        WHITE = -1,
        BLACK = 1,
        UNDEFINED = 0
    }

    class ChessBoard
    {
        private ColorEnum[,] board = new ColorEnum[8, 8];
        private List<Tuple<int, int>> blinkingPositions = new List<Tuple<int, int>>();
        public ColorEnum[,] BoardProperty
        {
            get
            {
                return this.board;
            }
        }

        public List<Tuple<int, int>> BlinkingPositions {
            get
            {
                return this.blinkingPositions;
            }
        }

        public void initialize()
        {
            for (int i = 0; i < 8; i++ )
                for (int j = 0; j < 8; j++)
            {
                board[i,j] = 0;
                if(i == 3 && j == 3)
                {
                    board[i, j] = ColorEnum.WHITE;
                }
                else if (i == 3 && j == 4)
                {
                    board[i, j] = ColorEnum.BLACK;
                }
                else if (i == 4 && j == 3)
                {
                    board[i, j] = ColorEnum.BLACK;
                }
                else if (i == 4 && j == 4)
                {
                    board[i, j] = ColorEnum.WHITE;
                }
            }
        }


        public void updateBoardArray(int i, int j, ColorEnum color)
        {
            if (i >= 0 && i < 8 && j >= 0 && j < 8)
            {
                board[i, j] = color;
            }
        }

        public Boolean decideToPutChessAI(ColorEnum color)
        {
            if (isValidToPutChess(0, 0, color))
            {
                putChess( 0,  0, color);
                return true;
            }
            else if (isValidToPutChess(0, 7, color))
            {
                putChess(0, 7, color);
                return true;
            }
            else if (isValidToPutChess(7, 0, color))
            {
                putChess(7, 0, color);
                return true;
            }
            else if (isValidToPutChess(7, 7, color))
            {
                putChess(7, 7, color);
                return true;
            }
            else if (tryToPutAtBoarder(color))
            {
                return true;
            }
            else if (tryToPutAtInnerCircle(color))
            {
                return true;
            }
            else if(tryToPutAtMediumCircle(color))
            {
                return true;
            }
            else if (tryToPutAtAroundCorners(color))
            {
                return true;
            }
            return false;
        }

        public Boolean checkIfPossibleToPutChess(ColorEnum color)
        {
            if (isValidToPutChess(0, 0, color))
            {
                return true;
            }
            else if (isValidToPutChess(0, 7, color))
            {
                return true;
            }
            else if (isValidToPutChess(7, 0, color))
            {
                return true;
            }
            else if (isValidToPutChess(7, 7, color))
            {
                return true;
            }
            else if (isPossibleToPutAtBoarder(color))
            {
                return true;
            }
            else if (isPossibleToPutAtInnerCircle(color))
            {
                return true;
            }
            else if (isPossibleToPutAtMediumCircle(color))
            {
                return true;
            }
            else if (isPossibleToPutAtAroundCorners(color))
            {
                return true;
            }
            return false;
        }

        public Boolean tryToPutAtAroundCorners(ColorEnum color)
        {
            if (isValidToPutChess(0, 1, color))
            {
                putChess(0, 1, color);
                return true;
            }
            else if (isValidToPutChess(1, 0, color))
            {
                putChess(1, 0, color);
                return true;
            }
            else if (isValidToPutChess(0, 6, color))
            {
                putChess(0, 6, color);
                return true;
            }
            else if (isValidToPutChess(6, 0, color))
            {
                putChess(6, 0, color);
                return true;
            }
            else if (isValidToPutChess(7, 6, color))
            {
                putChess(7, 6, color);
                return true;
            }
            else if (isValidToPutChess(6, 7, color))
            {
                putChess(6, 7, color);
                return true;
            }
            else if (isValidToPutChess(7, 1, color))
            {
                putChess(7, 1, color);
                return true;
            }
            else if (isValidToPutChess(1, 7, color))
            {
                putChess(1, 7, color);
                return true;
            }
            else if (isValidToPutChess(1, 1, color))
            {
                putChess(1, 1, color);
                return true;
            }
            else if (isValidToPutChess(1, 6, color))
            {
                putChess(1, 6, color);
                return true;
            }
            else if (isValidToPutChess(6, 1, color))
            {
                putChess(6, 1, color);
                return true;
            }
            else if (isValidToPutChess(6, 6, color))
            {
                putChess(6, 6, color);
                return true;
            }
            return false;
        }

        public Boolean isPossibleToPutAtAroundCorners(ColorEnum color)
        {
            if (isValidToPutChess(0, 1, color))
            {
                return true;
            }
            else if (isValidToPutChess(1, 0, color))
            {
                return true;
            }
            else if (isValidToPutChess(0, 6, color))
            {
                return true;
            }
            else if (isValidToPutChess(6, 0, color))
            {
                return true;
            }
            else if (isValidToPutChess(7, 6, color))
            {
                return true;
            }
            else if (isValidToPutChess(6, 7, color))
            {
                return true;
            }
            else if (isValidToPutChess(7, 1, color))
            {
                return true;
            }
            else if (isValidToPutChess(1, 7, color))
            {
                return true;
            }
            else if (isValidToPutChess(1, 1, color))
            {
                return true;
            }
            else if (isValidToPutChess(1, 6, color))
            {
                return true;
            }
            else if (isValidToPutChess(6, 1, color))
            {
                return true;
            }
            else if (isValidToPutChess(6, 6, color))
            {
                return true;
            }
            return false;
        }

        public Boolean tryToPutAtInnerCircle(ColorEnum color)
        {
            for (int i = 2; i < 6; i++)
            {
                if (isValidToPutChess(i, 2, color))
                {
                    putChess(i, 2, color);
                    return true;
                }
                else if (isValidToPutChess(i, 5, color))
                {
                    putChess(i, 5, color);
                    return true;
                }
                else if (isValidToPutChess(2, i, color))
                {
                    putChess(2, i, color);
                    return true;
                }
                else if (isValidToPutChess(5, i, color))
                {
                    putChess(5, i, color);
                    return true;
                }
            }
            return false;
        }

        public Boolean isPossibleToPutAtInnerCircle(ColorEnum color)
        {
            for (int i = 2; i < 6; i++)
            {
                if (isValidToPutChess(i, 2, color))
                {
                    return true;
                }
                else if (isValidToPutChess(i, 5, color))
                {
                    return true;
                }
                else if (isValidToPutChess(2, i, color))
                {
                    return true;
                }
                else if (isValidToPutChess(5, i, color))
                {
                    return true;
                }
            }
            return false;
        }

        public Boolean tryToPutAtMediumCircle(ColorEnum color)
        {
            for (int i = 2; i < 6; i++)
            {
                if (isValidToPutChess(i, 1, color))
                {
                    putChess(i, 1, color);
                    return true;
                }
                else if (isValidToPutChess(i, 6, color))
                {
                    putChess(i, 6, color);
                    return true;
                }
                else if (isValidToPutChess(1, i, color))
                {
                    putChess(1, i, color);
                    return true;
                }
                else if (isValidToPutChess(6, i, color))
                {
                    putChess(6, i, color);
                    return true;
                }
            }
            return false;
        }

        public Boolean isPossibleToPutAtMediumCircle(ColorEnum color)
        {
            for (int i = 2; i < 6; i++)
            {
                if (isValidToPutChess(i, 1, color))
                {
                    return true;
                }
                else if (isValidToPutChess(i, 6, color))
                {
                    return true;
                }
                else if (isValidToPutChess(1, i, color))
                {
                    return true;
                }
                else if (isValidToPutChess(6, i, color))
                {
                    return true;
                }
            }
            return false;
        }

        public Boolean tryToPutAtBoarder(ColorEnum color)
        {
            for (int i = 2; i < 6; i++)
            {
                if (isValidToPutChess(i, 0, color))
                {
                    putChess(i, 0, color);
                    return true;
                }
                else if (isValidToPutChess(0, i, color))
                {
                    putChess(0, i, color);
                    return true;
                }
                else if (isValidToPutChess(7, i, color))
                {
                    putChess(7, i, color);
                    return true;
                }
                else if (isValidToPutChess(i, 7, color))
                {
                    putChess(i, 7, color);
                    return true;
                }
            }

            return false;
        }

        public Boolean isPossibleToPutAtBoarder(ColorEnum color)
        {
            for (int i = 2; i < 6; i++)
            {
                if (isValidToPutChess(i, 0, color))
                {
                    return true;
                }
                else if (isValidToPutChess(0, i, color))
                {
                    return true;
                }
                else if (isValidToPutChess(7, i, color))
                {
                    return true;
                }
                else if (isValidToPutChess(i, 7, color))
                {
                    return true;
                }
            }

            return false;
        }

        public Boolean isValidToPutChess(int indexI, int indexJ, ColorEnum colorEnum)
        {
            if (board[indexI, indexJ] == ColorEnum.UNDEFINED)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (i == 0)
                    {
                        if ((indexI - 1 >= 0) && (int)board[indexI - 1, indexJ] * (int)colorEnum == -1)
                        {
                            for (int j = indexI - 2; j >= 0; j--)
                            {
                                if ((int)board[j, indexJ] * (int)colorEnum == 1)
                                {
                                    return true;
                                }
                                else if ((int)board[j, indexJ] * (int)colorEnum == 0)
                                {
                                    break;
                                }
                            }
                        }
                    }
                    else if (i == 1)
                    {
                        if ((indexJ - 1 >= 0) && (int)board[indexI, indexJ - 1] * (int)colorEnum == -1)
                        {
                            for (int j = indexJ - 2; j >= 0; j--)
                            {
                                if ((int)board[indexI, j] * (int)colorEnum == 1)
                                {
                                    return true;
                                }
                                else if ((int)board[indexI, j] * (int)colorEnum == 0)
                                {
                                    break;
                                }
                            }
                        }
                    }
                    else if (i == 2)
                    {
                        if ((indexI - 1 >= 0) && (indexJ - 1 >= 0) && (int)board[indexI - 1, indexJ - 1] * (int)colorEnum == -1)
                        {
                            for (int a = indexI - 2, b = indexJ - 2; a >= 0 && b >= 0; a--, b--)
                            {
                                if ((int)board[a, b] * (int)colorEnum == 1)
                                {
                                    return true;
                                }
                                else if ((int)board[a, b] * (int)colorEnum == 0)
                                {
                                    break;
                                }
                            }
                        }
                    }
                    else if (i == 3)
                    {
                        if ((indexI + 1 < 8) && (indexJ - 1 >= 0) && (int)board[indexI + 1, indexJ - 1] * (int)colorEnum == -1)
                        {
                            for (int a = indexI + 2, b = indexJ - 2; a < 8 && b >= 0; a++, b--)
                            {
                                if ((int)board[a, b] * (int)colorEnum == 1)
                                {
                                    return true;
                                }
                                else if ((int)board[a, b] * (int)colorEnum == 0)
                                {
                                    break;
                                }
                            }
                        }
                    }
                    else if (i == 4)
                    {
                        if ((indexJ + 1 < 8) && (int)board[indexI, indexJ + 1] * (int)colorEnum == -1)
                        {
                            for (int j = indexJ + 2; j < 8; j++)
                            {
                                if ((int)board[indexI, j] * (int)colorEnum == 1)
                                {
                                    return true;
                                }
                                else if ((int)board[indexI, j] * (int)colorEnum == 0)
                                {
                                    break;
                                }
                            }
                        }
                    }
                    else if (i == 5)
                    {
                        if ((indexI + 1 < 8) && (indexJ + 1 < 8) && (int)board[indexI + 1, indexJ + 1] * (int)colorEnum == -1)
                        {
                            for (int a = indexI + 2, b = indexJ + 2; a < 8 && b < 8; a++, b++)
                            {
                                if ((int)board[a, b] * (int)colorEnum == 1)
                                {
                                    return true;
                                }
                                else if ((int)board[a, b] * (int)colorEnum == 0)
                                {
                                    break;
                                }
                            }
                        }
                    }
                    else if (i == 6)
                    {
                        if ((indexI + 1 < 8) && (int)board[indexI + 1, indexJ] * (int)colorEnum == -1)
                        {
                            for (int j = indexI + 2; j < 8; j++)
                            {
                                if ((int)board[j, indexJ] * (int)colorEnum == 1)
                                {
                                    return true;
                                }
                                else if ((int)board[j, indexJ] * (int)colorEnum == 0)
                                {
                                    break;
                                }
                            }
                        }
                    }
                    else if (i == 7)
                    {
                        if ((indexI - 1 >= 0) && (indexJ + 1 < 8) && (int)board[indexI - 1, indexJ + 1] * (int)colorEnum == -1)
                        {
                            for (int a = indexI - 2, b = indexJ + 2; a >= 0 && b < 8; a--, b++)
                            {
                                if ((int)board[a, b] * (int)colorEnum == 1)
                                {
                                    return true;
                                }
                                else if ((int)board[a, b] * (int)colorEnum == 0)
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        public void putChess(int indexI, int indexJ, ColorEnum colorEnum)
        {
            for (int i = 0; i < 8; i++)
            {
                if (i == 0)
                {
                    if ((indexI - 1 >= 0) && (int)board[indexI - 1, indexJ] * (int)colorEnum == -1)
                    {
                        for (int j = indexI - 2; j >= 0; j--)
                        {
                            if ((int)board[j, indexJ] * (int)colorEnum == -1)
                            {
                                continue;
                            }
                            else if ((int)board[j, indexJ] * (int)colorEnum == 0)
                            {
                                break;
                            }
                            else if ((int)board[j, indexJ] * (int)colorEnum == 1)
                            {
                                blinkingPositions.Add(new Tuple<int, int>(j, indexJ));
                                for (int a = j + 1; a <= indexI; a++)
                                {
                                    blinkingPositions.Add(new Tuple<int, int>(a, indexJ));
                                    board[a, indexJ] = colorEnum;
                                }

                                break;
                            }
                        }
                    }
                }
                else if (i == 1)
                {
                    if ((indexJ - 1 >= 0) && (int)board[indexI, indexJ - 1] * (int)colorEnum == -1)
                    {
                        for (int j = indexJ - 2; j >= 0; j--)
                        {
                            if ((int)board[indexI, j] * (int)colorEnum == 1)
                            {
                                blinkingPositions.Add(new Tuple<int, int>(indexI, j));
                                for (int a = j + 1; a <= indexJ; a++)
                                {
                                    blinkingPositions.Add(new Tuple<int, int>(indexI, a));
                                    board[indexI, a] = colorEnum;
                                }
                                break;
                            }
                            else if ((int)board[indexI, j] * (int)colorEnum == -1)
                            {
                                continue;
                            }
                            else if ((int)board[indexI, j] * (int)colorEnum == 0)
                            {
                                break;
                            }
                        }
                    }
                }
                else if (i == 2)
                {
                    if ((indexI - 1 >= 0) && (indexJ - 1 >= 0) && (int)board[indexI - 1, indexJ - 1] * (int)colorEnum == -1)
                    {
                        for (int a = indexI - 2, b = indexJ - 2; a >= 0 && b >= 0; a--, b--)
                        {
                            if ((int)board[a, b] * (int)colorEnum == 1)
                            {
                                blinkingPositions.Add(new Tuple<int, int>(a, b));
                                for (int c = a + 1, d = b+1; c <= indexI && d<= indexJ; c++, d++)
                                {
                                    blinkingPositions.Add(new Tuple<int, int>(c, d));
                                    board[c, d] = colorEnum;
                                }
                                break;
                            }
                            else if ((int)board[a, b] * (int)colorEnum == 0)
                            {
                                break;
                            }
                            else if ((int)board[a, b] * (int)colorEnum == -1)
                            {
                                continue;
                            }
                        }
                    }
                }
                else if (i == 3)
                {
                    if ((indexI + 1 < 8) && (indexJ - 1 >= 0) && (int)board[indexI + 1, indexJ - 1] * (int)colorEnum == -1)
                    {
                        for (int a = indexI + 2, b = indexJ - 2; a < 8 && b >= 0; a++, b--)
                        {
                            if ((int)board[a, b] * (int)colorEnum == 1)
                            {
                                blinkingPositions.Add(new Tuple<int, int>(a, b));
                                for (int c = a - 1, d = b + 1; c >= indexI && d <= indexJ; c--, d++)
                                {
                                    blinkingPositions.Add(new Tuple<int, int>(c, d));
                                    board[c, d] = colorEnum;
                                }
                                break;
                            }
                            else if ((int)board[a, b] * (int)colorEnum == 0)
                            {
                                break;
                            }
                            else if ((int)board[a, b] * (int)colorEnum == -1)
                            {
                                continue;
                            }
                        }
                    }
                }
                else if (i == 4)
                {
                    if ((indexJ + 1 < 8) && (int)board[indexI, indexJ + 1] * (int)colorEnum == -1)
                    {
                        for (int j = indexJ + 2; j < 8; j++)
                        {
                            if ((int)board[indexI, j] * (int)colorEnum == 1)
                            {
                                blinkingPositions.Add(new Tuple<int, int>(indexI, j));
                                for (int a = j - 1; a >= indexJ; a--)
                                {
                                    blinkingPositions.Add(new Tuple<int, int>(indexI, a));
                                    board[indexI, a] = colorEnum;
                                }
                                break;
                            }
                            else if ((int)board[indexI, j] * (int)colorEnum == 0)
                            {
                                break;
                            }
                            else if ((int)board[indexI, j] * (int)colorEnum == -1)
                            {
                                continue;
                            }
                        }
                    }
                }
                else if (i == 5)
                {
                    if ((indexI + 1 < 8) && (indexJ + 1 < 8) && (int)board[indexI + 1, indexJ + 1] * (int)colorEnum == -1)
                    {
                        for (int a = indexI + 2, b = indexJ + 2; a < 8 && b < 8; a++, b++)
                        {
                            if ((int)board[a, b] * (int)colorEnum == 1)
                            {
                                blinkingPositions.Add(new Tuple<int, int>(a, b));
                                for (int c = a - 1, d = b - 1; c >= indexI && d >= indexJ; c--, d--)
                                {
                                    blinkingPositions.Add(new Tuple<int, int>(c, d));
                                    board[c, d] = colorEnum;
                                }
                                break;
                            }
                            else if ((int)board[a, b] * (int)colorEnum == 0)
                            {
                                break;
                            }
                            else if ((int)board[a, b] * (int)colorEnum == -1)
                            {
                                continue;
                            }
                        }
                    }
                }
                else if (i == 6)
                {
                    if ((indexI + 1 < 8) && (int)board[indexI + 1, indexJ] * (int)colorEnum == -1)
                    {
                        for (int j = indexI + 2; j < 8; j++)
                        {
                            if ((int)board[j, indexJ] * (int)colorEnum == 1)
                            {
                                blinkingPositions.Add(new Tuple<int, int>(j, indexJ));
                                for (int a = j - 1; a >= indexI; a--)
                                {
                                    blinkingPositions.Add(new Tuple<int, int>(a, indexJ));
                                    board[a, indexJ] = colorEnum;
                                }
                                break;
                            }
                            else if ((int)board[j, indexJ] * (int)colorEnum == 0)
                            {
                                break;
                            }
                            else if ((int)board[j, indexJ] * (int)colorEnum == -1)
                            {
                                continue;
                            }
                        }
                    }
                }
                else if (i == 7)
                {
                    if ((indexI - 1 >= 0) && (indexJ + 1 < 8) && (int)board[indexI - 1, indexJ + 1] * (int)colorEnum == -1)
                    {
                        for (int a = indexI - 2, b = indexJ + 2; a >= 0 && b < 8; a--, b++)
                        {
                            if ((int)board[a, b] * (int)colorEnum == 1)
                            {
                                blinkingPositions.Add(new Tuple<int, int>(a, b));
                                for (int c = a + 1, d = b - 1; c <= indexI && d >= indexJ; c++, d--)
                                {
                                    blinkingPositions.Add(new Tuple<int, int>(c, d));
                                    board[c, d] = colorEnum;
                                }
                                break;
                            }
                            else if ((int)board[a, b] * (int)colorEnum == 0)
                            {
                                break;
                            }
                            else if ((int)board[a, b] * (int)colorEnum == -1)
                            {
                                continue;
                            }
                        }
                    }
                }
            }
        }
    }
}
