using TMPro;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text statusText;
        public void UpdateStatus(string message)
        {
            statusText.text = message;
        }
    }
}