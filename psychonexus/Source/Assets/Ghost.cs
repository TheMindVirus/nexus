using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.LEGO.Minifig;
using Cinemachine;

public class Ghost : MonoBehaviour
{
    public List<GameObject> PlayableNPCs = new List <GameObject>();

    private RaycastHit hit = new RaycastHit();
    private Ray ray = new Ray();
    private Transform HitNPC = null;
    private Transform CursedNPC = null;
    private Transform PossessedNPC = null;
    private Color CursedNPC_DefaultColour = new Color(0.0f, 0.0f, 0.0f, 0.0f);
    private Color CursedNPC_HighlightColour = new Color(1.0f, 0.0f, 0.0f, 1.0f);
    private Material[] TempMaterials = null;
    private Vector3 TempFacing = new Vector3(0.0f, 0.0f, 0.0f);
    bool possessed = false;

    private GameObject CameraActual = null;
    private CinemachineBrain CameraBrain = null;
    private CinemachineFreeLook CameraSettings = null;

    private float ViewX = 0.0f;
    private float ViewY = 0.0f;
    private float DirectionX = 0.0f;
    private float DirectionY = 0.0f;
    private bool KeySelect = false;
    private bool KeyBack = false;
    private bool KeyJump = false;

    public void SetViewX(float vx) { ViewX = vx; }
    public void SetViewY(float vy) { ViewY = vy; }
    public void SetDirectionX(float dx) { DirectionX = dx; }
    public void SetDirectionY(float dy) { DirectionY = dy; }
    public void SetKeySelect(float ax) { KeySelect = (ax > 0.0f); }
    public void SetKeyBack(float by) { KeyBack = (by > 0.0f); }
    public void SetKeyJump(float cz) { KeyJump = (cz > 0.0f); }

    void Start()
    {
        foreach (GameObject go in PlayableNPCs)
        {
            Debug.Log("[INFO]: " + go.GetComponents<MonoBehaviour>()[0]);
            go.GetComponents<MinifigController>()[0].inputEnabled = false;
        }
        CameraActual = GameObject.FindWithTag("Ghost");
        CameraBrain = Camera.main.GetComponent<CinemachineBrain>();
        CameraSettings = CameraActual.GetComponent<CinemachineFreeLook>();
        CameraBrain.enabled = false;
    }

    void Update()
    {
        ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f)); //Center Screen Crosshairs
        if (Physics.Raycast(ray, out hit))
        {
            //Debug.Log("[INFO]: " + hit.collider.transform.gameObject.name);
            HitNPC = hit.collider.transform;
        }

        if ((HitNPC != null) && (HitNPC.gameObject.GetComponents<MinifigController>().Length > 0) && (!possessed))
        {
            if (CursedNPC == null)
            {
                CursedNPC = HitNPC;
                TempMaterials = CursedNPC.gameObject.GetComponent<Renderer>().materials;
                if (TempMaterials.Length > 0)
                {
                    CursedNPC_DefaultColour = TempMaterials[0].GetColor("_Color");
                    CursedNPC.gameObject.GetComponent<Renderer>().materials[0].SetColor("_Color", CursedNPC_HighlightColour);
                }
            }
            else { CursedNPC = HitNPC; }
        }
        else if (CursedNPC != null)
        {
            TempMaterials = CursedNPC.gameObject.GetComponent<Renderer>().materials;
            if (TempMaterials.Length > 0)
            {
                CursedNPC.gameObject.GetComponent<Renderer>().materials[0].SetColor("_Color", CursedNPC_DefaultColour);
                CursedNPC_DefaultColour = new Color(0.0f, 0.0f, 0.0f, 0.0f);
            }
            CursedNPC = null;
        }

        if ((KeySelect || KeyJump) && (CursedNPC != null)) { possessed = true; }
        else if (KeyBack) { possessed = false; }

        if ((possessed) && (PossessedNPC == null))
        {
            CursedNPC.gameObject.GetComponent<Renderer>().materials[0].SetColor("_Color", CursedNPC_DefaultColour);
            CursedNPC_DefaultColour = new Color(0.0f, 0.0f, 0.0f, 0.0f);
            PossessedNPC = CursedNPC;
            //CursedNPC = null; //!!!BUG: Null Reference Exception, this also mangles PossessedNPC
            Debug.Log("[INFO]: Possessed: " + PossessedNPC.gameObject.name);
            PossessedNPC.gameObject.GetComponents<MinifigController>()[0].inputEnabled = true;
            CameraSettings.m_LookAt = PossessedNPC;
            CameraSettings.m_Follow = PossessedNPC;
            CameraBrain.enabled = true;
        }
        else if ((!possessed) && (PossessedNPC != null))
        {
            Debug.Log("[INFO]: Return to Ghost Mode");
            PossessedNPC.gameObject.GetComponents<MinifigController>()[0].inputEnabled = false;
            PossessedNPC = null;
            CameraBrain.enabled = false;
            CameraSettings.m_LookAt = null;
            CameraSettings.m_Follow = null;
            TempFacing = Camera.main.transform.eulerAngles;
            CameraActual.transform.eulerAngles = new Vector3(TempFacing.x, TempFacing.y, 0.0f);
            Camera.main.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        }

        if (possessed) //Player Mode
        {
            CameraSettings.m_XAxis.Value += ViewX;
            CameraSettings.m_YAxis.Value += ViewY;
            PossessedNPC.gameObject.GetComponents<MinifigController>()[0].SetHorizontal(DirectionX);
            PossessedNPC.gameObject.GetComponents<MinifigController>()[0].SetVertical(-DirectionY);
            PossessedNPC.gameObject.GetComponents<MinifigController>()[0].SetOrbital((KeyJump) ? 1.0f : 0.0f);
        }

        if (!possessed) //Ghost Mode
        {
            CameraActual.transform.Rotate(new Vector3(ViewY, 0, 0), Space.Self);
            CameraActual.transform.Rotate(new Vector3(0, ViewX, 0), Space.World);
            CameraActual.transform.Translate(new Vector3(DirectionX, 0, 0), Space.Self);
            CameraActual.transform.Translate(new Vector3(0, 0, -DirectionY), Space.Self);
        }
    }
}
