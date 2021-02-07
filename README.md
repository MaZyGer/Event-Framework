Still in development. Name or other things can get changed.

# Event-Framework

Event Framework is Event-Based-ScriptableObject Framework which allows to make fast and easy way to communicate between multiple components.

![Player](https://i.imgur.com/zq2gtTs.png)  

In editmode  

![Player](https://i.imgur.com/igJzaFq.png)  

Notice how it changed the name in playmode. Runtime value will be the copy of the initial value every time you start the playmode. You can edit this and after the playmode it will reset to initial value. Also raise button will be available in the playmode.

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
