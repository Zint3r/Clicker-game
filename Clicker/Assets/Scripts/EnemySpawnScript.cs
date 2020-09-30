using System.Collections.Generic;
using UnityEngine;
public class EnemySpawnScript : MonoBehaviour
{    
    [SerializeField] private EnemyUiScript enemyUI = null;
    [SerializeField] private List<GameObject> enemyPrefabs = null;    
    [SerializeField] private GameObject enemyParentObj = null;
    private LinksScript links = null;
    public List<GameObject> EnemyPrefabs { get => enemyPrefabs; }
    void Start()
    {
        links = GetComponent<LinksScript>();
        EnemySpawn(0);
    }
    public void EnemySpawn(int enemyNumber, Vector3 enemyPosition)
    {
        enemyParentObj.transform.position = new Vector3(Camera.main.transform.position.x, 0, 0);
        GameObject newEnemy = Instantiate(enemyPrefabs[enemyNumber], enemyPosition, Quaternion.identity, enemyParentObj.transform);
        newEnemy.GetComponent<EnemyScript>().EnemyLevel = links.PlayerStats.LocationLevel;
        newEnemy.GetComponent<EnemyScript>().EnemyUI = enemyUI;
        enemyUI.NewEnemyStats(newEnemy.GetComponent<EnemyScript>().GetEnemyName(), newEnemy.GetComponent<EnemyScript>().GetEnemyLevel().ToString());
        links.GameMain.SetCurrentEnemy(newEnemy.GetComponent<EnemyScript>());
    }
    public void EnemySpawn(int enemyNumber)
    {
        GameObject newEnemy = Instantiate(enemyPrefabs[enemyNumber], enemyParentObj.transform);
        newEnemy.GetComponent<EnemyScript>().EnemyLevel = links.PlayerStats.LocationLevel;
        newEnemy.GetComponent<EnemyScript>().EnemyUI = enemyUI;
        enemyUI.NewEnemyStats(newEnemy.GetComponent<EnemyScript>().GetEnemyName(), newEnemy.GetComponent<EnemyScript>().GetEnemyLevel().ToString());
        links.GameMain.SetCurrentEnemy(newEnemy.GetComponent<EnemyScript>());
    }
}