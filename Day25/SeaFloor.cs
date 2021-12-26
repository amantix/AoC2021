namespace Day25;

public class SeaFloor
{
    private char[][]? _state;
    private char[][]? _nextState;
    private bool _isStable;

    public int Iteration { get; private set; }

    public static SeaFloor ReadFromFile(string fileName)
    {
        var state = File.ReadAllLines(fileName).Select(line => line.ToArray()).ToArray();
        var nextState = new char[state.Length][];
        for (var i = 0; i < state.Length; i++)
        {
            nextState[i] = new char[state[i].Length];
        }
        var seaFloor = new SeaFloor
        {
            _isStable = false,
            _state = state,
            _nextState = nextState
        };
        return seaFloor;
    }

    public void SimulateUntilStable()
    {
        while (NextStep()) {}
    }

    public override string ToString()
    {
        return string.Join('\n', (_state ?? Array.Empty<char[]>()).Select(line => string.Join("", line)));
    }

    private bool NextStep()
    {
        if (_isStable || _state == null || _nextState==null)
        {
            return false;
        }

        CopyState(_state, _nextState);
        //move east
        var moved = false;
        for (var i = 0; i < _state.Length; i++)
        {
            for (var j = 0; j < _state[i].Length; j++)
            {
                if (_state[i][j] == '>' && _state[i][(j + 1) % _state[i].Length] == '.')
                {
                    moved = true;
                    _nextState[i][j] = '.';
                    _nextState[i][(j + 1) % _state[i].Length] = '>';
                }
            }
        }

        CopyState(_nextState, _state);
        //move south
        for (var i = 0; i < _state.Length; i++)
        {
            for (var j = 0; j < _state[i].Length; j++)
            {
                if (_state[i][j] == 'v' && _state[(i + 1) % _state.Length][j] == '.')
                {
                    moved = true;
                    _nextState[i][j] = '.';
                    _nextState[(i + 1) % _state.Length][j] = 'v';
                }
            }
        }

        (_state, _nextState) = (_nextState, _state);
        Iteration++;
        return moved;
    }

    private static void CopyState(char[][] from, char[][] to)
    {
        for (var i = 0; i < from.Length; i++)
        {
            Array.Copy(from[i], to[i], @from[i].Length);
        }
    }
}