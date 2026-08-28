using UnityEngine;

public class Hole : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
       Ball b = other.GetComponent<Ball>();

        if (b != null)
        {
            if (b.Point == 0)

            {
                Gamemanager.instance.ShowString($"White Ball drop!!\nYou lose!!");
                Time.timeScale = 0f;
            }

            else
            {
                Gamemanager.instance.ShowScoreText(b.Point);
            }
            Destroy(b.gameObject);
        }
    }
}
