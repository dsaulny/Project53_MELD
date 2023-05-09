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

        new string[] { "This is a MELD rod, and it is an input for the MELD tool.  These rods can be found on the left cart with the other additional tools label MELD TOOL in small cardboard boxes",
        "Images/MELD Rod" },

        new string[] { "This is the baseplate of the MELD machine.  It is where the melding operation will be done or it is a standalone plate which needs to be positioned and secured using clamps",
        "Images/baseplate" },

        new string[] { "This is the remote jog handle, which can be found on the left side of the MELD interface.  Its main functionality is to control the movement of the MELD Tool.  By spinning the handle, you can adjust the x,y,z position of the tool.  In addition, there is also a deadman switch on the side for possible malfunction",
        "Images/remotehandle" },

        new string[] { "This is an aluminum loaf, which is the main product of this specific printing project.  The goal is to transform an aluminum rod through the process of melding into the final product. Be careful when handling after print!!!",
        "Images/ALloaf" },

        new string[] { "This is the MELD TOOL and the main processor of the MELD machine.  It will perform the melding process from whichever material that was fed to it will need to be positioned in order to print correctly",
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
            case "MELD rod":
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
            case "base plate":
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
            case "remote jog handle":
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
            case "AL loaf":
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
            case "MELD Tool":
                // Load text
                tutorialText.text = ObjDef[5][0];
                mediaPath = ObjDef[5][1];

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