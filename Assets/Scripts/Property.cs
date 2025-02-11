using UnityEngine;

public class Property : MonoBehaviour
{
    public int Level { get; private set; }
    public int AvailablePoints { get; private set; }
    public int Strength { get; private set; }
    public int Speed { get; private set; }
    public int Health { get; private set; }
    public int Cooldown { get; private set; }
    public int Hard { get; private set; }
    public int HardMax { get; private set; }
    public int EXP { get; private set; }

    void Start()
    {
        // Tải trạng thái khi bắt đầu
        LoadState();
        SaveState();
    }

    // Dữ liệu sẽ được tải trong phương thức LoadState()

    // Tăng level
    public void LevelUp()
    {
        if (Hard < HardMax && Hard < 2)
        {
            Hard++;
            SaveState();
        }
    }

    // Giảm level
    public void LevelDown()
    {
        if (Hard > 0)
        {
            Hard--;
            SaveState();
        }
    }

    // Cập nhật chỉ số nhân vật
    public bool UpdateStat(string statName, int value)
    {
        if (value > 0 && AvailablePoints >= value)
        {
            switch (statName.ToLower())
            {
                case "strength":
                    Strength += value;
                    break;
                case "speed":
                    Speed += value;
                    break;
                case "health":
                    Health += value;
                    break;
                case "cooldown":
                    Cooldown += value;
                    break;
                default:
                    return false;
            }
            AvailablePoints -= value;
            SaveState(); // Lưu lại trạng thái sau khi thay đổi
            return true;
        }
        else if (value < 0)
        {
            switch (statName.ToLower())
            {
                case "strength":
                    if (Strength + value >= 0) Strength += value; else return false;
                    break;
                case "speed":
                    if (Speed + value >= 0) Speed += value; else return false;
                    break;
                case "health":
                    if (Health + value >= 0) Health += value; else return false;
                    break;
                case "cooldown":
                    if (Cooldown + value >= 0) Cooldown += value; else return false;
                    break;
                default:
                    return false;
            }
            AvailablePoints -= value;
            SaveState(); // Lưu lại trạng thái sau khi thay đổi
            return true;
        }
        return false; // Trả về false nếu không hợp lệ
    }

    // Lưu trạng thái vào PlayerPrefs
    public void SaveState()
    {
        PlayerPrefs.SetInt("Level", Level);
        PlayerPrefs.SetInt("AvailablePoints", AvailablePoints);
        PlayerPrefs.SetInt("Strength", Strength);
        PlayerPrefs.SetInt("Speed", Speed);
        PlayerPrefs.SetInt("Health", Health);
        PlayerPrefs.SetInt("Cooldown", Cooldown);
        PlayerPrefs.SetInt("Hard", Hard);
        PlayerPrefs.SetInt("HardMax", HardMax);
        PlayerPrefs.SetInt("EXP", EXP);

        PlayerPrefs.Save();

    }

    // Tải trạng thái từ PlayerPrefs
    public void LoadState()
    {
        if (PlayerPrefs.HasKey("Level"))
        {
            Level = PlayerPrefs.GetInt("Level");
            AvailablePoints = PlayerPrefs.GetInt("AvailablePoints");
            Strength = PlayerPrefs.GetInt("Strength");
            Speed = PlayerPrefs.GetInt("Speed");
            Health = PlayerPrefs.GetInt("Health");
            Cooldown = PlayerPrefs.GetInt("Cooldown");
            Hard = PlayerPrefs.GetInt("Hard");
            HardMax = PlayerPrefs.GetInt("HardMax");
            EXP = PlayerPrefs.GetInt("EXP");

            Debug.Log("Trạng thái nhân vật đã được tải từ PlayerPrefs.");
        }
        else
        {
            Level = 1;
            AvailablePoints = 0;
            Strength = 1;
            Speed = 20;
            Health = 10;
            Cooldown = 0;
            Hard = 0;
            HardMax = 0;
            EXP = 0;

            Debug.Log("Không tìm thấy dữ liệu trạng thái, sử dụng trạng thái mặc định.");
        }
    }
}
