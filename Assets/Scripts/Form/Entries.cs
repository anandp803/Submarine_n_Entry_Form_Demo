using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class Entries : MonoBehaviour
{
    public Button button;
    public TMP_Text firstName;
    public TMP_Text lastName;
    public TMP_Text location;
    public TMP_Text S_No;
    public int index;
    public static event Action<int> EntryClicked;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        // Trigger the event to pass the index
        EntryClicked?.Invoke(index);    
        
    }

}
