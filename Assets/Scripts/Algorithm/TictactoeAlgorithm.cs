using System;
using UnityEngine;

public class TictactoeAlgorithm : Actor
{
    public static bool IsBoardLegal(EditorTile[,] board)
    {
        int rows = board.GetLength(0);
        int cols = board.GetLength(1);

        // 检查行
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols - 2; j++)
            {
                if (board[i, j] != null && board[i, j + 1] != null && board[i, j + 2] != null)
                {
                    return true;
                }
            }
        }

        // 检查列
        for (int i = 0; i < rows - 2; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (board[i, j] != null && board[i + 1, j] != null && board[i + 2, j] != null)
                {
                    return true;
                }
            }
        }

        // 检查对角线
        for (int i = 0; i < rows - 2; i++)
        {
            for (int j = 0; j < cols - 2; j++)
            {
                if (board[i, j] != null && board[i + 1, j + 1] != null && board[i + 2, j + 2] != null)
                {
                    return true;
                }
            }
        }

        // 检查反对角线
        for (int i = 0; i < rows - 2; i++)
        {
            for (int j = 2; j < cols; j++)
            {
                if (board[i, j] != null && board[i + 1, j - 1] != null && board[i + 2, j - 2] != null)
                {
                    return true;
                }
            }
        }

        return false;
    }
    
    
    public static bool CheckWin(Tile[,] board, EPieceType pieceType)
    {
        var intBoard = ConvertTilesToIntArray(board);
        return CheckWin(intBoard, pieceType);
    }
    
    public static bool CheckWin(int[,] intBoard, EPieceType pieceType)
    {
        int rows = intBoard.GetLength(0);
        int cols = intBoard.GetLength(1);
        int pieceValue = 1 + (int)pieceType;

        // 检查行
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols - 2; j++)
            {
                if (intBoard[i, j] == pieceValue && intBoard[i, j + 1] == pieceValue && intBoard[i, j + 2] == pieceValue)
                {
                    return true;
                }
            }
        }

        // 检查列
        for (int i = 0; i < rows - 2; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (intBoard[i, j] == pieceValue && intBoard[i + 1, j] == pieceValue && intBoard[i + 2, j] == pieceValue)
                {
                    return true;
                }
            }
        }

        // 检查对角线
        for (int i = 0; i < rows - 2; i++)
        {
            for (int j = 0; j < cols - 2; j++)
            {
                if (intBoard[i, j] == pieceValue && intBoard[i + 1, j + 1] == pieceValue && intBoard[i + 2, j + 2] == pieceValue)
                {
                    return true;
                }
            }
        }

        // 检查反对角线
        for (int i = 0; i < rows - 2; i++)
        {
            for (int j = 2; j < cols; j++)
            {
                if (intBoard[i, j] == pieceValue && intBoard[i + 1, j - 1] == pieceValue && intBoard[i + 2, j - 2] == pieceValue)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public static Vector2Int GetBestMove(Tile[,] board)
    {
        var intBoard = ConvertTilesToIntArray(board);
        int rows = intBoard.GetLength(0);
        int cols = intBoard.GetLength(1);
        int bestScore = int.MinValue;
        Vector2Int bestMove = new Vector2Int(-1, -1);
        int depth = 0;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (intBoard[i, j] == 0)
                {
                    intBoard[i, j] = 2;
                    int score = Minimax(intBoard, ref depth, false);
                    intBoard[i, j] = 0;

                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMove = new Vector2Int(j, i);
                    }
                }
            }
        }

        return bestMove;
    }

    private static int Minimax(int[,] board, ref int depth, bool isMaximizing)
    {
        if (CheckWin(board, EPieceType.PIECE_AI))
        {
            return isMaximizing ? -10 : 10;
        }

        if (CheckWin(board, EPieceType.PIECE_PLAYER))
        {
            return isMaximizing ? 10 : -10;
        }

        if (IsBoardFull(board))
        {
            return 0;
        }

        int bestScore = isMaximizing ? int.MinValue : int.MaxValue;
        if (depth > 5)//探索深度限制
        {
            depth = -1;
            return bestScore;
        }

        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (board[i, j] == 0)
                {
                    board[i, j] = isMaximizing ? 2 : 1;
                    depth++;
                    int score = Minimax(board, ref depth, !isMaximizing);
                    if (depth == -1) return bestScore;
                    board[i, j] = 0;

                    bestScore = isMaximizing ? Math.Max(score, bestScore) : Math.Min(score, bestScore);
                }
            }
        }

        return bestScore;
    }


    public static bool IsBoardFull(Tile[,] board)
    {
        return IsBoardFull(ConvertTilesToIntArray(board));
    }
    public static bool IsBoardFull(int[,] board)
    {
        int rows = board.GetLength(0);
        int cols = board.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (board[i, j] == 0)
                {
                    return false;
                }
            }
        }

        return true;
    }

    private static int[,] ConvertTilesToIntArray(Tile[,] tileMatrix)
    {
        int rows = tileMatrix.GetLength(0);
        int cols = tileMatrix.GetLength(1);

        int[,] result = new int[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Tile tile = tileMatrix[i, j];
                if (tile == null)
                {
                    result[i, j] = -1;
                }
                else if (tile.piece == null)
                {
                    result[i, j] = 0;
                }
                else
                {
                    switch (tile.piece.pieceType)
                    {
                        case EPieceType.PIECE_PLAYER:
                            result[i, j] = 1;
                            break;
                        case EPieceType.PIECE_AI:
                            result[i, j] = 2;
                            break;
                        default:
                            result[i, j] = 0;
                            break;
                    }
                }
            }
        }

        return result;
    }

}
