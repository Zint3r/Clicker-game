using UnityEngine;
public class ChestScript : MonoBehaviour
{
    [SerializeField] private float lifeTime = 3f;    
    private void Start()
    {        
        ChestLifeTime();
    }
    private void ChestLifeTime()
    {
        Destroy(gameObject, lifeTime);
    }
    public void OnChestClick()
    {        
        Destroy(gameObject);
    }    
}