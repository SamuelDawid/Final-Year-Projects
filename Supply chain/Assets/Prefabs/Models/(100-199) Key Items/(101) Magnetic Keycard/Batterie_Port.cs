using UnityEngine;

public class Batterie_Port : MonoBehaviour
{
    [SerializeField]
    private GameObject smoke,Electric;
    public int ItemID;
    [SerializeField]
    private Transform batteryTransform;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<KeyItem>() != null)
        {
            KeyItem x = other.GetComponent<KeyItem>();
            if (x.infoKID.GivenID == ItemID)
            {
                QuestManager.instance.quest[1].AddAmmount();
                other.GetComponent<BoxCollider>().enabled = false;
                other.GetComponent<Rigidbody>().isKinematic = true;
                other.transform.position = batteryTransform.transform.position;
                Destroy(smoke);
                Destroy(Electric);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
         if (other.gameObject.GetComponent<KeyItem>() == null)
        {
            KeyItem x = other.GetComponent<KeyItem>();
            if (x.infoKID.GivenID == ItemID)
            {
            QuestManager.instance.quest[1].MinusAmmount();
            }
       
         }
    }
}
