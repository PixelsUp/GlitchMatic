using UnityEngine;
using TMPro;  // For TextMeshPro InputFields and Text
using UnityEngine.UI;

public class AccountManager : MonoBehaviour
{
    // Reference to the TMP InputFields for username and password
    public TMP_InputField usernameInputField;
    public TMP_InputField passwordInputField;

    // Reference to the feedback text at the bottom
    public TMP_Text feedbackText;

    // Reference to the submit button (if you want to use one)
    public Button submitButton;

    private void Start()
    {
        // Optionally assign the button's onClick event to the SubmitLogin function
        if (submitButton != null)
        {
            submitButton.onClick.AddListener(SubmitLogin);
        }

        // Load saved user data (if available) when the scene starts
        LoadUserData();
    }

    // Function to handle user login when submit button is pressed
    public void SubmitLogin()
    {
        string username = usernameInputField.text;
        string password = passwordInputField.text;

        // Check if both username and password fields are not empty
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            feedbackText.text = "Username and Password must not be empty.";
            return;
        }

        // You can add custom logic here to verify username/password (e.g., against a database)
        feedbackText.text = "Login successful!"; // Simple feedback for the prototype

        // Save the entered data
        SaveUserData(username, password);
    }

    // Function to save the user's data (username and password) using PlayerPrefs
    private void SaveUserData(string username, string password)
    {
        PlayerPrefs.SetString("username", username);
        PlayerPrefs.SetString("password", password);
        PlayerPrefs.Save();
    }

    // Function to load saved user data (if available) and display it
    private void LoadUserData()
    {
        if (PlayerPrefs.HasKey("username") && PlayerPrefs.HasKey("password"))
        {
            string savedUsername = PlayerPrefs.GetString("username");
            string savedPassword = PlayerPrefs.GetString("password");

            usernameInputField.text = savedUsername;
            passwordInputField.text = savedPassword;

            feedbackText.text = "Welcome back, " + savedUsername + "!";
        }
        else
        {
            feedbackText.text = "Please enter your username and password.";
        }
    }

    // Optionally, a function to clear the saved user data
    public void ClearUserData()
    {
        PlayerPrefs.DeleteKey("username");
        PlayerPrefs.DeleteKey("password");
        PlayerPrefs.Save();

        usernameInputField.text = "";
        passwordInputField.text = "";
        feedbackText.text = "User data cleared.";
    }
}
