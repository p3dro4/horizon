using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyAssets.main_menu
{
    public class Buttons : MonoBehaviour
    {
        public void NewGame()
        {
            Debug.Log("New Game");
            SceneManager.LoadScene("MainScene");
        }


        public void LoadGame()
        {
            Debug.Log("Load Game");
            throw new System.NotImplementedException();
        }

        public void Options()
        {
            Debug.Log("Options");
            throw new System.NotImplementedException();
        }


        public void QuitGame()
        {
            Debug.Log("Quit Game");
            Application.Quit();
        }
    }
}