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

namespace FlatBuffers
{
    public struct FlatBufferVectorOfTable<T> : IFlatbufferVector, IFlatbufferCollection<T>
        where T : struct, IFlatbufferObject<T>
    {
        private ByteBuffer _bb;
        private int _start_pos;
        private int _count;

        public void __init(int i, ByteBuffer bb)
        {
            _bb = bb;
            _start_pos = i + sizeof(int);
            _count = bb.GetInt(i) / sizeof(int);
        }

        public ByteBuffer ByteBuffer { get { return _bb; } }

        public int Count { get { return _count; } }

        public T this[int index]
        {
            get
            {
                int offset = _start_pos + sizeof(int) * index;
                offset += _bb.GetInt(offset);
                T t = default(T);
                t.__init(offset, _bb);
                return t;
            }
        }

        public VectorOffset Clone(FlatBufferBuilder builder)
        {
            using (FlatBufferBuilder.CachedArray cache = builder.GetCachedArray(_count))
            {
                int[] offsets = cache.Array;

                for (int i = _count - 1; i >= 0; --i)
                {
                    int element_offset = _start_pos + i * sizeof(int); // offset to the vector element
                    element_offset += _bb.GetInt(element_offset); // offset to the table

                    T table = default(T);
                    table.__init(element_offset, _bb);

                    offsets[i] = table.Clone(builder).Value;
                }

                builder.StartVector(sizeof(int), _count, sizeof(int));
                for (int i = _count - 1; i >= 0; --i)
                {
                    builder.AddOffset(offsets[i]);
                }
                return builder.EndVector();
            }
        }
    }

    public struct FlatBufferVectorOfString : IFlatbufferVector, IFlatbufferCollection<string>
    {
        private ByteBuffer _bb;
        private int _start_pos;
        private int _count;

        public void __init(int i, ByteBuffer bb)
        {
            _bb = bb;
            _start_pos = i + sizeof(int);
            _count = bb.GetInt(i) / sizeof(int);
        }

        public ByteBuffer ByteBuffer { get { return _bb; } }

        public int Count { get { return _count; } }

        public string this[int index]
        {
            get
            {
                int o = _start_pos + sizeof(int) * index;
                o += _bb.GetInt(o);
                return _bb.GetStringUTF8(o + sizeof(int), _bb.GetInt(o));
            }
        }

        public VectorOffset Clone(FlatBufferBuilder builder)
        {
            using (FlatBufferBuilder.CachedArray cache = builder.GetCachedArray(_count))
            {
                int[] offsets = cache.Array;

                for (int i = _count - 1; i >= 0; --i)
                {
                    int element_offset = _start_pos + i * sizeof(int); // offset to the vector element
                    element_offset += _bb.GetInt(element_offset); // offset to the string

                    offsets[i] = builder.CloneString(_bb, element_offset).Value;
                }

                builder.StartVector(sizeof(int), _count, sizeof(int));
                for (int i = _count - 1; i >= 0; --i)
                {
                   builder.AddOffset(offsets[i]);
                }
                return builder.EndVector();
            }
        }
    }

    public struct FlatBufferVectorOfBool : IFlatbufferVector, IFlatbufferCollection<bool>
    {
        private ByteBuffer _bb;
        private int _start_pos;
        private int _count;

        public void __init(int i, ByteBuffer bb)
        {
            _bb = bb;
            _start_pos = i + sizeof(int);
            _count = bb.GetInt(i) / sizeof(byte);
        }

        public ByteBuffer ByteBuffer { get { return _bb; } }

        public int Count { get { return _count; } }

        public bool this[int index] { get { return _bb.Get(_start_pos + sizeof(byte) * index) != 0; } }

        public VectorOffset Clone(FlatBufferBuilder builder)
        {
            return builder.CloneVectorData(_bb, _start_pos, sizeof(byte), _count, sizeof(byte));
        }
    }

    public struct FlatBufferVectorOfSbyte : IFlatbufferVector, IFlatbufferCollection<sbyte>
    {
        private ByteBuffer _bb;
        private int _start_pos;
        private int _count;

        public void __init(int i, ByteBuffer bb)
        {
            _bb = bb;
            _start_pos = i + sizeof(int);
            _count = bb.GetInt(i) / sizeof(sbyte);
        }

        public ByteBuffer ByteBuffer { get { return _bb; } }

        public int Count { get { return _count; } }

        public sbyte this[int index] { get { return _bb.GetSbyte(_start_pos + sizeof(sbyte) * index); } }

        public VectorOffset Clone(FlatBufferBuilder builder)
        {
            return builder.CloneVectorData(_bb, _start_pos, sizeof(sbyte), _count, sizeof(sbyte));
        }
    }

    public struct FlatBufferVectorOfByte : IFlatbufferVector, IFlatbufferCollection<byte>
    {
        private ByteBuffer _bb;
        private int _start_pos;
        private int _count;

        public void __init(int i, ByteBuffer bb)
        {
            _bb = bb;
            _start_pos = i + sizeof(int);
            _count = bb.GetInt(i) / sizeof(byte);
        }

        public ByteBuffer ByteBuffer { get { return _bb; } }

        public int Count { get { return _count; } }

        public byte this[int index] { get { return _bb.Get(_start_pos + sizeof(byte) * index); } }

        public VectorOffset Clone(FlatBufferBuilder builder)
        {
            return builder.CloneVectorData(_bb, _start_pos, sizeof(byte), _count, sizeof(byte));
        }
    }

    public struct FlatBufferVectorOfShort : IFlatbufferVector, IFlatbufferCollection<short>
    {
        private ByteBuffer _bb;
        private int _start_pos;
        private int _count;

        public void __init(int i, ByteBuffer bb)
        {
            _bb = bb;
            _start_pos = i + sizeof(int);
            _count = bb.GetInt(i) / sizeof(short);
        }

        public ByteBuffer ByteBuffer { get { return _bb; } }

        public int Count { get { return _count; } }

        public short this[int index] { get { return _bb.GetShort(_start_pos + sizeof(short) * index); } }

        public VectorOffset Clone(FlatBufferBuilder builder)
        {
            return builder.CloneVectorData(_bb, _start_pos, sizeof(short), _count, sizeof(short));
        }
    }

    public struct FlatBufferVectorOfUshort : IFlatbufferVector, IFlatbufferCollection<ushort>
    {
        private ByteBuffer _bb;
        private int _start_pos;
        private int _count;

        public void __init(int i, ByteBuffer bb)
        {
            _bb = bb;
            _start_pos = i + sizeof(int);
            _count = bb.GetInt(i) / sizeof(ushort);
        }
        public ByteBuffer ByteBuffer { get { return _bb; } }

        public int Count { get { return _count; } }

        public ushort this[int index] { get { return _bb.GetUshort(_start_pos + sizeof(ushort) * index); } }

        public VectorOffset Clone(FlatBufferBuilder builder)
        {
            return builder.CloneVectorData(_bb, _start_pos, sizeof(ushort), _count, sizeof(ushort));
        }
    }

    public struct FlatBufferVectorOfInt : IFlatbufferVector, IFlatbufferCollection<int>
    {
        private ByteBuffer _bb;
        private int _start_pos;
        private int _count;

        public void __init(int i, ByteBuffer bb)
        {
            _bb = bb;
            _start_pos = i + sizeof(int);
            _count = bb.GetInt(i) / sizeof(int);
        }
        public ByteBuffer ByteBuffer { get { return _bb; } }

        public int Count { get { return _count; } }

        public int this[int index] { get { return _bb.GetInt(_start_pos + sizeof(int) * index); } }

        public VectorOffset Clone(FlatBufferBuilder builder)
        {
            return builder.CloneVectorData(_bb, _start_pos, sizeof(int), _count, sizeof(int));
        }
    }

    public struct FlatBufferVectorOfUint : IFlatbufferVector, IFlatbufferCollection<uint>
    {
        private ByteBuffer _bb;
        private int _start_pos;
        private int _count;

        public void __init(int i, ByteBuffer bb)
        {
            _bb = bb;
            _start_pos = i + sizeof(int);
            _count = bb.GetInt(i) / sizeof(uint);
        }
        public ByteBuffer ByteBuffer { get { return _bb; } }

        public int Count { get { return _count; } }

        public uint this[int index] { get { return _bb.GetUint(_start_pos + sizeof(uint) * index); } }

        public VectorOffset Clone(FlatBufferBuilder builder)
        {
            return builder.CloneVectorData(_bb, _start_pos, sizeof(uint), _count, sizeof(uint));
        }
    }

    public struct FlatBufferVectorOfLong : IFlatbufferVector, IFlatbufferCollection<long>
    {
        private ByteBuffer _bb;
        private int _start_pos;
        private int _count;

        public void __init(int i, ByteBuffer bb)
        {
            _bb = bb;
            _start_pos = i + sizeof(int);
            _count = bb.GetInt(i) / sizeof(long);
        }
        public ByteBuffer ByteBuffer { get { return _bb; } }

        public int Count { get { return _count; } }

        public long this[int index] { get { return _bb.GetLong(_start_pos + sizeof(long) * index); } }

        public VectorOffset Clone(FlatBufferBuilder builder)
        {
            return builder.CloneVectorData(_bb, _start_pos, sizeof(long), _count, sizeof(long));
        }
    }

    public struct FlatBufferVectorOfUlong : IFlatbufferVector, IFlatbufferCollection<ulong>
    {
        private ByteBuffer _bb;
        private int _start_pos;
        private int _count;

        public void __init(int i, ByteBuffer bb)
        {
            _bb = bb;
            _start_pos = i + sizeof(int);
            _count = bb.GetInt(i) / sizeof(ulong);
        }
        public ByteBuffer ByteBuffer { get { return _bb; } }

        public int Count { get { return _count; } }

        public ulong this[int index] { get { return _bb.GetUlong(_start_pos + sizeof(ulong) * index); } }

        public VectorOffset Clone(FlatBufferBuilder builder)
        {
            return builder.CloneVectorData(_bb, _start_pos, sizeof(ulong), _count, sizeof(ulong));
        }
    }

    public struct FlatBufferVectorOfFloat : IFlatbufferVector, IFlatbufferCollection<float>
    {
        private ByteBuffer _bb;
        private int _start_pos;
        private int _count;

        public void __init(int i, ByteBuffer bb)
        {
            _bb = bb;
            _start_pos = i + sizeof(int);
            _count = bb.GetInt(i) / sizeof(float);
        }
        public ByteBuffer ByteBuffer { get { return _bb; } }

        public int Count { get { return _count; } }

        public float this[int index] { get { return _bb.GetFloat(_start_pos + sizeof(float) * index); } }

        public VectorOffset Clone(FlatBufferBuilder builder)
        {
            return builder.CloneVectorData(_bb, _start_pos, sizeof(float), _count, sizeof(float));
        }
    }

    public struct FlatBufferVectorOfDouble : IFlatbufferVector, IFlatbufferCollection<double>
    {
        private ByteBuffer _bb;
        private int _start_pos;
        private int _count;

        public void __init(int i, ByteBuffer bb)
        {
            _bb = bb;
            _start_pos = i + sizeof(int);
            _count = bb.GetInt(i) / sizeof(double);
        }
        public ByteBuffer ByteBuffer { get { return _bb; } }

        public int Count { get { return _count; } }

        public double this[int index] { get { return _bb.GetDouble(_start_pos + sizeof(double) * index); } }

        public VectorOffset Clone(FlatBufferBuilder builder)
        {
            return builder.CloneVectorData(_bb, _start_pos, sizeof(double), _count, sizeof(double));
        }
    }
}
