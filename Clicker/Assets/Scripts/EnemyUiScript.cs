using UnityEngine;
using UnityEngine.UI;
public class EnemyUiScript : MonoBehaviour
{
    [SerializeField] private Text _enemyNameText = null;
    [SerializeField] private Text _enemyLevelText = null;
    [SerializeField] private Slider _enemySlider = null;    
    public void NewEnemyStats(string newName, string newLevel)
    {
        _enemyNameText.text = newName;
        _enemyLevelText.text = newLevel;
        _enemySlider.value = 1;
        OpenEnemyPanel();
    }
    public void ChangeEnemyHP(float damage)
    {        
        _enemySlider.value -= damage;
        if (_enemySlider.value <= 0)
        {
            CloseEnemyPanel();
        }
    }
    public void OpenEnemyPanel()
    {
        gameObject.SetActive(true);
    }
    public void CloseEnemyPanel()
    {
        gameObject.SetActive(false);
    }
}