using System.IO;
using UnityEngine;
using UnityEngine.UI;
using SFB; // Standalone File Browser
using UniVRM10;

public class FileBrowser : MonoBehaviour
{
    public Vrm10Instance vrmInstance; // VRM10 的實例
    public Button selectFileButton; // 用於選擇檔案的按鈕
    public Transform spawnPoint;   // 指定 VRM 模型生成的位置
    public Button aa;
    public Button angry;
    public Button blink;
    public Button blinkLeft;
    public Button blinkRight;
    public Button ee;
    public Button happy;
    public Button ih;
    public Button lookDown;
    public Button lookLeft;
    public Button lookRight;
    public Button lookUp;
    public Button neutral;
    public Button oh;
    public Button ou;
    public Button relaxed;
    public Button sad;
    public Button surprised;

    void Start()
    {
        // 綁定按鈕的點擊事件
        if (selectFileButton != null)
        {
            selectFileButton.onClick.AddListener(OpenFileBrowser);
        }
        else
        {
            Debug.LogError("未綁定 Select File 按鈕！");
        }
    }

    private void OpenFileBrowser()
    {
        // 打開檔案選擇對話框
        string[] paths = StandaloneFileBrowser.OpenFilePanel("選擇 VRM 檔案", "", "vrm", false);
        if (paths.Length > 0)
        {
            LoadVRM(paths[0]);
        }
    }

    private async void LoadVRM(string filePath)
    {
        // 確保檔案存在
        if (!File.Exists(filePath))
        {
            Debug.LogError("找不到指定的 VRM 檔案: " + filePath);
            return;
        }

        // 讀取 VRM 檔案的位元組資料
        byte[] vrmData = File.ReadAllBytes(filePath);

        // 使用 UniVRM 解析 VRM 檔案
        UniGLTF.RuntimeGltfInstance runtimeInstance = null;

        try
        {
            // 加載 VRM 10 檔案
            var vrm10Instance = await Vrm10.LoadBytesAsync(
                vrmData,
                canLoadVrm0X: true, // 是否允許加載 VRM 0.x 格式並進行轉換
                showMeshes: true    // 是否立即顯示網格
            );

            if (vrm10Instance != null)
            {
                // 將模型放置於場景中
                runtimeInstance = vrm10Instance.GetComponent<UniGLTF.RuntimeGltfInstance>();
                runtimeInstance.transform.position = spawnPoint != null ? spawnPoint.position : Vector3.zero;
                runtimeInstance.transform.rotation = Quaternion.identity;
                vrmInstance = runtimeInstance.GetComponent<Vrm10Instance>();
                addbutton();
                Debug.Log("VRM 模型加載成功並生成於場景中: " + filePath);
            }
            else
            {
                Debug.LogError("VRM 模型實例化失敗！");
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("VRM 加載失敗: " + ex.Message);
        }
    }
    public void addbutton()
    {
        aa.onClick.AddListener(() => SetExpression(ExpressionPreset.aa));
        angry.onClick.AddListener(() => SetExpression(ExpressionPreset.angry));
        blink.onClick.AddListener(() => SetExpression(ExpressionPreset.blink));
        blinkLeft.onClick.AddListener(() => SetExpression(ExpressionPreset.blinkLeft));
        blinkRight.onClick.AddListener(() => SetExpression(ExpressionPreset.blinkRight));
        ee.onClick.AddListener(() => SetExpression(ExpressionPreset.ee));
        happy.onClick.AddListener(() => SetExpression(ExpressionPreset.happy));
        ih.onClick.AddListener(() => SetExpression(ExpressionPreset.ih));
        lookDown.onClick.AddListener(() => SetExpression(ExpressionPreset.lookDown));
        lookLeft.onClick.AddListener(() => SetExpression(ExpressionPreset.lookLeft));
        lookRight.onClick.AddListener(() => SetExpression(ExpressionPreset.lookRight));
        lookUp.onClick.AddListener(() => SetExpression(ExpressionPreset.lookUp));
        neutral.onClick.AddListener(() => SetExpression(ExpressionPreset.neutral));
        oh.onClick.AddListener(() => SetExpression(ExpressionPreset.oh));
        ou.onClick.AddListener(() => SetExpression(ExpressionPreset.ou));
        relaxed.onClick.AddListener(() => SetExpression(ExpressionPreset.relaxed));
        sad.onClick.AddListener(() => SetExpression(ExpressionPreset.sad));
        surprised.onClick.AddListener(() => SetExpression(ExpressionPreset.surprised));
    }
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
