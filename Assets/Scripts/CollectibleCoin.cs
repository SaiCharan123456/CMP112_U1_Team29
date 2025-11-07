using UnityEngine;

//Inherits from a root class to make more efficient the use of multiple collectibles
public class CollectibleCoin : CollectiblesRoot
{

    public override void OnTriggerEnter(Collider player)
    {
        source.PlayOneShot(pickupSound, 1.0f);
        tempScore += 50;
        Debug.Log("Collected a coin");
        Debug.Log(tempScore); //Currently implemented to verify score is being updated uniquely. may not be needed in all collectible scripts
        Destroy(gameObject);
    }

}
