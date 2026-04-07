using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UITest : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Image image;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text.text = "meepis";
        image.transform.localScale = new Vector3(0.5f,1,1);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
