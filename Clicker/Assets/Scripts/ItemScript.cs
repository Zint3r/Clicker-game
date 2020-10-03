using UnityEngine;
public class ItemScript : MonoBehaviour
{
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private bool mpPotion = false;
    [SerializeField] private bool hpPotion = false;
    [SerializeField] private bool goldChest = false;
    public bool MpPotion { get => mpPotion; }
    public bool HpPotion { get => hpPotion; }
    public bool GoldChest { get => goldChest; }
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