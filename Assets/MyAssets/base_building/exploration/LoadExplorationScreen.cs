using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyAssets.base_building.exploration
{
    public class LoadExplorationScreen : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void LoadExploration()
        {
            SceneManager.LoadScene("Exploration");
        }
    }
}