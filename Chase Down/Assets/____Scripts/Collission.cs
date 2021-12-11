using UnityEngine;
using UnityEngine.UI;

public class Collission : MonoBehaviour
{
    
    public GameManager gameManager;
    public Text livesText;

    public int lives = 5;

    
    void OnTriggerEnter(Collider collisionInfo)
    {
        if (collisionInfo.GetComponent<Collider>().tag == "Enemy")
        {
            //FindObjectOfType<AudioManager>().Play("Player Death");

            lives--;
        }
    }

    void Update()
    {
        //Debug.Log(lives);

        livesText.text = lives.ToString();

        if (lives <= 0)
        {
            //Finds Another Script And activates the EndGame function
            //which Reloads The Scene.
            FindObjectOfType<GameManager>().EndGame();
        }
    }
}
