using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class win : MonoBehaviour
{
    public UIDocument uidocument;
    private UnityEngine.UIElements.Button replay;
    private UnityEngine.UIElements.Label youwin;
    void Start()
    {
        youwin = uidocument.rootVisualElement.Q<UnityEngine.UIElements.Label>("youwin");
        replay = uidocument.rootVisualElement.Q<UnityEngine.UIElements.Button>("replay");
        youwin.style.display = DisplayStyle.None;
        replay.style.display = DisplayStyle.None;
        replay.clicked += ReloadScene;
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object has the tag "Player"
        if (collision.gameObject.CompareTag("player"))
        {
            // Example: Destroy bullet after hit
            youwin.style.display = DisplayStyle.Flex;
            replay.style.display = DisplayStyle.Flex;
        }
    }
    void ReloadScene()
    {
        //reloading scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
