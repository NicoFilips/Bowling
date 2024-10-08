using System.Text;

namespace Bowling.Domain;

public class BowlingGame : IDisposable
{
    private readonly List<Frame> _frames = [];
    private int _currentFrameIndex = 0;
    private bool _gameOver = false;
    private bool _disposed = false;

    public BowlingGame()
    {
        _frames.AddRange(Enumerable.Range(0, 10).Select(i => new Frame { IsFinalFrame = i == 9 }));
    }

    public string AddThrow(int pins)
    {
        if (_gameOver)
            throw new InvalidOperationException("Das Spiel ist bereits beendet.");
        if (_currentFrameIndex < 0 || _currentFrameIndex >= _frames.Count)
            throw new ArgumentOutOfRangeException(nameof(_currentFrameIndex), "Frame-Index ist außerhalb des gültigen Bereichs.");

        var currentFrame = _frames[_currentFrameIndex];
        currentFrame.AddThrow(pins);

        if (_currentFrameIndex > 0)
            UpdatePreviousFrameBonuses(pins);

        string scoreBoard = GetScoreBoard();

        if (_currentFrameIndex == 9 && currentFrame.IsComplete)
            _gameOver = true;
        if (currentFrame.IsComplete && _currentFrameIndex < 9)
            _currentFrameIndex++;

        return scoreBoard;
    }

    private void UpdatePreviousFrameBonuses(int pins)
    {
        var previousFrame = _frames[_currentFrameIndex - 1];
        if (previousFrame.IsStrike)
        {
            previousFrame.ScoreWithBonus += pins;
            if (_currentFrameIndex > 1 && _frames[_currentFrameIndex - 2].IsStrike)
            {
                _frames[_currentFrameIndex - 2].ScoreWithBonus += pins;
            }
        }
        else if (previousFrame.IsSpare && previousFrame.Throws.Count == 2)
        {
            previousFrame.ScoreWithBonus += pins;
        }
    }

    private string GetScoreBoard()
    {
        var scoreBoard = new StringBuilder();
        int runningTotal = 0;

        int frameNumberToDisplay = _currentFrameIndex + 1;
        if (_currentFrameIndex == 9 && _frames[_currentFrameIndex].Throws.Count == 0)
            frameNumberToDisplay = 10;

        scoreBoard.Append($"{frameNumberToDisplay} | ");

        var history = new StringBuilder();
        for (int i = 0; i <= _currentFrameIndex; i++)
        {
            var frame = _frames[i];
            if (frame.Throws.Count > 0)
            {
                int frameScore = frame.Score + frame.ScoreWithBonus;
                runningTotal += frameScore;
                history.Append($"[({string.Join(",", frame.Throws)}){runningTotal}]");

                if (i < _currentFrameIndex)
                    history.Append(", ");
            }
        }

        scoreBoard.Append($"{history}");
        return scoreBoard.ToString().Trim();
    }

    public void Dispose()
    {
        if (_disposed)
            return;
        _frames.Clear();
        _disposed = true;
    }
}
