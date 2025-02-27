using DG.Tweening;
using UnityEngine;

public class CornBullet : MonoBehaviour
{
    float jumpPower = 2f;  // 점프(곡선) 높이 조절
    float duration = 1.5f;   // 전체 이동 시간

    int damage;
    Transform target;

    public void Initialize(int damage, Transform target)
    {
        this.damage = damage;
        this.target = target;
    }

    void Start()
    {
        if (target == null)
        {
            Debug.LogWarning("Target이 지정되지 않았습니다.");
            return;
        }

        transform.parent = target.transform;
        transform.DOLocalJump(Vector3.zero, jumpPower, 1, duration)
                 .OnComplete(HitEnemy);
    }

    void HitEnemy()
    {
        if (target != null)
        {
            IEnemy attackable = target.GetComponent<IEnemy>();
            if (attackable != null)
            {
                attackable.TakeDamage(damage, Seed.DefHowitzer);
            }
        }
        Destroy(gameObject);
    }
}
