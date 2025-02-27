using UnityEngine;

public class RiceBullet : MonoBehaviour
{
    [SerializeField] GameObject explosionPrefab;

    float limitHeight = 15f; // �Ѱ� y��ǥ
    float hAcc = 0.07f; // ��� ���ӵ�
    float dropSpeed = 13f; // �ϰ� �ӵ�

    int damage;
    Transform target;
    Vector3 targetPos;
    float initY;

    bool isRising = true;
    float hspeed = 0;

    public void Initialize(int damage, Transform target)
    {
        this.damage = damage;
        this.target = target;
        targetPos = target.transform.position;
        initY = transform.position.y;
    }

    void Update()
    {
        if (target != null)
            targetPos = target.position;

        if (isRising)
        {
            if (transform.position.y >= initY + limitHeight)
            {
                transform.position = new Vector3(targetPos.x, targetPos.y + limitHeight);
                isRising = false;
            }
            hspeed += hAcc;
            transform.position += new Vector3(0, hspeed) * Time.deltaTime;
        }
        else
        {
            if (transform.position.y <= targetPos.y - 0.1f)
            {
                var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                explosion.GetComponent<RiceExplosion>().Initialize(damage);
                Destroy(gameObject);
            }
            transform.position -= new Vector3(0, dropSpeed) * Time.deltaTime;
        }
    }
}
