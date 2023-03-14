using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class TextPopUp : MonoBehaviour
{

    TextMeshProUGUI TM;
    public float characterPopupDelay = 0.1f;
    private bool finishedText;
    private bool skipText;
    [SerializeField] string firstMessage;
    [SerializeField] string lastMessage;

    // Start is called before the first frame update
    void Start()
    {
        finishedText = true;
        TM = GameObject.FindWithTag("ParagraphText").GetComponentInChildren<TextMeshProUGUI>();
        startMessages();
    }

    private void Update() {
        if (!finishedText) {if(Input.GetKeyDown(KeyCode.Mouse0)) {skipText = true;}}
    }

    private async void startMessages()
    {
        StartCoroutine("popUp", firstMessage);
        while(!finishedText) {await Task.Yield();}
        gameObject.SetActive(true);
        StartCoroutine("popUp", lastMessage);
    }

    public IEnumerator popUp(string message)
    {
        finishedText = false;
        skipText = false;
        gameObject.SetActive(true);
        yield return StartCoroutine("DisplayText", message);
        yield return StartCoroutine("waitForKeyPress", KeyCode.Mouse0);
        gameObject.SetActive(false);
        finishedText = true;       
    }

     private IEnumerator DisplayText(string message)
    {
        TM.text = "";
        int alphaIndex = 0;

        foreach(char c in message.ToCharArray())
        {
            if((skipText) && (alphaIndex < message.ToCharArray().Length  - 5)) {TM.text = message; break;}
            alphaIndex++;
            TM.text = message.Substring(0, alphaIndex);
            yield return new WaitForSecondsRealtime(characterPopupDelay);
        }
        yield return null;
    }

    private IEnumerator waitForKeyPress(KeyCode key)
    {
        bool done = false;
        while(!done) // essentially a "while true", but with a bool to break out naturally
        {
            if(Input.GetKeyDown(key))
            {
                done = true; // breaks the loop
            }
            yield return 0; // wait until next frame, then continue execution from here (loop continues)
        }
        yield return null;
        // now this function returns
    }
}
