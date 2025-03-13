using System;
using System.Text;

namespace JQControls
{
    public class JsonBuilder : IDisposable
    {
        protected StringBuilder stringBuilder;

        public JsonBuilder()
        {
            stringBuilder = new StringBuilder();
        }

        public void Dispose()
        {
            stringBuilder.Length = 0;
        }

        public override string ToString()
        {
            if (stringBuilder.Length == 0) return null;
            return "{" + stringBuilder.ToString() + "}";
        }

        protected void internalAdd(string key, string value)
        {
            if (stringBuilder.Length != 0) stringBuilder.Append(",");
            stringBuilder.Append(key);
            stringBuilder.Append(":");
            stringBuilder.Append(value);
        }

        public void Add(string key, string value)
        {
            Add(key, value, true);
        }

        public void Add(string key, string value, bool addQuotes)
        {
            if (addQuotes)
            {
                internalAdd(key, "'" + value.Replace("'", @"\'") + "'");
            }
            else
            {
                internalAdd(key, value);
            }
        }

        public void Add(string key, bool value)
        {
            internalAdd(key, value ? "true" : "false");
        }

        public void Add(string key, int value)
        {
            internalAdd(key, value.ToString());
        }

        public void Add(string key, params string[] value)
        {
            StringBuilder valueBuilder = new StringBuilder("[");
            for (int i = 0; i < value.Length; i++)
            {
                if (i != 0) valueBuilder.Append(",");
                valueBuilder.Append("'");
                valueBuilder.Append(value[i].Replace("'", @"\'"));
                valueBuilder.Append("'");
            }
            valueBuilder.Append("]");
            internalAdd(key, valueBuilder.ToString());
        }

        public void Add(string key, SimpleItemCollection value)
        {
            StringBuilder valueBuilder = new StringBuilder("[");
            for (int i = 0; i < value.Count; i++)
            {
                if (i != 0) valueBuilder.Append(",");
                valueBuilder.Append("'");
                valueBuilder.Append(value[i].Value.Replace("'", @"\'"));
                valueBuilder.Append("'");
            }
            valueBuilder.Append("]");
            internalAdd(key, valueBuilder.ToString());
        }
    }
}
