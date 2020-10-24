using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class CheckSlotScript : MonoBehaviour
{
    private string path = null;
    [SerializeField] private Sprite heroIcon = null;
    [SerializeField] private Image slotImage = null;
    void Start()
    {
        path = Application.dataPath + "/Save.json";
        CheckHeroesSlots();
    }
    private void CheckHeroesSlots()
    {
        if (File.Exists(path) == true)
        {
            slotImage.sprite = heroIcon;
        }
    }
}