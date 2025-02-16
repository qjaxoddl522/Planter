using UnityEngine;
using System.Collections;

public class FlashEffect : MonoBehaviour
{
    public float flashDuration;
    [SerializeField] Material whiteOverlayMaterial;

    SpriteRenderer spriteRenderer;
    Material originalMaterial;
    Material overlayMaterialInstance;

    Coroutine flashCoroutine;
    float elapsedTime = 0f;

    void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
    }

    // �ܺο��� ȿ���� �����ϱ� ���� �޼���
    public void PlayWhiteFlash()
    {
        // �̹� �ڷ�ƾ�� ���� ���̶��, elapsedTime�� 0���� �ʱ�ȭ
        if (flashCoroutine != null)
        {
            elapsedTime = 0f;
            if (overlayMaterialInstance != null)
            {
                overlayMaterialInstance.SetFloat("_Overlay", 1f);
            }
            return;
        }

        // ���� ������ ���, �������� ���͸��� ���� �� ����
        overlayMaterialInstance = Instantiate(whiteOverlayMaterial);
        overlayMaterialInstance.SetTexture("_MainTex", originalMaterial.mainTexture);
        overlayMaterialInstance.SetFloat("_Overlay", 1f);

        spriteRenderer.material = overlayMaterialInstance;
        flashCoroutine = StartCoroutine(FlashCoroutine());
    }

    IEnumerator FlashCoroutine()
    {
        elapsedTime = 0f;
        while (elapsedTime < flashDuration)
        {
            elapsedTime += Time.deltaTime;
            float overlayValue = Mathf.Lerp(1f, 0f, elapsedTime / flashDuration);
            overlayMaterialInstance.SetFloat("_Overlay", overlayValue);
            yield return null;
        }
        overlayMaterialInstance.SetFloat("_Overlay", 0f);
        spriteRenderer.material = originalMaterial;
        flashCoroutine = null;
    }
}
