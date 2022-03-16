using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public int x;
    public int y;

    private Text _text;

    void Start()
    {
       var textObject = GameObject.Find("Text");
       _text = textObject.GetComponent<Text>();
    }

    private void HideAllButtons()
	{
		var objects = GameObject.FindGameObjectsWithTag("Button");
		foreach(var o in objects)
		{
			o.SetActive(false);
		}
	}

    void Update() {  
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                foreach(var cell in hit.transform.GetComponentsInChildren<Cell>())
                {
                    _text.text = cell.ToString();
                    HideAllButtons();
                }
                foreach(var city in hit.transform.GetComponentsInChildren<City>())
                {
                    _text.text = city.location.ToString();
                    city.ShowButtons();
                }
                foreach(var block in hit.transform.GetComponentsInChildren<Block>())
                {
                    _text.text = block.location.ToString();
                    block.ShowButtons();
                }
                foreach(var landLot in hit.transform.GetComponentsInChildren<LandLot>())
                {
                    _text.text = landLot.location.ToString();
                    landLot.ShowButtons();
                }
                foreach(var facility in hit.transform.GetComponentsInChildren<Facility>())
                {
                    _text.text = facility.location.ToString();
                    facility.ShowButtons();
                }
            }
        }
    }

    public override string ToString() => @$"Cell:
    X: {x}
    Y: {y}";
}
