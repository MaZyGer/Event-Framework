Still in development. Name or other things can get changed.

# Event-Framework

Event Framework is Event-Based-ScriptableObject Framework which allows to make fast and easy way to communicate between multiple components.

![Player](https://i.imgur.com/zq2gtTs.png)

```CSharp
public class Player : MonoBehaviour
{
    public EventActionInt Mana;
    public EventActionPlayer TargetSelection;
    
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
Update without problems your UI with the event listener component  
![Text Updater](https://i.imgur.com/pHWLKaz.png)
