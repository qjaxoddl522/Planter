using DG.Tweening;
using UnityEngine;

public class CornBullet : MonoBehaviour
{
    float jumpPower = 2f;  // ����(�) ���� ����
    float duration = 1.5f;   // ��ü �̵� �ð�

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
            Debug.LogWarning("Target�� �������� �ʾҽ��ϴ�.");
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
