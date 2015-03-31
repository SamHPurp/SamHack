using UnityEngine;
using System.Collections;

public class PlayerControl : Actor
{
    public Generation mapGenerator;
    public CameraControl theMainCamera;
    int seed = 10;
    PlayerControl instance;

    void Awake()
    {
        instance = this;
        location = this.transform;
        this.tag = "Player";

        mapGenerator = FindObjectOfType<Generation>();
    }
	
	void Update()
	{
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PsuedoRandom.ShowRandomNumbers(seed);
        }

        if (canMove && !paralysed)
        {
            if (Input.GetKey(KeyCode.O))
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

            if (Input.GetKey(KeyCode.C))
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

            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.Keypad4)) // left
                Movement.Move(location, mapGenerator, theMainCamera, instance, -Vector3.right);

            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Keypad8)) // up
                Movement.Move(location, mapGenerator, theMainCamera, instance, Vector3.up);

            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Keypad6)) // right
                Movement.Move(location, mapGenerator, theMainCamera, instance, Vector3.right);

            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.Keypad2)) // down
                Movement.Move(location, mapGenerator, theMainCamera, instance, -Vector3.up);

            if (canMoveDiagonally)
            {
                if (Input.GetKeyDown(KeyCode.Keypad3)) // down right
                    Movement.Move(location, mapGenerator, theMainCamera, instance, Vector3.right, -Vector3.up);

                if (Input.GetKeyDown(KeyCode.Keypad1)) // down left
                    Movement.Move(location, mapGenerator, theMainCamera, instance, -Vector3.right, -Vector3.up);

                if (Input.GetKeyDown(KeyCode.Keypad7)) // up left
                    Movement.Move(location, mapGenerator, theMainCamera, instance, -Vector3.right, Vector3.up);

                if (Input.GetKeyDown(KeyCode.Keypad9)) // up right
                    Movement.Move(location, mapGenerator, theMainCamera, instance, Vector3.right, Vector3.up);
            }
        }
	}
}