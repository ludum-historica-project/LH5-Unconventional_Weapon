using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerUI : MonoBehaviour
{
    public Image healthBar;
    public Image dustBar;
    public PlayerHealth playerHealth;
    public DustController dustController;
    public void UpdateHealth()
    {
        healthBar.fillAmount = playerHealth.currentHealth / playerHealth.maxHealth;
    }

    public void UpdateDust()
    {
        dustBar.fillAmount = dustController.dustCount / dustController.maxDust;
    }
}
