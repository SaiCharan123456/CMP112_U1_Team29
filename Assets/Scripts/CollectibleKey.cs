using UnityEngine;

//Inherits from a root class to make more efficient the use of multiple collectibles
public class CollectibleKey : CollectiblesRoot
{

    public override void OnTriggerEnter(Collider player)
    {
        tempScore += 99;
        Debug.Log("Collected a key");
        Debug.Log(tempScore); //Currently implemented to verify score is being updated uniquely. may not be needed in all collectible scripts
        Destroy(gameObject);
    }

}
