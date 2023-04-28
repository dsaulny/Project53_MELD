using UnityEngine;
using TMPro;
using UnityEngine.Video;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tutorialText;
    [SerializeField] private Image tutorialImage;
    [SerializeField] private VideoPlayer tutorialVideo;
    [SerializeField] private UserData userData;
    [SerializeField] private LoginManager loginManager;
    public RawImage Video_RawImage;
    //[SerializeField] private GameObject uniqueSlide;
    //[SerializeField] private GameObject mainSlide;
    //public GameObject[] checkboxes;

    private string[][] tutorialSteps = new string[][]
    {
        //Note that the actual indexing starts at 0

        //1
        new string[] { "Welcome to the safety section of the MELD tutorial! The most important thing we want you to leave with after this tutorial is all of your limbs! So please take this section seriously. \n\nThe MELD machine has numerous safety mechanisms built into itself. But there are steps you will take where you can harm yourself."},
       
        //2
        new string[] { "Before we begin, make sure you take off all loose jewelry like long necklaces and clothing items such as unzipped jackets. \n\nIf you have long hair, tie it back so it does not get in the way."},
        
        //3
        new string[] { "Make sure the area around you is clear. There should be ample space to move in front of the MELD machine and the console. With the headset, you are more prone to tripping, so check the floor as well. \n\nPage 1/2"},
        
        //4
        new string[] { "If you have not done so yet, put on safety glasses. There is a step where you use an air spray to clear the base plate and metal flakes will fly towards you. The HoloLens alone will not ensure protection from the flakes damaging your eyes. \n\nPage 2/2"},
        
        //5
        new string[] { "As said before, the MELD machine has many safety features. Let’s walk through them. \n\nWhen the MELD machine is running, the doors must remain closed. If the doors open, the machine will stop (like a microwave), but it will mess up the print. \n\nPage 1/2", 
        "Images/remoteImage" },
        
        //6
        new string[] { "There is an emergency stop button at the stop left of the console. If something goes wrong, do not hesitate to press the button. Prints are cheap to restart, repairs and medical bills are not cheap. \n\nThe “remote jog handle” also has an emergency stop and a “deadman” switch. The deadman switch means you must hold the switch down in order to move the actuator during preparation. We will cover this in the tutorial. \n\n Page 2/2", 
        "Images/remoteImage" },
        
        //7
        new string[] { "Lastly, and this is the most important, do not touch the printed material after you begin the print. The material is extremely hot and will burn you. There are special gloves to do so, but we will not have you use them. Instead, you must wait for it to cool after printing.\n\nThat’s it! We will remind you of safety procedures to follow when you arrive at the step. "},
        
        //8
        new string[] {
            "Please double check to make sure you followed these safety protocols:\nSafety checklist:\nNo loose jewelry or clothing\nTied back hair\nArea is clear of obstacles\nSafety glasses"
        },

        //9
        new string[] {
            "Now that you understand the safety procedures and the controls, we can start printing!"
        },

        //10
        new string[] {
            "First, we need to turn on the machine. Go to the back of the machine and twist the power switch.",
            "Videos/Slide08"
        },

        //Slide09
        //11
        new string[] {
            "Next, turn on the water. The switch is to the right of the machine when looking from the back. The water is needed to cool down the machine during printing. \n\nThen, return to the front of the machine.",
            "Videos/Slide09"
        },

        //12
        new string[] {
            "To the right of the machine is a “console”, a large screen with a keyboard underneath it. Turn on the console by holding the power button at the top right of the console until the screen lights up.",
            "Videos/Slide10"
        },

        //13
        new string[] {
            "The console is where most of the control is done. Once the software is loaded, reset the emergency and enable all the drives. You may need to close any pop up menus.",
            "Videos/Slide11"
        },

        //14
        new string[] {
            "Check the faults button (it is outlined in an orange box in the picture). The button should be gray. If the button is red, ask the expert present with you for help.",
            "Images/faults"
        },

        //15
        new string[] {
            "To print, you need two materials: A printing substrate and printing material. The substrate is 4 inches by 12 inches and is the platform that will hold the print. The material is a ⅜ inch by ⅜ inch aluminum bar coated in graphite. The machine can only hold a bar of length up to 21 inches, so you will need multiple bars.",
            "Images/SubstrateAndRod"
        },

        //16
        new string[] {
            "The purchasing and preparation of these materials should already be done for this tutorial. You should have 1 substrate and at least 3 bars of length 18 inches (you can use more bars if you would like, but 3 is a good starting place).",
            "Images/SubstrateAndRod"
        }, 

        //17
        new string[] {
            "The following is an overview of the next steps, so do not perform any of these quite yet: we will need to insert the printing material (a.k.a the feed) and set the substrate in position. First, you set the substrate in position and tighten it down. Then you calibrate the positioning of the tool inside the machine. Lastly, you set a reload position and insert the feed. Before we set the substrate in position, we may need to move the platform closer to the door and/or move the tool away from the platform depending on its current position."
        }, 

        //18
        new string[] {
            "Pick up the controller to the left of the console (picture for reference). It is called a “Remote Jog Handle” but we will call it the controller. The controller allows you to manually move the machine.",
            "Images/remoteImage"
        },

        //19
        new string[] {
            "There are 6 control buttons that you will be using. \n\nThe first 3 buttons are the X, Y, and Z buttons. These set which axis you want to move the machine in (X is left/right, Y is forward/backward, Z is up/down). The tool only moves along the Z axis, while the platform moves along the X and Y axes."
        },

        //20
        new string[] {
            "The second three buttons are the x1, x10, and x100 buttons. These set which speed you want to move the machine with \n\nThe black wheel at the bottom allows you to move the machine in the positive or negative direction with respect to the axis that you choose (X, Y, or Z). To the right is positive and to the left is negative.\n\nPage 1/2"
        },

        new string[] {
            "Finally, the button 'MPG Feed' will toggle the mode of the controller so that you can move the machine.\n\n Page 2/2"
        },

        //21
        new string[] {
            "The controller has two safety buttons \n\nThe big red button at the top is an emergency stop. Do not be afraid to use it! It is better that you ruin the run than ruin the machine (one is cheap, the other is not)."
        },

        //22
        new string[] {
            "The big yellow button on the side is called a “deadman switch”. It is a safety switch that prevents the controller from working when the button is not activated. There are two switches in the button, so that if the button is either not held down far enough or held down too far, then the controller will not work."
        },

        //23
        new string[] {
            "Let’s begin the process. \n\nFirst, press “MPG Feed” on the controller and make sure the red light by the button turns on.\n\nThen, if the tool is too close to the platform, move it up so that it is out of the way. Then you can move the platform so that the substrate holder is comfortably within reach.",
            "Images/EmptyPlatform"
        },

        //19 in manual
        new string[] {
            "In order to set the substrate in place, you may need to twist the pieces that will tightly hold the substrate out of the way. You will also need to remove the 2 side-supports on the substrate holder that are closer to the door. These are custom holders to provide additional support and further prevent small movements in the baseplate (yes, there is a lot of pressure and friction!)",
            "Videos/Slide19"
        }, 

        //25
        new string[] {
            "Inside the substrate holder on the platform in the machine, there are exposed wires used for thermal measurements. When setting the plate into the slot, be careful not to damage the wires.",
            "Images/Wires"
        },

        //25
        new string[] {
            "Set the substrate into the holder while being mindful of the wires.",
            "Videos/Slide21"
        },

        //26
        new string[] {
            "Re-twist the pieces on top of the substrate and set the golden-colored holders back into place.",
            "Videos/Slide22"
        },

        //27
        new string[] {
            "Using a quarter-inch allen wrench, tighten the 8 screws. There will be immense pressure and vibration from the friction, so keeping the baseplate in place is very important. Remember: righty tighty, lefty loosey \n\nThe allen wrench can be found in the tool cart to the left \n\nPlease return the tools before continuing to the next step.",
            "Images/AllenWrench",
            "Videos/Slide23"
        }, 

        //I think this is wrong?
        //28
        new string[] {
            "For a smooth print, it is good to wipe the substrate with acetone. Using a cloth and the bottle of acetone, squirt acetone onto the substrate and wipe the acetone across the surface. Do this 1-2 times, depending on if the plate is fully coated. Excess will be wiped off."//,
            //"Videos/AcetoneSpray"
        },

        //29
        new string[] {
            "The substrate is ready to be used for printing! Next, we will calibrate the machine \n\nThe current position can be found under the “Traverse” section on the console. When calibrating the machine, it is very important that you are as precise as possible. A thousandth of an inch seems trivial to us, but that is the accuracy of which the machine operates.",
            "Images/xyz"
        },

        //30
        new string[] {
            "You will need to calibrate all three axes. The Z is the hardest, so we will start with X and Y. The X and Y positions need to be adjusted so that the actuator is above where we want to start printing. \n\nFor the Y-axis, the actuator needs to be lined up in the middle of the substrate (halfway on the short side). You will need to go around the side of the machine and look through the window to properly align this.",
            "Images/PrintLocation",
            "Videos/Slide26"
        },

        //31
        new string[] {
            "For the X-axis, the actuator needs to be positioned near the end of the substrate (on the long side). The substrate is 12 inches in length and the loaf that you will print will be 9 inches long. This means you should move the actuator in the X axis to 1.5 inches away from the end of the plate so that the print can fit on the substrate. \n\nYou will need to adjust both together. Remember to toggle the 'MPG Feed'",
            "Images/PrintLocation"
        },

        //32
        new string[] {
            "Once they are both at the correct location, press “Zero X” and “Zero Y”. The machine will now reference the current X and Y locations as the 0 coordinate along those axes.",
            "Images/zeroXzeroY"
        },

        //33
        new string[] {
            "Setting the Z axis is tricky. For as precise of a print as we want, you will need to slide a piece of paper back and forth underneath the tool as you slowly lower it. Remember earlier when we said the machine operates to the accuracy of a thousandth of an inch? That’s why we are using paper. Cool, huh?"
        },

        //34
        new string[] {
            "Before we move at the 1x speed, move the tool down in the Z direction until there is only a centimeter between the tool and the substrate. You can start with 100x speed, but slow down to 10x when you get close."
        }, 

        //35
        new string[] {
            "When there is only a centimeter left, set the speed to 1x. \n\nThis speed moves so slowly that you cannot see the movement, so you will need to check the numbers from the console to see how far or fast you are moving. It is not necessary, but helpful for reference.\n\nThis next part will require using both hands at once."
        },

        //36
        new string[] {
            "Use your left hand to slide a piece of paper back and forth underneath the tool. Do not move your finger underneath the tool, only the paper! Your finger will otherwise get smooshed, guaranteed. There should be a piece of paper in the tool cart.",
            "Videos/Slide31"
        },

        //37
        new string[] {
            "Then, place the controller on the ledge of the machine. Hold the controller between your thumb and your middle finger, which will be on the deadman switch. Your index finger will slowly move the wheel. Do not spin the wheel fast, or your accuracy will be off \n\nWhen the paper stops sliding and gets caught under the tool, stop moving the wheel. Then set “Zero Z” on the console.",
            "Videos/Slide31"
        },

        //38
        new string[] {
            "Now that the machine is calibrated, we can set a reload position. Move the actuator far above the substrate so you can easily fit a long rod underneath it. DO NOT move the Z axis down into the plate. Move it at 1x speed in the + direction and watch the numbers on the screen to make sure you are going in the right direction before switching to a faster speed.\n\nAlso, move the platform (X,Y) towards the doors."
        },

        //39
        new string[] {
            "When the substrate is near the door and the actuator is positioned high, press “Set reload”. Between each “run” of the machine, you will need to reload the feed, and the machine will automatically return to this position.",
            "Images/SetReload"
        },

        //40
        new string[] {
            "To add feed to the machine, you need to put metal bars into the tool manually. The feed goes into a hole underneath the tool (see picture). For this tutorial, we are using aluminum 6061 rods coated in graphite. The graphite is used as a lubricant and can get onto your hands. You can put on gloves if you’d like, which can be found in the tool drawer. Just make sure not to dirty the console screen.",
            "Images/bottom_tool"
        },

        //41
        new string[] {
            "Take a rod and slide it into the hole under the tool. If the rod is not going in without significant force, get a new rod \n\nFor future inserts, the substrate and printed material will be hot, so practice awareness in not touching the material on the platform. The tool will also be hot, although not as hot, and you can tap it when inserting the rod without getting burned.",
            "Videos/Slide35"
        },

        //42
        new string[] {
            "There is a push rod inside the machine that pushes the rod down onto the printing substrate. The rod will be flattened into a “loaf-like” object via friction. \n\nThis is the main benefit of the MELD machine: you can create metal objects without melting the material."
        },

        //43
        new string[] {
            "Close the doors of the machine. You are done interacting inside the machine until after the first run of your print."
        },

        //44
        new string[] {
            "The printer uses Gcode as commands that instruct it on how to print. We will briefly cover it, but you do not need to memorize it for this tutorial.\n\nSome syntax for Gcode:\nG<v> are movement commands specified by code v. For example, G01 is \"linear interpolation\", which moves the machine.\nX<vx>, Y<vy>, Z<vz> sets the coordinates of the point for the machine to move to.\nF<v> sets the velocity."
        },

        //45
        new string[] {
            "For example, you may have G01 X5.0 F5.0 (G<v> X<vx> F<v>). You can specify the units for the Gcode, but in our tutorial, we will be using inches."
        },

        //46
        new string[] {
            "Example G Code can be seen to the right.\n\nFor line number 6, G01 says move in a linear direction. X2.5 says move 2.5 inches. F3.0 says move at 3.0 inches per minute.",
            "Images/GCode"
        },

        //47
        new string[] {
            "Line 7 moves the actuator up 0.02 inches at 0.9 inches per minute while moving 0.075 inches in the negative X direction. Line 8 does the same thing, but in the positive X direction.\n\nLine 9 moves the actuator in the opposite x-direction for 2.5 inches at a rate of 3 inches per minute.",
            "Images/GCode"
        },

        //48
        new string[] {
            "On the console, press the “Gcode” button. Find and select “6-Hamed0AL6061-T0.06.nc”.",
            "Images/MELD_GCode"
        },

        //49
        new string[] {
            "The Gcode follows these simple steps:\nM04 S300 sets the spindle speed at 300 rotations per second, the speed at which the tool rotates.\nM24 S6000 sets the rod push down speed at 6 inches per minute.\nX-9 tells the source to move to current x (0 at the beginning) minus nine, or nine inches.\n\nPage 1/2",
            "Images/MELD_GCode"
        },

        new string[] {
            "Then it moves vertically in a zig-zag direction up to the next layer.\nFinally, it moves back nine inches to the initial location.\nOverall, it goes left 9 inches, up to the next layer, then right 9 inches.\nAfter that, it stops and returns to the reload position.\n\nPage 2/2",
            "Images/MELD_GCode"
        },

        //50
        new string[] {
            "The Gcode is now loaded, but it will not run until we manually start it. However, we must first “warm up” the machine, or quite literally, heat up the tool, as well as set the settings for the machine on the console. Get ready for it to get loud!",
            "Images/StartCSV"
        },

        //51
        new string[] {
            "Press “Start CSV Recording”. The machine will then begin recording everything that takes place during the print and store it in a .csv file that you can view at a later time.",
            "Images/StartCSV"
        },

        //52
        new string[] {
            "On the console, under the “Spindle” section, press start, then press “Tool Cooling On” when the mini-menu pops up.\n\nThe tool cooling starts the water pump that pumps water into the water jacket–a jacket that wraps around the tool and cools it. That is why we turned on the water switch at the beginning of the tutorial.",
            "Videos/Slide42"
        },

        //53
        new string[] {
            "If you go to line 14 in the Gcode, you can see the command “M04 S300” which means our Gcode runs the spindle at 300 rotations per minute (RPM). To warm up the machine, we will set the spindle speed to 350.",
            "Videos/Slide43"
        },

        //54
        new string[] {
            "The box under “Speed (rpm)” may automatically be set to 200. Press the box and enter 350.\n\nThat’s all for the spindle. The actuator is a bit more difficult.",
            "Videos/Slide43"
        },

        //55
        new string[] {
            "Under the “Actuator” section, press settings.\n\nSet the soft limit to 19. This limits how far the actuator will push. This means the push rod will only go down 19 inches from its max height of 20.5 inches. We don’t want the pushrod itself pushing onto the substrate, which is why we need the soft limit."
            //"Videos/Actuator"
        },

        //56
        new string[] {
            "Set “0.1 ipm Velocity Force” to 4800. It determines at which actuator force level the actuator speed will be automatically reduced to 0.1 inch/minute (ipm) to help prevent damage to the machine. This will not automatically stop the actuator, only reduce its speed, so you need to be ready to stop the actuator. The actuator is (most of the time) the only thing that can damage the machine. If the actuator is not running, the machine will usually be fine."
            //"Videos/Actuator"
        },

        //57
        new string[] {
            "Set “Resume Force” to 3800. When the actuator drops to “Resume Force” value, the actuator will operate at the previous setpoint velocity.",
            "Videos/Actuator"
        },

        //58
        new string[] {
            "The tool should still be at the reload position you set. We need to move it to where it will begin printing.\n\nOn the console, select “Go To XY=0”, which will move the platform to the X and Y zero coordinates.",
            "Images/GoTo"
        },

        //59
        new string[] {
            "Next, we need to lower the Z-axis. \n\nAt the 0 coordinate you set, it means that the tool is touching the printing substrate. The tool has small protrusions on the bottom of it that are 0.09 inches thick. The layer is printed at 0.06 inches thick, so we want to reduce the 0.09 inch gap between the tool and the substrate by 0.03 inches so that the space between the bottom of the tool and the substrate is 0.06 inches.",
            "Images/ToolProtrusions"
        },

        //60
        new string[] {
            "Using the controller, lower the Z-axis close to 0 at a higher speed, then set the speed to 1x and slowly lower the Z coordinate to -0.0300.\n\nNow that the knob is pressing on the substrate and the spindle is spinning at 350 rpm, there is friction, so the temperature is rising.",
            "Videos/Slide47"
        },

        //61
        new string[] {
            "Before beginning the Gcode, we need to stabilize the actuator. You will need to pay attention to the graph at the bottom, as well as the numbers above titled “Force”, “Speed”, and “Position”. \n\nSpeed will be 0 when the actuator is stopped. When it is turned on, it will go to 2.00 inches per minute (ipm), as that is what is set under the “Speed” section at the top left of the Actuator box.",
            "Images/Force"
        },

        //62
        new string[] {
            "Position is the position of the push rod that pushes the feed material down into the substrate. At 0, it is at the very top. As the actuator is turned on, the rod will move down at 2.00 ipm, or what the speed is set to. It will take a couple of inches before it reaches the feed material that you inserted into the machine.\n\n“Force” is the force of which the push rod pushes down on the feed material. At first, it is very unstable, so we will need to stabilize it.",
            "Images/Force"
        },

        //63
        new string[] {
            "On the side of the screen is a green start button and a red stop button under the label “Actuator”. When you press start, the speed will go to 2.00 ipm and the position will increase as the push rod approaches the feed material. ",
            "Images/Force_2"
        },

        //64
        new string[] {
            "When the pushrod reaches and starts pushing the feed material, the force will increase as the actuator begins pushing the feed material onto the substrate. If we let it go without interruption, the force will go far too high and hit the 4800 pound per inch (lb/in) limit we set. The machine can tolerate 5000 lb/in, but it will not work properly. The limit we set only slows down the actuator speed.",
            "Images/Force"
        },

        new string[] {
            "Notice: Read the next two steps before performing the action. Then come back and follow these steps so you know what to expect."
        },

        //65
        new string[] {
            "You will need to press the start button, wait for the force to reach 2000, press stop, wait for the force to drop to 1000, then press start again. You will need to repeat this process until it stabilizes, meaning when you press start, the force won’t go up to 2000 and will instead plateau. Then you can leave it running. It can take 5-8 repetitions before this happens.",
            "Videos/Slide50"
        },

        //66
        new string[] {
            "Leave your finger by the stop button after pressing the start button. It will increase very fast.\n\nOnce it stabilizes, go to the next step.",
            "Videos/Slide50"
        },

        //Double check this text sizing
        //67
        new string[] {
            "Before starting the Gcode, you need to see the material start to come out from the tool onto the substrate. This will happen when the torque is about 100 under the “Spindle” section.\n\nIncrease the speed from 2.00 ipm to 3.00 ipm under the actuator. After a few seconds, increase it to 3.5, then to 4. Then increase the FPO from 70 to 80 to 90 to 100 until the torque is 100.\n\nAs soon as the torque reaches about 100, or you see material start to form underneath the tool, you can press start Gcode.",
            "Videos/Slide51",
            "Images/Torque"
        },

        //68
        new string[] {
            "The Gcode will take over the machine from here, setting the spindle speed to 300 and the actuator speed to 6.00, as well as moving the machine."
        },

        //Double check for sizing
        //69
        //commented out for Dr. Nik
        new string[] {
            "The first layer will usually look bad. A good layer looks smooth. A bad layer has bumps and edges. You should decrease the FRO to 70% (or lower, if needed) if the layer looks really bad (reference picture). When you change the FRO, use the +10% or -10% buttons."
            //"Images/firstLayer"
        },

        //commented out for Dr. Nik
        new string[] {
            "Once it stops, raises, and goes back to the right, set the FPO back to 100% and leave it there.\n\nYou may run out of feed material if starting the first layer took a long time. That is okay. The next layer will mostly cover it up."
            //"Images/secondLayer"
        },

        //70
        new string[] {
            "Once the run is finished, you will need to prepare for the next run.\n\nIf you did not run out of material, you need to remove the leftover. It should leave the bar on the plate. If you have remaining material and it is not on the plate, go to the next step.\n\nDo not touch the printed material with your bare hands! It will burn you!",
            "Videos/Slide53"
        }, 

        //71
        new string[] {
            "If the bar is on the plate, then get a pair of fliers from the tool cart, open the doors, and use the pliers to grab the bar. Then place the bar in the leftover bucket.\n\nDo not touch the printed material with your bare hands! It will burn you!",
            "Videos/Slide53"
        },

        //72
        new string[] {
            "If the bar was not left on the plate, then you need to manually have the actuator push the material out. Change the soft limit to an inch longer than the rod (in our case, 20 inches) by going to “Settings” under the “Actuator” section, above the graph. Close the menu when you are done. \n\nThen, under the “Actuator” section, above the graph, change the position to 0.5 inches longer than the rod (18.5 inches) and press “Go To”. "
        },

        //73
        new string[] {
            "After a few seconds, once the pushrod reaches the position, 0.5 inches of the rod should stick out. If it is not, then increase the position to 19, then 19.5 until it sticks out (If the soft limit was still at 19, then we could not go to position 19.5). Then change the position to 0 and press “Go To” and change the soft limit back to what it was under settings (in our case, 18 inches).\n\nDo not touch the printed material with your bare hands! It will burn you!"
        },

        //74
        new string[] {
            "Once the position reaches 0 inches (you will hear a click, and you can check the position on the console), get a pair of fliers from the tool cart, open the doors, and use the pliers to grab the bar. Then place the bar in the leftover bucket.\n\nDo not touch the printed material with your bare hands! It will burn you!"
        }, 

        //75
        new string[] {
            "You may also notice metal flakes covering the printed layer. We will use an air hose, but be careful: the flakes will go flying. Make sure your safety glasses are on! Make sure everyone around you is wearing safety glasses and are at a safe distance! Do not touch the printed material! After checking for safety, pick up the air hose from the left side of the console, point the air hose at the flakes on the plate and pull the lever in short bursts to clear the plate of metal flakes.",
            "Videos/Slide55"
        },

        //76
        new string[] {
            "Get a new rod for feed material. Without touching the hot printed material, insert the rod into the tool as done earlier in the tutorial, but be careful: the tool may still be hot. Tap the rod in instead of pushing your finger against the tool bit.",
            "Videos/Slide56"
        },

        //77
        new string[] {
            "Close the doors. The machine will not run with the doors open.\n\nAt the top right of the console, press “Go To Last”",
            "Images/GoToLast"
        },

        //78
        new string[] {
            "The machine will not go all the way down on the Z-axis for safety reasons. Once the machine stops moving, you will see on the console that for the Z-axis, the “Work” column reads 0.3400 for the first run and the “M50 Last” column reads 0.0900 for the first run. That means the tool is currently at Z coordinate 0.3400 but the last position was at Z coordinate 0.0900. For the later runs, these values will be higher."//,
            //"Videos/ResetZ"
        },

        //79
        new string[] {
            "As done before, for the spindle, press “Start” on the console, then press “Turn On Cooling”. Pick up the controller and traverse the tool down to what the “Last” column reads (for the first run, it will be 0.0900)."//,
            //"Videos/ResetZ"
        },

        //80
        new string[] {
            "Now we need to do the same thing with the actuator. However, this time it will stabilize a lot faster.\n\nSet the speed to 2.5 (faster than the 2.0 from the first run)",
            "Videos/Slide59"
        },

        //81
        new string[] {
            "You will need to press the Actuator start button, wait for the force to reach 2000, press stop, wait for the force to drop to 1000, then press start again. You will need to repeat this process until it stabilizes, meaning when you press start, the force won’t go up to 2000 and will instead plateau. Then you can leave it running. It may only take one repetition this time.",
            "Videos/Slide59"
        },

        //82
        new string[] {
            "Watch the torque under the Spindle section. When the torque reaches 100, start the Gcode using the Gcode buttons on the left side of the console.\n\nLet the Gcode do the rest of the work!",
            "Images/Torque"
        },

        //83
        new string[] {
            "Once the machine is done, you will repeat the previous steps as you did after the first run. We suggest you do at least one more run so that you use 3 bars. Now that the machine is prepped and warmed up, each run is easy. It is preparing the machine that takes the longest.\n\nIf you want to do another run, press “Another Run”, and it will return you to the relevant step. Otherwise, Press “Finish print” to continue."
        },

        //84
        new string[] {
            "On the console, press “Stop CSV Recording”. Now you will need to remove the substrate to retrieve your print. Do not touch the printed material with your bare hands! It will burn you!"
            //"Videos/ChangeAllenWrench"
        },

        new string[] {
            "Open the doors and get the allen wrench back from the cart that you used earlier. Untighten the screws, but do not touch anything other than the wrench with your hands. For the wrench, twist the lever on the back to change the direction it operates."
            //"Videos/ChangeAllenWrench"
        },

        //85
        new string[] {
            "There are two ways to remove the substrate. You can either use the big white glove, or you can use a pair of pliers.\n\nYou will need to twist the holders off of the substrate and remove the two golden colored holders. Then you can extract the substrate and place it on a platform that can hold hot material. We have a piece of wood to hold the substrate while it cools off.",
            "Videos/Slide63"
        },

        //86
        new string[] {
            "Once the material cools off (an hour later), you can remove the printed part from the substrate and reuse the substrate. You can also recycle the printed part if you do not want it. You do not need to do that for this tutorial."
        },

        //87
        new string[] {
            "The last step is to turn off the machine.\n\nUnder the “Spindle”, press the purge button. This purges the water from the machine to help prevent rust. After a couple of seconds, press purge again.",
            "Videos/Slide65"
        },

        new string[] {
            "At the top right of the console, press the power-button symbol (on the screen, not the physical red button) and then in the pop up menu, press “Shut down”. Wait until the screen completely shuts off, then the machine should make a noticeable noise a couple of seconds after, which means you can continue.",
            "Videos/Slide65"
        },

        //88
        new string[] {
            "At the top right of this page, you should see a 'Follow Me' option next to the exit button. This will make it so that this UI element moves with you. Please press this before completing the next step.\n\nNow, go to the back of the machine and twist the power switch off.",
            "Videos/Slide66"
        },  

        //89
        new string[] {
            "Finally, turn off the water. The switch is to the right of the machine when looking from the back.\n\nThen, return to the front of the machine.",
            "Videos/Slide67"
        },

        //90
        new string[] {
            "Congratulations! You have finished the tutorial!\n\nYou may now disable the 'Follow Me' functionality if you wish. If you want to do the tutorial again, press the 'Next' button. Alternatively, you can return to the Home screen by pressing 'Close' at the top right."
        },
    };

    private void Start()
    {
        loginManager = LoginManager.Instance;
        userData = loginManager.LoadUsersData();
        if (userData != null)
        {
            LoadProgress(userData.progress);
        }
    }

    private void LoadProgress(int step)
    {
        if (step < 0 || step >= tutorialSteps.Length) return;
        /*
        if (step == 7)
        {
            uniqueSlide.SetActive(true);
            mainSlide.SetActive(false);
        }
        else
        {
        */
            //uniqueSlide.SetActive(false);
            //mainSlide.SetActive(true);

            // Clear previous content
            tutorialText.text = "";
            tutorialImage.sprite = null;
            tutorialImage.enabled = false;
            tutorialVideo.clip = null;
            tutorialVideo.enabled = false;
            Video_RawImage.enabled = false;

            // Load text
            tutorialText.text = tutorialSteps[step][0];

                // Load image or video if available
                if (tutorialSteps[step].Length > 1)
                {
                    string mediaPath = tutorialSteps[step][1];

                    if (mediaPath.StartsWith("Images/"))
                    {
                        //Sprite imageSprite = Resources.Load<Sprite>(mediaPath);
                        Sprite imageSprite = LoadImageSprite(mediaPath);

                        if (imageSprite != null)
                        {
                            tutorialImage.sprite = imageSprite;
                            tutorialImage.enabled = true;
                        }
                    }
                    else if (mediaPath.StartsWith("Videos/"))
                    {
                        VideoClip videoClip = Resources.Load<VideoClip>(mediaPath);

                        if (videoClip != null)
                        {
                            Video_RawImage.enabled = true;
                            tutorialVideo.clip = videoClip;
                            tutorialVideo.enabled = true;
                            OnVideoPrepared(tutorialVideo);
                            tutorialVideo.Play();
                        }
                    }

                    if (tutorialSteps[step].Length > 2)
                    {
                        mediaPath = tutorialSteps[step][2];

                        if (mediaPath.StartsWith("Images/"))
                        {
                            //Sprite imageSprite = Resources.Load<Sprite>(mediaPath);
                            Sprite imageSprite = LoadImageSprite(mediaPath);

                            if (imageSprite != null)
                            {
                                tutorialImage.sprite = imageSprite;
                                tutorialImage.enabled = true;
                            }
                        }
                        else if (mediaPath.StartsWith("Videos/"))
                        {
                            VideoClip videoClip = Resources.Load<VideoClip>(mediaPath);

                            if (videoClip != null)
                            {
                                Video_RawImage.enabled = true;
                                tutorialVideo.clip = videoClip;
                                tutorialVideo.enabled = true;
                                OnVideoPrepared(tutorialVideo);
                                tutorialVideo.Play();
                            }
                        }
                    }
                }
        //}

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

    private void OnVideoPrepared(VideoPlayer vp)
    {
        if (vp.width > vp.height)
        {
            // Rotate the RawImage 90 degrees if the video is 1920x1080
            Video_RawImage.rectTransform.localRotation = Quaternion.Euler(0, 0, -90);
        }
        else
        {
            // No rotation for 1080x1920 videos
            Video_RawImage.rectTransform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        vp.Play();
    }

    public void GoToNextStep()
    {
        /*
        int currentProgress = userData.progress;

        if (currentProgress == 7)
        {
            if (AreAllBoxesChecked())
            {
                // Proceed to the next step if all checkboxes are ticked
                if (currentProgress < tutorialSteps.Length - 1)
                {
                    currentProgress++;
                    LoadProgress(currentProgress);
                    // Save the new progress value to the UserData instance for the current user
                    loginManager.SaveUserProgress(currentProgress);
                }
            }
            else
            {
                // Display a message or play a sound to indicate that the user must tick all checkboxes before proceeding
                Debug.Log("Please tick all checkboxes before proceeding.");
            }
        }
        else
        {
            // Proceed to the next step as usual
            if (currentProgress < tutorialSteps.Length - 1)
            {
                currentProgress++;
                LoadProgress(currentProgress);
                // Save the new progress value to the UserData instance for the current user
                loginManager.SaveUserProgress(currentProgress);
            }
        }
        */
        int currentProgress = userData.progress;

        if (currentProgress < tutorialSteps.Length - 1)
        {
            currentProgress++;
            LoadProgress(currentProgress);
            // Save the new progress value to the UserData instance for the current user
            loginManager.SaveUserProgress(currentProgress);
        }
        else
        {
            currentProgress = 0;
            LoadProgress(currentProgress);
            // Save the new progress value to the UserData instance for the current user
            loginManager.SaveUserProgress(currentProgress);
        }
    }
    

    public void GoToPreviousStep()
    {
        //Debug.Log("Previous called");

        int currentProgress = userData.progress;
        if (currentProgress > 0)
        {
            currentProgress--;
            LoadProgress(currentProgress);
            // Save the new progress value to the UserData instance for the current user
            loginManager.SaveUserProgress(currentProgress);
        }
    }

    /*
    public bool AreAllBoxesChecked()
    {
        foreach (GameObject checkbox in checkboxes)
        {
            Transform checkBoxCheck = checkbox.transform.Find("CheckBoxCheck");

            if (!checkBoxCheck.gameObject.activeSelf)
            {
                return false;
            }
        }
        return true;
    }
    */
}
