using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : GenericSingletone<GameManager>
{
    public int playerScore = 0;
    public void inscreaseScore(int amount)          //함수를 통해서 증가시켜준다.
    {
        playerScore += amount;
    }
}
