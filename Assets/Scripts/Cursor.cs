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
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                NetworkManager.Instance.ShowButtons(hit.transform);
                var address = hit.transform.GetComponent<Address>();
                var ground = hit.transform.GetComponent<Ground>();
                var room = hit.transform.GetComponent<Room>();
                if (address != null)
                {
                    Move(address.item.x, address.item.y);
                }
                if (ground != null)
                {
                    Move(ground.item.x, ground.item.y);
                }
                if (room != null)
                {
                    Move(room.item.x, room.item.y);
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
