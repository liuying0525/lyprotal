using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Utility.Newtonsoft.Extension
{
    /// <summary>
    /// 限制 Json.net 序列化的属性
    /// </summary>
    public class LimitPropsContractResolver : DefaultContractResolver
    {
        string[] props = null;
        public LimitPropsContractResolver(string[] props)
        {
            //指定要序列化属性的清单

            this.props = props;
        }
        protected override IList<JsonProperty> CreateProperties(Type type,
        MemberSerialization memberSerialization)
        {
            IList<JsonProperty> list = base.CreateProperties(type, memberSerialization);

            //只保留清单有列出的属性

            return list.Where(p => props.Contains(p.PropertyName)).ToList();
        }
    }
}
