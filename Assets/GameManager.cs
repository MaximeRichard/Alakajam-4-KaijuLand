using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Transform
        playerSpawnPoint,
        startPoint,
        patternsSpawnPoint;

    internal void OnTemplateEnd(TemplateManager templateManager)
    {
        Destroy(spawnedPatterns.Peek());
    }

    internal void OnTemplateStart(TemplateManager templateManager)
    {
        InstantiateRandomPattern();
    }


    public Vector3 Origin = new Vector3(0, 10000, 0);

    public GameObject[] patternsPrefabs;
    public GameObject spawnPointPrefab, playerPrefab;
    public Queue<TemplateManager> spawnedPatterns;

    public CameraFollow2D cameraTracker;
    public FollowPlayerY backgroundFollow;
    public Transform debug;

    private Rigidbody2D playerRigid;

    public float distToSpawnNext = 2f;
    public float distBeforeDestroy = 10f;
    private float currentDistTravelled = 0f;
    private float currentDistToSpawnNext;
    public float scrollFactor = 1f;

    public float playerVelocity;

    public static GameManager Instance { get; internal set; }

    private void Awake()
    {
        Instance = this;
        currentDistToSpawnNext = distToSpawnNext;
        spawnedPatterns = new Queue<TemplateManager>();
    }

    public void Start()
    {
        SpawnPlayer();
    }


    public void SpawnPlayer()
    {
        GameObject sPoint = Instantiate(spawnPointPrefab);
        sPoint.transform.position = startPoint.position;
        GameObject player = Instantiate(playerPrefab);
        playerRigid = player.GetComponent<Rigidbody2D>();
        player.transform.position = playerSpawnPoint.position;
        cameraTracker.target = player.transform;
        spawnedPatterns.Enqueue(sPoint.GetComponent<TemplateManager>());

    }

    public void InstantiateRandomPattern()
    {
        int id = UnityEngine.Random.Range(0, patternsPrefabs.Length);
        GameObject go = Instantiate(patternsPrefabs[id]);
        go.transform.position = spawnedPatterns.Peek().transform.position - Vector3.up * 20;
        spawnedPatterns.Enqueue(go.GetComponent<TemplateManager>());

    }

    private void LateUpdate()
    {
        playerVelocity = playerRigid.velocity.y;
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
        }

        if (currentDistToSpawnNext <= 0)
        {
            currentDistToSpawnNext = distToSpawnNext;
            InstantiateRandomPattern();
        }
    }


}
