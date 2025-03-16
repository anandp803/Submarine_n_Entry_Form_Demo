[System.Serializable]
public class Profile
{
    public string firstName;
    public string lastName;
    public string location;

    public Profile(string firstName, string lastName, string location)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.location = location;
    }
}
