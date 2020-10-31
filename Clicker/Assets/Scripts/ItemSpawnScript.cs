using UnityEngine;
public class ItemSpawnScript : MonoBehaviour
{
    private LinksScript links = null;
    [SerializeField] private RectTransform spawnPanel = null;
    [SerializeField] private GameObject[] chestObj = null;
    [SerializeField] private float timeTospawnChest = 30;
    private float currentTimer = 0;
    private void Start()
    {
        links = GetComponent<LinksScript>();
    }
    private void Update()
    {
        if (links.PlayerController.IsBackGroundMove == false)
        {
            TimeToSpawn(timeTospawnChest);
        }        
    }   
    public void SpawnChest()
    {        
        GameObject obj = Instantiate(chestObj[RandomizeItem()]);
        float posX = spawnPanel.rect.width / 300;
        float posY = spawnPanel.transform.position.y / 40;
        obj.transform.position += new Vector3(Random.Range(-posX, posX) + Camera.main.transform.position.x, Camera.main.transform.position.y - posY, 0);       
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