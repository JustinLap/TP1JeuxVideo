using UnityEngine;
using TMPro;

public class VictoryUI : MonoBehaviour
{
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private TMP_Text victoryText;
    [SerializeField] private AudioSource victoryMusic;

    private void OnEnable()
    {
        Finder.EventChannels.OnLevelEnd += ShowVictory;
    }

    private void OnDisable()
    {
        Finder.EventChannels.OnLevelEnd -= ShowVictory;
    }

    private void ShowVictory()
    {
        victoryPanel.SetActive(true);
        victoryText.text = "Victoire !";
        victoryText.color = new Color(73f / 255f, 255f / 255f, 122f / 255f);
        victoryText.fontSize = 200;
        victoryMusic?.Play();
    }
}
