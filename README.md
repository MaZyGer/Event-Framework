Still in development. Name or other things can get changed.

# Event-Framework

Event Framework is Event-Based-ScriptableObject Framework which allows to make fast and easy way to communicate between multiple components.

![Player](https://i.imgur.com/PWryxxJ.png)  

You can use ScriptableObject directly without generic type like EventActionInt and handle changes at your own. Or you use EventActionValue<int> which will add values like in this example (above image).

```CSharp
public class Player : MonoBehaviour
{
    public EventActionValue<int> Mana;
    public EventActionValue<Player> TargetSelection;
    
    void Start()
    {
        // Fire just the event to update the value
        Mana.Raise()
    }
    
    void UseMana()
    {
        // Will also fire event while changing the value
        Mana.Value -= 10;
    }
    
    void SelectSelfAsTarget()
    {
        TargetSelection.Value = this;
    }
}
```
Update without problems your UI with the event listener component. Also the ConverToEvent is an helper which includes conversation between different types like int to float.
![Text Updater](https://i.imgur.com/8BSbsgJ.png)
