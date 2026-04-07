using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShaderHealthBar : MonoBehaviour
{

    public TextMeshProUGUI healthText;
    public Image barImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        barImage.material = new Material(barImage.material);
        SetHealthBar(33, 100);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetHealthBar(int current, int max)
    {
        healthText.text = current.ToString() + "/" + max.ToString();
        barImage.material.SetFloat("_Fill", (float)current/(float)max);
    }
}
