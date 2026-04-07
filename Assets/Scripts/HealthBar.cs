using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HealthBar : MonoBehaviour
{

    public TextMeshProUGUI healthText;
    public Image frontImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetHealthBar(25,100);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetHealthBar(int current, int max)
    {
        healthText.text = current.ToString() + " / " + max.ToString();
        frontImage.transform.localScale = new Vector3((float)current/(float)max,1,1);
    }

}
