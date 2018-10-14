using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject[] patternsPrefabs;
    public GameObject spawnPointPrefab, playerPrefab;
    public Queue<TemplateManager> spawnedPatterns;

    public CameraFollow2D cameraTracker;
    public Transform origin;

    public int patternsBeforeEnd = 15;
    public int powerObjective;

    public Text powerValue;
    public Slider advancement;


    private Rigidbody2D playerRigid;
    private PlayerManager playerMgr;

    

    public float distToSpawnNext = 2f;
    public float distBeforeDestroy = 10f;
    private float currentDistTravelled = 0f;
    private float currentDistToSpawnNext;
    public float scrollFactor = 1f;



    public float playerVelocity;

    public static GameManager Instance { get; internal set; }
    GameObject lastEnqued;
    private int patternsToPass;
    public GameObject endPattern;

    private void Awake()
    {
        Instance = this;
        currentDistToSpawnNext = distToSpawnNext;
        spawnedPatterns = new Queue<TemplateManager>();
    }

    public void Start()
    {
        patternsToPass = patternsBeforeEnd;
        SpawnPlayer();
    }


    public void SpawnPlayer()
    {
        GameObject sPoint = Instantiate(spawnPointPrefab);
        sPoint.transform.position = origin.position;
        GameObject player = Instantiate(playerPrefab);
        playerRigid = player.GetComponent<Rigidbody2D>();
        playerMgr = player.GetComponent<PlayerManager>();
        player.transform.position = GameObject.FindGameObjectWithTag("PlayerSpawnPoint").transform.position;
        cameraTracker.target = player.transform;
        spawnedPatterns.Enqueue(sPoint.GetComponent<TemplateManager>());
        lastEnqued = sPoint;
    }

    public void InstantiateRandomPattern()
    {
        int id = UnityEngine.Random.Range(0, patternsPrefabs.Length);
        GameObject go = Instantiate(patternsPrefabs[id]);
        go.transform.position = lastEnqued.transform.position - Vector3.up * 20;
        spawnedPatterns.Enqueue(go.GetComponent<TemplateManager>());
        lastEnqued = go;

    }

    private void InstatiateLastPattern()
    {
        GameObject go = Instantiate(endPattern);
        go.transform.position = lastEnqued.transform.position - Vector3.up * 20;
        spawnedPatterns.Enqueue(go.GetComponent<TemplateManager>());
        lastEnqued = go;
    }

    internal void OnTemplateEnd(TemplateManager templateManager)
    {
        Destroy(spawnedPatterns.Dequeue().gameObject);
    }

    internal void OnTemplateStart(TemplateManager templateManager)
    {
        if(patternsToPass > 0)
        {
            InstantiateRandomPattern();
            patternsToPass--;
        }
        else
        {
            InstatiateLastPattern();
        }
        
    }

   

    private void LateUpdate()
    {
        playerVelocity = playerRigid.velocity.y;
        if (playerRigid.velocity.y != 0)
        {
            float distThisFrame = playerRigid.velocity.y * Time.deltaTime;
            currentDistTravelled -= distThisFrame;
        }

        powerValue.text = playerMgr.CurrentPower.ToString();
        advancement.value = (origin.position.y - playerMgr.transform.position.y) / (patternsBeforeEnd * 20f);

    }


}
