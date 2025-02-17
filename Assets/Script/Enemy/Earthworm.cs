using UnityEngine;

public class Earthworm : EnemyBase
{
    [Header("Earthworm")]
    [Tooltip("땅파고 이동하는 최대거리")] [SerializeField] float hideMoveDist;

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
        // 위치 조정
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

        // 현재 상태에서 계속 실행
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
        // 종료 시 1회 실행
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

        // 시작 시 1회 실행
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
