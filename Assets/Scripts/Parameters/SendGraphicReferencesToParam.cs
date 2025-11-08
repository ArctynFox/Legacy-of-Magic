using UnityEngine;

public class SendGraphicReferencesToParam : MonoBehaviour
{
    [SerializeField]
    GameObject[] hearts = new GameObject[5];
    [SerializeField]
    GameObject[] spells = new GameObject[5];

    void Awake()
    {
        Debug.Log("Passing heart and spell gameobjects to param.");
        Parameters.singleton.setLifeGraphicArray(hearts);
        Parameters.singleton.setBombGraphicArray(spells);
    }
}
