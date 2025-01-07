using UnityEngine;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Text;



public class CharacterMomen : MonoBehaviour {
    public float speed = 4.0f;
    private Animator playerAnim;
    private SpriteRenderer playerSpriteImage;
    private UdpClient udpClient;
    private MovementData movementReceive;
    private Vector2 playerPosition;

    void Start() {
        udpClient = new UdpClient();
        udpClient.Connect("127.0.0.1", 5000);
        Task.Run(ReceiveGameState);
    }

    void Awake() {
        playerAnim = (Animator)GetComponent(typeof(Animator));
        playerSpriteImage = (SpriteRenderer)GetComponent(typeof(SpriteRenderer));
        playerPosition = new Vector2(transform.position.x, transform.position.y);
    }

    async void ReceiveGameState() {
        while (true) {
            var result = await udpClient.ReceiveAsync();
            string json = Encoding.UTF8.GetString(result.Buffer);
            movementReceive = JsonUtility.FromJson<MovementData>(json);
        }
    }

    private void SendPositionToServer(Vector2 position, float dx, float dy) {
        try {
            MovementData movementData = new MovementData {
                x = position.x,
                y = position.y,
                dx = dx,
                dy = dy,
            };
            string dataToSend = JsonUtility.ToJson(movementData);
            byte[] dataBytes = Encoding.UTF8.GetBytes(dataToSend);
            udpClient.Send(dataBytes, dataBytes.Length);
        }
        catch (System.Exception ex) {
            Debug.LogError($"Error sending position to server: {ex.Message}");
        }
    }

    void Update() {
        float dx = Input.GetAxis("Horizontal");
        float dy = Input.GetAxis("Vertical");

        if (dx != 0 || dy != 0) {
            playerPosition += new Vector2(dx, dy) * speed * Time.deltaTime;
            SendPositionToServer(playerPosition, dx, dy);
        }
    }

    void FixedUpdate() {
        float dx = 0f;
        float dy = 0f;

        if (movementReceive != null) {
            playerPosition = new Vector2(movementReceive.x, movementReceive.y);
            transform.position = playerPosition;
            dx = movementReceive.dx;
            dy = movementReceive.dy;
            movementReceive = null;
        }

        if (dx == 0 && dy == 0) {
            playerAnim.SetBool("moving", false);
        }
        else {
            playerAnim.SetBool("moving", true);
        }

        if (dy != 0) {
            playerAnim.SetBool("xMove", false);
            playerSpriteImage.flipX = false;

            if (dy > 0) {
                playerAnim.SetInteger("yMove", 1);
            }
            else if (dy < 0) {
                playerAnim.SetInteger("yMove", -1);
            }
        }
        else {
            playerAnim.SetInteger("yMove", 0);
            if (dx > 0) {
                playerAnim.SetBool("xMove", true);
                playerSpriteImage.flipX = false;
            }
            else if (dx < 0) {
                playerSpriteImage.flipX = true;
                playerAnim.SetBool("xMove", true);
            }
            else {
                playerAnim.SetBool("xMove", false);
            }
        }
    }
}
