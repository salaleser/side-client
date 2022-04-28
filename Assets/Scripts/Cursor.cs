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
		if (!NetworkManager.Instance.shading.activeSelf && Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                NetworkManager.Instance.ShowButtons(hit.transform);
                var ground = hit.transform.GetComponent<Ground>();
                if (ground != null)
                {
                    Move(ground.groundItem.x, ground.groundItem.y);
                }
                
                var region = hit.transform.GetComponent<Region>();
				if (region != null)
                {
                    Move(region.regionItem.x, region.regionItem.y);
                }

                var city = hit.transform.GetComponent<City>();
                if (city != null)
                {
                    Move(city.cityItem.x, city.cityItem.y);
                }

                var block = hit.transform.GetComponent<Block>();
                if (block != null)
                {
                    Move(block.blockItem.x, block.blockItem.y);
                }

                var parcel = hit.transform.GetComponent<Parcel>();
                if (parcel != null)
                {
                    Move(parcel.parcelItem.x, parcel.parcelItem.y);
                }

				var floor = hit.transform.GetComponent<Floor>();
                if (floor != null)
                {
                    Move(floor.floorItem.x, floor.floorItem.y);
                }

                var room = hit.transform.GetComponent<Room>();
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

		if (!_commandMode)
		{
			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				command.text = "1/-";
				_commandX = 1;
				_commandMode = true;
				Move(_commandX, 0);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				command.text = "2/-";
				_commandX = 2;
				_commandMode = true;
				Move(_commandX, 0);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha3))
			{
				command.text = "3/-";
				_commandX = 3;
				_commandMode = true;
				Move(_commandX, 0);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha4))
			{
				command.text = "4/-";
				_commandX = 4;
				_commandMode = true;
				Move(_commandX, 0);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha5))
			{
				command.text = "5/-";
				_commandX = 5;
				_commandMode = true;
				Move(_commandX, 0);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha6))
			{
				command.text = "6/-";
				_commandX = 6;
				_commandMode = true;
				Move(_commandX, 0);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha7))
			{
				command.text = "7/-";
				_commandX = 7;
				_commandMode = true;
				Move(_commandX, 0);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha8))
			{
				command.text = "8/-";
				_commandX = 8;
				_commandMode = true;
				Move(_commandX, 0);
			}
		}
		else
		{
			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				command.text = $"{_commandX}/1";
				_commandY = 1;
				_commandMode = false;
				Move(_commandX, _commandY);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				command.text = $"{_commandX}/2";
				_commandY = 2;
				_commandMode = false;
				Move(_commandX, _commandY);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha3))
			{
				command.text = $"{_commandX}/3";
				_commandY = 3;
				_commandMode = false;
				Move(_commandX, _commandY);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha4))
			{
				command.text = $"{_commandX}/4";
				_commandY = 4;
				_commandMode = false;
				Move(_commandX, _commandY);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha5))
			{
				command.text = $"{_commandX}/5";
				_commandY = 5;
				_commandMode = false;
				Move(_commandX, _commandY);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha6))
			{
				command.text = $"{_commandX}/6";
				_commandY = 6;
				_commandMode = false;
				Move(_commandX, _commandY);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha7))
			{
				command.text = $"{_commandX}/7";
				_commandY = 7;
				_commandMode = false;
				Move(_commandX, _commandY);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha8))
			{
				command.text = $"{_commandX}/8";
				_commandY = 8;
				_commandMode = false;
				Move(_commandX, _commandY);
			}
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
