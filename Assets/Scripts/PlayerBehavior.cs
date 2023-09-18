using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    public float speed = 150;
    public int lastNoteNumber = -1;
    public int currentNoteNumber = -1;
    public float thruster1 = 0;
    public float starter1;
    public float starter2;
    public float starter3;
    public Rigidbody ballRb;
    public bool hasStarted = false;
    public bool engaged = false;
    public Material[] paddleMaterials;
    private Renderer rend;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
        GameObject ball = GameObject.FindGameObjectWithTag("Ball");
        if (ball != null)
        {
            ballRb = ball.GetComponent<Rigidbody>();
        }
        InputSystem.onDeviceChange += (device, change) =>
        {
            if (change != InputDeviceChange.Added) return;
            var midiDevice = device as Minis.MidiDevice;
            if (midiDevice == null) return;

            midiDevice.onWillNoteOn += (note, velocity) => {
                Debug.Log(string.Format(
                    "Note On #{0} ({1}) vel:{2:0.00} ch:{3} dev:'{4}'",
                    note.noteNumber,
                    note.noteNumber.GetType(),
                    note.shortDisplayName,
                    velocity,
                    velocity.GetType(),
                    (note.device as Minis.MidiDevice)?.channel,
                    note.device.description.product
                ));
                currentNoteNumber = note.noteNumber;
            };

            midiDevice.onWillControlChange += (cc, value) => {
                // Note that you can't use the cc object (the first argument)
                // to read the CC value because the state hasn't been updated
                // yet (as this is "will" event). The cc object is only useful
                // to determine the target control element (CC number, channel
                // number, device name, etc.) Use value (the second argument)
                // as an input control value.
                Debug.Log(string.Format(
                    "CC #{0} ({1}) value:{2:0.00} ch:{3} dev:'{4}'",
                    cc.controlNumber,
                    cc.shortDisplayName,
                    value,
                    (cc.device as Minis.MidiDevice)?.channel,
                    cc.device.description.product
                ));
                if (cc.controlNumber == 1) {
                    thruster1 = value;
                }
                if (cc.controlNumber == 16) {
                    starter1 = value;
                }
                if (cc.controlNumber == 17) {
                    starter2 = value;
                }
                if (cc.controlNumber == 18) {
                    starter3 = value;
                }
            };
        };

    }

    private void OnMove(InputValue keyPressed)
    {
        //Debug.Log(keyPressed.Get());

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        // ballRb.AddForce(new Vector3(0.0f, 0.0f, 5.0f) * 5);

        // if (rb.position.x <= -3 || rb.position.x >=14) {
        //     rb.AddForce(new Vector3(0.0f, 0.0f, 0.0f));
        // }
        //note movement
        Debug.Log(starter1);
        Debug.Log(starter2);
        Debug.Log(starter3);
        if (lastNoteNumber == -1 && currentNoteNumber != -1) {

            lastNoteNumber = currentNoteNumber;
            //rb.AddForce(new Vector3(0.0f, 10.0f, 0.0f) * speed);
            if (!hasStarted) {
                ballRb.velocity = (new Vector3(0.0f, 0.0f, 10.0f));
                hasStarted = true;
            }
        }

        else if (currentNoteNumber < lastNoteNumber) {
            rb.AddForce(new Vector3(-10.0f, 0.0f, 0.0f) * speed);
            lastNoteNumber = currentNoteNumber;
        }

        //else if (currentNoteNumber == lastNoteNumber + 1 && rb.position.x < 13.25) {
        else if (currentNoteNumber > lastNoteNumber) {
            rb.AddForce(new Vector3(10.0f, 0.0f, 0.0f) * speed);
            lastNoteNumber = currentNoteNumber;
        };

        //cc movement
        rb.AddForce(new Vector3(0.0f, 100.0f, 0.0f) * thruster1);

        if (!hasStarted) {
            if (starter1 > .5f && starter2 > .5f && starter3 > .5 && !engaged) {
                engaged = true;
                rend.material = paddleMaterials[1];
            }
            if (starter1 < .5f && starter2 < .5f && starter3 < .5f && engaged) {
                hasStarted = true;
                engaged = false;
                ballRb.velocity = (new Vector3(0.0f, 0.0f, 10.0f));
                rend.material = paddleMaterials[0];

            }
        }


    }
};
