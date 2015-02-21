using UnityEngine;
using System.Collections;

namespace MyNamespace
{



public class ShowTextBubble : MonoBehaviour
{
    #region class variables

    public Sprite[] sprites;
    public UnityEngine.UI.Image TextBubble;
    public UnityEngine.UI.Image InnerImage;
    public UnityEngine.UI.Text innerText;

    public float dissapearSpeed = 10f;
    public float appearingSpeed = 20f;

    public bool appear = true;

    public enum imagetype
    {
        stop,
        goHome,
        chaseFlag,
        bringFlagHome,
        taggedRunningHome,
        wandering,
        untag,
        tagged
    }

    #endregion

    #region unity functions


	// Update is called once per frame
	void Update () {

	    if (appear == true)
	    {
	        float alphaVal = TextBubble.color.a; //since both textbubble and innertext will appear and dissapear at the same time, we'll make them have the same alpha value
            alphaVal = Mathf.Lerp(alphaVal, 1f, appearingSpeed);
            //    Color innerTextColor = innerText.color;
            Color textbubbleColor = TextBubble.color;
            Color innerImageColor = InnerImage.color;
	        textbubbleColor.a = alphaVal;
            innerImageColor.a = alphaVal;
	        //innerTextColor.a = alphaVal;
            TextBubble.color = textbubbleColor;
            InnerImage.color = textbubbleColor;
            //innerText.color = innerTextColor;
	        if (alphaVal >0.999f)
	            appear = false;
         
	    }
	    else
	    {
            float alphaVal = TextBubble.color.a; //since both textbubble and innertext will appear and dissapear at the same time, we'll make them have the same alpha value
            alphaVal = Mathf.Lerp(alphaVal, 0,  dissapearSpeed);
            //    Color innerTextColor = innerText.color;
            Color textbubbleColor = TextBubble.color;
            Color innerImageColor = InnerImage.color;
            textbubbleColor.a = alphaVal;
            innerImageColor.a = alphaVal;
            //innerTextColor.a = alphaVal;
            TextBubble.color = textbubbleColor;
            InnerImage.color = textbubbleColor;
            //innerText.color = innerTextColor;
	        if (alphaVal < 0.1f)
	        {
             
	            gameObject.SetActive(false);
	        }

	    }
	}

    private void Start()
    {
       Color  textbubbleColor = TextBubble.color;
       Color innerImageColor = InnerImage.color;
       ///Color  innerTextColor = innerText.color;

        textbubbleColor.a = 0f;
       // innerTextColor.a = 0f;
        innerImageColor.a = 0;

        TextBubble.color = textbubbleColor;
        //innerText.color = innerTextColor;
        innerText.color = innerImageColor; 

    }

    void OnEnable()
    {
        appear = true;
    }
    #endregion
    public void ApplyNewText(string text)
    {
        appear = true;
        innerText.text = text;
    }
    

    public void ApplyNewImage(imagetype type, string tag)
    {
        switch (type)
        {
            case imagetype.chaseFlag:
                if(tag == "orange")
                InnerImage.sprite = sprites[1];
                else
                    InnerImage.sprite = sprites[0];
                break;
            case imagetype.goHome:
                InnerImage.sprite = sprites[4];
                break;
            case imagetype.wandering:
                InnerImage.sprite = sprites[3];
                break;
            case imagetype.stop:
                InnerImage.sprite = sprites[2];
                break;
            case imagetype.tagged:
                InnerImage.sprite = sprites[5];
                break;
            case imagetype.untag:
                InnerImage.sprite = sprites[6];
                break;
            case imagetype.taggedRunningHome:
                InnerImage.sprite = sprites[7];
                break;

        }
    }
}
}