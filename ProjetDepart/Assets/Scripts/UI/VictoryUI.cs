using UnityEngine;
using TMPro;

public class VictoryUI : MonoBehaviour
{
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private TMP_Text victoryText;
    [SerializeField] private AudioSource victoryMusic;

    private void OnEnable()
    {
        Finder.EventChannels.OnLevelWin += ShowVictory;
    }

    private void OnDisable()
    {
        Finder.EventChannels.OnLevelWin -= ShowVictory;
    }

    private void ShowVictory()
    {
        victoryPanel.SetActive(true);
        victoryText.text = "Victoire !";
        
        Color color;
        if (ColorUtility.TryParseHtmlString("#49FF7A", out color))
        {
            victoryText.color = color;
        }
        victoryText.fontSize = 200;
        victoryMusic?.Play();
    }
}
