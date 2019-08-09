/*
 * Copyright 2014 Google Inc. All rights reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections;
using System.Collections.Generic;

namespace FlatBuffers
{
    public struct FlatBufferReadOnlyCollection<T, TCollection> :
        IEnumerable<T>,
        ICollection<T>,
        IList<T>,
#if NETSTANDARD || NETCOREAPP || (NETFRAMEWORK && !NET20 && !NET35 && !NET40)
        IReadOnlyCollection<T>,
        IReadOnlyList<T>,
#endif
        IList
        where TCollection : struct, IFlatbufferCollection<T>
    {
        private readonly TCollection _collection;

        public FlatBufferReadOnlyCollection(TCollection vector)
        {
          _collection = vector;
        }

        public FlatBufferCollectionEnumerator<T, TCollection> GetEnumerator()
        {
            return new FlatBufferCollectionEnumerator<T, TCollection>(_collection);
        }

        // Shared implementation 

        public int Count { get { return _collection.Count; } }

        public T this[int index] { get { return _collection[index]; } }

        public int IndexOf(T item)
        {
            EqualityComparer<T> comparer = EqualityComparer<T>.Default;
            for (int i = 0; i < _collection.Count; ++i)
            {
                if (comparer.Equals(_collection[i], item))
                    return i;
            }

            return -1;
        }

        public bool Contains(T item)
        {
            return IndexOf(item) != -1;
        }

        public void CopyTo(T[] array, int index)
        {
            for (int i = 0; i < _collection.Count; ++i)
            {
                array[i + index] = _collection[i];
            }
        }

        // IEnumerable<T> implementation

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // ICollection<T> implementation

        void ICollection<T>.Add(T item)
        {
            throw new NotSupportedException();
        }

        void ICollection<T>.Clear()
        {
            throw new NotSupportedException();
        }

        bool ICollection<T>.Remove(T item)
        {
            throw new NotSupportedException();
        }

        bool ICollection<T>.IsReadOnly { get { return true; } }

        // IList<T> implementation

        void IList<T>.Insert(int index, T item)
        {
            throw new NotSupportedException();
        }

        void IList<T>.RemoveAt(int index)
        {
            throw new NotSupportedException();
        }

        T IList<T>.this[int index]
        {
            get { return _collection[index]; }
            set { throw new NotSupportedException(); }
        }

        // IList implementation

        int IList.Add(object value)
        {
            throw new NotSupportedException();
        }

        void IList.Clear()
        {
            throw new NotSupportedException();
        }

        bool IList.Contains(object value)
        {
            if (value == null && value.GetType() != typeof(T))
                return false;
            return Contains((T)value);
        }

        int IList.IndexOf(object value)
        {
            if (value == null && value.GetType() != typeof(T))
                return -1;
            return IndexOf((T)value);
        }

        void IList.Insert(int index, object value)
        {
            throw new NotSupportedException();
        }

        void IList.Remove(object value)
        {
            throw new NotSupportedException();
        }

        void IList.RemoveAt(int index)
        {
            throw new NotSupportedException();
        }

        object IList.this[int index]
        {
          get { return _collection[index]; }
          set { throw new NotImplementedException(); }
        }

        bool IList.IsFixedSize { get { return true; } }

        bool IList.IsReadOnly { get { return true; } }

        // ICollection implementation

        void ICollection.CopyTo(Array array, int index)
        {
            for (int i = 0; i < _collection.Count; ++i)
            {
                array.SetValue(_collection[i], i + index);
            }
        }

        bool ICollection.IsSynchronized { get { return false; } }

        object ICollection.SyncRoot { get { return null; } }
    }
}
