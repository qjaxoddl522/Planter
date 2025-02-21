using DG.Tweening;
using UnityEngine;

public interface ISpawnBehavior
{
    void Spawn(GameObject coin);
}

public class JumpSpawn : ISpawnBehavior
{
    public void Spawn(GameObject coin)
    {
        coin.transform.DOJump(new Vector3(
            coin.transform.position.x + Random.Range(-1.5f, 1.5f),
            coin.transform.position.y + Random.Range(-0.5f, 0.5f)),
            1.5f, 1, 1f).SetEase(Ease.Linear);
    }
}

public class SimpleSpawn : ISpawnBehavior
{
    public void Spawn(GameObject coin)
    {
        Debug.Log("SimpleSpawn: ������ �ܼ��� �����˴ϴ�!");
    }
}

public interface ICoinClicker
{
    void SetSpawnBehavior(ISpawnBehavior behavior);
    CoinPresenter coinPresenter { get; set; }
}

public class CoinClicker : MonoBehaviour, ICoinClicker
{
    public CoinPresenter coinPresenter { get; set; }
    ISpawnBehavior spawnBehavior;

    [SerializeField] GameObject effectPrefab;

    void Start()
    {
        spawnBehavior.Spawn(gameObject);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] hitAll = Physics2D.RaycastAll(mousePos, Vector2.zero);

            foreach (RaycastHit2D hit in hitAll)
            {
                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    coinPresenter.GetCoin(1);
                    Instantiate(effectPrefab, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
            }
        }
    }

    public void SetSpawnBehavior(ISpawnBehavior behavior)
    {
        spawnBehavior = behavior;
    }
}
