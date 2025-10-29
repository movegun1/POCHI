using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 프리팹과 그에 대응하는 거리를 저장하는 구조체
[System.Serializable]
public struct PrefabWithDistance
{
    public GameObject prefab;  // 추가할 프리팹
    public float spawnDistance;  // 이 프리팹이 추가될 거리
}

public class Spawner : MonoBehaviour
{
    public static Spawner instance;
    public Transform[] spawnPoints;   // Inspector에서 할당해야 함
    public Transform[] groundPoints;  // Inspector에서 할당해야 함
    public GameObject[] fly_prefabs;  // Inspector에서 할당해야 함
    public GameObject[] ground_prefabs; // Inspector에서 할당해야 함

    public List<PrefabWithDistance> additionalFlyPrefabs;  // 추가할 fly 프리팹과 거리 리스트
    public List<PrefabWithDistance> additionalGroundPrefabs;  // 추가할 ground 프리팹과 거리 리스트

    private List<int> addedFlyPrefabIndexes = new List<int>();  // 추가된 fly 프리팹의 인덱스 저장
    private List<int> addedGroundPrefabIndexes = new List<int>();  // 추가된 ground 프리팹의 인덱스 저장

    private float lastCheckedDistance = 0f; // 마지막으로 체크한 거리

    private int lastUsedGroundPointIndex = -1; // 마지막으로 사용된 ground point 인덱스

    // 플레이어 오브젝트의 Transform을 참조하기 위해 추가
    public Transform playerTransform;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // 추가 프리팹들의 스폰 거리를 10배로 조정
        MultiplySpawnDistance(additionalFlyPrefabs, 8);
        MultiplySpawnDistance(additionalGroundPrefabs, 8);

        spawnPoints = FilterPoints(spawnPoints);
        groundPoints = FilterPoints(groundPoints);
    }

    void Update()
    {
        if (playerTransform != null)
        {
            // 플레이어의 x좌표를 정수로 변환하여 거리로 사용
            float currentDistance = Mathf.Abs(Mathf.Floor(playerTransform.position.x));

            // currentDistance가 25를 처음 넘을 때 한 번만 호출
            if (currentDistance > 18f && lastCheckedDistance <= 18f)
            {
                lastCheckedDistance = currentDistance;

                // Fly_Spawn 4번 호출
                for (int i = 0; i < 4; i++)
                {
                    Fly_Spawn();
                }

                // Ground_Spawn 2번 호출
                for (int i = 0; i < 2; i++)
                {
                    Ground_Spawn();
                }
            }

            // fly 프리팹 추가 처리
            for (int i = 0; i < additionalFlyPrefabs.Count; i++)
            {
                if (currentDistance >= additionalFlyPrefabs[i].spawnDistance && !addedFlyPrefabIndexes.Contains(i))
                {
                    AddFlyPrefab(additionalFlyPrefabs[i].prefab);
                    addedFlyPrefabIndexes.Add(i);  // 중복 추가 방지
                }
            }

            // ground 프리팹 추가 처리
            for (int i = 0; i < additionalGroundPrefabs.Count; i++)
            {
                if (currentDistance >= additionalGroundPrefabs[i].spawnDistance && !addedGroundPrefabIndexes.Contains(i))
                {
                    AddGroundPrefab(additionalGroundPrefabs[i].prefab);
                    addedGroundPrefabIndexes.Add(i);  // 중복 추가 방지
                }
            }
        }
    }

    // 스폰 거리를 multiplier로 조정하는 함수
    private void MultiplySpawnDistance(List<PrefabWithDistance> prefabs, float multiplier)
    {
        for (int i = 0; i < prefabs.Count; i++)
        {
            PrefabWithDistance tempPrefab = prefabs[i];
            tempPrefab.spawnDistance *= multiplier;
            prefabs[i] = tempPrefab;
        }
    }

    public void Fly_Spawn()
    {
        if (fly_prefabs == null || fly_prefabs.Length == 0 || spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("Fly 프리팹 또는 스폰 지점이 설정되지 않았습니다.");
            return;
        }

        int randomPrefabIndex = Random.Range(0, fly_prefabs.Length);
        int randomSpawnPointIndex = Random.Range(0, spawnPoints.Length);

        GameObject prefabToSpawn = fly_prefabs[randomPrefabIndex];
        Transform spawnPoint = spawnPoints[randomSpawnPointIndex];

        Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);
    }

    public void Ground_Spawn()
    {
        if (ground_prefabs == null || ground_prefabs.Length == 0 || groundPoints == null || groundPoints.Length == 0)
        {
            Debug.LogError("Ground 프리팹 또는 스폰 지점이 설정되지 않았습니다.");
            return;
        }

        int randomPrefabIndex = Random.Range(0, ground_prefabs.Length);
        int randomSpawnPointIndex;

        // 이전에 사용된 인덱스를 제외하고 새로운 인덱스를 선택
        do
        {
            randomSpawnPointIndex = Random.Range(0, groundPoints.Length);
        } while (randomSpawnPointIndex == lastUsedGroundPointIndex && groundPoints.Length > 1);

        // 업데이트된 마지막 인덱스 저장
        lastUsedGroundPointIndex = randomSpawnPointIndex;

        GameObject prefabToSpawn = ground_prefabs[randomPrefabIndex];
        Transform groundPoint = groundPoints[randomSpawnPointIndex];

        float prefabY = prefabToSpawn.transform.position.y;
        Vector3 spawnPosition = new Vector3(groundPoint.position.x, prefabY, groundPoint.position.z);

        Instantiate(prefabToSpawn, spawnPosition, groundPoint.rotation);
    }

    private void AddFlyPrefab(GameObject prefab)
    {
        List<GameObject> flyPrefabsList = new List<GameObject>(fly_prefabs);
        flyPrefabsList.Add(prefab);
        fly_prefabs = flyPrefabsList.ToArray();
        Debug.Log("추가된 Fly 프리팹: " + prefab.name);
    }

    private void AddGroundPrefab(GameObject prefab)
    {
        List<GameObject> groundPrefabsList = new List<GameObject>(ground_prefabs);
        groundPrefabsList.Add(prefab);
        ground_prefabs = groundPrefabsList.ToArray();
        Debug.Log("추가된 Ground 프리팹: " + prefab.name);
    }

    private Transform[] FilterPoints(Transform[] points)
    {
        List<Transform> filteredPoints = new List<Transform>();
        foreach (Transform point in points)
        {
            if (point != this.transform)
            {
                filteredPoints.Add(point);
            }
        }
        return filteredPoints.ToArray();
    }
}
