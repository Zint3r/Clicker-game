using UnityEngine;
public class BackGroundScript : MonoBehaviour
{
    [SerializeField] private GameObject[] backGround = null;
    private LinksScript links = null;
    private int count;
    private int levelMap;
    private float xPos;
    private GameObject[] backGroundsPositions = null;    
    public GameObject[] BackGroundsPositions { get => backGroundsPositions; }
    void Start()
    {
        levelMap = ScenesParametrs.selectedLevelMap;
        links = FindObjectOfType<LinksScript>();
        count = links.GameMain.AllEnemyCount;
        backGroundsPositions = new GameObject[count];
        xPos = backGround[levelMap].GetComponent<SpriteRenderer>().sprite.texture.width / 100f;        
        InstantiateBackGrounds();
    }
    private void InstantiateBackGrounds()
    {
        for (int i = 0; i < count; i++)
        {
            GameObject newObj = Instantiate(backGround[levelMap], gameObject.transform);
            newObj.transform.position += new Vector3(xPos * i, 0, 0);
            BackGroundsPositions[i] = newObj;
        }        
    }
}