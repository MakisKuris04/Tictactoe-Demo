using UnityEngine;

public class GameStart : MonoBehaviour
{
    private void Awake()
    {
        GameObject contextObj = new GameObject("ContextManager", typeof(ContextManager));
        contextObj.transform.SetParent(transform);

        GameObject uiObj = new GameObject("UIManager", typeof(UIManager));
        uiObj.transform.SetParent(transform);

        GameObject boardObj = new GameObject("BoardManager", typeof(BoardManager));
        boardObj.transform.SetParent(transform);
        
        GameObject roundObj = new GameObject("RoundManager", typeof(RoundManager));
        roundObj.transform.SetParent(transform);
    }
}