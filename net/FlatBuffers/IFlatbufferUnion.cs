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

namespace FlatBuffers
{
    /// <summary>
    /// This is the base for all Union types
    /// </summary>
    public interface IFlatbufferUnion<TEnum> where TEnum : struct
    {
        void __init(TEnum type, int _tableOffset, ByteBuffer _bb);

        int Clone(FlatBufferBuilder builder);

        TEnum Type { get; }

        TTable Get<TTable>() where TTable : struct, IFlatbufferObject<TTable>;
    }

    /// <summary>
    /// This is the base for all collections of Union types
    /// </summary>
    public interface IFlatbufferUnionCollection
    {
        void __init(int enumVectorOffset, int tableVectorOffset, ByteBuffer bb);
        VectorOffset Clone(FlatBufferBuilder builder);
    }
}
