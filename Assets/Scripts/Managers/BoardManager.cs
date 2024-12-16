using System;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

public class BoardManager : MonoActor
{
    private GameObject boardObj;
    private EditorBoard editorOfBoard;
    
    private GameObject tileView_A;
    private GameObject tileView_B;
    private GameObject piece_Player;
    private GameObject piece_AI;
    
    private Tile[,] tileMatrix;
    private bool isPlayerTurn;
    public static ObjectPool<GameObject> tilePoolA;
    public static ObjectPool<GameObject> tilePoolB;
    public static ObjectPool<GameObject> pieceAIPool;
    public static ObjectPool<GameObject> piecePlayerPool;

    private void Awake()
    {
        LoadResources();
        boardObj = new GameObject("Board");
        ObjectPoolInit();
    }

    private void Start()
    {
        editorOfBoard.GenerateEditorBoard();
        RegisterSignals();
    }

    private void RegisterSignals()
    {
        SignalCenter.AddListener<EditorTile[,]>(ESignalType.GENERATE_BOARD, GenerateBoard);
        SignalCenter.AddListener<EPieceType, Vector2Int>(ESignalType.SET_PIECE, SetPiece);
        
        SignalCenter.AddListener(ESignalType.PLAYER_TURN, PlayerTurn);
        SignalCenter.AddListener(ESignalType.AI_TURN, AITurn);
        
        SignalCenter.AddListener(ESignalType.CLEAR_BOARD, ClearBoard);
        SignalCenter.AddListener(ESignalType.CLEAR_PIECES, ClearPieces);
    }

    #region Board Setting
    
    private void GenerateBoard(EditorTile[,] tiles)
    {
        Debug.Log("Generate Board");
        tileMatrix = new Tile[tiles.GetLength(0),tiles.GetLength(1)];
        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                SetTile(tiles[x, y]);
            }
        }
    }

    private void SetTile(EditorTile editorTile)
    {
        if(editorTile== null) return;
        var pos = editorTile.pos;
        var mountPoint = editorTile.viewMountPos;
        
        //data
        var data = new DataOfTile(pos);
        
        //view
        var viewObj = (pos.y + pos.x) % 2 == 0 ? tilePoolA.Get() : tilePoolB.Get();
        var view = viewObj.GetComponent<ViewOfTile>();
        view.transform.position = mountPoint.position;
        view.transform.SetParent(boardObj.transform);
        view.pos = pos;
        view.gameObject.SetActive(true);
        
        tileMatrix[pos.y, pos.x] = new Tile(pos, data, view);
    }

    private Tile GetTile(Vector2Int pos)
    {
        return tileMatrix[pos.y, pos.x];
    }
    
    private void SetPiece(EPieceType pieceType, Vector2Int pos)
    {
        var tile = GetTile(pos);
        if (tile == null)
        {
            Debug.Log($"Tile {pos} is null");
            return;
        }
        
        if (tile.hasPiece)
        {
            Debug.Log("Tile has Piece");
            return;
        }

        if (!isPlayerTurn && pieceType == EPieceType.PIECE_PLAYER)
        {
            Debug.Log("Now is AI Turn");
            return;
        }

        Debug.Log($"Set piece {pos}");
        var viewObj = pieceType == EPieceType.PIECE_PLAYER ? piecePlayerPool.Get() : pieceAIPool.Get();
        viewObj.transform.position = tile.view.transform.position;
        viewObj.SetActive(true);
        tile.SetPiece(pieceType, viewObj);

        //胜利
        if (TictactoeAlgorithm.CheckWin(tileMatrix, pieceType))
        {
            Debug.Log($"###################Player {pieceType} Win###################");
            SignalCenter.Dispatch(ESignalType.CLOSE_UI, typeof(InGameUIActor));
            SignalCenter.Dispatch<Type, object>(ESignalType.OPEN_UI, typeof(RoundEndUIActor), pieceType);
            return;
        }
        //平局
        if (TictactoeAlgorithm.IsBoardFull(tileMatrix))
        {
            Debug.Log("################### D R A W ###################");
            SignalCenter.Dispatch(ESignalType.CLOSE_UI, typeof(InGameUIActor));
            SignalCenter.Dispatch<Type, object>(ESignalType.OPEN_UI, typeof(RoundEndUIActor), null);
            return;
        }
        
        var nextRoundState = isPlayerTurn? ERoundState.AI_TURN : ERoundState.PLAYER_TURN;
        SignalCenter.Dispatch(ESignalType.ROUND_STATE_CHANGE, nextRoundState);
    }

    private void ClearBoard()
    {
        if (tileMatrix == null) return;
        for (int x = 0; x < tileMatrix.GetLength(0); x++)
        {
            for (int y = 0; y < tileMatrix.GetLength(1); y++)
            {
                var tile = tileMatrix[x, y];
                tile?.OnRemove();
            }
        }
        tileMatrix = null;
    }

    private void ClearPieces()
    {
        if (tileMatrix == null) return;
        for (int x = 0; x < tileMatrix.GetLength(0); x++)
        {
            for (int y = 0; y < tileMatrix.GetLength(1); y++)
            {
                var tile = tileMatrix[x, y];
                if (tile != null && tile.hasPiece)
                {
                    tile.piece.OnRemove();
                    tile.piece = null;
                }
            }
        }
    }

    #endregion

    #region AI & Player Turn

    private void AITurn()
    {
        isPlayerTurn = false;
        var bestMove = TictactoeAlgorithm.GetBestMove(tileMatrix);
        SetPiece(EPieceType.PIECE_AI, bestMove);
    }

    private void PlayerTurn()
    {
        isPlayerTurn = true;
    }
    
    #endregion

    private void LoadResources()
    {
        tileView_A = Instantiate(Resources.Load<GameObject>(ResourcePath.TILE_VIEW_A));
        tileView_B = Instantiate(Resources.Load<GameObject>(ResourcePath.TILE_VIEW_B));
        tileView_A.AddComponent<ViewOfTile>();
        tileView_B.AddComponent<ViewOfTile>();
        tileView_A.SetActive(false);
        tileView_B.SetActive(false);
        
        piece_Player = Instantiate(Resources.Load<GameObject>(ResourcePath.PIECE_PLAYER));
        piece_AI = Instantiate(Resources.Load<GameObject>(ResourcePath.PIECE_AI));
        piece_Player.AddComponent<ViewOfPiece>();
        piece_AI.AddComponent<ViewOfPiece>();
        piece_Player.SetActive(false);
        piece_AI.SetActive(false);
    }
    
    private void ObjectPoolInit()
    {
        editorOfBoard = new GameObject("EditorBoard").AddComponent<EditorBoard>();
        tilePoolA = new ObjectPool<GameObject>(() => Instantiate(tileView_A),
            view => { view.gameObject.SetActive(true); },
            view => { view.gameObject.SetActive(false); },
            view => { Object.Destroy(view.gameObject); },
            false, 10, 70);
        tilePoolB = new ObjectPool<GameObject>(() => Instantiate(tileView_B),
            view => { view.gameObject.SetActive(true); },
            view => { view.gameObject.SetActive(false); },
            view => { Object.Destroy(view.gameObject); },
            false, 10, 70);
        pieceAIPool = new ObjectPool<GameObject>(() => Instantiate(piece_AI),
            view => { view.gameObject.SetActive(true); },
            view => { view.gameObject.SetActive(false); },
            view => { Object.Destroy(view.gameObject); },
            false, 10, 70);
        piecePlayerPool = new ObjectPool<GameObject>(() => Instantiate(piece_Player),
            view => { view.gameObject.SetActive(true); },
            view => { view.gameObject.SetActive(false); },
            view => { Object.Destroy(view.gameObject); },
            false, 10, 70);
    }
}


#region Tile & Piece

public class Tile
{
    public Vector2Int pos;
    public DataOfTile data;
    public ViewOfTile view;
    
    public Piece piece;
    public bool hasPiece => piece != null;
    
    public Tile(Vector2Int pos, DataOfTile data, ViewOfTile view)
    {
        this.pos = pos;
        this.data = data;
        this.view = view;
    }

    public void SetPiece(EPieceType type, GameObject pieceViewObj)
    {
        pieceViewObj.transform.SetParent(view.pieceMountPos);
        pieceViewObj.SetActive(true);
        
        var pieceData = new DataOfPiece(pos);
        var pieceView = pieceViewObj.GetComponent<ViewOfPiece>();
        piece = new Piece(type, pieceData, pieceView);
    }

    public void OnRemove()
    {
        if(hasPiece)
            piece.OnRemove();
        if ((pos.y + pos.x) % 2 == 0)
            BoardManager.tilePoolA.Release(view.gameObject);
        else
            BoardManager.tilePoolB.Release(view.gameObject);
    }
}

public class Piece
{
    public EPieceType pieceType;
    public DataOfPiece pieceData;
    public ViewOfPiece pieceView;

    public Piece(EPieceType type, DataOfPiece pieceData, ViewOfPiece pieceView)
    {
        pieceType = type;
        this.pieceData = pieceData;
        this.pieceView = pieceView;
    }
    
    public void OnRemove()
    {
        if (pieceType == EPieceType.PIECE_PLAYER)
            BoardManager.piecePlayerPool.Release(pieceView.gameObject);
        else
            BoardManager.pieceAIPool.Release(pieceView.gameObject);
    }
}

public enum EPieceType
{
    PIECE_PLAYER,
    PIECE_AI
}

#endregion
