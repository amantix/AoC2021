namespace Day18;

public class Pair
{
    private Item? _left;
    private Item? _right;
    private int _length;

    public int GetMagnitude()
    {
        if (_left != null && _right != null)
        {
            return 3 * _left.Magnitude + 2 * _right.Magnitude;
        }
        return 0;
    }

    public override string ToString()
    {
        return $"[{_left},{_right}]";
    }

    public static Pair CreatePair(string str)
    {
        str = str[1..];
        var left = Item.CreateItem(str);
        str = str[(left.Length + 1)..];
        var right = Item.CreateItem(str);
        var pair = new Pair {_left = left, _right = right, _length = left.Length + right.Length + 3};
        return pair;
    }

    public static Pair operator +(Pair left, Pair right)
    {
        var leftItem = Item.CreateItem(left.ToString());
        var rightItem = Item.CreateItem(right.ToString());
        var pair = new Pair {_left = leftItem, _right = rightItem, _length = leftItem.Length + rightItem.Length + 3};
        return Reduce(pair);
    }

    public static Pair Reduce(Pair pairCopy)
    {
        var exploded = true;
        var splitted = true;
        while (exploded || splitted)
        {
            exploded = true;
            while (exploded)
            {
                exploded = Item.Explode(pairCopy);
            }

            splitted = Item.Split(pairCopy);
        }

        return pairCopy;
    }

    private class Item
    {
        public int Magnitude => _isRegularNumber ? _number!.Value : _pair!.GetMagnitude();
        public int Length { get; private set; }
        private bool _isRegularNumber;
        private int? _number;
        private Pair? _pair;

        public static bool Split(Pair pair)
        {
            var stack = new Stack<Item>();
            stack.Push(pair._right!);
            stack.Push(pair._left!);
            while (stack.Count > 0)
            {
                var item = stack.Pop();
                switch (item._isRegularNumber)
                {
                    case true when item._number >= 10:
                        item._isRegularNumber = false;
                        item._pair = new Pair
                        {
                            _left = new Item {_isRegularNumber = true, _number = item._number / 2},
                            _right = new Item {_isRegularNumber = true, _number = item._number / 2 + item._number % 2}
                        };
                        item.Length = 5;
                        return true;
                    case false:
                        stack.Push(item._pair!._right!);
                        stack.Push(item._pair!._left!);
                        break;
                }
            }

            return false;
        }

        public static bool Explode(Pair pair)
        {
            var exploded = false;
            var queue = new Queue<(Item item, int level, Item? left, Item? right)>();
            queue.Enqueue((pair._left!, 1, null, pair._right));
            queue.Enqueue((pair._right!, 1, pair._left, null));
            while (queue.Count > 0)
            {
                var (item, level, left, right) = queue.Dequeue();
                if (level >= 4
                    && !item._isRegularNumber
                    && item._pair!._left!._isRegularNumber
                    && item._pair!._right!._isRegularNumber)
                {
                    if (left != null)
                    {
                        while (!left!._isRegularNumber)
                        {
                            left = left._pair!._right;
                        }

                        left._number += item._pair._left._number;
                    }

                    if (right != null)
                    {
                        while (!right!._isRegularNumber)
                        {
                            right = right._pair!._left;
                        }

                        right._number += item._pair._right._number;
                    }

                    item._isRegularNumber = true;
                    item._number = 0;
                    item._pair = null;
                    item.Length = 3;
                    exploded = true;
                    continue;
                }

                if (!item._isRegularNumber && !item._pair!._left!._isRegularNumber)
                {
                    queue.Enqueue((item._pair._left, level + 1, left, item._pair._right));
                }

                if (!item._isRegularNumber && !item._pair!._right!._isRegularNumber)
                {
                    queue.Enqueue((item._pair._right, level + 1, item._pair._left, right));
                }
            }

            return exploded;
        }

        public override string ToString()
        {
            return _isRegularNumber
                ? _number.ToString()!
                : _pair!.ToString();
        }

        public static Item CreateItem(string str)
        {
            if (char.IsDigit(str[0]))
            {
                return new Item {_number = str[0] - '0', _isRegularNumber = true, Length = 1};
            }

            var pair = CreatePair(str);
            return new Item {_number = null, _isRegularNumber = false, _pair = pair, Length = pair._length};
        }
    }
}