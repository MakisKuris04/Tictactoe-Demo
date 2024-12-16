using System;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class EditorTile : MonoActor
{
    private static Material highlightMaterial;
    
    public Vector2Int pos;
    public Transform viewMountPos;
    private Material originalMaterial; // 原始材质
    private Renderer renderer;
    private bool isSelected = false;
    
    public Action<EditorTile> setTileAction;
    public Action<EditorTile> removeTileAction;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
        originalMaterial = renderer.material;
        highlightMaterial = Resources.Load<Material>(ResourcePath.MATERIAL_HIGHLIGHT);
        viewMountPos = transform.Find("TilePos");
    }

    private void OnMouseEnter()
    {
        ChangeToHighlightMaterial();
    }

    private void OnMouseExit()
    {
        RevertToOriginalMaterial();
    }

    private void OnMouseDown()
    {
        if(!isSelected)
            SetTile();
        else
            RemoveTile();
    }

    public void SetTile()
    {
        isSelected = true;
        setTileAction?.Invoke(this);
    }

    public void RemoveTile()
    {
        isSelected = false;
        removeTileAction?.Invoke(this);
        RevertToOriginalMaterial();
    }

    private void ChangeToHighlightMaterial()
    {
        if (highlightMaterial != null && renderer != null)
        {
            renderer.material = highlightMaterial;
        }
        else
        {
            Debug.LogWarning("Highlight material is not assigned or renderer is null");
        }
    }
    
    private void RevertToOriginalMaterial()
    {
        if (isSelected) return;
        if (renderer != null && originalMaterial != null)
        {
            renderer.material = originalMaterial; // 恢复原始材质
        }
    }
}