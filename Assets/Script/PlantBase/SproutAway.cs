using UnityEngine;

public class SproutAway : MonoBehaviour
{
    [SerializeField] GameObject effectPrefab;
    [SerializeField] float jumpPowerMin;
    [SerializeField] float jumpPowerMax;
    [SerializeField] float gravity;
    [SerializeField] float hSpeedMin;
    [SerializeField] float hSpeedMax;
    [SerializeField] float durationMin;
    [SerializeField] float durationMax;
    public bool isAway = false;

    float rotAngle;
    float hSpeed;
    float vSpeed;
    float duration;

    void Start()
    {
        rotAngle = Random.Range(1, 5);
        hSpeed = (Random.Range(0, 2) * 2 - 1) * Random.Range(hSpeedMin, hSpeedMax);
        vSpeed = Random.Range(jumpPowerMin, jumpPowerMax);
        duration = Random.Range(durationMin, durationMax);
    }

    void Update()
    {
        if (isAway)
        {
            transform.SetParent(null, true);

            transform.position += new Vector3(hSpeed, vSpeed) * Time.deltaTime;
            transform.Rotate(0, 0, rotAngle);
            vSpeed -= gravity;
            duration -= Time.deltaTime;
        }

        if (duration <= 0)
        {
            Instantiate(effectPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
