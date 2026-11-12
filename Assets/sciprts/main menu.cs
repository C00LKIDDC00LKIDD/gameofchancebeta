using UnityEngine;
using UnityEngine.SceneManagement;
public class mainmenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("main");
        


    }
    public void main()
    {
        SceneManager.LoadSceneAsync("main menu");



    }
    public void credits()
    {
        SceneManager.LoadSceneAsync("credits");



    }
    public void howtoplay()
    {
        SceneManager.LoadSceneAsync("htp");



    }
    public void howtoplayfornoobs()
    {
        SceneManager.LoadSceneAsync("htpfn");



    }
}
