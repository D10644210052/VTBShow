using UnityEngine;
using UnityEngine.UI;
using UniVRM10; // 確保已正確安裝並引用 UniVRM10 套件

public class VRMExpressionController : MonoBehaviour
{
    public GameObject vrmModel; // 指向已加載的 VRM 模型
    public Button blinkButton;  // 用於觸發 Blink 動作的按鈕
    public Button happyButton;  // 用於觸發 Happy 動作的按鈕

    public Vrm10Instance vrmInstance; // VRM10 的實例

    void Start()
    {
        if (vrmModel != null)
        {
            // 獲取 Vrm10Instance，該組件包含表情和動畫控制
            vrmInstance = vrmModel.GetComponent<Vrm10Instance>();
            if (vrmInstance == null)
            {
                Debug.LogError("未找到 Vrm10Instance 組件，請確認 VRM 模型是否正確加載。");
                return;
            }
        }

        // 設置按鈕的點擊事件
        if (blinkButton != null)
        {
            blinkButton.onClick.AddListener(() => SetExpression(ExpressionPreset.blink));
        }

        if (happyButton != null)
        {
            happyButton.onClick.AddListener(() => SetExpression(ExpressionPreset.happy));
        }
    }

    /// <summary>
    /// 設置表情動畫
    /// </summary>
    /// <param name="preset">表情的預設值（如 Blink、Happy）</param>
    public void SetExpression(ExpressionPreset preset)
    {
        if (vrmInstance == null)
        {
            Debug.LogError("Vrm10Instance 尚未初始化！");
            return;
        }

        // 使用 ExpressionKey 根據預設值創建鍵值
        var expressionKey = ExpressionKey.CreateFromPreset(preset);

        // 設置表情的權重（0.0 到 1.0）
        vrmInstance.Runtime.Expression.SetWeight(expressionKey, 1.0f);
        Debug.Log($"已成功設置表情: {preset}");
    }
}
