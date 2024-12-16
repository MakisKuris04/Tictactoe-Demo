using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EditorBoard : MonoActor
{
    private int width = 15;//限定奇数
    private int height = 9;//限定奇数
    private float radius = 0.5f;
    float zOffset = -1.5f;
    
    private GameObject editorTile;
    private Stack<EditorTile> selectionStack = new Stack<EditorTile>();
    private EditorTile[,] selectedTiles;
    
    public ObjectPool<GameObject> editorTilePool;

    private void Awake()
    {
        editorTile ??= Instantiate(Resources.Load<GameObject>(ResourcePath.EDITOR_TILE));
        editorTile.AddComponent<EditorTile>();
        editorTile.SetActive(false);
        gameObject.SetActive(false);
        editorTilePool = new ObjectPool<GameObject>(() => Instantiate(editorTile), tile => tile.SetActive(true), tile => tile.SetActive(false), Destroy, true, 10, 200);
    }

    public EditorBoard()
    { 
        RegisterSignals();
    }

    private void RegisterSignals()
    {
        SignalCenter.AddListener(ESignalType.EDIT_BOARD_START, BeginEditBoard);
    }

    private void Update()
    {
        //bool isCtrlDown = Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt);
        if (Input.GetKeyDown(KeyCode.Z))//编辑器内测试Ctrl + Z会和Unity快捷键冲突，使用Z代替
        {
            RevertLastSelection();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            ClearEditorBoard();
        }
        
        //Temp:地图生成
        if (Input.GetKeyDown(KeyCode.G))
        {
            EndEditBoard();
        }
    }

    private void BeginEditBoard()
    {
        gameObject.SetActive(true);
        ClearEditorBoard();
        SignalCenter.Dispatch<Type, object>(ESignalType.OPEN_UI, typeof(EditorUIActor), null);
        SignalCenter.Dispatch(ESignalType.CLEAR_BOARD);
    }
    
    public void GenerateEditorBoard()
    {
        selectedTiles ??= new EditorTile[height, width];
        
        float x = 0;
        float y = 0;
        float offset = radius * 2;

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                x = j - (width - 1) / 2;
                y = (height - 1) / 2 - i;
                
                GameObject tileObj = Instantiate(editorTile, new Vector3(x * offset, y * offset, zOffset), Quaternion.identity);
                var tile = tileObj.GetComponent<EditorTile>();
                tile.pos = new Vector2Int(j, i);
                tile.setTileAction = SetTile;
                tile.removeTileAction = RemoveTile;
                
                tileObj.transform.SetParent(transform);
                tileObj.SetActive(true);
            }
        }
    }

    private void EndEditBoard()
    {
        if (!TictactoeAlgorithm.IsBoardLegal(selectedTiles))
        {
            Debug.Log("Board is not legal");
            SignalCenter.Dispatch(ESignalType.GENERATE_WARNING_TIP);
            return;
        }
        
        SignalCenter.Dispatch(ESignalType.CLOSE_UI, typeof(EditorUIActor));
        SignalCenter.Dispatch<EditorTile[,]>(ESignalType.GENERATE_BOARD, selectedTiles);
        SignalCenter.Dispatch(ESignalType.ROUND_STATE_CHANGE, ERoundState.DRAW_LOTS);

        gameObject.SetActive(false);
    }

    private void SetTile(EditorTile tile)
    {
        if(selectedTiles[tile.pos.y, tile.pos.x] is not null) return;
        Debug.Log("SetTile");
        
        selectionStack.Push(tile);
        selectedTiles[tile.pos.y, tile.pos.x] = tile;
    }

    private void RemoveTile(EditorTile tile)
    {
        if(selectedTiles[tile.pos.y, tile.pos.x] is not null) return;
        Debug.Log("RemoveTile");
        
        EditorTileBackToPool(tile);
    }

    private void RevertLastSelection()
    {
        if(0 == selectionStack.Count) return;
        var lastSelection = selectionStack.Peek();
        lastSelection.RemoveTile();
        selectionStack.Pop();
        EditorTileBackToPool(lastSelection);
    }

    private void ClearEditorBoard()
    {
        while(selectionStack.Count > 0)
            RevertLastSelection();
        selectedTiles = new EditorTile[height, width];
    }

    private void EditorTileBackToPool(EditorTile tile)
    {
        selectedTiles[tile.pos.y, tile.pos.x] = null;
        //todo:对象池回收
    }
}
