using UnityEngine;

//Inherits from a root class to make more efficient the use of multiple collectibles
public class CollectibleHealth : CollectiblesRoot
{

    public override void OnTriggerEnter(Collider player)
    {
        source.PlayOneShot(pickupSound, 1.0f);
        tempScore += 3;
        Debug.Log("Collected some health");
        Debug.Log(tempScore); //Currently implemented to verify score is being updated uniquely. may not be needed in all collectible scripts
        playerHealth += 5;
        Debug.Log(playerHealth);
        Destroy(gameObject);
    }

}
