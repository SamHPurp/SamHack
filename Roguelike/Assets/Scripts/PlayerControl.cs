using UnityEngine;
using System.Collections;

public class PlayerControl : Actor
{
    void Awake()
    {
        this.tag = "Player";
    }
	
	void Update()
	{

        if (!paralysed)
        {
            if (Input.GetKeyDown(KeyCode.Equals))
            {
                Action.UseStairs(location, mapGenerator, mapGenerator.theManager);
            }

            if (Input.GetKey(KeyCode.O)) // Open Doors
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.Keypad4)) // left
                    Action.OpenDoor(location, mapGenerator, -Vector3.right);

                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Keypad8)) // up
                    Action.OpenDoor(location, mapGenerator, Vector3.up);

                if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Keypad6)) // right
                    Action.OpenDoor(location, mapGenerator, Vector3.right);

                if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.Keypad2)) // down
                    Action.OpenDoor(location, mapGenerator, -Vector3.up);
            }

            if (Input.GetKey(KeyCode.C)) // Close Doors
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.Keypad4)) // left
                    Action.CloseDoor(location, mapGenerator, -Vector3.right);

                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Keypad8)) // up
                    Action.CloseDoor(location, mapGenerator, Vector3.up);

                if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Keypad6)) // right
                    Action.CloseDoor(location, mapGenerator, Vector3.right);

                if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.Keypad2)) // down
                    Action.CloseDoor(location, mapGenerator, -Vector3.up);
            }

            if (canMove && !Input.GetKey(KeyCode.O) && !Input.GetKey(KeyCode.C)) // Expand with other movement keys in future
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.Keypad4)) // left
                {
                    Movement.Move(location, -Vector3.right, Vector3.zero);
                    GameFlow.ExecuteTurn();
                }

                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Keypad8)) // up
                {
                    Movement.Move(location, Vector3.up, Vector3.zero);
                    GameFlow.ExecuteTurn();
                }

                if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Keypad6)) // right
                {
                    Movement.Move(location, Vector3.right, Vector3.zero);
                    GameFlow.ExecuteTurn();
                }

                if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.Keypad2)) // down
                {
                    Movement.Move(location, -Vector3.up, Vector3.zero);
                    GameFlow.ExecuteTurn();
                }

                if (canMoveDiagonally)
                {
                    if (Input.GetKeyDown(KeyCode.Keypad3)) // down right
                    {
                        Movement.Move(location, Vector3.right, -Vector3.up);
                        GameFlow.ExecuteTurn();
                    }

                    if (Input.GetKeyDown(KeyCode.Keypad1)) // down left
                    {
                        Movement.Move(location, -Vector3.right, -Vector3.up);
                        GameFlow.ExecuteTurn();
                    }

                    if (Input.GetKeyDown(KeyCode.Keypad7)) // up left
                    {
                        Movement.Move(location, -Vector3.right, Vector3.up);
                        GameFlow.ExecuteTurn();
                    }

                    if (Input.GetKeyDown(KeyCode.Keypad9)) // up right
                    {
                        Movement.Move(location, Vector3.right, Vector3.up);
                        GameFlow.ExecuteTurn();
                    }
                }
            }
        }

        Camera.main.GetComponent<CameraControl>().UpdateLocation((int)transform.position.x, (int)transform.position.y);
	}

    protected override void Register() // Stops player register on the DungeonMaster
    {
    }
}