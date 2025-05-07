using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text statusText;
        
        public Button resetButton;
        
        public void UpdateStatus(string message)
        {
            statusText.text = message;
        }
    }
}