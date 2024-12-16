using UnityEngine;
using UnityEngine.UI;

public class TipFadeUp : MonoBehaviour
{
    public Vector3 riseSpeed = Vector3.up * 10.0f; // 上升速度
    public float fadeSpeed = 0.5f; // 淡出速度
    private bool isInPool = true;

    private Text textComponent;
    private Color originalColor;

    private void Start()
    {
        textComponent = GetComponent<Text>();
        originalColor = textComponent.color;
    }

    public void OutPool()
    {
        isInPool = false;
    }

    private void Update()
    {
        if(isInPool) return;
        transform.Translate(riseSpeed * Time.deltaTime);

        Color currentColor = textComponent.color;
        currentColor.a -= fadeSpeed * Time.deltaTime;
        textComponent.color = currentColor;

        if (currentColor.a <= 0)
        {
            textComponent.color = originalColor;
            isInPool = true;
            EditorUIActor.tipPool.Release(gameObject);
        }
    }
}