using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M3ThemeChange : MonoBehaviour
{
    Color pastelColor;
    Color classicColor;

    // Start is called before the first frame update
    void Start()
    {
        ColorUtility.TryParseHtmlString("#FFACD6", out pastelColor);
        ColorUtility.TryParseHtmlString("#ACFFD5", out classicColor);

        if (PlayerPrefs.GetString("Theme") == "Pastel" || PlayerPrefs.GetString("Theme") == "Bold")
        {
            gameObject.GetComponent<SpriteRenderer>().color = pastelColor;
        }
        else if (PlayerPrefs.GetString("Theme") == "Classic")
        {
            gameObject.GetComponent<SpriteRenderer>().color = classicColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
