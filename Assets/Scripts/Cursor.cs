using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Cursor : MonoBehaviour
{
    public TMP_Text command;

	private bool _commandMode;
	private int _commandX;
	private int _commandY;

    private void Update() {
		if ((GameObject.FindWithTag("Window") == null) && Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                NetworkManager.Instance.ShowButtons(hit.transform);
                var ground = hit.transform.GetComponent<Entities.Cells.Ground>();
                if (ground != null)
                {
                    Move(ground.groundItem.x, ground.groundItem.y);
                }

				var galaxy = hit.transform.GetComponent<Entities.Cells.Galaxy>();
				if (galaxy != null)
                {
                    Move(1, 1);
                }
                
                var system = hit.transform.GetComponent<Entities.Cells.System>();
				if (system != null)
                {
                    Move(1, 1);
                }

				var planet = hit.transform.GetComponent<Entities.Cells.Planet>();
				if (planet != null)
                {
                    Move(1, 1);
                }

				var continent = hit.transform.GetComponent<Entities.Cells.Continent>();
				if (continent != null)
                {
                    Move(1, 1);
                }

                var region = hit.transform.GetComponent<Entities.Cells.Region>();
				if (region != null)
                {
                    Move(region.regionItem.x, region.regionItem.y);
                }

                var city = hit.transform.GetComponent<Entities.Cells.City>();
                if (city != null)
                {
                    Move(city.cityItem.x, city.cityItem.y);
                }

                var block = hit.transform.GetComponent<Entities.Cells.Block>();
                if (block != null)
                {
                    Move(block.blockItem.x, block.blockItem.y);
                }

                var parcel = hit.transform.GetComponent<Entities.Cells.Parcel>();
                if (parcel != null)
                {
                    Move(parcel.parcelItem.x, parcel.parcelItem.y);
                }

				var floor = hit.transform.GetComponent<Entities.Cells.Floor>();
                if (floor != null)
                {
                    Move(floor.floorItem.x, floor.floorItem.y);
                }

                var room = hit.transform.GetComponent<Entities.Cells.Room>();
                if (room != null)
                {
                    Move(room.roomItem.x, room.roomItem.y);
                }
            }
        }

		if (Input.GetKeyDown(KeyCode.H))
		{
			Move((int)this.transform.position.x - 1, (int)this.transform.position.z);
		}
		else if (Input.GetKeyDown(KeyCode.J))
		{
			Move((int)this.transform.position.x, (int)this.transform.position.z - 1);
		}
		else if (Input.GetKeyDown(KeyCode.K))
		{
			Move((int)this.transform.position.x, (int)this.transform.position.z + 1);
		}
		else if (Input.GetKeyDown(KeyCode.L))
		{
			Move((int)this.transform.position.x + 1, (int)this.transform.position.z);
		}
    }

    private void Move(int x, int y)
    {
        this.transform.SetPositionAndRotation(new Vector3(x, 0, y), Quaternion.identity);
		GameManager.Instance.cursorX = x;
		GameManager.Instance.cursorY = y;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.transform.name);
        foreach(var contact in collision.contacts)
        {
            Debug.Log(contact.otherCollider.name);
            Debug.DrawRay(contact.point, contact.normal, Color.white);
            NetworkManager.Instance.ShowButtons(contact.otherCollider.transform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        NetworkManager.Instance.ShowButtons(other.transform);
    }
}
