using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelicPlacer : MonoBehaviour
{
    public GameObject relicContainer;
    public Image relicImage;
    public RelicSlot relicSlot;
    public Relic testRelic;

    public void PlaceRelic(Relic relic)
    {
        RelicSlot newRelicSlot = Instantiate(relicSlot, transform.position, Quaternion.identity, relicContainer.transform);
        relic.slot = newRelicSlot;
        //transform.Translate(new Vector3 (100 * GetComponentInParent<Canvas>().scaleFactor, 0, 0));
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            PlaceRelic(testRelic);
        }
    }
}
