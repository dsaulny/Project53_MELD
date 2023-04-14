using System;
using System.Collections.Generic;

[Serializable]
public class ProfileData
{
    public string profileName = "Lab_User_1";

    public List<User> userList = new List<User>() { 
        new User() { 
            name = "Taylor",
            hasStartedTutorial = true,
            hasCompletedSafetyTraining = true,
            totalTimeTaken = 1000,
            currentSection = 5
        },
        new User() { 
            name = "Hayden",
            hasStartedTutorial = true,
            hasCompletedSafetyTraining = false,
            totalTimeTaken = 200,
            currentSection = 1
        },
        new User() { 
            name = "Donald",
        },
        new User() { 
            name = "Tuan",
            hasStartedTutorial = true,
            hasCompletedSafetyTraining = false,
            totalTimeTaken = 1000,
            currentSection = 5
        },
        new User() { 
            //Uses default constructor, a new user named John
        },
        new User() { 
            //Uses default constructor, a new user named John
        },
        new User() { 
            //Uses default constructor, a new user named John
        },
        new User() { 
            //Uses default constructor, a new user named John
        },
        new User() { 
            //Uses default constructor, a new user named John
        },
        new User() { 
            //Uses default constructor, a new user named John
        },
        new User() { 
            //Uses default constructor, a new user named John
        },
    };
}