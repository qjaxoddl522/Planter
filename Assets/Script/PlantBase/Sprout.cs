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
    
    public PlantData plantData { get; set; }
    public PlantSpot plantSpot { get; set; }
    public CoinPresenter coinPresenter { get; set; }
    public bool isDirectionLeft { get; set; }
    GrowthController growthController;

    int depth;

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
            plantSpriteRenderer.sprite = plantData.plantSprite;
            plantSpriteHeight = plantSpriteRenderer.bounds.size.y;

            float pivotOffsetY = plantSpriteRenderer.sprite.pivot.y / plantSpriteRenderer.sprite.pixelsPerUnit;
            float offsetY = plantSpriteHeight - pivotOffsetY;

            plantSpriteTransform.position = new Vector3(
                plantSpriteTransform.position.x,
                plantSpriteTransform.position.y - offsetY,
                plantSpriteTransform.position.z);

            depth = -(int)(transform.parent.position.y + 0.5f)*2;
            plantSpriteRenderer.sortingOrder = depth;
            sproutSpriteRenderer.sortingOrder = depth + 1;
            spriteMask.frontSortingOrder = depth;
            spriteMask.backSortingOrder = depth - 1;

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
    }

    void HandleGrowthComplete()
    {
        Vector3 targetPos = new Vector3(transform.position.x, transform.position.y + plantSpriteHeight, transform.position.z);
        sproutSpriteTransform.GetComponent<SproutAway>().isAway = true;
        plantSpriteRenderer.maskInteraction = SpriteMaskInteraction.None;
        spriteTransform.DOJump(targetPos, 1f, 1, 1f).SetEase(Ease.Linear).OnComplete(Plant);
    }

    void Plant()
    {
        var plant = Instantiate(plantData.plantPrefab);
        plant.transform.SetParent(transform.parent);
        plant.transform.position = transform.position;
        plant.GetComponent<SpriteRenderer>().sortingOrder = depth;
        plant.GetComponent<IPlantable>().plantData = plantData;
        plant.GetComponent<IPlantable>().plantSpot = plantSpot;
        plant.GetComponent<IPlantable>().coinPresenter = coinPresenter;
        plant.GetComponent<IPlantable>().isDirectionLeft = isDirectionLeft;
        transform.parent.GetComponent<IPlantSpot>().MyPlant = plant.GetComponent<IPlantable>();
        Destroy(gameObject);
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