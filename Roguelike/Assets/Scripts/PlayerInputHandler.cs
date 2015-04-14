using UnityEngine;
using System.Collections;

public class PlayerInputHandler : MonoBehaviour
{
    public Player playerController;

    public Transform location
    {
        get
        {
            return this.transform;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("SAVING");
            SaveLoad.SaveGame();
            Debug.Log("Saving complete");
        }

        if (!playerController.paralysed)
        {
            if (Input.GetKeyDown(KeyCode.Equals))
            {
                Action.UseStairs(location);
            }

            if (Input.GetKey(KeyCode.O)) // Open Doors
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.Keypad4)) // left
                    Action.OpenDoor(location, -Vector3.right);

                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Keypad8)) // up
                    Action.OpenDoor(location, Vector3.up);

                if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Keypad6)) // right
                    Action.OpenDoor(location, Vector3.right);

                if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.Keypad2)) // down
                    Action.OpenDoor(location, -Vector3.up);
            }

            if (Input.GetKey(KeyCode.C)) // Close Doors
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.Keypad4)) // left
                    Action.CloseDoor(location, -Vector3.right);

                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Keypad8)) // up
                    Action.CloseDoor(location, Vector3.up);

                if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Keypad6)) // right
                    Action.CloseDoor(location, Vector3.right);

                if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.Keypad2)) // down
                    Action.CloseDoor(location, -Vector3.up);
            }

            if (playerController.canMove && !Input.GetKey(KeyCode.O) && !Input.GetKey(KeyCode.C)) // Expand with other movement keys in future
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.Keypad4)) // left
                    Action.AssessAction(location, -Vector3.right, Vector3.zero);

                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Keypad8)) // up
                    Action.AssessAction(location, Vector3.up, Vector3.zero);

                if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Keypad6)) // right
                    Action.AssessAction(location, Vector3.right, Vector3.zero);

                if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.Keypad2)) // down
                    Action.AssessAction(location, -Vector3.up, Vector3.zero);

                if (playerController.canMoveDiagonally)
                {
                    if (Input.GetKeyDown(KeyCode.Keypad3)) // down right
                        Action.AssessAction(location, Vector3.right, -Vector3.up);

                    if (Input.GetKeyDown(KeyCode.Keypad1)) // down left
                        Action.AssessAction(location, -Vector3.right, -Vector3.up);

                    if (Input.GetKeyDown(KeyCode.Keypad7)) // up left
                        Action.AssessAction(location, -Vector3.right, Vector3.up);

                    if (Input.GetKeyDown(KeyCode.Keypad9)) // up right
                        Action.AssessAction(location, Vector3.right, Vector3.up);
                }
            }
        }

        Camera.main.GetComponent<CameraControl>().UpdateLocation((int)location.position.x, (int)location.position.y);
    }
}