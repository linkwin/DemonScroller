using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsUpdater : MonoBehaviour {
    public enum DisplayStyle {Text, SpriteStack, SpriteSwap};

    public Sprite[] SwapSprites;

    public IntVariable score;
    public DisplayStyle pointDisplayStyle;

    private int prevScore;

	void Start () {
        prevScore = score.InitialValue;
        switch (pointDisplayStyle)
        {
            case DisplayStyle.SpriteStack:
                //disable every counter above initial value
                for (int i = score.InitialValue; i < transform.childCount; i++)
                    transform.GetChild(i).gameObject.SetActive(false);
                break;

            case DisplayStyle.Text:
                transform.GetComponent<Text>().text = "" + score.InitialValue;
                break;
            case DisplayStyle.SpriteSwap:
                GetComponent<Image>().sprite = SwapSprites[score.RuntimeValue];
                break;
        }
	}
	
	void Update () {
        //if score changes
        //if (score.RuntimeValue != prevScore)
            HandleUpdate();
	}

    void HandleUpdate()
    {
        switch (pointDisplayStyle)
        {
            case DisplayStyle.SpriteStack:
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                    if (i < score.RuntimeValue)
                        transform.GetChild(i).gameObject.SetActive(true);
                }
                //if (score.RuntimeValue < prevScore)
                //    transform.GetChild(prevScore - 1).gameObject.SetActive(false);
                //else
                //    transform.GetChild(score.RuntimeValue - 1).gameObject.SetActive(true);
                //prevScore = score.RuntimeValue;
                break;

            case DisplayStyle.Text:
                this.GetComponent<Text>().text = "" + score.RuntimeValue;
                break;
            case DisplayStyle.SpriteSwap:
                GetComponent<Image>().sprite = SwapSprites[score.RuntimeValue];
                break;
        }
    }
}
