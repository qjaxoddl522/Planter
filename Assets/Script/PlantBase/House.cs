using DG.Tweening;
using UnityEngine;

public class House : MonoBehaviour, IHitable
{
    [Tooltip("1회 좌에서 우로 가는데 걸리는 시간")]
    [SerializeField] float shakeSpeed;
    [SerializeField] GameObject smokeEffectPrefab;

    public void TakeDamage(int damage)
    {
        if (!GameProcessManager.Instance.isGameEnd)
        {
            GameProcessManager.Instance.GameEnd();
            GameOver();
        }
    }

    void GameOver()
    {
        Instantiate(smokeEffectPrefab, transform);
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMoveX(transform.position.x + 0.05f, shakeSpeed / 2))
           .Append(transform.DOMoveX(transform.position.x - 0.05f, shakeSpeed))
           .Append(transform.DOMoveX(transform.position.x, shakeSpeed / 2))
           .SetLoops(-1, LoopType.Restart);
    }
}
