using UnityEngine;

public class CamaraFollow : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    void Update()
    {
        this.gameObject.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y , this.gameObject.transform.position.z);
    }
}