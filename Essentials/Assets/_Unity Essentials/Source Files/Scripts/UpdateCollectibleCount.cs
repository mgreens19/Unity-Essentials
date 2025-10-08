using UnityEngine;
using TMPro;

public class UpdateCollectibleCount : MonoBehaviour
{
    private TextMeshProUGUI collectibleText;

    void Start()
    {
        collectibleText = GetComponent<TextMeshProUGUI>();
        if (collectibleText == null)
        {
            Debug.LogError("This script needs a TextMeshProUGUI component!");
            return;
        }
    }

    void Update()
    {
        if (CollectibleManager.instance != null)
        {
            int remaining = CollectibleManager.instance.GetRemainingCollectibles();
            collectibleText.text = $"Collectibles remaining: {remaining}";
        }
    }
}
