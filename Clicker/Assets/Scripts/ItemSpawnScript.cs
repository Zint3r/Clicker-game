using UnityEngine;
public class ItemSpawnScript : MonoBehaviour
{
    [SerializeField] private RectTransform spawnPanel = null;
    [SerializeField] private GameObject[] chestObj = null;
    [SerializeField] private float timeTospawnChest = 30;
    private float currentTimer = 0;
    private void Update()
    {
        TimeToSpawn(timeTospawnChest);
    }   
    public void SpawnChest()
    {        
        GameObject obj = Instantiate(chestObj[RandomizeItem()]);
        float posX = (Screen.width - spawnPanel.rect.width) / 50;
        float posY = (Screen.height - spawnPanel.rect.height) / 150;
        obj.transform.position += new Vector3(Random.Range(-posX, posX) + Camera.main.transform.position.x, Random.Range(-posY, posY) + Camera.main.transform.position.y, 0);       
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
    private int RandomizeItem()
    {
        int number = Random.Range(1, 100);
        if (number < 50)
        {
            return 0;
        }
        else if (number > 50 && number < 75)
        {
            return 1;
        }
        else
        {
            return 2;
        }
    }
}