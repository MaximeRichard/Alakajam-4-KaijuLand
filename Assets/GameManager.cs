﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject[] patternsPrefabs;
    public GameObject spawnPointPrefab, playerPrefab;
    public Queue<TemplateManager> spawnedPatterns;

    public CameraFollow2D cameraTracker;
    public Transform origin;

    private Rigidbody2D playerRigid;

    public float distToSpawnNext = 2f;
    public float distBeforeDestroy = 10f;
    private float currentDistTravelled = 0f;
    private float currentDistToSpawnNext;
    public float scrollFactor = 1f;

    public float playerVelocity;

    public static GameManager Instance { get; internal set; }
    GameObject lastEnqued;

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
        sPoint.transform.position = origin.position;
        GameObject player = Instantiate(playerPrefab);
        playerRigid = player.GetComponent<Rigidbody2D>();
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

    internal void OnTemplateEnd(TemplateManager templateManager)
    {
        Destroy(spawnedPatterns.Dequeue().gameObject);
    }

    internal void OnTemplateStart(TemplateManager templateManager)
    {
        InstantiateRandomPattern();
    }

    private void LateUpdate()
    {
        playerVelocity = playerRigid.velocity.y;
        if (playerRigid.velocity.y != 0)
        {
            float distThisFrame = playerRigid.velocity.y * Time.deltaTime;
            currentDistTravelled -= distThisFrame;
        }

    }


}
