using UnityEngine;

public class GrassDecorator : MonoBehaviour
{
    [SerializeField] int grassNum;
    [SerializeField] Sprite[] grassSprites;
    int rowSize = 10, colSize = 6;

    void Start()
    {
        for (int i = 0; i < grassNum; i++)
        {
            int row = Random.Range(-rowSize/2, rowSize/2+1);
            int col = Random.Range(-colSize/2, colSize/2+1);
            int spriteIndex = Random.Range(0, grassSprites.Length);
            GameObject grass = new GameObject("Grass");
            grass.transform.parent = transform;
            grass.transform.position = new Vector3(row, col, 0);
            grass.AddComponent<SpriteRenderer>().sprite = grassSprites[spriteIndex];
        }
    }
}
