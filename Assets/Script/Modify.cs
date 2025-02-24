using System.Collections.Generic;
using UnityEngine;

public static class Modify
{
    public static int GetDepth(float y)
    {
        return -(int)(y * 10);
    }

    public static void Shuffle<T>(this IList<T> list)
    {
        // 끝에서부터 하나씩 순회하면서 랜덤 인덱스와 스왑
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);

            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
