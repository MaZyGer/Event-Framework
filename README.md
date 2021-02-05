# Event-Framework

Event Framework is Event-Based-ScriptableObject Framework which allows to make fast and easy way to communicate between multiple components.

![Player](https://i.imgur.com/8Myjt8Z.png)

```CSharp
public class Player : MonoBehaviour
{
    // EventActionValue may get soon name change
    public EventActionValue<int> Mana = new EventActionValue<int>(100);
    
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
}
```
Update without problems your UI with the event listener component  
![Text Updater](https://i.imgur.com/pHWLKaz.png)
