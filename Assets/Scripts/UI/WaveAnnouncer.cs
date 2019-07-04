using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WaveAnnouncer : MonoBehaviour
{
    public WaveController waveController;

    public TextMeshProUGUI waveAnnouncementText;

    Animator _animator;
    public List<string> announcements = new List<string>();
    const string topFormat = "<size=30>{0}</size>\r\n";

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void ChangeWave()
    {
        string chosenAnnouncement = announcements[Random.Range(0, announcements.Count)];
        string formatedAnnouncement = string.Format(topFormat, chosenAnnouncement);
        waveAnnouncementText.text = formatedAnnouncement + waveController.nextWave.name;
        _animator.SetTrigger("AnnounceWave");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
