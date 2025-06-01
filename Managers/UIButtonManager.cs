using UnityEngine;
using UnityEngine.UI;

public class UIButtonManager : MonoBehaviour
{

    private void Start()
    {
        transform.Find("NewGameButton").GetComponent<Button>().onClick.AddListener(()=> LoadSceneManager.Instance.LoadSceneAsync(1));
        transform.Find("QuitButton").GetComponent<Button>().onClick.AddListener(()=> Application.Quit());
    }

}
