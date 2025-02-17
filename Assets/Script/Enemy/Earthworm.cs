using UnityEngine;

public class Earthworm : EnemyBase
{
    [Header("Earthworm")]
    [Tooltip("���İ� �̵��ϴ� �ִ�Ÿ�")] [SerializeField] float hideMoveDist;

    bool isHideUsed = false;

    public override void TakeDamage(int damage, Seed attacker)
    {
        if (!IsHidden)
        {
            Hp -= damage;
            if (Hp <= 0)
            {
                DestroyEnemy();
            }
            flashEffect.PlayWhiteFlash();
        }
    }

    public void Hide()
    {
        spriteRenderer.color = new Color(1, 1, 1, 0);
        IsHidden = true;
        Invoke("HideUp", 2f);
    }

    void HideUp()
    {
        // ��ġ ����
        float plantDist = int.MaxValue;
        var plant = FindClosestPlant();
        if (plant != null)
            plantDist = Mathf.Abs(plant.transform.position.x - transform.position.x) - Range;
        transform.position += new Vector3(Mathf.Min(hideMoveDist, plantDist), 0) * (isDirectionLeft ? -1 : 1);
        
        ChangeState(EnemyState.HideUp);
    }

    public void HideEnd()
    {
        IsHidden = false;
        ChangeState(EnemyState.Walking);
    }

    void Update()
    {
        AttackCooltime -= Time.deltaTime;

        // ���� ���¿��� ��� ����
        switch (currentState)
        {
            case EnemyState.Idle:
                if (AttackCooltime <= 0)
                {
                    if (CheckForAttack())
                    {
                        ChangeState(EnemyState.Attacking);
                    }
                    else
                    {
                        ChangeState(EnemyState.Walking);
                    }
                }
                break;
            case EnemyState.Walking:
                if (CheckForAttack())
                    ChangeState(EnemyState.Idle);
                else if (Hp <= MaxHp / 2 && !isHideUsed)
                    ChangeState(EnemyState.HideDown);
                else
                    transform.position += new Vector3(Speed * Time.deltaTime, 0, 0) * (isDirectionLeft ? -1 : 1);
                break;
        }
    }

    public override void ChangeState(EnemyState newState)
    {
        // ���� �� 1ȸ ����
        switch (currentState)
        {
            case EnemyState.Idle:
                animator.ResetTrigger("Idle");
                break;
            case EnemyState.Walking:
                animator.ResetTrigger("Walk");
                break;
            case EnemyState.Attacking:
                animator.ResetTrigger("Attack");
                break;
        }

        // ���� �� 1ȸ ����
        switch (newState)
        {
            case EnemyState.Idle:
                animator.SetTrigger("Idle");
                spriteRenderer.sprite = enemyData.IdleSprite;
                break;
            case EnemyState.Walking:
                animator.SetTrigger("Walk");
                break;
            case EnemyState.Attacking:
                animator.SetTrigger("Attack");
                AttackCooltime = AttackMaxCooltime;
                break;
            case EnemyState.HideDown:
                animator.SetTrigger("HideDown");
                isHideUsed = true;
                break;
            case EnemyState.HideUp:
                animator.SetTrigger("HideUp");
                spriteRenderer.color = new Color(1, 1, 1, 1);
                break;
        }

        currentState = newState;
    }
}
