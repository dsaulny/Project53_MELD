using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Microsoft.MixedReality.Toolkit.UI;

public class ObjDictController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tutorialText;
    [SerializeField] private Image tutorialImage;
    public TMP_Text className;

    private string[][] ObjDef = new string[][]
    {
        new string[] { "This is an emergency stop button.  It is located at the top left of the MELD console.  It is used to stop all operation in the case of possible dangers that is notified through warnings or any beliefs that something is wrong",
        "Images/emergency" },

        new string[] { "This is the baseplate of the MELD machine.  The base plate is what holds the substrate to the platform of the machine. When placing the substrate, make sure the base plate screws hold the substrate down tightly!",
        "Images/baseplate" },

        new string[] { "This is the remote jog handle, which can be found on the left side of the MELD interface. The controller to move the machine. Press MPG Feed, select the axis you want to move, select the speed, hold the deadman switch, and then twist the black knob. If you don't think it's working, look at the numbers on the screen. Select speed 100x if you're far away from the platform",
        "Images/remotehandle" },

        new string[] { "This is an aluminum loaf, which is the main product of this specific printing project. The machine cannot print parts smaller than this. The part usually needs machining to finalize the product. Be careful when handling after print!!!",
        "Images/ALloaf" },

        new string[] { "This is the connected MELD tool and it is the equivalent of the nozzel in a normal 3D printer. It is where the feed material comes out of as it presses down on the substrate. The tool can determine the width of the loaf.  Here the tool has a water cooling jacket attached to it.",
        "Images/bottom_tool" },

    };

    public void fillPanel()
    {
        string name = className.text;
        string mediaPath;
        tutorialText.text = "";
        tutorialImage.sprite = null;
        tutorialImage.enabled = false;
        Sprite imageSprite = null;
        switch (name)
        {
            case "emergency button":
                // Load text
                tutorialText.text = ObjDef[0][0];
                mediaPath = ObjDef[0][1];

                if (mediaPath.StartsWith("Images/"))
                {
                    
                    imageSprite = LoadImageSprite(mediaPath);

                    if (imageSprite != null)
                    {
                        tutorialImage.sprite = imageSprite;
                        tutorialImage.enabled = true;
                    }
                }
                break;
            /*case "MELD rod":
                // Load text
                tutorialText.text = ObjDef[1][0];
                mediaPath = ObjDef[1][1];

                if (mediaPath.StartsWith("Images/"))
                {
                    
                    imageSprite = LoadImageSprite(mediaPath);

                    if (imageSprite != null)
                    {
                        tutorialImage.sprite = imageSprite;
                        tutorialImage.enabled = true;
                    }
                }
                break;*/
            case "base plate":
                // Load text
                tutorialText.text = ObjDef[1][0];
                mediaPath = ObjDef[1][1];

                if (mediaPath.StartsWith("Images/"))
                {
                    
                    imageSprite = LoadImageSprite(mediaPath);

                    if (imageSprite != null)
                    {
                        tutorialImage.sprite = imageSprite;
                        tutorialImage.enabled = true;
                    }
                }
                break;
            case "remote jog handle":
                // Load text
                tutorialText.text = ObjDef[2][0];
                mediaPath = ObjDef[2][1];

                if (mediaPath.StartsWith("Images/"))
                {
                    
                    imageSprite = LoadImageSprite(mediaPath);

                    if (imageSprite != null)
                    {
                        tutorialImage.sprite = imageSprite;
                        tutorialImage.enabled = true;
                    }
                }
                break;
            case "AL loaf":
                // Load text
                tutorialText.text = ObjDef[3][0];
                mediaPath = ObjDef[3][1];

                if (mediaPath.StartsWith("Images/"))
                {
                    
                    imageSprite = LoadImageSprite(mediaPath);

                    if (imageSprite != null)
                    {
                        tutorialImage.sprite = imageSprite;
                        tutorialImage.enabled = true;
                    }
                }
                break;
            case "MELD Tool":
                // Load text
                tutorialText.text = ObjDef[4][0];
                mediaPath = ObjDef[4][1];

                if (mediaPath.StartsWith("Images/"))
                {
                    
                    imageSprite = LoadImageSprite(mediaPath);

                    if (imageSprite != null)
                    {
                        tutorialImage.sprite = imageSprite;
                        tutorialImage.enabled = true;
                    }
                }
                break;
        }
        Sprite LoadImageSprite(string imagePath)
        {
            Texture2D texture = Resources.Load<Texture2D>(imagePath);
            if (texture == null)
            {
                Debug.LogError("Failed to load texture at path: " + imagePath);
                return null;
            }
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }
    }
}