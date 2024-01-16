using UnityEngine;
using TMPro;

public class GridObjectController : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void SetText(int count)
    {
        text.text = count.ToString();
    }
}
