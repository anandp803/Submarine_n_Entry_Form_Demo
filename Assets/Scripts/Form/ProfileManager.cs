using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ProfileManager : MonoBehaviour
{
    [SerializeField]
    private TextAsset jsonFile; 
    [SerializeField]
    private List<Profile> profilesVal = new List<Profile>();

    void Start()
    {
        if (jsonFile != null)
        {
            LoadProfiles();
        }
        else
        {
            Debug.LogError("JSON file reference is missing!");
        }
    }

    // Load profiles from the JSON file
    public void LoadProfiles()
    {
        if (jsonFile != null)
        {
            string json = jsonFile.text;           
            profilesVal = JsonUtility.FromJson<ProfileList>(json).profiles;
            Debug.Log(profilesVal.Count + " profiles loaded.");
        }
    }

    // Save profiles to the JSON file (not applicable for TextAsset)
    public void SaveProfiles()
    {
        string json = JsonUtility.ToJson(new ProfileList { profiles = profilesVal }, true);
        string filePath = Path.Combine(Application.dataPath, "Resources", "profiles.json");
        File.WriteAllText(filePath, json);
        Debug.Log("Profiles saved to: " + filePath);
    }

    // Add a new profile
    public void AddProfile(string firstName, string lastName, string location)
    {
        Profile newProfile = new Profile(firstName, lastName, location);
        print(newProfile.firstName);
        profilesVal.Add(newProfile);
        SaveProfiles();
    }

    // Edit an existing profile
    public void EditProfile(int index, string firstName, string lastName, string location)
    {
        if (index >= 0 && index < profilesVal.Count)
        {
            profilesVal[index].firstName = firstName;
            profilesVal[index].lastName = lastName;
            profilesVal[index].location = location;
            SaveProfiles();
        }
    }

    // Delete a profile
    public void DeleteProfile(int index)
    {
        if (index >= 0 && index < profilesVal.Count)
        {
            profilesVal.RemoveAt(index);
            SaveProfiles();
        }
    }

    // Get all profiles
    public List<Profile> GetProfiles()
    {
        Debug.Log(profilesVal.Count + " return profiles loaded.");
        return profilesVal;
    }

    // Helper class for serializing a list of profiles
    [System.Serializable]
    public class ProfileList
    {
        public List<Profile> profiles;
    }
}
