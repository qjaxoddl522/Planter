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

    // 외부에서 효과를 실행하기 위한 메서드
    public void PlayWhiteFlash()
    {
        // 이미 코루틴이 실행 중이라면, elapsedTime만 0으로 초기화
        if (flashCoroutine != null)
        {
            elapsedTime = 0f;
            if (overlayMaterialInstance != null)
            {
                overlayMaterialInstance.SetFloat("_Overlay", 1f);
            }
            return;
        }

        // 새로 시작할 경우, 오버레이 머터리얼 생성 및 설정
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
