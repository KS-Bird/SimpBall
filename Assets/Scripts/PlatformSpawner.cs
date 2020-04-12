using System.Collections;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject PlatformPrefab;
    public GameObject[,] Platforms = new GameObject[4,6];
    public int RowIndex = 0;
    public int ColumnIndex = 0;

    public Transform BallPosition;

    public int MapLength = 0;
    public int MapPartYDistance = 10;
    public float Platform_Y = 0;
    public float Y_Alpha;

    public float Y_DistanceMin = 1.5f;
    public float Y_DistanceMax = 3.5f;

    public float X_DistanceMin = -2.8f;
    public float X_DistanceMax = 2.8f;
    public float X_Alpha;

    public bool BIsSpawning = false;

    public GameObject BallPrefab;
    void Start()
    {
        for (int i = 0; i < Platforms.GetLength(0); ++i)
        {
            for (int j = 0; j < Platforms.GetLength(1); ++j)
            {
                Platforms[i, j] = Instantiate(PlatformPrefab, new Vector2(25, 0), Quaternion.identity);
            }
        }
    }
    void Update()
    {
        StartCoroutine(SpawnPlatforms());
    }

    IEnumerator SpawnPlatforms()
    {
        if (!BIsSpawning)
        {
            if (BallPosition.position.y >= MapLength - MapPartYDistance * 2)
            {
                BIsSpawning = true;

                if (MapLength != 0 && Random.Range(0, 5) != 0)
                {
                    Instantiate(BallPrefab, new Vector2(-2, MapLength), Quaternion.Euler(new Vector3(0f, 0f, -45f)));
                }

                while (true)
                {
                    Y_Alpha = Random.Range(Y_DistanceMin, Y_DistanceMax);
                    Platform_Y += Y_Alpha;

                    if (Platform_Y > MapLength + MapPartYDistance)
                    {
                        Platform_Y -= Y_Alpha;
                        break;
                    }

                    X_Alpha = Random.Range(X_DistanceMin, X_DistanceMax);

                    Platforms[RowIndex, ColumnIndex].SetActive(false);
                    Platforms[RowIndex, ColumnIndex].SetActive(true);
                    Platforms[RowIndex, ColumnIndex].transform.position = new Vector2(X_Alpha, Platform_Y);
                    ColumnIndex++;

                    yield return null;
                }

                MapLength += MapPartYDistance;
                RowIndex = (MapLength / 10) % 4;
                ColumnIndex = 0;

                BIsSpawning = false;
            }
        }
    }
}
