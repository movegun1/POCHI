using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �����հ� �׿� �����ϴ� �Ÿ��� �����ϴ� ����ü
[System.Serializable]
public struct PrefabWithDistance
{
    public GameObject prefab;  // �߰��� ������
    public float spawnDistance;  // �� �������� �߰��� �Ÿ�
}

public class Spawner : MonoBehaviour
{
    public static Spawner instance;
    public Transform[] spawnPoints;   // Inspector���� �Ҵ��ؾ� ��
    public Transform[] groundPoints;  // Inspector���� �Ҵ��ؾ� ��
    public GameObject[] fly_prefabs;  // Inspector���� �Ҵ��ؾ� ��
    public GameObject[] ground_prefabs; // Inspector���� �Ҵ��ؾ� ��

    public List<PrefabWithDistance> additionalFlyPrefabs;  // �߰��� fly �����հ� �Ÿ� ����Ʈ
    public List<PrefabWithDistance> additionalGroundPrefabs;  // �߰��� ground �����հ� �Ÿ� ����Ʈ

    private List<int> addedFlyPrefabIndexes = new List<int>();  // �߰��� fly �������� �ε��� ����
    private List<int> addedGroundPrefabIndexes = new List<int>();  // �߰��� ground �������� �ε��� ����

    private float lastCheckedDistance = 0f; // ���������� üũ�� �Ÿ�

    private int lastUsedGroundPointIndex = -1; // ���������� ���� ground point �ε���

    // �÷��̾� ������Ʈ�� Transform�� �����ϱ� ���� �߰�
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
        // �߰� �����յ��� ���� �Ÿ��� 10��� ����
        MultiplySpawnDistance(additionalFlyPrefabs, 8);
        MultiplySpawnDistance(additionalGroundPrefabs, 8);

        spawnPoints = FilterPoints(spawnPoints);
        groundPoints = FilterPoints(groundPoints);
    }

    void Update()
    {
        if (playerTransform != null)
        {
            // �÷��̾��� x��ǥ�� ������ ��ȯ�Ͽ� �Ÿ��� ���
            float currentDistance = Mathf.Abs(Mathf.Floor(playerTransform.position.x));

            // currentDistance�� 25�� ó�� ���� �� �� ���� ȣ��
            if (currentDistance > 18f && lastCheckedDistance <= 18f)
            {
                lastCheckedDistance = currentDistance;

                // Fly_Spawn 4�� ȣ��
                for (int i = 0; i < 4; i++)
                {
                    Fly_Spawn();
                }

                // Ground_Spawn 2�� ȣ��
                for (int i = 0; i < 2; i++)
                {
                    Ground_Spawn();
                }
            }

            // fly ������ �߰� ó��
            for (int i = 0; i < additionalFlyPrefabs.Count; i++)
            {
                if (currentDistance >= additionalFlyPrefabs[i].spawnDistance && !addedFlyPrefabIndexes.Contains(i))
                {
                    AddFlyPrefab(additionalFlyPrefabs[i].prefab);
                    addedFlyPrefabIndexes.Add(i);  // �ߺ� �߰� ����
                }
            }

            // ground ������ �߰� ó��
            for (int i = 0; i < additionalGroundPrefabs.Count; i++)
            {
                if (currentDistance >= additionalGroundPrefabs[i].spawnDistance && !addedGroundPrefabIndexes.Contains(i))
                {
                    AddGroundPrefab(additionalGroundPrefabs[i].prefab);
                    addedGroundPrefabIndexes.Add(i);  // �ߺ� �߰� ����
                }
            }
        }
    }

    // ���� �Ÿ��� multiplier�� �����ϴ� �Լ�
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
            Debug.LogError("Fly ������ �Ǵ� ���� ������ �������� �ʾҽ��ϴ�.");
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
            Debug.LogError("Ground ������ �Ǵ� ���� ������ �������� �ʾҽ��ϴ�.");
            return;
        }

        int randomPrefabIndex = Random.Range(0, ground_prefabs.Length);
        int randomSpawnPointIndex;

        // ������ ���� �ε����� �����ϰ� ���ο� �ε����� ����
        do
        {
            randomSpawnPointIndex = Random.Range(0, groundPoints.Length);
        } while (randomSpawnPointIndex == lastUsedGroundPointIndex && groundPoints.Length > 1);

        // ������Ʈ�� ������ �ε��� ����
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
        Debug.Log("�߰��� Fly ������: " + prefab.name);
    }

    private void AddGroundPrefab(GameObject prefab)
    {
        List<GameObject> groundPrefabsList = new List<GameObject>(ground_prefabs);
        groundPrefabsList.Add(prefab);
        ground_prefabs = groundPrefabsList.ToArray();
        Debug.Log("�߰��� Ground ������: " + prefab.name);
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
