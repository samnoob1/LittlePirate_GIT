using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlottingSystem : MonoBehaviour
{
    public float underWaterDrag = 3f;
    public float underWaterAngularDrag = 1f;

    public float airDrag = 0f;
    public float airAngularDrag = 0.05f;

    public float flottingPower = 15f;
    public float waterHeight = 0f;

    public Transform[] floatPoints;

    public int smallForce =  10;
    public float timeSmallForce = 2f;

    bool underWater;
    bool shipHaveToFloat = true;

    int floatPointUnderWater;

    Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(ApplySmallForceFromTop(timeSmallForce));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        floatPointUnderWater = 0;

        for (int i = 0; i < floatPoints.Length; i++) 
        {
            //Différence de hauteur (objet et eau)
            float difference = floatPoints[i].position.y - waterHeight;

            //Objet sous l'eau
            if (difference < 0)
            {
                //On applique la force vers le haut selon puissance flottement et distance sous l'eau (objet + profond = force + grande)
                rb.AddForceAtPosition((Vector3.up * flottingPower * Mathf.Abs(difference)), floatPoints[i].position, ForceMode.Force);

                floatPointUnderWater++;

                if (!underWater)
                {
                    underWater = true;
                    SwitchState(true);
                }
            }
        }
        
        if(underWater && (floatPointUnderWater == 0)) //Objet au dessus de l'eau
        {
            underWater = false;
            SwitchState(false);
        }
    }

    void SwitchState(bool _isUnderWater) 
    {
        //Fonction qui maj le drag et l'angularDrag du rb / Appelé lors d'un changement d'état

        if (_isUnderWater) 
        { 
            rb.drag = underWaterDrag;
            rb.angularDrag = underWaterAngularDrag;

        }
        else 
        {
            rb.drag = airDrag;
            rb.angularDrag = airAngularDrag;
        }
    }
    private IEnumerator ApplySmallForceFromTop(float _time) 
    {
        while (shipHaveToFloat)
        {
            yield return new WaitForSeconds(_time);
            flottingPower = 80;
            yield return new WaitForSeconds(_time);
            flottingPower = 75;
        }      
    }
}
