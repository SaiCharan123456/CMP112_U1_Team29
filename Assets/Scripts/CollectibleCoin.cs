using UnityEngine;

//Inherits from a root class to make more efficient the use of multiple collectibles
public class CollectibleCoin : CollectiblesRoot
{

    public override void OnTriggerEnter(Collider other)
    {
        source.PlayOneShot(pickupSound, 1.0f); //Run the mechanics of object collection
        playerState.instance.score += 50;

        Debug.Log("Collected a coin"); //Log the changes
        Debug.Log("Score " + playerState.instance.score);

        Destroy(gameObject); //Remove the object
    }

}
