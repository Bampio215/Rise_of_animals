using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ExpController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Exptxt;
    [SerializeField] private TextMeshProUGUI Leveltxt;
    public int CurrentEXP;
    public GameObject projectilePrefab;
    public int level = 1;
    public int stat = 0;
    public int exp;
    [SerializeField] private int TargetEXP;
    [SerializeField] private Image EXPprocessBar;
    void Start()
    {
        LoadPlayerData();
        TargetEXP = 100 * level;
        CurrentEXP = exp;
    }
    void Update()
    {
        Exptxt.text = CurrentEXP + " / " + TargetEXP;
        xpController();
    }
    public void xpController()
    {
        Leveltxt.text = "Level : " + level.ToString();
        EXPprocessBar.fillAmount = (float)CurrentEXP / (float)TargetEXP;
        if (CurrentEXP >= TargetEXP)
        {
            CurrentEXP = CurrentEXP - TargetEXP;
            TargetEXP += 100;
            level++;
            stat++;
            PlayerPrefs.SetInt("Level", level);
            PlayerPrefs.SetInt("AvailablePoints", stat);
            PlayerPrefs.SetInt("EXP", exp);
            PlayerPrefs.Save();
        }
        PlayerPrefs.SetInt("EXP", CurrentEXP);
        PlayerPrefs.Save();
    }
    void LoadPlayerData()
    {

        if (PlayerPrefs.HasKey("Level"))
        {
            level = PlayerPrefs.GetInt("Level");
        }
        if (PlayerPrefs.HasKey("AvailablePoints"))
        {
            stat = PlayerPrefs.GetInt("AvailablePoints");
        }
        if (PlayerPrefs.HasKey("EXP"))
        {
            exp = PlayerPrefs.GetInt("EXP");
        }

    }
}
