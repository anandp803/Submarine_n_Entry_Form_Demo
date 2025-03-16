using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System;

public class ProfileFormUI : MonoBehaviour
{
    public TMP_InputField firstNameInput;
    public TMP_InputField lastNameInput;
    public TMP_InputField locationInput;
    public Button addButton;
    public Button editButton;
    public Button deleteButton;   
    public GameObject entryPrefabParent;
    public GameObject entryPrefab;
    
    private ProfileManager profileManager;
    private int currentEditIndex = -1;
    
    void Start()
    {
        Entries.EntryClicked += SetProfileForEditing;
        profileManager = GetComponent<ProfileManager>();

        addButton.onClick.AddListener(AddProfile);
        editButton.onClick.AddListener(EditProfile);
        deleteButton.onClick.AddListener(DeleteProfile);

        Invoke("UpdateProfileList", 0.1f);
    }

    // Add profile from the input fields
    private void AddProfile()
    {
        string firstName = firstNameInput.text;
        string lastName = lastNameInput.text;
        string location = locationInput.text;

        profileManager.AddProfile(firstName, lastName, location);
        UpdateProfileList();
        ClearInputs();
    }

    // Edit the selected profile
    private void EditProfile()
    {
        if (currentEditIndex >= 0)
        {
            string firstName = firstNameInput.text;
            string lastName = lastNameInput.text;
            string location = locationInput.text;

            profileManager.EditProfile(currentEditIndex, firstName, lastName, location);
            UpdateProfileList();
            ClearInputs();
        }
    }

    // Delete the selected profile
    private void DeleteProfile()
    {
        if (currentEditIndex >= 0)
        {
            profileManager.DeleteProfile(currentEditIndex);
            UpdateProfileList();
            ClearInputs();
        }
    }

    // Update the profile list displayed in the UI
    private void UpdateProfileList()
    {
        List<Profile> profiles = profileManager.GetProfiles();         
        foreach (Transform child in entryPrefabParent.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < profiles.Count; i++)
        {
            GameObject entry = Instantiate(entryPrefab, entryPrefabParent.transform);
            Entries entryScript = entry.GetComponent<Entries>();
            entryScript.firstName.text = profiles[i].firstName;
            entryScript.lastName.text = profiles[i].lastName;
            entryScript.location.text = profiles[i].location;
            entryScript.S_No.text = (i + 1).ToString();
            entryScript.index = i;           
        }
    }

    // Clear the input fields
    private void ClearInputs()
    {
        firstNameInput.text = "";
        lastNameInput.text = "";
        locationInput.text = "";
    }

    // Set the selected profile index for editing
    public void SetProfileForEditing(int index)
    {
        if (index >= 0 && index < profileManager.GetProfiles().Count)
        {
            Profile profile = profileManager.GetProfiles()[index];
            firstNameInput.text = profile.firstName;
            lastNameInput.text = profile.lastName;
            locationInput.text = profile.location;

            currentEditIndex = index;
        }
        // Reset all to default color
        foreach (Transform child in entryPrefabParent.transform)
        {
            child.GetComponent<Image>().color = Color.white; 
        }
         // Highlight selected child
        if (index >= 0 && index < entryPrefabParent.transform.childCount)
        {
            Transform selectedChild = entryPrefabParent.transform.GetChild(index);
            selectedChild.GetComponent<Image>().color = Color.yellow;
        }
    }
}
