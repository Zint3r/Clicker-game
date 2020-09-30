﻿using UnityEngine;
public class BackGroundScript : MonoBehaviour
{
    [SerializeField] private GameObject backGround = null;
    private LinksScript links = null;
    private int count;
    private float xPos;
    private GameObject[] backGroundsPositions = null;    
    public GameObject[] BackGroundsPositions { get => backGroundsPositions; }
    void Start()
    {
        links = FindObjectOfType<LinksScript>();
        count = links.EnemySpawn.EnemyPrefabs.Count;
        backGroundsPositions = new GameObject[count];
        xPos = backGround.GetComponent<SpriteRenderer>().sprite.texture.width / 100f;        
        InstantiateBackGrounds();
    }
    private void InstantiateBackGrounds()
    {
        for (int i = 0; i < count - 1; i++)
        {
            GameObject newObj = Instantiate(backGround, gameObject.transform);
            newObj.transform.position += new Vector3(xPos * i, 0, 0);
            BackGroundsPositions[i] = newObj;
        }        
    }
}