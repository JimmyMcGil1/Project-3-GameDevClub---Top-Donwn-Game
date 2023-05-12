using UnityEngine;
using UnityEngine.UI;

public class ChangeButton : MonoBehaviour
{
    public Sprite newSprite;
    private Sprite oldSprite;
    private bool isClicked = false;
    private Button button;
    private Image buttonImage;

    void Start()
    {
        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>();
        oldSprite = buttonImage.sprite;
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        if (!isClicked)
        {
            buttonImage.sprite = newSprite;
            isClicked = true;
        }
        else
        {
            buttonImage.sprite = oldSprite;
            isClicked = false;
        }
    }
}
