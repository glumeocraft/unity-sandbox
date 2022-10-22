namespace BattleCalculator
{
    public interface IUnit
    {
        int ArmorValue { get; set; }
        int AttackValue { get; set; }
        Program.Moves CurrentMove { get; set; }
        int DefenseValue { get; set; }
        int Direction { get; set; }
        string Egyeb { get; set; }
        int Hp { get; set; }
        bool NewEntry { get; set; }
        int SpecialValue { get; set; }
        int UserId { get; set; }
    }
}