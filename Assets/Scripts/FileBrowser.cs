using System.IO;
using UnityEngine;
using UnityEngine.UI;
using SFB; // Standalone File Browser
using UniVRM10;

public class FileBrowser : MonoBehaviour
{
    public Vrm10Instance vrmInstance; // VRM10 �����
    public Button selectFileButton; // �Ω����ɮת����s
    public Transform spawnPoint;   // ���w VRM �ҫ��ͦ�����m
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
        // �j�w���s���I���ƥ�
        if (selectFileButton != null)
        {
            selectFileButton.onClick.AddListener(OpenFileBrowser);
        }
        else
        {
            Debug.LogError("���j�w Select File ���s�I");
        }
    }

    private void OpenFileBrowser()
    {
        // ���}�ɮ׿�ܹ�ܮ�
        string[] paths = StandaloneFileBrowser.OpenFilePanel("��� VRM �ɮ�", "", "vrm", false);
        if (paths.Length > 0)
        {
            LoadVRM(paths[0]);
        }
    }

    private async void LoadVRM(string filePath)
    {
        // �T�O�ɮצs�b
        if (!File.Exists(filePath))
        {
            Debug.LogError("�䤣����w�� VRM �ɮ�: " + filePath);
            return;
        }

        // Ū�� VRM �ɮת��줸�ո��
        byte[] vrmData = File.ReadAllBytes(filePath);

        // �ϥ� UniVRM �ѪR VRM �ɮ�
        UniGLTF.RuntimeGltfInstance runtimeInstance = null;

        try
        {
            // �[�� VRM 10 �ɮ�
            var vrm10Instance = await Vrm10.LoadBytesAsync(
                vrmData,
                canLoadVrm0X: true, // �O�_���\�[�� VRM 0.x �榡�öi���ഫ
                showMeshes: true    // �O�_�ߧY��ܺ���
            );

            if (vrm10Instance != null)
            {
                // �N�ҫ���m�������
                runtimeInstance = vrm10Instance.GetComponent<UniGLTF.RuntimeGltfInstance>();
                runtimeInstance.transform.position = spawnPoint != null ? spawnPoint.position : Vector3.zero;
                runtimeInstance.transform.rotation = Quaternion.identity;
                vrmInstance = runtimeInstance.GetComponent<Vrm10Instance>();
                addbutton();
                Debug.Log("VRM �ҫ��[�����\�åͦ��������: " + filePath);
            }
            else
            {
                Debug.LogError("VRM �ҫ���Ҥƥ��ѡI");
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("VRM �[������: " + ex.Message);
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
            Debug.LogError("Vrm10Instance �|����l�ơI");
            return;
        }

        // �ϥ� ExpressionKey �ھڹw�]�ȳЫ����
        var expressionKey = ExpressionKey.CreateFromPreset(preset);

        // �]�m�����v���]0.0 �� 1.0�^
        vrmInstance.Runtime.Expression.SetWeight(expressionKey, 1.0f);
        Debug.Log($"�w���\�]�m��: {preset}");
    }
}
