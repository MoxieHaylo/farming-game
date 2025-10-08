using UnityEngine;

public class Plant0 : MonoBehaviour, IPlant
{
    public int datePlanted;
    private IDayProvider dayProvider;
    private int lastLoggedDay = -1;

    [Header("Growth Sprites")]
    public Sprite seedSprite;
    public Sprite sproutSprite;
    public Sprite smallPlantSprite;
    public Sprite fullPlantSprite;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        dayProvider = FindFirstObjectByType<DayTracker>();
        if (dayProvider != null)
        {
            SetDay(dayProvider.Day);
        }
        else
        {
            Debug.LogError("No IDayProvider (DayTracker) found in scene!");
        }

        UpdateGrowthSprite(0); // Show starting sprite immediately
    }

    void Update()
    {
        CheckGrowth();
    }

    public void SetDay(int day)
    {
        datePlanted = day;
        Debug.Log("Planted on day: " + datePlanted);
    }

    void CheckGrowth()
    {
        if (dayProvider == null) return;

        int currentDay = dayProvider.Day;
        int daysGrowing = currentDay - datePlanted;

        if (currentDay != lastLoggedDay)
        {
            Debug.Log("Has been growing for " + daysGrowing + " day(s)");
            lastLoggedDay = currentDay;
            UpdateGrowthSprite(daysGrowing);
        }
    }

    void UpdateGrowthSprite(int days)
    {
        if (days < 1)
            spriteRenderer.sprite = seedSprite;
        else if (days < 3)
            spriteRenderer.sprite = sproutSprite;
        else if (days < 5)
            spriteRenderer.sprite = smallPlantSprite;
        else
            spriteRenderer.sprite = fullPlantSprite;
    }
}
