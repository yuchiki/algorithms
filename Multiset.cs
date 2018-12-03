namespace Collections {
    using System.Collections.Generic;
    using System.Collections;
    using System.Linq;
    using System;

    class Multiset<T> : ICollection<T> {
        // TODO: 順番がグチャグチャなので整理する
        Dictionary<T, int> occurrence;

        public Multiset(Multiset<T> source) : this(source.occurrence) {}
        public Multiset(Dictionary<T, int> source) {
            occurrence = new Dictionary<T, int>(source);
            Validate();
        }

        public Multiset(IEnumerable<T> source) {
            occurrence = new Dictionary<T, int>();
            AddRange(source);
        }

        public void Add(T data) {
            if (!occurrence.ContainsKey(data)) {
                occurrence[data] = 1;
                return;
            }
            occurrence[data]++;
        }

        public void Clear() => occurrence.Clear();

        public void Add(T key, int value) {
            if (value <= 0) throw new ArgumentException();
            if (!occurrence.ContainsKey(key)) {
                occurrence[key] = value;
                return;
            }
            occurrence[key] += value;
        }

        public void Add(KeyValuePair<T, int> pair) => Add(pair.Key, pair.Value);

        public void AddRange(IEnumerable<T> source) =>
        source.ToList().ForEach(Add);

        public bool Remove(T data) {
            if (!occurrence.ContainsKey(data)) return false;
            occurrence[data]--;
            if (occurrence[data] == 0) occurrence.Remove(data);
            return true;
        }

        // IDictionary
        public int this [T key] {
            get => occurrence.ContainsKey(key) ? occurrence[key] : 0;
            set {
                if (value <= 0) throw new ArgumentException();
                occurrence[key] = value;
            }
        }

        public ICollection<T> Keys => occurrence.Keys;
        public ICollection<int> Values => occurrence.Values;

        public bool ContainsKey(T key) => occurrence.ContainsKey(key);
        public bool Contains(T key) => ContainsKey(key);
        public bool TryGetValue(T key, out int value) {
            value = this [key];
            return true;
        }
        // /IDictionary

        public int Count => occurrence.Values.Sum();
        public bool IsReadOnly => false;
        public void CopyTo(T[] array, int index) {
            foreach (var pair in occurrence) {
                for (int i = 0; i < pair.Value; i++) {
                    array[index] = pair.Key;
                    index++;
                }
            }
        }

        public IEnumerator<T> GetEnumerator() {
            foreach (var pair in occurrence)
                for (int i = 0; i < pair.Value; i++) yield return pair.Key;

        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        public void Validate() {
            if (occurrence.Values.Any(x => x <= 0)) throw new ArgumentException();
        }
    }
}
