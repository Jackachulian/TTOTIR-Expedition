using UnityEngine;

namespace TTOTIR.Combat
{
    public class GameCamera : MonoBehaviour
    {
        [Tooltip("The player to follow")]
        [SerializeField] GameObject player;

        [Tooltip("Distance in units that the camera will move when the mouse is at the edge of the screen.")]
        [SerializeField] float viewDistance = 2f;

        [Tooltip("Speed of the camera following the player (not the muse).")]
        [SerializeField] float playerLookTime = 0.1f;

        [Tooltip("Speed of the camera following the mouse (not the player).")]
        [SerializeField] float mouseLookTime = 0.25f;


        // The offset of the player to this camera. Saved when the game starts, so whatever position it is
        // relative to the player, it will stay in.
        [SerializeField] Vector3 playerOffset;

        void Start()
        {
            playerOffset = transform.position - player.transform.position;
        }

        Vector3 baseOffset;
        Vector3 mouseOffset;
        Vector3 baseVelocity;
        Vector3 mouseVelocity;

        static readonly Vector3 cameraHorizontal = new Vector3(1,0,-1).normalized;
        static readonly Vector3 cameraVertical = new Vector3(1,0,1).normalized;

        void Update()
        {
            Vector3 targetBasePosition = player.transform.position;
            targetBasePosition.y = 1;
            targetBasePosition += playerOffset;
            baseOffset = Vector3.SmoothDamp(baseOffset, targetBasePosition, ref baseVelocity, playerLookTime);

            Vector2 mousePos = Input.mousePosition;
            Vector3 targetMouseOffset = 
            (cameraHorizontal * (mousePos.x - Screen.width/2) * viewDistance*2/Screen.width) + 
            (cameraVertical * (mousePos.y - Screen.height/2) * viewDistance*2/Screen.height);
            mouseOffset = Vector3.SmoothDamp(mouseOffset, targetMouseOffset, ref mouseVelocity, mouseLookTime);

            transform.position = baseOffset + mouseOffset;
        }
    }
}