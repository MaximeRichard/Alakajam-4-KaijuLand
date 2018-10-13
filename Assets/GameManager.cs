using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Transform
        playerSpawnPoint,
        startPoint,
        patternsSpawnPoint;
    public TextureScroller
        scrollingSide_L,
        scrollingSide_R,
        scrollingBackground;

    internal void DestroyObj(GameObject gameObject)
    {
        foreach (var item in spawnedPatterns)
        {
            spawnedPatterns.Remove(new Pair<float, GameObject>(0, gameObject));
        }
        Destroy(gameObject);
    }

    public GameObject[] patternsPrefabs;
    public GameObject spawnPointPrefab, playerPrefab;
    public List<Pair<float, GameObject>> spawnedPatterns;

    public CameraFollow2D cameraTracker;
    public FollowPlayerY backgroundFollow;

    private Rigidbody2D playerRigid;

    public float distToSpawnNext = 2f;
    public float distBeforeDestroy = 10f;
    private float currentDistTravelled = 0f;
    private float currentDistToSpawnNext;
    public float scrollFactor = 1f;

    public static GameManager Instance { get; internal set; }

    private void Awake()
    {
        Instance = this;
        currentDistToSpawnNext = distToSpawnNext;
        spawnedPatterns = new List<Pair<float, GameObject>>();
    }

    public void Start()
    {
        SpawnPlayer();
        scrollingSide_L.Init();
        scrollingSide_R.Init();
    }


    public void SpawnPlayer()
    {
        GameObject sPoint = Instantiate(spawnPointPrefab);
        sPoint.transform.position = startPoint.position;
        GameObject player = Instantiate(playerPrefab);
        playerRigid = player.GetComponent<Rigidbody2D>();
        player.transform.position = playerSpawnPoint.position;
        cameraTracker.target = player.transform;
        spawnedPatterns.Add(new Pair<float, GameObject>(distBeforeDestroy, sPoint));
        backgroundFollow.target = player.transform;

    }

    public void InstantiateRandomPattern()
    {
        int id = Random.Range(0, patternsPrefabs.Length);
        GameObject go = Instantiate(patternsPrefabs[id]);
        go.transform.position = patternsSpawnPoint.position;
        spawnedPatterns.Add(new Pair<float, GameObject>(distBeforeDestroy, go));

    }

    private void LateUpdate()
    {
        if (playerRigid.velocity.y != 0)
        {

            float distThisFrame = playerRigid.velocity.y * Time.deltaTime;
            currentDistToSpawnNext += distThisFrame;
            currentDistTravelled -= distThisFrame;
            List<Pair<float, GameObject>> toDelete = new List<Pair<float, GameObject>>();
            foreach (var item in spawnedPatterns)
            {
                item.A += distThisFrame;
                if (item.A <= 0) toDelete.Add(item);
            }
            foreach(var item in toDelete)
            {
                Destroy(item.B);
                spawnedPatterns.Remove(item);
            }
            scrollingSide_L.Scroll(playerRigid.velocity.y * scrollFactor);
            scrollingSide_R.Scroll(playerRigid.velocity.y * scrollFactor);
        }

        if (currentDistToSpawnNext <= 0)
        {
            currentDistToSpawnNext = distToSpawnNext;
            InstantiateRandomPattern();
        }
    }


}
