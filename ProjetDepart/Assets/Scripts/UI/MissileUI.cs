using TMPro;
using UnityEngine;

namespace UI
{
    public class MissileUI : MonoBehaviour
    {
        [SerializeField] private string format = "{0:0}";
        
        private TMP_Text missileText;

        private void Awake()
        {
            missileText = GetComponent<TMP_Text>();
        }

        private void Update()
        {
            missileText.text = string.Format(format,
                Finder.SpaceMarine.MissilesAmount);
        }
    }
}