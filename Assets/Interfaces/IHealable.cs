public interface IHealable 
{
    float MaxHealth { get; set; }
    float CurrentHealth { get; set; }

    void Heal(float amount) { }
}
