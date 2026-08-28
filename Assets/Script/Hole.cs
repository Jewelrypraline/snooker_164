using UnityEngine;

public class Hole : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
       Ball b = other.GetComponent<Ball>();

        if (b != null)
        {
            if (b.Point == 0)
            Gamemanager.instance.ShowScoreText(b.Point);
            Destroy(b.gameObject);
        }
    }
}
