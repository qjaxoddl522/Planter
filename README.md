# Planter
[만들래] 10분 게임 콘테스트 방치편 출품작

## 🖥️ 프로젝트 소개
1분 동안 식물을 심고, 1분 동안 방어하여 5일 동안 생존하면 승리하는 게임입니다.

![화면 캡처 2025-02-28 145458](https://github.com/user-attachments/assets/52230ec4-e546-4ba2-bc97-04ad4ef8a72c)

## ⏰ 개발 기간
* 25.2.01. ~ 25.2.28.

## ⚙️ 개발 환경
* Unity 6 (6000.0.31f1)

## 🦾 주요 로직 구성
### 데이터 관리
![image](https://github.com/user-attachments/assets/66c01910-b892-4cc6-9ce8-0e3bfcfe5079)
- 정적인 데이터를 쉽게 변경하고 관리하기 위해 Scriptable Object를 사용하였다.

![image](https://github.com/user-attachments/assets/98ee1551-1e99-4ffb-8c1a-e70de693a7dd)
- 적의 구성을 알려주기 위해 공격할 적의 종류와 양을 미리 지정하고, 이 정보를 바탕으로 표기하였다.

### UI
![image](https://github.com/user-attachments/assets/38273032-0df8-40a3-b5df-a25d52a437b0)
- MVVM을 적용하기에는 소규모 프로젝트였기 때문에 구성이 복잡하지 않으면서 데이터와의 결합도를 낮출 수 있는 MVP 패턴을 사용하였다.
- 각각 시간과 코인에 대한 MVP를 만들었다.

### 적과 식물의 로직
```csharp
// 벌은 인식하지 못하도록 오버라이딩
protected override Transform FindClosestEnemy()
{
    EnemyBase[] enemies = FindObjectsByType<EnemyBase>(FindObjectsSortMode.None);
    Transform closestEnemy = null;
    float closestDistance = Mathf.Infinity;

    foreach (EnemyBase enemy in enemies)
    {
        if ((isDirectionLeft && transform.position.x < enemy.transform.position.x) ||
        (!isDirectionLeft && transform.position.x > enemy.transform.position.x))
            continue;

        float distance = Mathf.Abs(transform.position.x - enemy.transform.position.x);
        if (distance <= closestDistance && !enemy.IsHidden && enemy.enemyData.enemyID != Enemy.Bee)
        {
            closestDistance = distance;
            closestEnemy = enemy.transform;
        }
    }

    return closestEnemy;
}
```
- 각각 Base 클래스를 만들고, 이를 상속받아 각자 필요한 기능은 오버라이딩하여 변경하였다.
- 예를 들면 완두콩은 벌을 타격할 수 없으므로, 적을 감지하는 FindClosestEnemy 메서드를 Base 클래스에 있는 메서드를 사용하지 않고, 오버라이딩하여 벌을 제외시키도록 구성하였다.
