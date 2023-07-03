using System.Collections.ObjectModel;

namespace EDHPlayerTracker;

public partial class MainPage : ContentPage
{
    public ObservableCollection<Player> Players { get; set; } = new ObservableCollection<Player>()
    {
        new Player(),
        new Player(),
        new Player(),
        new Player(),
    };

    public MainPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    private void SetActive(int i)
    {
        for (int j = 0; j < 4; j++)
        {
            if (i == j)
            {
                Players[j].Activate();
            }
            else
            {
                Players[j].Deactivate();
            }
        }
    }

    private void Timer_Clicked(object sender, EventArgs e)
    {
        var i = int.Parse((sender as Button).AutomationId);
        SetActive(i);
    }

    private void HpUp_Clicked(object sender, EventArgs e)
    {
        var i = int.Parse((sender as Button).AutomationId);
        ++Players[i].Health;
    }

    private void HpDown_Clicked(object sender, EventArgs e)
    {
        var i = int.Parse((sender as Button).AutomationId);
        --Players[i].Health;
    }
}

