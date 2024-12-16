using UnityEngine;

public class ViewOfTile : MonoActor
{
    public Vector2Int pos;
    public Transform pieceMountPos => transform.Find("PiecePos");
    private static Material highlightMaterial; // 高亮材质
    private Material originalMaterial; // 原始材质
    private Renderer tileRenderer; // tile 的渲染器

    private void Awake()
    {
        highlightMaterial = Resources.Load<Material>(ResourcePath.MATERIAL_HIGHLIGHT);
        tileRenderer = GetComponent<Renderer>();
        originalMaterial = tileRenderer.material;
    }

    private void OnMouseEnter()
    {
        tileRenderer.material = highlightMaterial;
    }

    private void OnMouseExit()
    {
        tileRenderer.material = originalMaterial;
    }

    private void OnMouseDown()
    {
        SignalCenter.Dispatch<EPieceType, Vector2Int>(ESignalType.SET_PIECE, EPieceType.PIECE_PLAYER, pos);
    }
}