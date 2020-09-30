﻿using UnityEngine;
public class ChestsSpawnScript : MonoBehaviour
{
    [SerializeField] private RectTransform spawnPanel = null;
    [SerializeField] private GameObject chestObj = null;
    [SerializeField] private float timeTospawnChest = 30;
    private float currentTimer = 0;
    private void Update()
    {
        TimeToSpawn(timeTospawnChest);
    }   
    public void SpawnChest()
    {
        GameObject obj = Instantiate(chestObj);
        float posX = (Screen.width - spawnPanel.rect.width) / 50;
        float posY = (Screen.height - spawnPanel.rect.height) / 150;
        obj.transform.position += new Vector3(Random.Range(- posX, posX), Random.Range(-posY, posY), 0);       
    }
    private void TimeToSpawn(float timeToSpawn)
    {
        if (currentTimer < timeToSpawn)
        {
            currentTimer += Time.deltaTime;
        }
        else
        {
            SpawnChest();
            currentTimer = 0;
        }
    }
}