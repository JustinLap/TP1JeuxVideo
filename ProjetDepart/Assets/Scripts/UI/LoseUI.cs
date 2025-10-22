using TMPro;
using UnityEngine;

namespace UI
{
    public class LoseUI : MonoBehaviour
    {
        [SerializeField] private GameObject losePanel;
        [SerializeField] private TMP_Text loseText;

        private void OnEnable()
        {
            Finder.EventChannels.OnLevelLose += ShowLose;
        }

        private void OnDisable()
        {
            Finder.EventChannels.OnLevelLose -= ShowLose;
        }

        private void ShowLose()
        {
            losePanel.SetActive(true);
            loseText.text = "Défaite !";
            
            // Source: ChatGPT
            // Prompt: loseText.color = new Color(FF4A56);
            Color color;
            if (ColorUtility.TryParseHtmlString("#FF4A56", out color))
            {
                loseText.color = color;
            }
            loseText.fontSize = 200;
        }
    }
}