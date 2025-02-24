using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class PlantSpotGenerator : MonoBehaviour
{
    [SerializeField] OtherSystemData sysData;
    [SerializeField] CoinPresenter coinPresenter;
    [SerializeField] TimePresenter timePresenter;
    [SerializeField] GameObject plantSpotPrefab;
    [SerializeField] GameObject spotSignPrefab;

    Transform leftSignTr, rightSignTr;
    int leftSpotLength, rightSpotLength;

    void Start()
    {
        leftSpotLength = sysData.initSpotLength;
        rightSpotLength = sysData.initSpotLength;

        for (int i = 1; i <= sysData.initSpotLength; i++)
        {
            CreateSpotColumn(i);
            CreateSpotColumn(-i);
        }

        leftSignTr = Instantiate(spotSignPrefab, GetSpotPos(-(leftSpotLength + 1), 0), Quaternion.identity).transform;
        var leftSign = leftSignTr.GetComponent<ISpotSign>();
        leftSign.coinPresenter = coinPresenter;
        leftSign.timePresenter = timePresenter;
        leftSign.isLeft = true;
        leftSign.OnSpotExtended += ExtendPlantSpot;
        leftSign.price = sysData.spotExtendPrice;

        rightSignTr = Instantiate(spotSignPrefab, GetSpotPos(rightSpotLength + 1, 0), Quaternion.identity).transform;
        var rightSign = rightSignTr.GetComponent<ISpotSign>();
        rightSign.coinPresenter = coinPresenter;
        rightSign.timePresenter = timePresenter;
        rightSign.isLeft = false;
        rightSign.OnSpotExtended += ExtendPlantSpot;
        rightSign.price = sysData.spotExtendPrice;
    }

    public void CreateSpotColumn(int xRow)
    {
        CreatePlantSpot(GetSpotPos(xRow, -1f));
        CreatePlantSpot(GetSpotPos(xRow, 0f));
        CreatePlantSpot(GetSpotPos(xRow, 1f));
    }

    void CreatePlantSpot(Vector2 pos)
    {
        var inst = Instantiate(plantSpotPrefab, transform);
        inst.transform.position = pos;
        inst.GetComponent<IPlantSpot>().coinPresenter = coinPresenter;
    }

    void ExtendPlantSpot(bool isLeft)
    {
        if (isLeft)
        {
            leftSpotLength++;
            CreateSpotColumn(-leftSpotLength);
            leftSignTr.position = GetSpotPos(-(leftSpotLength + 1), 0);
            if (leftSpotLength == sysData.maxSpotLength)
                leftSignTr.gameObject.SetActive(false);
        }
        else
        {
            rightSpotLength++;
            CreateSpotColumn(rightSpotLength);
            rightSignTr.position = GetSpotPos(rightSpotLength + 1, 0);
            if (rightSpotLength == sysData.maxSpotLength)
                rightSignTr.gameObject.SetActive(false);
        }
    }

    Vector2 GetSpotPos(int xRow, float y)
    {
        int dir = xRow > 0 ? 1 : -1;
        return new Vector2(-(dir * 0.5f + xRow * 1.2f), y);
    }
}
