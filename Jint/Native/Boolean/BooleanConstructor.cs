﻿using Jint.Native.Function;
using Jint.Native.Object;
using Jint.Runtime;

namespace Jint.Native.Boolean
{
    public sealed class BooleanConstructor : FunctionInstance, IConstructor
    {
        private readonly Engine _engine;

        private BooleanConstructor(Engine engine): base(engine, null, null, false)
        {
        }

        public static BooleanConstructor CreateBooleanConstructor(Engine engine)
        {
            var obj = new BooleanConstructor(engine);
            obj.Extensible = true;

            // The value of the [[Prototype]] internal property of the Boolean constructor is the Function prototype object 
            obj.Prototype = engine.Function.PrototypeObject;
            obj.PrototypeObject = BooleanPrototype.CreatePrototypeObject(engine, obj);

            obj.FastAddProperty("length", 1, false, false, false);

            // The initial value of Boolean.prototype is the Boolean prototype object
            obj.FastAddProperty("prototype", obj.PrototypeObject, false, false, false);

            return obj;
        }

        public void Configure()
        {

        }

        public override object Call(object thisObject, object[] arguments)
        {
            if (arguments.Length == 0)
            {
                return false;
            }

            return TypeConverter.ToBoolean(arguments[0]);
        }

        /// <summary>
        /// http://www.ecma-international.org/ecma-262/5.1/#sec-15.7.2.1
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public ObjectInstance Construct(object[] arguments)
        {
            return Construct(TypeConverter.ToBoolean(arguments.Length > 0 ? arguments[0] : Undefined.Instance));
        }

        public BooleanPrototype PrototypeObject { get; private set; }

        public BooleanInstance Construct(bool value)
        {
            var instance = new BooleanInstance(_engine);
            instance.Prototype = PrototypeObject;
            instance.PrimitiveValue = value;
            instance.Extensible = true;

            return instance;
        }
    }
}