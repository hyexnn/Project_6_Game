using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SGSprialShot : SGBaseShot
{
    public float startAngle = 180;              //시작 각도
    public float shiftAngle = 10f;              //움직이는 각도
    public float betweenDelay = 0.2f;
    private int nowIndex;
    private float delayTimer;

    public override void Shot()             //Shot 필수 구현 함수 (SGBaseShot
    {
        if (projectileNum <= 0 || projectileSpeed <= 0)     //옵션값 검사
        {
            return;
        }
        if (_shooting)
        {
            return;
        }

        _shooting = true;
        nowIndex = 0;
        delayTimer = 0;
    }
    protected virtual void Update()
    {
        if (_shooting == false)
        {
            return;
        }
        delayTimer -= SGTimer.Instance.deltaTime;
        while (delayTimer <= 0)
        {
            SGProjectile projectile = GetProjectile(transform.position);
            if (projectile == null) //총알이 없을경우 종료
            {
                FinishedShot();
                return;
            }
            float angle = startAngle + (shiftAngle * nowIndex);
            ShotProjectile(projectile, projectileSpeed, angle);
            projectile.UpdateMove(-delayTimer);

            nowIndex++;
            FiredShot();

            if (nowIndex >= projectileNum)
            {
                FinishedShot();
                return;
            }
            delayTimer += betweenDelay;
        }
    }
}
