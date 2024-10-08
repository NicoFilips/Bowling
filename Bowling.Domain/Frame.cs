namespace Bowling.Domain;

public class Frame
{
    public List<int> Throws { get; } = [];
    public int Score => Throws.Sum();
    public int ScoreWithBonus { get; set; }
    public bool IsFinalFrame { get; set; }

    public bool IsComplete => IsFinalFrame ? Throws.Count == 3 || (Throws.Count == 2 && !IsStrike && !IsSpare) : Throws.Count == 2 || IsStrike;

    public void AddThrow(int pins)
    {
        if (!IsFinalFrame && Throws.Count >= 2)
            throw new InvalidOperationException("Frame ist bereits abgeschlossen.");

        if (IsFinalFrame && Throws.Count >= 3)
            throw new InvalidOperationException("Finaler Frame ist bereits abgeschlossen.");

        if (!IsFinalFrame && Throws.Sum() + pins > 10)
            throw new InvalidOperationException("Die Summe der Würfe in einem Frame darf nicht größer als 10 sein.");

        Throws.Add(pins);
    }

    public bool IsStrike => Throws.Count > 0 && Throws[0] == 10;
    public bool IsSpare => Throws.Count == 2 && Throws[0] + Throws[1] == 10;
}
