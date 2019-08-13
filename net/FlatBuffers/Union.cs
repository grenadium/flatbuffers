namespace FlatBuffers
{
    public struct Union<TEnum> where TEnum : struct
    {
        public TEnum type { get; private set; }
        public int bb_pos { get; private set; }
        public ByteBuffer bb { get; private set; }

        // Re-init the internal state with an external buffer {@code ByteBuffer} and an offset within.
        public Union(TEnum _t, int _i, ByteBuffer _bb)
        {
            type = _t;
            bb = _bb;
            bb_pos = _i;
        }

        public TTable __to<TTable>() where TTable : IFlatbufferObject
        {
            TTable t = default(TTable);
            t.__init(bb_pos, bb);
            return t;
        }

        public static VectorOffset __clone<TUnion, TUnionCollection>(FlatBufferBuilder builder, TUnionCollection collection)
            where TUnion : struct, IFlatbufferUnion<TEnum>
            where TUnionCollection : struct, IFlatbufferCollection<TUnion>
        {
            using (FlatBufferBuilder.CachedArray cache = builder.GetCachedArray(collection.Count))
            {
                int[] offsets = cache.Array;
                for (int i = collection.Count - 1; i >= 0; i--)
                {
                    offsets[i] = collection[i].Clone(builder);
                }

                builder.StartVector(sizeof(int), collection.Count, sizeof(int));
                for (int i = collection.Count - 1; i >= 0; --i)
                {
                    builder.AddOffset(offsets[i]);
                }
                return builder.EndVector();
            }
        }
    }
}
