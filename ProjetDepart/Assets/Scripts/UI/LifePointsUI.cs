using TMPro;
using UnityEngine;

namespace UI
{
    public class LifePointsUI : MonoBehaviour
    {
        [SerializeField] private string format = "{0:000}";
        
        private TMP_Text lifePointsText;

        private void Awake()
        {
            lifePointsText = GetComponent<TMP_Text>();
        }

        private void Update()
        {
            lifePointsText.text = string.Format(format,
                Finder.SpaceMarine.LifePoints);
        }
    }
}