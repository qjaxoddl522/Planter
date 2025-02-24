using DG.Tweening;
using System;
using UnityEngine;

public class Sprout : MonoBehaviour, IPlantable
{
    [SerializeField] float growAnimationDuration;
    [SerializeField] Transform spriteTransform;
    [SerializeField] Transform sproutSpriteTransform;
    [SerializeField] Transform plantSpriteTransform;
    [SerializeField] SpriteMask spriteMask;

    SpriteRenderer plantSpriteRenderer;
    SpriteRenderer sproutSpriteRenderer;
    float plantSpriteHeight;

    GrowthController growthController;
    int depth;

    [Header("Interface")]
    public Vector2 centerPos { get; set; }
    public PlantData plantData { get; set; }
    public PlantSpot plantSpot { get; set; }
    public CoinPresenter coinPresenter { get; set; }
    public bool isDirectionLeft { get; set; }

    void Awake()
    {
        growthController = gameObject.AddComponent<GrowthController>();
        plantSpriteRenderer = plantSpriteTransform.GetComponent<SpriteRenderer>();
        sproutSpriteRenderer = sproutSpriteTransform.GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        if (plantSpriteRenderer != null)
        {
            // 중심위치 구하기
            Sprite sprite = plantData.plantSprite;
            Vector2 centerPixel = sprite.rect.size * 0.5f;
            Vector2 offsetPixel = centerPixel - sprite.pivot;
            Vector2 offset = offsetPixel / sprite.pixelsPerUnit;
            centerPos = (Vector2)plantSpriteRenderer.transform.position + offset;

            // 식물 스프라이트 땅속에 넣기
            plantSpriteRenderer.sprite = plantData.plantSprite;
            plantSpriteHeight = plantSpriteRenderer.bounds.size.y;

            float pivotOffsetY = plantSpriteRenderer.sprite.pivot.y / plantSpriteRenderer.sprite.pixelsPerUnit;
            float offsetY = plantSpriteHeight - pivotOffsetY;

            plantSpriteTransform.position = new Vector3(
                plantSpriteTransform.position.x,
                plantSpriteTransform.position.y - offsetY,
                plantSpriteTransform.position.z);

            // 정렬 순서 설정
            depth = Modify.GetDepth(transform.position.y);
            plantSpriteRenderer.sortingOrder = depth;
            sproutSpriteRenderer.sortingOrder = depth + 1;
            spriteMask.frontSortingOrder = depth;
            spriteMask.backSortingOrder = depth - 1;

            // 방향 설정
            isDirectionLeft = transform.parent.position.x < 0;
            plantSpriteRenderer.flipX = isDirectionLeft;
        }

        growthController.growthDuration = plantData.growthTime;
        growthController.OnGrowthHalf += HandleGrowthHalf;
        growthController.OnGrowthComplete += HandleGrowthComplete;
    }

    void HandleGrowthHalf()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(spriteTransform.DOMoveY(spriteTransform.position.y + plantSpriteHeight / 2, growAnimationDuration).SetEase(Ease.OutCubic))
           .Insert(0, 
           spriteTransform.DOScale(0.9f, growAnimationDuration / 4).SetEase(Ease.OutCubic))
           .Insert(growAnimationDuration / 4, 
           spriteTransform.DOScale(1.2f, growAnimationDuration / 2).SetEase(Ease.InCubic))
           .Insert(growAnimationDuration / 4 * 3, 
           spriteTransform.DOScale(1.0f, growAnimationDuration / 4).SetEase(Ease.OutCubic))
           ;

        AudioManager.Instance.PlaySFX(AudioManager.SFX.Weep);
    }

    void HandleGrowthComplete()
    {
        Vector3 targetPos = new Vector3(transform.position.x, transform.position.y + plantSpriteHeight, transform.position.z);
        sproutSpriteTransform.GetComponent<SproutAway>().isAway = true;
        plantSpriteRenderer.maskInteraction = SpriteMaskInteraction.None;
        spriteTransform.DOJump(targetPos, 1f, 1, 1f).SetEase(Ease.Linear).OnComplete(Plant);

        AudioManager.Instance.PlaySFX(AudioManager.SFX.Pop);
    }

    void Plant()
    {
        var instance = Instantiate(plantData.plantPrefab);
        instance.transform.SetParent(transform.parent);
        instance.transform.position = transform.position;
        instance.GetComponent<SpriteRenderer>().sortingOrder = depth;

        var plantable = instance.GetComponent<IPlantable>();
        plantable.centerPos = centerPos;
        plantable.plantData = plantData;
        plantable.plantSpot = plantSpot;
        plantable.coinPresenter = coinPresenter;
        plantable.isDirectionLeft = isDirectionLeft;
        transform.parent.GetComponent<IPlantSpot>().MyPlant = instance.GetComponent<IPlantable>();
        DestroyPlant();
    }

    public void DestroyPlant()
    {
        Destroy(gameObject);
    }
}

public class GrowthController : MonoBehaviour
{
    public float growthDuration;
    float elapsedTime;
    bool isGrowHalf;
    public event Action OnGrowthHalf;
    public event Action OnGrowthComplete;

    private void Update()
    {
        if (TimePresenter.isDaytime)
            elapsedTime += Time.deltaTime;

        if (!isGrowHalf && elapsedTime >= growthDuration / 2)
        {
            OnGrowthHalf?.Invoke();
            isGrowHalf = true;
        }

        if (elapsedTime >= growthDuration)
        {
            OnGrowthComplete?.Invoke();
            enabled = false;
        }
    }
}