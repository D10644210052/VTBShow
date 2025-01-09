using UnityEngine;
using UnityEngine.UI;
using UniVRM10; // �T�O�w���T�w�˨äޥ� UniVRM10 �M��

public class VRMExpressionController : MonoBehaviour
{
    public GameObject vrmModel; // ���V�w�[���� VRM �ҫ�
    public Button blinkButton;  // �Ω�Ĳ�o Blink �ʧ@�����s
    public Button happyButton;  // �Ω�Ĳ�o Happy �ʧ@�����s

    public Vrm10Instance vrmInstance; // VRM10 �����

    void Start()
    {
        if (vrmModel != null)
        {
            // ��� Vrm10Instance�A�Ӳե�]�t���M�ʵe����
            vrmInstance = vrmModel.GetComponent<Vrm10Instance>();
            if (vrmInstance == null)
            {
                Debug.LogError("����� Vrm10Instance �ե�A�нT�{ VRM �ҫ��O�_���T�[���C");
                return;
            }
        }

        // �]�m���s���I���ƥ�
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
    /// �]�m���ʵe
    /// </summary>
    /// <param name="preset">�����w�]�ȡ]�p Blink�BHappy�^</param>
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
