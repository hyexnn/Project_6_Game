using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class SGProjectileGroup : MonoBehaviour
{
    public GameObject centerBullet;     //중심점 총알

    public int numOfTentacles = 3;
    public int numOfBulletsForOnTentales = 3;
    public float distanceBetweenBullets = 0.5f;
    public bool enableCenterBullet = true;

    public SGUtil.AXIS m_axisMove = SGUtil.AXIS.X_AND_Y;
    public float m_rotationSpeed = 90.0f;

    private Transform rootTransfrom;        //중심점 총알 위치값
    private void Awake()
    {
        rootTransfrom = new GameObject("Root").GetComponent<Transform>();
        rootTransfrom.SetParent(transform, false);

        if (numOfTentacles<2)
        {
            return;
        }
        float addAngle = 360.0f / numOfTentacles;
        for (int i = 0; i < numOfTentacles; i++)
        {
            Quaternion quat = Quaternion.identity;
            switch (m_axisMove)
            {
                case SGUtil.AXIS.X_AND_Y:
                    quat = Quaternion.Euler(new Vector3(0.0f, 0.0f, addAngle * i));
                    break;
                case SGUtil.AXIS.X_AND_Z:
                    quat = Quaternion.Euler(new Vector3(0.0f, addAngle * i, 0.0f));
                    break;
                default:
                    break;
            }
            for (int k = 0; k < numOfBulletsForOnTentales; k++)
            {
                var transBullet = ((GameObject)Instantiate(centerBullet, rootTransfrom)).GetComponent<Transform>();
                switch (m_axisMove)
                {
                    case SGUtil.AXIS.X_AND_Y:
                        transBullet.position += (quat * Vector3.up * ((k + 1) * distanceBetweenBullets));
                        break;
                    case SGUtil.AXIS.X_AND_Z:
                        transBullet.position += (quat * Vector3.forward * ((k + 1) * distanceBetweenBullets));
                        break;
                    default:
                        break;
                }
            }
        }
    }
    public void UpdateRotate()
    {
        switch (m_axisMove) 
        {
            case SGUtil.AXIS.X_AND_Y:
                rootTransfrom.eulerAngles += new Vector3(0.0f, 0.0f, m_rotationSpeed * SGTimer.Instance.deltaTime);
                break;
            case SGUtil.AXIS.X_AND_Z:
                rootTransfrom.eulerAngles += new Vector3(0.0f, -m_rotationSpeed * SGTimer.Instance.deltaTime, 0.0f);
                break;
            default:
                break;
        }
    }
}
